using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

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

    public class Comparator
    {

        private int MIN_SENTENCE_LENGTH = 1;

        public Comparator(int minSentenceLength)
        {
            this.MIN_SENTENCE_LENGTH = minSentenceLength - 1;
        }

        public List<Result> compare(String pathToFile1, String pathToFile2)
        {
            return findAllCommonSentences(pathToFile1, pathToFile2);
        }

        private List<Result> findAllCommonSentences(String pathToFile1, String pathToFile2)
        {
            List<Result> commonSentences = new List<Result>();

            String[] allWordsOfFile1 = loadFile(pathToFile1);
            String[] allWordsOfFile2 = loadFile(pathToFile2);

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
                            if(result.Sentence.Split(' ').Length < MIN_SENTENCE_LENGTH)
                            {
                                throw new Exception("WTF"); 
                            }
                            commonSentences.Add(result);
                        }
                    }
                }

            }

            return commonSentences;
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
