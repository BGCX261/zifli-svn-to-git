using System;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Ipc;
using System.Runtime.Remoting.Channels.Tcp;
using System.Collections;

namespace ZifliService
{
   /// <remarks>
   /// Sample server to demonstrate the use of .NET remoting and IPC.
   /// </remarks>
   ///
    public class remotingController : MarshalByRefObject
    {
        public remotingController()
        {
            Logger.WriteEvent("remotingController constructor initiated blah.", ZifliService._DEBUG);
        }

        public void initiatePoll()
        {
            Poller aPoller = new Poller();
            Result aResult = aPoller.queryProviderByName("Win32_PerfFormattedData_PerfOS_Processor",
                "_Total", "PercentProcessorTime");
        }
    }

    public class ServerRemoting
    {
        public static void createChannelTcp()
        {
            TcpServerChannel chan = new TcpServerChannel(ZifliService._PORT);
            ChannelServices.RegisterChannel(chan, false);
            Logger.WriteEvent("Server channel created.", ZifliService._DEBUG);

            RemotingConfiguration.RegisterWellKnownServiceType(
                Type.GetType("ZifliService.remotingController"),
                "ZifliServer",
                WellKnownObjectMode.Singleton);
            Logger.WriteEvent("Singleton created.", ZifliService._DEBUG);
        }
    }
}