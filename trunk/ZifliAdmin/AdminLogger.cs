using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace ZifliAdmin
{
    class AdminLogger
    {
        public static void WriteEvent(string strEvent, int intLL)
        {
            string sSource = "Zifli Client";
            string sEvent = strEvent;
            string sLog = "Application";
            int intLogLevel = intLL;

            if (!EventLog.SourceExists(sSource)) EventLog.CreateEventSource(sSource, sLog);

            switch (intLogLevel >= ZifliForm._LOGLEVEL)
            {
                case true:
                    EventLog.WriteEntry(sSource, sEvent);
                    break;
            }
        }
    }
}
