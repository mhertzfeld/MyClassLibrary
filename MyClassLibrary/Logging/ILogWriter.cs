using System;
using System.Diagnostics;


namespace MyClassLibrary.Logging
{
    public interface ILogWriter
    {
        void WriteLogEntry(Exception exception);

        void WriteLogEntry(TraceEventType traceEventType, String title, String message, String category = null, Int32 priority = 0, Int32 eventId = 0);
    }
}