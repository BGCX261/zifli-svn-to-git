using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace ZifliService
{
    class Logger
    {
        public static void WriteEvent(string strEvent, int intLL)
        {
            string sSource = "Zifli Service";
            string sEvent = strEvent;
            string sLog = "Application";
            int intLogLevel = intLL;

            if (!EventLog.SourceExists(sSource)) EventLog.CreateEventSource(sSource, sLog);

            switch (intLogLevel >= ZifliService._LOGLEVEL)
            {
                case true:
                    EventLog.WriteEntry(sSource, sEvent);
                    break;
            }
        }
    }
}
