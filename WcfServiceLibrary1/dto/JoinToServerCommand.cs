using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WcfServiceLibrary1
{
    public class JoinToServerCommand
    {
        private string processorInfo;

        public string ProcessorInfo { get => processorInfo; set => processorInfo = value; }
    }
}
