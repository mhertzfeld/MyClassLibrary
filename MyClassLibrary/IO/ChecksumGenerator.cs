using MyClassLibrary.Logging;
using System;
using System.IO;
using System.Security.Cryptography;


namespace MyClassLibrary.IO
{
    public static class ChecksumGenerator
    {
        public static Boolean GenerateChecksum<T_LogWriter>(String filePath, out String checksum)
            where T_LogWriter : Logging.ILogWriter, new()
        {
            if (filePath == null)
            {
                throw new ArgumentNullException("filePath");
            }

            Byte[] byteArray = null;

            checksum = null;

            FileStream fileStream = null;

            SHA256Managed sha256Managed = new SHA256Managed();

            try
            {
                fileStream = File.OpenRead(filePath);

                byteArray = sha256Managed.ComputeHash(fileStream);

                fileStream.Close();
            }
            catch (Exception exception)
            {
                LoggingUtilities.WriteLogEntry<T_LogWriter>(exception);

                return false;
            }
            finally
            {
                fileStream.Dispose();
            }

            if (byteArray == null)
            {
                return false;
            }
            else
            {
                checksum = BitConverter.ToString(byteArray).Replace("-", String.Empty);

                return true;
            }
        }
    }
}