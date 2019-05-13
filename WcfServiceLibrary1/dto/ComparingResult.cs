
using System.Collections.Generic;

namespace WcfServiceLibrary1
{
    public class CommonSentence
    {
        private SentenceIndexes file1;
        private SentenceIndexes file2;

        public SentenceIndexes File1 { get => file1; set => file1 = value; }
        public SentenceIndexes File2 { get => file2; set => file2 = value; }
    }
    public class SentenceIndexes
    {
        private int firstWordIndex;
        private int lastWordIndexIndex;

        public int FirstWordIndex { get => firstWordIndex; set => firstWordIndex = value; }
        public int LastWordIndexIndex { get => lastWordIndexIndex; set => lastWordIndexIndex = value; }

        public int getLength()
        {
            return lastWordIndexIndex - firstWordIndex + 1;
        }
    }

    public class ComparingResult
    {

        private string pairId;
        private List<CommonSentence> commonSentences;

        public List<CommonSentence> CommonSentences { get => commonSentences; set => commonSentences = value; }
        public string PairId { get => pairId; set => pairId = value; }
    }

}
