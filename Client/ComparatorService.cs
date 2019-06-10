using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;

namespace Client
{
    public class SentenceIndexes
    {
        private int firstWordIndex;
        private int lastWordIndex;

        public int FirstWordIndex { get => firstWordIndex; set => firstWordIndex = value; }
        public int LastWordIndex { get => lastWordIndex; set => lastWordIndex = value; }

        public int getLength()
        {
            return lastWordIndex - firstWordIndex + 1;
        }

        public override int GetHashCode()
        {
            return lastWordIndex * 10 + firstWordIndex;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as SentenceIndexes);
        }

        public bool Equals(SentenceIndexes sentenceIndexes)
        {
            return sentenceIndexes != null && firstWordIndex.Equals(sentenceIndexes.FirstWordIndex) && lastWordIndex.Equals(sentenceIndexes.LastWordIndex);
        }
    }

    public class Result
    {

        private SentenceIndexes file1;
        private SentenceIndexes file2;

        private String sentence;


        public string Sentence { get => sentence; set => sentence = value; }
        public SentenceIndexes File1 { get => file1; set => file1 = value; }
        public SentenceIndexes File2 { get => file2; set => file2 = value; }

        public Result()
        {
            file1 = new SentenceIndexes();
            file2 = new SentenceIndexes();
        }
    }

    public class ResultOfComparison
    {

        public double PercentOfSimilarityF1 { set; get; }
        public double PercentOfSimilarityF2 { set; get; }
        public List<Result> Results { set; get; }
    }


    public class Comparator
    {

        private int MIN_SENTENCE_LENGTH = 1;

        public Comparator(int minSentenceLength)
        {
            this.MIN_SENTENCE_LENGTH = minSentenceLength - 1;
        }

        public ResultOfComparison compare(String pathToFile1, String pathToFile2)
        {
            String[] allWordsOfFile1 = loadFile(pathToFile1);
            String[] allWordsOfFile2 = loadFile(pathToFile2);

            var commonSentences = findAllCommonSentences(allWordsOfFile1, allWordsOfFile2);
            HashSet<SentenceIndexes> indexesSetF1 = new HashSet<SentenceIndexes>();
            HashSet<SentenceIndexes> indexesSetF2 = new HashSet<SentenceIndexes>();

            foreach (Result sentence in commonSentences)
            {
                indexesSetF1.Add(sentence.File1);
                indexesSetF2.Add(sentence.File2);
            }

            double percentOfSimilarityF1 = calucatePercentOfSimilarity(indexesSetF1, allWordsOfFile1);
            double percentOfSimilarityF2 = calucatePercentOfSimilarity(indexesSetF2, allWordsOfFile2);

            return new ResultOfComparison() { Results = commonSentences, PercentOfSimilarityF1 = percentOfSimilarityF1, PercentOfSimilarityF2 = percentOfSimilarityF2 };
        }

        private int countWords(SentenceIndexes indexes)
        {
            return indexes.LastWordIndex - indexes.FirstWordIndex + 1;
        }

        private double calucatePercentOfSimilarity(HashSet<SentenceIndexes> indexesSet, String[] allWordsInFile)
        {
            if (indexesSet.Count() == 0)
                return 0;

            List<SentenceIndexes> uniqueIdxs = indexesSet.ToList();
            uniqueIdxs.Sort((idxs1, idxs2) => idxs1.FirstWordIndex.CompareTo(idxs2.FirstWordIndex));

            HashSet<SentenceIndexes> newIndexesSet = new HashSet<SentenceIndexes>();

            int startIdx = uniqueIdxs[0].FirstWordIndex;
            int endIdx = uniqueIdxs[0].LastWordIndex;
            uniqueIdxs.Add(new SentenceIndexes() { FirstWordIndex = int.MaxValue - 1, LastWordIndex = int.MaxValue });

            foreach (SentenceIndexes indexes in uniqueIdxs)
            {
                if (indexes.FirstWordIndex > endIdx)
                {
                    newIndexesSet.Add(new SentenceIndexes() { FirstWordIndex = startIdx, LastWordIndex = endIdx });
                    startIdx = indexes.FirstWordIndex;
                    endIdx = indexes.LastWordIndex;
                } else
                {
                    endIdx = indexes.LastWordIndex > endIdx ? indexes.LastWordIndex : endIdx;
                }
            }

            int similarWordsCount = 0;
            foreach (SentenceIndexes indexes in newIndexesSet)
            {
                similarWordsCount += indexes.getLength();
            }


            return ((double)similarWordsCount / allWordsInFile.Length) * 100;
        }



        private List<Result> findAllCommonSentences(String[] allWordsOfFile1, String[] allWordsOfFile2)
        {
            List<Result> commonSentences = new List<Result>();

            for (int i = 0; i < allWordsOfFile1.Length; ++i)
            {
                for (int j = 0; j < allWordsOfFile2.Length; ++j)
                {
                    if (allWordsOfFile1[i].Equals(allWordsOfFile2[j]))
                    {
                        var result = new Result();

                        result.File1.FirstWordIndex = i;
                        result.File2.FirstWordIndex = j;
                        int delta = 1;

                        while ((i + delta < allWordsOfFile1.Length)
                            && (j + delta < allWordsOfFile2.Length)
                            && allWordsOfFile1[i + delta].Equals(allWordsOfFile2[j + delta], StringComparison.InvariantCultureIgnoreCase))
                        {
                            ++delta;
                        }

                        result.File1.LastWordIndex = i + delta - 1;
                        result.File2.LastWordIndex = j + delta - 1;

                        if (delta > MIN_SENTENCE_LENGTH)
                        {
                            result.Sentence = getSentence(allWordsOfFile1, result.File1);
                            if (result.Sentence.Split(' ').Length < MIN_SENTENCE_LENGTH)
                            {
                                throw new Exception("WTF");
                            }
                            if (!commonSentences.Any(sen => contains(sen.File1, result.File1) && contains(sen.File2, result.File2)))
                                commonSentences.Add(result);
                        }
                    }
                }

            }

            return commonSentences;
        }

        private bool contains(SentenceIndexes indexes1, SentenceIndexes index2)
        {
            return indexes1.FirstWordIndex <= index2.FirstWordIndex && indexes1.LastWordIndex >= index2.LastWordIndex;
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
            return reader.ReadToEnd().Split((char[])null, StringSplitOptions.RemoveEmptyEntries);

        }

    }
}
