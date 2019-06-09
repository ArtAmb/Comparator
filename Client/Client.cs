using System;
using System.Text;
using System.IO;
using Client.ComparatorReference;
using System.Threading;
using System.Collections.Generic;
using WcfServiceLibrary1;
using System.Linq;
using System.Xml.Serialization;
using System.Xml;
using System.Runtime.Serialization.Formatters.Binary;

namespace Client
{
    public class ClientProgram
    {
        class DownloadedFile
        {
            private String serverFileName;
            private String clientFileName;
            private TimeSpan downloadingTime;

            public string ServerFileName { get => serverFileName; set => serverFileName = value; }
            public string ClientFileName { get => clientFileName; set => clientFileName = value; }
            public TimeSpan DownloadingTime { get => downloadingTime; set => downloadingTime = value; }
        }

        private string filePathRoot = @"client-files\";
        private ComparatorServiceClient comparatorService;
        private Thread heartbeatThread;
        string uuid;
        string processorInfo;
        private Comparator comparator;
        private Dictionary<String, DownloadedFile> downloadedFilesByServerNames = new Dictionary<String, DownloadedFile>();

        public ClientProgram()
        {
            try { 
            this.processorInfo = new ProcessorService().getProcessorInfo();
            } catch (Exception ex)
            {
                this.processorInfo = "";
            }
            initClient();
        }

        private void initClient()
        {
            downloadedFilesByServerNames.Clear();
            comparatorService = new ComparatorServiceClient();
            heartbeatThread = new Thread(() =>
            {
                while (true)
                {
                    try { 
                    Thread.Sleep(1000);
                    comparatorService.heartbeat(uuid);
                    Console.WriteLine("BUM BUM");
                    } catch(Exception ex)
                    {
                        Console.WriteLine(ex);
                        heartbeatThread.Abort();
                        heartbeatThread = null;
                        comparatorService.Abort();
                        comparatorService = null;
                    }
                }

            });
        }

        public void start()
        {
            while(true) {
                try
                {
                    doClientAction();
                } catch(Exception ex)
                {
                    heartbeatThread.Abort();
                    Console.WriteLine(ex);
                    Console.WriteLine("");
                    Console.WriteLine("");
                    Console.WriteLine("Something wrong happen with server ... Restart....");
                    Console.WriteLine("##################################################");
                    Console.WriteLine("##################################################");
                    Console.WriteLine("##################################################");
                    Console.WriteLine("##################################################");
                    Console.WriteLine("");
                    
                }
            }
        }

        private void doClientAction()
        {
            var opts = joinToServer();
            this.uuid = opts.ClientUUID;
            this.comparator = new Comparator(opts.MinWordsInSentence);

            heartbeatThread.Start();

            while (true)
            {
                FilesToCompare filesToCompare = comparatorService.fetchFilesToCompare(uuid);
                if (filesToCompare == null)
                {
                    Console.WriteLine("Brak plikow do porownania....");
                    Thread.Sleep(1000);
                    continue;
                }

                var file1 = downloadFile(filesToCompare.FileName1);
                var file2 = downloadFile(filesToCompare.FileName2);


                Console.WriteLine("Pobieranie zakonczone, rozpoczynam porownanie");
                ComparingResult comparingResult = compare(filesToCompare, file1, file2);
                Console.WriteLine("Porownanie zakonczone, rozpoczynam wysylanie wyniku do server");

                comparatorService.finishComparing(comparingResult);
                Console.WriteLine("Serwer odebral wyniki");
            }
        }

        private ServerOptions joinToServer()
        {
            while (true)
            {
                var serverOptions = tryToJoinToServer();
               
                if (serverOptions != null)
                {
                    return serverOptions;
                }
                   

                Thread.Sleep(1000);
            }

        }

        private ServerOptions tryToJoinToServer()
        {
            try
            {
                initClient();
                JoinToServerCommand joinToServerCommand = new JoinToServerCommand();
                joinToServerCommand.ProcessorInfo = processorInfo;

                return comparatorService.joinToServer(joinToServerCommand);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }


        }

        private ComparingResult compare(FilesToCompare filesToCompare, DownloadedFile file1, DownloadedFile file2)
        {
            String fileName1 = file1.ClientFileName;
            String fileName2 = file2.ClientFileName;

            DateTime startTime = DateTime.Now;
            
            List<Result> commonSentences = comparator.compare(filePathRoot + fileName1, filePathRoot + fileName2);

            ComparingResult result = new ComparingResult();
            result.PairId = filesToCompare.Id;
            result.CommonSentences = toCommonSentenceWS(commonSentences);

           

            result.ComparingTime = DateTime.Now - startTime;
            result.SendStartTime = DateTime.Now;
            result.File1DownloadingTime = file1.DownloadingTime;
            result.File2DownloadingTime = file2.DownloadingTime;
            result.ClientUUID = this.uuid;

            return result;
        }

        private List<CommonSentence> toCommonSentenceWS(List<Result> commonSentences)
        {
            return commonSentences.Select(sen =>
            {
                var tmp = new CommonSentence();
                tmp.File1 = mapToWSDTO(sen.File1);
                tmp.File2 = mapToWSDTO(sen.File2);

                return tmp;
            }).ToList();
        }

        private Stream toStream(List<Result> commonSentences)
        {

            var commonSentencesWS = toCommonSentenceWS(commonSentences);


            XmlSerializer serializer = new XmlSerializer(commonSentences.GetType());
            using (MemoryStream stream = new MemoryStream())
            {
                serializer.Serialize(stream, commonSentences);
                stream.Position = 0;
            }

            return null;
        }

        private WcfServiceLibrary1.SentenceIndexes mapToWSDTO(SentenceIndexes file)
        {
            var tmp = new WcfServiceLibrary1.SentenceIndexes();

            tmp.FirstWordIndex = file.FirstWordIndex;
            tmp.LastWordIndexIndex = file.LastWordIndex;

            return tmp;
        }

        private DownloadedFile downloadFile(String fileName)
        {
            DownloadedFile downloadedFile; // = new DownloadedFile(); ?????
            if (downloadedFilesByServerNames.TryGetValue(fileName, out downloadedFile))
            {
                DownloadedFile tmp = new DownloadedFile();
                tmp.ServerFileName = downloadedFile.ServerFileName;
                tmp.ClientFileName = downloadedFile.ClientFileName;
                tmp.DownloadingTime = TimeSpan.Zero;

                return tmp;
            }


            DateTime startTime = DateTime.Now;
            var stream = comparatorService.downloadFile(fileName);

            StreamReader reader = new StreamReader(stream);
            var fileContent = reader.ReadToEnd();
            var bytes = Encoding.ASCII.GetBytes(fileContent);


            //Console.WriteLine("Zawartosc pobranego pliku: " + fileContent);

            if (!Directory.Exists(filePathRoot))
            {
                Directory.CreateDirectory(filePathRoot);
            }

            String newFileName = Guid.NewGuid().ToString() + ".txt";
            using (var fileStream = File.OpenWrite(filePathRoot + newFileName))
            {
                fileStream.Write(bytes, 0, bytes.Length);
            }

            Console.WriteLine("Plik '" + fileName + "' zapisany");

           

            downloadedFile = new DownloadedFile();
            downloadedFile.ServerFileName = fileName;
            downloadedFile.ClientFileName = newFileName;
            downloadedFile.DownloadingTime = DateTime.Now - startTime;

            downloadedFilesByServerNames.Add(fileName, downloadedFile);

            return downloadedFile;
        }

        private int GetObjectSize(object TestObject)
        {
            BinaryFormatter bf = new BinaryFormatter();
            MemoryStream ms = new MemoryStream();
            byte[] Array;
            bf.Serialize(ms, TestObject);
            Array = ms.ToArray();
            return Array.Length;
        }
    }
}
