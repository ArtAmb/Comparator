using Server;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading;
using WcfServiceLibrary1.comparator;

namespace WcfServiceLibrary1
{

    public class ComparatorService : IComparatorService
    {
        public Stream downloadFile(string fileName)
        {
            FileStream stream = new FileStream(Path.Combine(DataContainer.read().PathToFiles, fileName), FileMode.Open, FileAccess.Read);
            DataContainer.read().LogMessages.Add("Downloading file " + fileName);
            //var bytes = File.ReadAllBytes();
            //stream.Write(bytes, 0, bytes.Length);
            //stream.Position = 0;


            return stream;
        }

        public FilesToCompare fetchFilesToCompare(String uuid)
        {
            Worker worker = DataContainer.read().AllClients
                .Where(client => client.Uuid.Equals(uuid))
                .First();

            IEnumerable<string> ownedFiles = worker.OwnedFiles.Select(file => file.Md5Sum);


            var pairToCompare = findFilesToCompare(ownedFiles);
            if (pairToCompare == null)
                return null;

            pairToCompare.ComparingState = ComparingState.IN_PROGRESS;
            worker.CurrentComparingPair = pairToCompare.Id;

            if (!ownedFiles.Contains(pairToCompare.File1MD5))
                worker.OwnedFiles.Add(createFileToCompare(pairToCompare.FileName1, pairToCompare.File1MD5));

            if (!ownedFiles.Contains(pairToCompare.File2MD5))
                worker.OwnedFiles.Add(createFileToCompare(pairToCompare.FileName2, pairToCompare.File2MD5));

            return pairToCompare;
        }

        private FileToCompare createFileToCompare(String name, String md5)
        {
            return new FileToCompare(name, md5);
        }

        private FilesToCompare findFilesToCompare(IEnumerable<string> ownedFiles)
        {
            var pairsToCompare = DataContainer.read()
                .UniquePairsOfFilesToCompare
                .Where(pair => ComparingState.NEW.Equals(pair.ComparingState));

            if (pairsToCompare.Count() == 0)
            {
                return null;
            }

            var possibleToCompare = pairsToCompare.Where(pair => ownedFiles.Contains(pair.File1MD5) || ownedFiles.Contains(pair.File2MD5));

            if (possibleToCompare.Count() > 0)
            {
                return possibleToCompare.First();
            }

            return pairsToCompare.First();
        }


        public void finishComparing(ComparingResult comparingResult)
        {
            FinishComparingService.getInstance().finishComparing(comparingResult);
        }

        public ServerOptions joinToServer(JoinToServerCommand command)
        {
            var netAddress = readEndpointNetAddress();

            string szAddress = netAddress.Address;
            int nPort = netAddress.Port;

            DataContainer.read().LogMessages.Add("Id address " + szAddress);
            DataContainer.read().LogMessages.Add("PORT " + nPort);

            Worker worker = new Worker();

            worker.Ip = szAddress;
            worker.Port = nPort.ToString();
            worker.Uuid = Guid.NewGuid().ToString();
            worker.LastTime = DateTime.Now;
            worker.ProcessorInfo = command.ProcessorInfo;

            DataContainer.read().AllClients.Add(worker);

            ServerOptions options = new ServerOptions();
            options.ClientUUID = worker.Uuid;
            options.MinWordsInSentence = DataContainer.read().MinWordsInSentence;

            return options;
        }

        public void heartbeat(String uuid)
        {

            var netAddress = readEndpointNetAddress();

            //DataContainer.read().LogMessages.Add("bum bum");
            string szAddress = netAddress.Address;
            int nPort = netAddress.Port;

            //DataContainer.read().LogMessages.Add("Id address " + szAddress);
            //DataContainer.read().LogMessages.Add("PORT " + nPort);


            var worker = DataContainer.read().AllClients
                .Where(client => client.Uuid.Equals(uuid))
                .First();

            worker.LastTime = DateTime.Now;
        }

        private RemoteEndpointMessageProperty readEndpointNetAddress()
        {
            OperationContext oOperationContext = OperationContext.Current;
            MessageProperties oMessageProperties = oOperationContext.IncomingMessageProperties;
            return (RemoteEndpointMessageProperty)oMessageProperties[RemoteEndpointMessageProperty.Name];
        }

    }


    
}
