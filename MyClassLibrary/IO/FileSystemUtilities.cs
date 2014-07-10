using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;


namespace MyClassLibrary.IO
{
    public static class FileSystemUtilities
    {
        public static Boolean CheckFileForLock(FileInfo fileInfo, FileMode fileMode, FileAccess fileAccess, out Boolean fileLocked)
        {
            try
            {
                fileLocked = CheckForFileLock(fileInfo, fileMode, fileAccess);
            }
            catch (Exception exception)
            {
                Trace.WriteLine(exception);

                fileLocked = false;

                return false;
            }

            return true;
        }

        public static Boolean CheckFileForLock(String filePath, FileMode fileMode, FileAccess fileAccess, out Boolean fileLocked)
        {
            try
            {
                fileLocked = CheckForFileLock(filePath, fileMode, fileAccess);
            }
            catch (Exception exception)
            {
                Trace.WriteLine(exception);

                fileLocked = false;

                return false;
            }

            return true;
        }

        public static Boolean CreateNewDirectory(String path, out DirectoryInfo directoryInfo)
        {
            try
            {
                directoryInfo = Directory.CreateDirectory(path);
            }
            catch (Exception exception)
            {
                Trace.WriteLine(exception);

                directoryInfo = null;

                return false;
            }

            return true;
        }

        public static Boolean DeleteFile(FileInfo fileInfo)
        {
            try
            {
                fileInfo.Delete();
            }
            catch (Exception exception)
            {
                Trace.WriteLine(exception);

                return false;
            }

            return true;
        }

        public static Boolean DeleteFile(String path)
        {
            try
            {
                File.Delete(path);
            }
            catch (Exception exception)
            {
                Trace.WriteLine(exception);

                return false;
            }

            return true;
        }

        public static Boolean GetDirectoryInfo(String path, out DirectoryInfo directoryInfo)
        {
            try
            {
                directoryInfo = new DirectoryInfo(path);
            }
            catch (Exception exception)
            {
                Trace.WriteLine(exception);

                directoryInfo = null;

                return false;
            }

            return true;
        }

        public static Boolean GetDirectoryInfoArray(DirectoryInfo directoryInfo, out DirectoryInfo[] directoryInfoArray)
        {
            try
            {
                directoryInfoArray = directoryInfo.GetDirectories();
            }
            catch (Exception exception)
            {
                Trace.WriteLine(exception);

                directoryInfoArray = null;

                return false;
            }

            return true;
        }

        public static Boolean GetDirectoryPathArray(String path, out String[] directoryPathArray)
        {
            try
            {
                directoryPathArray = Directory.GetDirectories(path);
            }
            catch (Exception exception)
            {
                Trace.WriteLine(exception);

                directoryPathArray = null;

                return false;
            }

            return true;
        }

        public static Boolean GetFileInfo(String filePath, out FileInfo fileInfo)
        {
            try
            {
                fileInfo = new FileInfo(filePath);
            }
            catch (Exception exception)
            {
                Trace.WriteLine(exception);

                fileInfo = null;

                return false;
            }

            return true;
        }

        public static Boolean GetFileInfoArray(DirectoryInfo directoryInfo, out FileInfo[] fileInfoArray)
        {
            try
            {
                fileInfoArray = directoryInfo.GetFiles();
            }
            catch (Exception exception)
            {
                Trace.WriteLine(exception);

                fileInfoArray = null;

                return false;
            }

            return true;
        }

        public static Boolean MoveFile(FileInfo fileInfo, String destinationFilePath)
        {
            try
            {
                fileInfo.MoveTo(destinationFilePath);
            }
            catch (Exception exception)
            {
                Trace.WriteLine(exception);

                return false;
            }

            return true;
        }

        public static Boolean MoveFile(String sourceFilePath, String destinationFilePath)
        {
            try
            {
                File.Move(sourceFilePath, destinationFilePath);
            }
            catch (Exception exception)
            {
                Trace.WriteLine(exception);

                return false;
            }

            return true;
        }

        public static Boolean ReadAllLinesOfTextFile(FileInfo fileInfo, out String[] stringArray)
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
                Trace.WriteLine(exception);

                stringArray = null;

                return false;
            }

            stringArray = stringList.ToArray();

            return true;
        }

        public static Boolean ReadAllLinesOfTextFile(String filePath, out String[] stringArray)
        {
            try
            {
                stringArray = File.ReadAllLines(filePath);
            }
            catch (Exception exception)
            {
                Trace.WriteLine(exception);

                stringArray = null;

                return false;
            }

            return true;
        }

        public static Boolean WriteAllTextToFile(FileInfo fileInfo, String text)
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
                Trace.WriteLine(exception);

                return false;
            }

            return true;
        }

        public static Boolean WriteAllTextToFile(String filePath, String text)
        {
            try
            {
                File.WriteAllText(filePath, text);
            }
            catch (Exception exception)
            {
                Trace.WriteLine(exception);

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