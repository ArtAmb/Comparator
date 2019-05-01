using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.ServiceModel;
using System.ServiceModel.Description;
using WcfServiceLibrary1;

namespace Server
{
    public class ComparatorServer
    {
        private static ComparatorServer instance;

        public static ComparatorServer getInstance()
        {
            if (instance == null)
            {
                instance = new ComparatorServer();
            }

            return instance;
        }

        private Boolean running = false;
        private List<FileToCompare> allFiles = new List<FileToCompare>();
        private List<FilesToCompare> uniquePairsOfFilesToCompare = new List<FilesToCompare>();

        public List<FileToCompare> AllFiles { get => allFiles; }
        public List<FilesToCompare> UniquePairsOfFilesToCompare { get => uniquePairsOfFilesToCompare; }

        private ComparatorServer()
        {

        }

        public void start(string directoryWithFiles)
        {
            if (running)
                return;

            running = true;


            loadFiles(directoryWithFiles);

            fillUniqueParisOfFilesToCompare();


            DataDTO data = new DataDTO();
            data.PathToFiles = directoryWithFiles;
            data.UniquePairsOfFilesToCompare = UniquePairsOfFilesToCompare;
            data.AllFiles = AllFiles;

            DataContainer.loadData(data);

            startHosting();
        }

        private void loadFiles(string directoryWithFiles)
        {
            string[] filesPaths = Directory.GetFiles(directoryWithFiles, "*.txt");
            foreach (string filePath in filesPaths)
            {
                FileToCompare file = new FileToCompare();

                file.Md5Sum = calculateMd5(filePath);
                file.Path = filePath;
                file.FileName = Path.GetFileName(filePath);

                AllFiles.Add(file);
            }
        }

        private void fillUniqueParisOfFilesToCompare()
        {
            for (int i = 0; i < AllFiles.Count - 1; ++i)
                for (int j = i + 1; j < AllFiles.Count; ++j)
                {
                    FilesToCompare filesToCompare = new FilesToCompare();
                    filesToCompare.File1MD5 = AllFiles[i].Md5Sum;
                    filesToCompare.File2MD5 = AllFiles[j].Md5Sum;

                    UniquePairsOfFilesToCompare.Add(filesToCompare);
                }
        }

        private void startHosting()
        {
            Uri baseAddress = new Uri("http://localhost:8000/ComparatorService");
            ServiceHost host = new ServiceHost(typeof(ComparatorService), baseAddress);

            host.AddServiceEndpoint(typeof(IComparatorService), new WSHttpBinding(), "ComparatorService");

            ServiceMetadataBehavior smb = new ServiceMetadataBehavior();
            smb.HttpGetEnabled = true;
            host.Description.Behaviors.Add(smb);

            host.Open();
        }

        private string calculateMd5(String file)
        {
            using (var md5 = MD5.Create())
            {
                using (var stream = File.OpenRead(file))
                {
                    var hash = md5.ComputeHash(stream);
                    return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
                }
            }
        }




    }
}


