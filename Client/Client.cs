using System;
using System.Text;
using System.IO;
using Client.ComparatorReference;
using System.Threading;
using System.Collections.Generic;
using WcfServiceLibrary1;
using System.Linq;

namespace Client
{
    public class ClientProgram
    {

        private string filePathRoot = @"client-files\";
        private ComparatorServiceClient comparatorService;
        private Thread heartbeatThread;
        string uuid;
        private Comparator comparator = new Comparator();

        public ClientProgram()
        {
            initClient();
        }

        private void initClient()
        {
            comparatorService = new ComparatorServiceClient();
            heartbeatThread = new Thread(() =>
            {
                while (true)
                {
                    try { 
                    Thread.Sleep(1000);
                    comparatorService.heartbeat(uuid);
                    } catch(Exception ex)
                    {
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
            this.uuid = joinToServer();
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

                String fileName1 = downloadFile(filesToCompare.FileName1);
                String fileName2 = downloadFile(filesToCompare.FileName2);

                ComparingResult comparingResult = compare(filesToCompare, fileName1, fileName2);


                comparatorService.finishComparing(comparingResult);
            }
        }

        private String joinToServer()
        {
            while (true)
            {
                String uuid = tryToJoinToServer();
                if (uuid != null)
                    return uuid;

                Thread.Sleep(1000);
            }

        }

        private String tryToJoinToServer()
        {
            try
            {
                initClient();
                return comparatorService.joinToServer();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }


        }

        private ComparingResult compare(FilesToCompare filesToCompare, String fileName1, String fileName2)
        {
            List<Result> commonSentences = comparator.compare(filePathRoot + fileName1, filePathRoot + fileName2);

            ComparingResult result = new ComparingResult();
            result.PairId = filesToCompare.Id;
            result.CommonSentences = commonSentences.Select(sen =>
            {
                var tmp = new CommonSentence();
                tmp.File1 = mapToWSDTO(sen.File1);
                tmp.File2 = mapToWSDTO(sen.File2);

                return tmp;
            }).ToList();

            return result;
        }

        private WcfServiceLibrary1.SentenceIndexes mapToWSDTO(SentenceIndexes file)
        {
            var tmp = new WcfServiceLibrary1.SentenceIndexes();

            tmp.FirstWordIndex = file.FirstWordIndex;
            tmp.LastWordIndexIndex = file.LastWordIndex;

            return tmp;
        }

        private String downloadFile(String fileName)
        {
            var stream = comparatorService.downloadFile(fileName);

            StreamReader reader = new StreamReader(stream);
            var fileContent = reader.ReadToEnd();
            var bytes = Encoding.ASCII.GetBytes(fileContent);


            Console.WriteLine("Zawartosc pobranego pliku: " + fileContent);

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

            return newFileName;
        }
    }
}
