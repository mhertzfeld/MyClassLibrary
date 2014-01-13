using MyClassLibrary.Logging;
using System;
using System.IO;


namespace MyClassLibrary.IO
{
    public static class FileSystemUtilities
    {
        public static Boolean GetDirectoryInfo<T_LogWriter>(String path, out DirectoryInfo directoryInfo)
            where T_LogWriter : Logging.ILogWriter, new()
        {
            try
            {
                directoryInfo = new DirectoryInfo(path);
                //return directoryInfo.Exists;
            }
            catch (Exception exception)
            {
                LoggingTools.WriteLogEntry<T_LogWriter>(exception);

                directoryInfo = null;

                return false;
            }

            return true;
        }

        public static Boolean GetFileInfoArray<T_LogWriter>(DirectoryInfo directoryInfo, out FileInfo[] fileInfoArray)
            where T_LogWriter : Logging.ILogWriter, new()
        {
            try
            {
                fileInfoArray = directoryInfo.GetFiles();
            }
            catch (Exception exception)
            {
                LoggingTools.WriteLogEntry<T_LogWriter>(exception);

                fileInfoArray = null;

                return false;
            }

            return true;
        }

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