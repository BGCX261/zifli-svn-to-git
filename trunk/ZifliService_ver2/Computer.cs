using System;
using System.Collections.Generic;
using System.Collections;
using System.Management;
using System.Text;
using System.Text.RegularExpressions;

namespace ZifliService
{
    class Computer
    {
        private ArrayList alInterfaces;

        private Interface oActiveInterface;

        public ArrayList getInterfaceNames()
        {
            ArrayList alArrInterfaces = new ArrayList();
            foreach (Interface oInterface in this.alInterfaces)
            {
                if (oInterface.getValidInt())
                {
                    alArrInterfaces.Add(oInterface.getName());
                }
            }
            return alArrInterfaces;
        }

        public void addALInterfaces()
        {
            Interface objInterface = new Interface();
            alInterfaces = new ArrayList();

            foreach (Interface alInterface in objInterface.queryForAllInterfaces("*", "Win32_NetworkAdapter"))
            {
                try
                {
                    this.alInterfaces.Add(alInterface);
                    Logger.WriteEvent("Interface added to ArrayList: " +
                        alInterface.getName().ToString(), ZifliService._DEBUG);
                }
                catch
                {
                    this.alInterfaces.Add("Nothing");
                    Logger.WriteEvent("Interface \"Nothing\" added to ArrayList: " +
                        alInterface.getName().ToString(), ZifliService._WARN);
                }
            }
        }

        public string getActiveMACAdress(Interface oIFace)
        {
            return oIFace.getMacAddress();
        }

        public Interface getInterfaceByName(string strIfaceName)
        {
            foreach (Interface oIface in this.alInterfaces)
            {
                if (oIface.getName() == strIfaceName)
                {
                    this.oActiveInterface = oIface;
                    return oIface;
                }
            }
            Logger.WriteEvent("About to return NULL for getInterfaceByName.",
                ZifliService._WARN);
            return null;
        }
    }

    public class Interface
    {
        private string strDescription;

        private string strMacAddress;

        private string strName;

        private string strIndex;

        private bool boolValidInt;

        public void setValidInt(bool boolValidInt)
        {
            this.boolValidInt = boolValidInt;
        }

        public void setDescription(string strDesc)
        {
            this.strDescription = strDesc;
        }

        public void setMacAddress(string strMAC)
        {
            this.strMacAddress = strMAC;
        }

        public void setName(string strName)
        {
            this.strName = strName;
        }

        public void setIndex(string strIndex)
        {
            this.strIndex = strIndex;
        }

        public string getName()
        {
            return this.strName;
        }

        public bool getValidInt()
        {
            return this.boolValidInt;
        }

        public string getDescription()
        {
            return this.strDescription;
        }

        public string getMacAddress()
        {
            return this.strMacAddress;
        }

        public string getIndex()
        {
            return this.strIndex;
        }

        public ArrayList queryForAllInterfaces(string strAttrib, string strWMIProvider)
        {
            ArrayList alInterfaces = new ArrayList();
            string strQuery = "SELECT " + strAttrib + " FROM " + strWMIProvider;
            ConnectionOptions oConn = new ConnectionOptions();
            System.Management.ManagementScope oMs = new ManagementScope("\\\\localhost", oConn);
            System.Management.ObjectQuery oQuery = new ObjectQuery(strQuery);
            ManagementObjectSearcher oSearcher = new ManagementObjectSearcher(oMs, oQuery);
            ManagementObjectCollection oReturnCollection = oSearcher.Get();

            foreach (ManagementObject oReturn in oReturnCollection)
            {
                Interface oIntface = new Interface();
                string strDesc;
                string strName;
                string strMacAddress;
                string strIndex;

                strName = oReturn["Name"].ToString();
                //Logger.WriteEvent("Interface name: " + oReturn["Name"].ToString(),
                //    ZifliService._DEBUG);

                strIndex = oReturn["Index"].ToString();
                //Logger.WriteEvent("Interface index: " + oReturn["Index"].ToString(),
                //    ZifliService._DEBUG);

                try
                {
                    strDesc = oReturn["Description"].ToString();
                //    Logger.WriteEvent("Interface description: " + oReturn["Description"].ToString(),
                //        ZifliService._DEBUG);
                }
                catch
                {
                    strDesc = "";
                    Logger.WriteEvent("Interface name is empty.", ZifliService._WARN);
                }

                try
                {
                    strMacAddress = oReturn["MACAddress"].ToString();
                //    Logger.WriteEvent("Interface MAC address: " + oReturn["MACAddress"].ToString(),
                //        ZifliService._DEBUG);
                }
                catch
                {
                    strMacAddress = "";
                    Logger.WriteEvent("Interface MAC address is empty.", ZifliService._WARN);
                }

                if (strMacAddress == "00:00:00:00:00:00")
                {
                    oIntface.setValidInt(false);
                    Logger.WriteEvent("Interface mac address is \"00:00:00:00:00:00\"",
                        ZifliService._WARN);
                    Logger.WriteEvent("Invalid mac address.", ZifliService._WARN);
                }
                else
                {
                    oIntface.setValidInt(true);
                }

                string strREMatcher = "[Ee]thernet|[Nn]etwork";

                if (Regex.IsMatch(strDesc, strREMatcher))
                {
                    //Logger.WriteEvent("An ethernet or network regex match.",
                    //    ZifliService._INFO);
                    if (!Regex.IsMatch(strDesc, "[Mm]iniport|WAN|Wan|wan"))
                    {
                    //    Logger.WriteEvent("Not a miniport or wan regex match. Valid interface.",
                    //        ZifliService._INFO);
                        oIntface.setValidInt(true);
                    }
                    else
                    {
                    //    Logger.WriteEvent("A miniport or wan regex match. Invalid interface.",
                    //        ZifliService._INFO);
                        oIntface.setValidInt(false);
                    }
                }
                else
                {
                //    Logger.WriteEvent("Not an ethernet or network regex match.",
                //        ZifliService._INFO);
                    oIntface.setValidInt(false);
                }
                oIntface.setDescription(strDesc);
                oIntface.setName(strName);
                oIntface.setMacAddress(strMacAddress);
                oIntface.setIndex(strIndex);
                alInterfaces.Add(oIntface);
            }
            return alInterfaces;
        }
    }
}
