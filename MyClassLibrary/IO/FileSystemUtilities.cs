using MyClassLibrary.Logging;
using System;
using System.IO;


namespace MyClassLibrary.IO
{
    public static class FileSystemUtilities
    {
        public static Boolean MoveFile<T_LogWriter>(String sourceFileName, String destinationFileName)
            where T_LogWriter : Logging.ILogWriter, new ()
        {
            try
            {
                File.Move(sourceFileName, destinationFileName);
            }
            catch (Exception exception)
            {
                LoggingTools.WriteLogEntry<T_LogWriter>(exception);

                return false;
            }

            return true;
        }
    }
}