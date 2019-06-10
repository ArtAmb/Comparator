using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace WcfServiceLibrary1.comparator
{
    public class FinishComparingService
    {

        private static FinishComparingService instance; 
        public static FinishComparingService getInstance()
        {
            if(instance == null)
            {
                instance = new FinishComparingService();
            }

            return instance;
        }

        public String finishComparing(ComparingResult comparingResult)
        {

            var comparedPair = DataContainer.read().UniquePairsOfFilesToCompare
                .Where(pair => pair.Id.Equals(comparingResult.PairId))
                .First();

            String newFileName = @"comparing_result_" + comparedPair.FileName1.Replace(".txt", "") + "#" + comparedPair.FileName2.Replace(".txt", "") + ".json";
            comparedPair.ComparingState = ComparingState.COMPARED;
            comparedPair.ComparingResult = comparingResult;
            comparedPair.ComparingResultFile = newFileName;
            comparedPair.ComparedBy = getClient(comparingResult.ClientUUID);

            var tmp = new FilesToCompare();
            tmp.Id = "REFRESH";
            DataContainer.read().UniquePairsOfFilesToCompare.Add(tmp);
            DataContainer.read().UniquePairsOfFilesToCompare.Remove(tmp);

            if (DataContainer.read().UniquePairsOfFilesToCompare.Where(pair => pair.ComparingResult == null).Count() == 0)
            {
                DataContainer.read().LogMessages.Add("Wszystkie pliki zostały porównane");
            }

            return newFileName;
        }

        private Worker getClient(String clientUUID)
        {
            IEnumerable<Worker> clients = DataContainer.read().AllClients.Where(cl => cl.Uuid.Equals(clientUUID));
            if (clients.Count() == 0)
            {
                Worker tmp = new Worker();
                tmp.Ip = "-1";
                tmp.Port = "-1";
                tmp.ProcessorInfo = "";
                return tmp;
            }

            return clients.First();
        }

        public void finishComparing(ComparingResultWithStreamRequest request)
        {
            String newFileName = finishComparing(request.comparingResult);

            /*Thread thread = new Thread(() =>
            {
                saveResultToJsonFile(newFileName, request.commonSentencesStream);
            });

            thread.Start(); */
        }

        private void saveResultToJsonFile(String fileName, Stream streamStrInJsonFormat)
        {
            String filePathRoot = DataContainer.read().PathToFiles + @"\comparing_results_json";
            if (!Directory.Exists(filePathRoot))
            {
                Directory.CreateDirectory(filePathRoot);
            }

            String pathToFile = Path.Combine(filePathRoot, fileName);
            if (File.Exists(pathToFile))
            {
                File.Delete(pathToFile);
            }
            using (var fileStream = File.OpenWrite(pathToFile))
            {
                streamStrInJsonFormat.CopyTo(fileStream);
            }
        }

       





        private void saveComparingResultsInTxt(FilesToCompare comparedPair, ComparingResult comparingResult)
        {
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

    }
}
