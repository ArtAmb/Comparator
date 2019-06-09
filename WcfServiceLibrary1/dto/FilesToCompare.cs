using System;

namespace WcfServiceLibrary1
{
    public class FilesToCompare
    {
        String id;
        String fileName1;
        String fileName2;

        String file1MD5;
        String file2MD5;

        ComparingResult comparingResult;

        ComparingState comparingState;
        private String comparingResultFile;


        public string File1MD5 { get => file1MD5; set => file1MD5 = value; }
        public string File2MD5 { get => file2MD5; set => file2MD5 = value; }
        public ComparingResult ComparingResult { get => comparingResult; set => comparingResult = value; }
        public ComparingState ComparingState { get => comparingState; set => comparingState = value; }
        public Worker ComparedBy { get; set; }
        public string FileName1 { get => fileName1; set => fileName1 = value; }
        public string FileName2 { get => fileName2; set => fileName2 = value; }
        public string Id { get => id; set => id = value; }
        public string ComparingResultFile { get => comparingResultFile; set => comparingResultFile = value; }
    }
}
