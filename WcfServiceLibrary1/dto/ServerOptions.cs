using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WcfServiceLibrary1
{
    public class ServerOptions
    {
        private String clientUUID;

        private int minWordsInSentence;

        public int MinWordsInSentence { get => minWordsInSentence; set => minWordsInSentence = value; }
        public string ClientUUID { get => clientUUID; set => clientUUID = value; }
    }
}
