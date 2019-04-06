using System;
using System.IO;
using System.ServiceModel;

namespace WcfServiceLibrary1
{

    [ServiceContract(Namespace = "http://achilles.tu.kielce.pl")]    public interface IComparatorService
    {
        [OperationContract]        void joinToServer();

        [OperationContract]        FilesToCompare fetchFilesToCompare();

        [OperationContract]        Stream downloadFile(String fileName);


        [OperationContract]        void finishComparing(ComparingResult comparingResult);
    }


}
