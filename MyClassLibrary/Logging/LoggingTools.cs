using System;
using System.Diagnostics;


namespace MyClassLibrary.Logging
{
    public static class LoggingTools
    {
        public static void WriteLogEntry<T_LogWriter>(Exception exception)
            where T_LogWriter : Logging.ILogWriter, new()
        {
            T_LogWriter logWriter = new T_LogWriter();
            logWriter.WriteToLog(exception);
        }

        public static void WriteLogEntry<T_LogWriter>(TraceEventType traceEventType, String title, String message, String category = null, Int32 priority = 0, Int32 eventId = 0)
    where T_LogWriter : Logging.ILogWriter, new()
        {
            T_LogWriter logWriter = new T_LogWriter();
            logWriter.WriteToLog(traceEventType, title, message, category, priority, eventId);
        }
    }
}