using System;
using System.Text;
using System.IO;
using Client.ComparatorReference;


namespace Client
{
    public class ClientProgram
    {

        private string filePathRoot = @"client-files\";

        public void start()
        {
            Console.WriteLine("Press any key to start ...");
            Console.ReadKey();
            ComparatorServiceClient comparatorService = new ComparatorServiceClient();
            comparatorService.joinToServer();

            /*
            String fileName = "test.txt";
            var stream = comparatorService.downloadFile(fileName);

            StreamReader reader = new StreamReader(stream);
            var fileContent = reader.ReadToEnd();
            var bytes = Encoding.ASCII.GetBytes(fileContent);


            Console.WriteLine("Zawartosc pobranego pliku: " + fileContent);

            if (!Directory.Exists(filePathRoot))
            {
                Directory.CreateDirectory(filePathRoot);
            }

            using (var fileStream = File.OpenWrite(filePathRoot + fileName))
            {
                fileStream.Write(bytes, 0, bytes.Length);
            }*/

            //File.WriteAllText(, fileContent);
            Console.WriteLine("Plik zapisany... ");
            Console.ReadKey();
        }
    }
}
