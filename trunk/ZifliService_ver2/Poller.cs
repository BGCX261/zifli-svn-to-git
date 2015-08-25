using System;
using System.Collections.Generic;
using System.Text;
using System.Management;

namespace ZifliService
{
    class Poller
    {
        public Result queryProviderByName(string strWMIProvider, string strName, string strAttrib)
        {
            string strQuery = "select " + strAttrib + " from " + strWMIProvider +
                " where Name='" + strName + "'";
            Logger.WriteEvent("Query: " + strQuery, ZifliService._DEBUG);

            // number of times to poll during given polling period
            int j = ZifliService._POLLS;

            // array to hold the values of all polls during this polling period
            float[] arrValues = new float[j];

            Result oResult = new Result();

            for (int i = 0; i < j; i++)
            {
                ConnectionOptions oConn = new ConnectionOptions();
                System.Management.ManagementScope oMs = new ManagementScope("\\\\localhost", oConn);
                System.Management.ObjectQuery oQuery = new ObjectQuery(strQuery);
                ManagementObjectSearcher oSearcher = new ManagementObjectSearcher(oMs, oQuery);
                ManagementObjectCollection oReturnCollection = oSearcher.Get();

                foreach (ManagementObject oReturn in oReturnCollection)
                {
                    try
                    {
                        arrValues[i] = (float)Convert.ToDouble(oReturn[strAttrib].ToString());
                        Logger.WriteEvent("Conversion succeeded and result: " +
                            (float)Convert.ToDouble(oReturn[strAttrib].ToString()),
                            ZifliService._DEBUG);
                    }
                    catch
                    {
                        Logger.WriteEvent("failed to convert string to decimal.",
                            ZifliService._ERR);
                    }
                }
            }
            oResult.addValues(arrValues);
            Result.computeMean(oResult);
            Result.computeSD(oResult);
            Logger.WriteEvent("Mean = " + oResult.getMean().ToString(),
                ZifliService._INFO);
            Logger.WriteEvent("SD = " + oResult.getSD().ToString(),
                ZifliService._INFO);

            return oResult;
        }
    }
}
