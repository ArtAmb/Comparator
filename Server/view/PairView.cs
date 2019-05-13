using System;
using WcfServiceLibrary1;

namespace Server.view
{
    public class PairView
    {
        private String file1Name;
        private String file2Name;
        private ComparingState state;
        private String resultFile;

        public string File1Name { get => file1Name; set => file1Name = value; }
        public string File2Name { get => file2Name; set => file2Name = value; }
        public ComparingState State { get => state; set => state = value; }
        public string ResultFile { get => resultFile; set => resultFile = value; }

        public PairView(FilesToCompare filesToCompare)
        {
            this.file1Name = filesToCompare.FileName1;
            this.file2Name = filesToCompare.FileName2;
            this.state = filesToCompare.ComparingState;
            this.resultFile = filesToCompare.ComparingResultFile;
        }
    }
}
