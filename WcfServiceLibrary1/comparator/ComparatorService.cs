using Server;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;

namespace WcfServiceLibrary1
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class ComparatorService : IComparatorService
    {
        private String path = @"E:\komparator\server-files\";

        public Stream downloadFile(string fileName)
        {
            MemoryStream stream = new MemoryStream();
            Console.WriteLine("Downloading file " + fileName);
            var bytes = File.ReadAllBytes(DataContainer.read().PathToFiles + @"\" + fileName);
            stream.Write(bytes, 0, bytes.Length);
            stream.Position = 0;

            return stream;
        }

        public FilesToCompare fetchFilesToCompare(String uuid)
        {
            Worker worker = DataContainer.read().AllClients
                .Where(client => client.Uuid.Equals(uuid))
                .First();

            IEnumerable<string> ownedFiles = worker.OwnedFiles.Select(file => file.Md5Sum);


            var pairToCompare = findFilesToCompare(ownedFiles);
            if (pairToCompare == null)
                return null;

            pairToCompare.ComparingState = ComparingState.IN_PROGRESS;
            worker.CurrentComparingPair = pairToCompare.Id;

            if (!ownedFiles.Contains(pairToCompare.File1MD5))
                worker.OwnedFiles.Add(createFileToCompare(pairToCompare.FileName1, pairToCompare.File1MD5));

            if (!ownedFiles.Contains(pairToCompare.File2MD5))
                worker.OwnedFiles.Add(createFileToCompare(pairToCompare.FileName2, pairToCompare.File2MD5));

            return pairToCompare;
        }

        private FileToCompare createFileToCompare(String name, String md5)
        {
            return new FileToCompare(name, md5);
        }

        private FilesToCompare findFilesToCompare(IEnumerable<string> ownedFiles)
        {
            var pairsToCompare = DataContainer.read()
                .UniquePairsOfFilesToCompare
                .Where(pair => ComparingState.NEW.Equals(pair.ComparingState));

            if (pairsToCompare.Count() == 0)
            {
                return null;
            }

            var possibleToCompare = pairsToCompare.Where(pair => ownedFiles.Contains(pair.File1MD5) || ownedFiles.Contains(pair.File2MD5));

            if (possibleToCompare.Count() > 0)
            {
                return possibleToCompare.First();
            }

            return pairsToCompare.First();
        }

        public void finishComparing(ComparingResult comparingResult)
        {
            var comparedPair = DataContainer.read().UniquePairsOfFilesToCompare
                .Where(pair => pair.Id.Equals(comparingResult.PairId))
                .First();

            comparedPair.ComparingState = ComparingState.COMPARED;
            comparedPair.ComparingResult = comparingResult;
            String[] file1 = loadFile(DataContainer.read().PathToFiles + @"\" + comparedPair.FileName1);

            var comparingResultDescription = comparingResult.CommonSentences.Select(comSen =>
            {
                String file1IdxText = toFileIdxDesc("File1", comSen.File1);
                String file2IdxText = toFileIdxDesc("File2", comSen.File2);
                String sentence = getSentence(file1, comSen.File1);

                return String.Join(Environment.NewLine, file1IdxText, file2IdxText, sentence);
            }).ToList();

            var text = String.Join(Environment.NewLine
                + "========================================"
                + Environment.NewLine, comparingResultDescription);


            var bytes = Encoding.ASCII.GetBytes(text);
            String filePathRoot = DataContainer.read().PathToFiles + @"\comparing_results";
            if (!Directory.Exists(filePathRoot))
            {
                Directory.CreateDirectory(filePathRoot);
            }

            String newFileName = @"comparing_result_" + comparedPair.FileName1.Replace(".txt", "") + "#" + comparedPair.FileName2.Replace(".txt", "") + ".txt";
            String pathToFile = filePathRoot + @"\" + newFileName;
            if (File.Exists(pathToFile))
            {
                File.Delete(pathToFile);
            }
            using (var fileStream = File.OpenWrite(pathToFile))
            {
                fileStream.Write(bytes, 0, bytes.Length);
            }

            comparedPair.ComparingResultFile = newFileName;
            DataContainer.read().LogMessages.Add("Wynik porownania w pliku " + newFileName);

            if (DataContainer.read().UniquePairsOfFilesToCompare.Where(pair => pair.ComparingResult == null).Count() == 0)
            {
                DataContainer.read().LogMessages.Add("Wszystkie pliki zostały porównane");
            }

            var tmp = new FilesToCompare();
            tmp.Id = "REFRESH";
            DataContainer.read().UniquePairsOfFilesToCompare.Add(tmp);
            DataContainer.read().UniquePairsOfFilesToCompare.Remove(tmp);
        }

        private string toFileIdxDesc(string name, SentenceIndexes file1)
        {
            return name + " " + file1.FirstWordIndex + " --> " + file1.LastWordIndexIndex;
        }

        private String getSentence(String[] fileSetences, SentenceIndexes indexes)
        {
            String[] wordsOfSentence = new String[indexes.getLength()];
            Array.Copy(fileSetences, indexes.FirstWordIndex, wordsOfSentence, 0, indexes.getLength());

            return String.Join(" ", wordsOfSentence);
        }

        private String[] loadFile(String pathToFile)
        {
            MemoryStream stream = new MemoryStream();
            var bytes = File.ReadAllBytes(pathToFile);
            stream.Write(bytes, 0, bytes.Length);
            stream.Position = 0;

            StreamReader reader = new StreamReader(stream);
            return reader.ReadToEnd().Split(null);

        }

        public ServerOptions joinToServer(JoinToServerCommand command)
        {
            var netAddress = readEndpointNetAddress();

            string szAddress = netAddress.Address;
            int nPort = netAddress.Port;

            DataContainer.read().LogMessages.Add("Id address " + szAddress);
            DataContainer.read().LogMessages.Add("PORT " + nPort);

            Worker worker = new Worker();

            worker.Ip = szAddress;
            worker.Port = nPort.ToString();
            worker.Uuid = Guid.NewGuid().ToString();
            worker.LastTime = DateTime.Now;
            worker.ProcessorInfo = command.ProcessorInfo;

            DataContainer.read().AllClients.Add(worker);

            ServerOptions options = new ServerOptions();
            options.ClientUUID = worker.Uuid;
            options.MinWordsInSentence = DataContainer.read().MinWordsInSentence;

            return options;
        }

        public void heartbeat(String uuid)
        {

            var netAddress = readEndpointNetAddress();

            //DataContainer.read().LogMessages.Add("bum bum");
            string szAddress = netAddress.Address;
            int nPort = netAddress.Port;

            //DataContainer.read().LogMessages.Add("Id address " + szAddress);
            //DataContainer.read().LogMessages.Add("PORT " + nPort);


            var worker = DataContainer.read().AllClients
                .Where(client => client.Uuid.Equals(uuid))
                .First();

            worker.LastTime = DateTime.Now;
        }

        private RemoteEndpointMessageProperty readEndpointNetAddress()
        {
            OperationContext oOperationContext = OperationContext.Current;
            MessageProperties oMessageProperties = oOperationContext.IncomingMessageProperties;
            return (RemoteEndpointMessageProperty)oMessageProperties[RemoteEndpointMessageProperty.Name];
        }
    }
}
