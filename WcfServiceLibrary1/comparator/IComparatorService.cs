using System;
using System.IO;
using System.ServiceModel;

namespace WcfServiceLibrary1
{

    [ServiceContract(Namespace = "http://achilles.tu.kielce.pl")]    public interface IComparatorService
    {
        [OperationContract]        String joinToServer();

        [OperationContract]        FilesToCompare fetchFilesToCompare(String uuid);

        [OperationContract]        Stream downloadFile(String fileName);


        [OperationContract]        void finishComparing(ComparingResult comparingResult);

        [OperationContract]
        void heartbeat(String uuid);
    }


}
