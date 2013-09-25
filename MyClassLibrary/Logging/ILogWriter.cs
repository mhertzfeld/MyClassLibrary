using System;
using System.Diagnostics;


namespace MyClassLibrary.Logging
{
    public interface ILogWriter
        : IDisposable
    {
        void WriteLogEntry(Exception exception);

        void WriteLogEntry(String message);

        void WriteLogEntry(String message, String category, Int32 priority, Int32 eventId, TraceEventType traceEventType, String title);
    }
}