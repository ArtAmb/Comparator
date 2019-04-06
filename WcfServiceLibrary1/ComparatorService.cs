using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WcfServiceLibrary1
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class ComparatorService : IComparatorService
    {
        private String path = @"E:\komparator\server-files\";

        public Stream downloadFile(string fileName)
        {
            MemoryStream stream = new MemoryStream();
            Console.WriteLine("Downloading file " + fileName);
            var bytes = File.ReadAllBytes(path + fileName);
            stream.Write(bytes, 0, bytes.Length);
            stream.Position = 0;

            return stream;
        }

        public FilesToCompare fetchFilesToCompare()
        {
            throw new NotImplementedException();
        }

        public void finishComparing(ComparingResult comparingResult)
        {
            throw new NotImplementedException();
        }

        public void joinToServer()
        {
            Console.WriteLine("New client join to server");
        }

        public void heartbeat()
        {
            Console.WriteLine("bum bum");
        }
    }
}
