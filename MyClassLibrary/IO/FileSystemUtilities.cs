using MyClassLibrary.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;


namespace MyClassLibrary.IO
{
    public static class FileSystemUtilities
    {
        public static Boolean CheckFileForLock<T_LogWriter>(FileInfo fileInfo, FileMode fileMode, FileAccess fileAccess, out Boolean fileLocked)
            where T_LogWriter : Logging.ILogWriter, new()
        {
            try
            {
                fileLocked = CheckForFileLock(fileInfo, fileMode, fileAccess);
            }
            catch (Exception exception)
            {
                LoggingUtilities.WriteLogEntry<T_LogWriter>(exception);

                fileLocked = false;

                return false;
            }

            return true;
        }

        public static Boolean CheckFileForLock<T_LogWriter>(String filePath, FileMode fileMode, FileAccess fileAccess, out Boolean fileLocked)
            where T_LogWriter : Logging.ILogWriter, new()
        {
            try
            {
                fileLocked = CheckForFileLock(filePath, fileMode, fileAccess);
            }
            catch (Exception exception)
            {
                LoggingUtilities.WriteLogEntry<T_LogWriter>(exception);

                fileLocked = false;

                return false;
            }

            return true;
        }

        public static Boolean CreateNewDirectory<T_LogWriter>(String path, out DirectoryInfo directoryInfo)
            where T_LogWriter : Logging.ILogWriter, new()
        {
            try
            {
                directoryInfo = Directory.CreateDirectory(path);
            }
            catch (Exception exception)
            {
                LoggingUtilities.WriteLogEntry<T_LogWriter>(exception);

                directoryInfo = null;

                return false;
            }

            return true;
        }

        public static Boolean DeleteFile<T_LogWriter>(FileInfo fileInfo)
            where T_LogWriter : Logging.ILogWriter, new()
        {
            try
            {
                fileInfo.Delete();
            }
            catch (Exception exception)
            {
                LoggingUtilities.WriteLogEntry<T_LogWriter>(exception);

                return false;
            }

            return true;
        }

        public static Boolean DeleteFile<T_LogWriter>(String path)
            where T_LogWriter : Logging.ILogWriter, new()
        {
            try
            {
                File.Delete(path);
            }
            catch (Exception exception)
            {
                LoggingUtilities.WriteLogEntry<T_LogWriter>(exception);

                return false;
            }

            return true;
        }

        public static Boolean GetDirectoryInfo<T_LogWriter>(String path, out DirectoryInfo directoryInfo)
            where T_LogWriter : Logging.ILogWriter, new()
        {
            try
            {
                directoryInfo = new DirectoryInfo(path);
            }
            catch (Exception exception)
            {
                LoggingUtilities.WriteLogEntry<T_LogWriter>(exception);

                directoryInfo = null;

                return false;
            }

            return true;
        }

        public static Boolean GetDirectoryInfoArray<T_LogWriter>(DirectoryInfo directoryInfo, out DirectoryInfo[] directoryInfoArray)
            where T_LogWriter : Logging.ILogWriter, new()
        {
            try
            {
                directoryInfoArray = directoryInfo.GetDirectories();
            }
            catch (Exception exception)
            {
                LoggingUtilities.WriteLogEntry<T_LogWriter>(exception);

                directoryInfoArray = null;

                return false;
            }

            return true;
        }

        public static Boolean GetDirectoryPathArray<T_LogWriter>(String path, out String[] directoryPathArray)
            where T_LogWriter : Logging.ILogWriter, new()
        {
            try
            {
                directoryPathArray = Directory.GetDirectories(path);
            }
            catch (Exception exception)
            {
                LoggingUtilities.WriteLogEntry<T_LogWriter>(exception);

                directoryPathArray = null;

                return false;
            }

            return true;
        }

        public static Boolean GetFileInfo<T_LogWriter>(String filePath, out FileInfo fileInfo)
            where T_LogWriter : Logging.ILogWriter, new()
        {
            try
            {
                fileInfo = new FileInfo(filePath);
            }
            catch (Exception exception)
            {
                LoggingUtilities.WriteLogEntry<T_LogWriter>(exception);

                fileInfo = null;

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
                LoggingUtilities.WriteLogEntry<T_LogWriter>(exception);

                fileInfoArray = null;

                return false;
            }

            return true;
        }

        public static Boolean MoveFile<T_LogWriter>(FileInfo fileInfo, String destinationFilePath)
            where T_LogWriter : Logging.ILogWriter, new()
        {
            try
            {
                fileInfo.MoveTo(destinationFilePath);
            }
            catch (Exception exception)
            {
                LoggingUtilities.WriteLogEntry<T_LogWriter>(exception);

                return false;
            }

            return true;
        }

        public static Boolean MoveFile<T_LogWriter>(String sourceFilePath, String destinationFilePath)
            where T_LogWriter : Logging.ILogWriter, new ()
        {
            try
            {
                File.Move(sourceFilePath, destinationFilePath);
            }
            catch (Exception exception)
            {
                LoggingUtilities.WriteLogEntry<T_LogWriter>(exception);

                return false;
            }

            return true;
        }

        public static Boolean ReadAllLinesOfTextFile<T_LogWriter>(FileInfo fileInfo, out String[] stringArray)
            where T_LogWriter : Logging.ILogWriter, new()
        {
            List<String> stringList = new List<String>();

            try
            {
                using (StreamReader streamReader = fileInfo.OpenText())
                {

                    String line;
                    
                    while ((line = streamReader.ReadLine()) != null)
                    {
                        stringList.Add(line);
                    }
                }
            }
            catch (Exception exception)
            {
                LoggingUtilities.WriteLogEntry<T_LogWriter>(exception);

                stringArray = null;

                return false;
            }

            stringArray = stringList.ToArray();

            return true;
        }

        public static Boolean ReadAllLinesOfTextFile<T_LogWriter>(String filePath, out String[] stringArray)
            where T_LogWriter : Logging.ILogWriter, new()
        {
            try
            {
                stringArray = File.ReadAllLines(filePath);
            }
            catch (Exception exception)
            {
                LoggingUtilities.WriteLogEntry<T_LogWriter>(exception);

                stringArray = null;

                return false;
            }

            return true;
        }

        public static Boolean WriteAllTextToFile<T_LogWriter>(FileInfo fileInfo, String text)
            where T_LogWriter : Logging.ILogWriter, new()
        {
            try
            {
                using (FileStream fileStream = fileInfo.OpenWrite())
                {
                    Byte[] byteArray = new UTF8Encoding(true).GetBytes(text);

                    fileStream.Write(byteArray, 0, byteArray.Length);
                }
            }
            catch (Exception exception)
            {
                LoggingUtilities.WriteLogEntry<T_LogWriter>(exception);

                return false;
            }

            return true;
        }

        public static Boolean WriteAllTextToFile<T_LogWriter>(String filePath, String text)
            where T_LogWriter : Logging.ILogWriter, new()
        {
            try
            {
                File.WriteAllText(filePath, text);
            }
            catch (Exception exception)
            {
                LoggingUtilities.WriteLogEntry<T_LogWriter>(exception);

                return false;
            }

            return true;
        }


        //STATIC FUNCTIONS
        private static Boolean CheckForFileLock(FileInfo fileInfo, FileMode fileMode, FileAccess fileAccess)
        {
            try
            {
                using (FileStream fileStream = fileInfo.Open(fileMode, fileAccess)) { }

                return false;
            }
            catch (IOException ioException)
            {
                Int32 errorCode = (Int32)Marshal.GetHRForException(ioException) & ((1 << 16) - 1);

                if ((errorCode == 32) || (errorCode == 33))
                { return true; }
                else
                { throw ioException; }
            }
        }

        private static Boolean CheckForFileLock(String  filePath, FileMode fileMode, FileAccess fileAccess)
        {
            try
            {
                using (FileStream fileStream = File.Open(filePath, fileMode, fileAccess)) { }

                return false;
            }
            catch (IOException ioException)
            {
                Int32 errorCode = (Int32)Marshal.GetHRForException(ioException) & ((1 << 16) - 1);

                if ((errorCode == 32) || (errorCode == 33))
                { return true; }
                else
                { throw ioException; }
            }
        }
    }
}