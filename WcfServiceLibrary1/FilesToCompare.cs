using System;

namespace WcfServiceLibrary1
{
    public class FilesToCompare
    {
        String fileName1;
        String fileName2;

        String file1MD5;
        String file2MD5;

        ComparingResult comparingResult;

        ComparingState comparingState;

        public string File1MD5 { get => file1MD5; set => file1MD5 = value; }
        public string File2MD5 { get => file2MD5; set => file2MD5 = value; }
        public ComparingResult ComparingResult { get => comparingResult; set => comparingResult = value; }
        public ComparingState ComparingState { get => comparingState; set => comparingState = value; }
    }
}
