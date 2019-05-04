using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WcfServiceLibrary1;

namespace Server.view
{
    public class WorkerView
    {
        private String ip;
        private String port;
        private String uuid;
        private String currentComparingPair;

        private DateTime lastTime;

        public string Ip { get => ip; set => ip = value; }
        public string Port { get => port; set => port = value; }
        public string Uuid { get => uuid; set => uuid = value; }

        public DateTime LastTime { get => lastTime; set => lastTime = value; }
        public string CurrentComparingPair { get => currentComparingPair; set => currentComparingPair = value; }

        public WorkerView(Worker worker)
        {
            this.ip = worker.Ip;
            this.port = worker.Port;
            this.uuid = worker.Uuid;
            this.currentComparingPair = worker.CurrentComparingPair;

            this.lastTime = worker.LastTime;
        }

    }
}
