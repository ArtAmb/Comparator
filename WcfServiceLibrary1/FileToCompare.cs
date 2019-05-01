using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public class FileToCompare
    {
        private string fileName;
        private string path;
        private string md5Sum;

        public string FileName { get => fileName; set => fileName = value; }
        public string Path { get => path; set => path = value; }
        public string Md5Sum { get => md5Sum; set => md5Sum = value; }
    }
}
