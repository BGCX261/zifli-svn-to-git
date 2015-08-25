using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Ipc;
using System.Runtime.Remoting.Channels.Tcp;
using System.Windows.Forms;
using System.Runtime.Serialization.Formatters;
using System.Reflection;
using ZifliService;

namespace ZifliAdmin
{
    public class ClientRemoting
    {
        public static void createChannel()
        {
            Hashtable ht = new Hashtable();
            ht.Add("name", "ZifliClient");
            ht.Add("portName", "ZifliClient");
            ht.Add("typeFilterLevel", "Full");

            //IpcClientChannel chan = new IpcClientChannel(
            IpcChannel chan = new IpcChannel(
                ht,
                new BinaryClientFormatterSinkProvider(ht, null),
                new BinaryServerFormatterSinkProvider(ht, null));
            ChannelServices.RegisterChannel(chan, false);
            AdminLogger.WriteEvent("Client channel created.",
                ZifliForm._DEBUG);
        }

        public static void createChannelTcp()
        {
            TcpClientChannel chan = new TcpClientChannel();
            ChannelServices.RegisterChannel(chan, false);
            AdminLogger.WriteEvent("Client channel created.",
                ZifliForm._DEBUG);

            /*svcController obj = (svcController)Activator.GetObject(
                typeof(ZifliService.svcController),
                "tcp://localhost:11975/ZifliService");
            AdminLogger.WriteEvent("Activator established.",
                ZifliForm._DEBUG);*/
        }

        public static void remotePollTcp(ZifliForm aForm)
        {
            remotingController obj = (remotingController)
                Activator.GetObject(typeof(ZifliService.remotingController),
                "tcp://localhost:11975/ZifliServer");
            try
            {
                try
                {
                    obj.initiatePoll();
                    aForm.labelResult_Modify(true);
                }
                catch
                {
                    AdminLogger.WriteEvent("Initiate \"out of band\" poll failed.",
                        ZifliForm._ERR);
                    aForm.labelResult_Modify(false);
                }
            }
            catch
            {
                AdminLogger.WriteEvent("Failed to achieve IPC channel.",
                    ZifliForm._ERR);
            }
        }
    }
}