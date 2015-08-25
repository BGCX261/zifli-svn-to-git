using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Management;
using System.Net;
using System.IO;

namespace ZifliService
{
    class Misc
    {
        private string sQuery;

        public string GetMachineName()
        {
            string sBoxName = null;
            this.sQuery = "select CSName from Win32_OperatingSystem";
            
            ConnectionOptions oXn = new ConnectionOptions();
            ManagementScope oScope = new ManagementScope("\\\\localhost", oXn);
            ObjectQuery oQuery = new ObjectQuery(this.sQuery);
            ManagementObjectSearcher oSearcher = new ManagementObjectSearcher(oScope, oQuery);
            ManagementObjectCollection oResults = oSearcher.Get();

            foreach (ManagementObject oResult in oResults)
            {
                try
                {
                    sBoxName = oResult["CSName"].ToString();
                    Logger.WriteEvent("Computer name: " + sBoxName, ZifliService._DEBUG);
                }
                catch
                {
                    Logger.WriteEvent("Computer name is indeterminable.", ZifliService._WARN);
                }

            }
            return sBoxName;
        }

        public string GetMacAddress(int intAdapter)
        {
            string sMacAddress = "--:--:--:--:--:--";
            this.sQuery = "select MACAddress from Win32_NetworkAdapter where DeviceID='intAdapter'";

            ConnectionOptions oXn = new ConnectionOptions();
            ManagementScope oScope = new ManagementScope("\\\\localhost", oXn);
            ObjectQuery oQuery = new ObjectQuery(this.sQuery);
            ManagementObjectSearcher oSearcher = new ManagementObjectSearcher(oScope, oQuery);
            ManagementObjectCollection oResults = oSearcher.Get();

            foreach (ManagementObject oResult in oResults)
            {
                sMacAddress = oResult["MACAddress"].ToString();
                Logger.WriteEvent("MACAddress is: " + sMacAddress, ZifliService._DEBUG);
            }
            return sMacAddress;
        }

        public long getUnixTimeStamp(DateTime date_time_convert)
        {
            DateTime date_time_base = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            TimeSpan span = date_time_convert.ToUniversalTime() - date_time_base;
            return (long)span.TotalSeconds;
        }

        public void uploadResults(string strMAC, string strDSN, Result aResult,
            string strMachine, long longTimeStamp, string strEmail, string strPass)
        {
            Logger.WriteEvent("Uploading results now...", ZifliService._INFO);

            string strMACAddress = strMAC;
            string strDSName = strDSN;
            string strMachineName = strMachine;
            long lTimeStamp = longTimeStamp;
            string strEmailAddress = strEmail;
            string strPassword = strPass;

            string strURI = "http://www.zifli.com/upload.php";
            string postData = "mac=" + strMACAddress
                + "&dsname=" + strDSName
                + "&value=" + aResult.getMean()
                + "&computer_name=" + strMachineName
                + "&timestamp=" + lTimeStamp
                + "&email=" + strEmailAddress
                + "&password=" + strPassword;

            Logger.WriteEvent("Post data: " + postData, ZifliService._DEBUG);

            byte[] byteArray = Encoding.UTF8.GetBytes(postData);
            WebRequest request = WebRequest.Create(strURI);
            request.ContentType = "application/x-www-form-urlencoded";
            request.Credentials = CredentialCache.DefaultCredentials;
            request.Method = "POST";

            try
            {
                Stream dataStream = request.GetRequestStream();
                dataStream.Write(byteArray, 0, byteArray.Length);
                dataStream.Close();
            }
            catch
            {
                Logger.WriteEvent("Connection error. Unable to submit data to server.",
                    ZifliService._WARN);
            }

            try
            {
                WebResponse response = request.GetResponse();
                Stream data = response.GetResponseStream();
                StreamReader streamReader = new StreamReader(data);
                string responseServer = streamReader.ReadToEnd();
                data.Close();
            }
            catch
            {
                Logger.WriteEvent("Connection error. No response from server.",
                    ZifliService._WARN);
            }
        }
    }
}
