using System;
using System.Management;
using System.Text;

namespace WcfServiceLibrary1
{
    public class ProcessorService
    {

        public String getProcessorInfo()
        {
            SelectQuery Sq = new SelectQuery("Win32_Processor");
            ManagementObjectSearcher objOSDetails = new ManagementObjectSearcher(Sq);
            ManagementObjectCollection osDetailsCollection = objOSDetails.Get();

            StringBuilder sb = new StringBuilder();
            foreach (ManagementObject mo in osDetailsCollection)
            {
                sb.AppendLine(string.Format("{0} ({1}) {2}MHz",
                    getProperty(mo, "Name").Trim(),
                    getProperty(mo, "NumberOfLogicalProcessors").Trim(),
                    getProperty(mo, "MaxClockSpeed").Trim()));
            }

            return sb.ToString();
        }

        private String getProperty(ManagementObject mo, String propertyName)
        {
            try
            {
                return mo[propertyName].ToString();
            }
            catch (Exception ex)
            {
                return "";
            }
        }
    }
}
