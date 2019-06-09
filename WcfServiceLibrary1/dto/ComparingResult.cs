
using System;
using System.Collections.Generic;
using System.IO;
using System.ServiceModel;

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
        private string clientUUID;
        private TimeSpan comparingTime;
        private List<CommonSentence> commonSentences;
        private DateTime sendStartTime;
        private TimeSpan file1DownloadingTime;
        private TimeSpan file2DownloadingTime;
        private TimeSpan sendingTime;

        public List<CommonSentence> CommonSentences { get => commonSentences; set => commonSentences = value; }
        public string PairId { get => pairId; set => pairId = value; }
        public TimeSpan ComparingTime { get => comparingTime; set => comparingTime = value; }
        
        public TimeSpan File1DownloadingTime { get => file1DownloadingTime; set => file1DownloadingTime = value; }
        public TimeSpan File2DownloadingTime { get => file2DownloadingTime; set => file2DownloadingTime = value; }
        public DateTime SendStartTime { get => sendStartTime; set => sendStartTime = value; }
        public TimeSpan SendingTime { get => sendingTime; set => sendingTime = value; }
        public string ClientUUID { get => clientUUID; set => clientUUID = value; }
    }

    [MessageContract]
    public class ComparingResultWithStreamRequest
    {
        [MessageHeader]
        public ComparingResult comparingResult;

        [MessageBodyMember]
        public Stream commonSentencesStream;
    }

}
