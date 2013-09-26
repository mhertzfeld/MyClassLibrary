using System;


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
    }
}