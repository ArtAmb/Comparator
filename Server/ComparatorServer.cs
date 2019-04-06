using System;
using System.ServiceModel;
using System.ServiceModel.Description;
using WcfServiceLibrary1;

namespace Server
{
    public class ComparatorServer
    {
        private Boolean running = false;


        public void start()
        {
            if (running)
                return;

            
            Uri baseAddress = new Uri("http://localhost:8000/ComparatorService");
            ServiceHost host = new ServiceHost(typeof(ComparatorService), baseAddress);

            host.AddServiceEndpoint(typeof(IComparatorService), new WSHttpBinding(), "ComparatorService");

            ServiceMetadataBehavior smb = new ServiceMetadataBehavior();
            smb.HttpGetEnabled = true;
            host.Description.Behaviors.Add(smb);

            host.Open();
            running = true;
        }



    }
}
