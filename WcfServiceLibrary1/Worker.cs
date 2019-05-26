using Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WcfServiceLibrary1
{
    public class Worker
    {
        private String ip;
        private String port;
        private String uuid;
        private String currentComparingPair;
        private String processorInfo;

        private DateTime lastTime;

        private List<FileToCompare> ownedFiles = new List<FileToCompare>();

      
        public string Port { get => port; set => port = value; }
        public List<FileToCompare> OwnedFiles { get => ownedFiles; }
        public string Uuid { get => uuid; set => uuid = value; }
        public DateTime LastTime { get => lastTime; set => lastTime = value; }
        public string CurrentComparingPair { get => currentComparingPair; set => currentComparingPair = value; }
        public string Ip { get => ip; set => ip = value; }
        public string ProcessorInfo { get => processorInfo; set => processorInfo = value; }
    }
}
