using System;
using System.Text;
using System.IO;
using Client.ComparatorReference;
using System.Threading;

namespace Client
{
    public class ClientProgram
    {

        private string filePathRoot = @"client-files\";
        private ComparatorServiceClient comparatorService;
        private Thread heartbeatThread;
        string uuid;

        public ClientProgram()
        {
            comparatorService = new ComparatorServiceClient();
            heartbeatThread = new Thread(() =>
            {
                while (true)
                {
                    Thread.Sleep(1000);
                    comparatorService.heartbeat(uuid);
                }

            });
        }

        public void start()
        {
            Console.WriteLine("Press any key to start ...");
            Console.ReadKey();
            
            this.uuid = comparatorService.joinToServer();
            heartbeatThread.Start();

            WcfServiceLibrary1.FilesToCompare filesToCompare = comparatorService.fetchFilesToCompare(uuid);
            if(filesToCompare == null)
            {
                Console.WriteLine("Brak plikow to porownania....");
                return;
            }

            downloadFile(filesToCompare.FileName1);
            downloadFile(filesToCompare.FileName2);



            //File.WriteAllText(, fileContent);
            Console.WriteLine("Plik zapisany... ");
            Console.ReadKey();
            heartbeatThread.Abort();
        }

        private void downloadFile(String fileName)
        {
            var stream = comparatorService.downloadFile(fileName);

            StreamReader reader = new StreamReader(stream);
            var fileContent = reader.ReadToEnd();
            var bytes = Encoding.ASCII.GetBytes(fileContent);


            Console.WriteLine("Zawartosc pobranego pliku: " + fileContent);

            if (!Directory.Exists(filePathRoot))
            {
                Directory.CreateDirectory(filePathRoot);
            }

            using (var fileStream = File.OpenWrite(filePathRoot + Guid.NewGuid().ToString() + ".txt"))
            {
                fileStream.Write(bytes, 0, bytes.Length);
            }
        }
    }
}
