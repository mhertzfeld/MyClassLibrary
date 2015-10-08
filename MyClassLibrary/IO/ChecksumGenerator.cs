using System;
using System.Diagnostics;
using System.IO;
using System.Security.Cryptography;


namespace MyClassLibrary.IO
{
    public static class ChecksumGenerator
    {
        public static Boolean GenerateChecksum(String filePath, out String checksum)
        {
            if (filePath == null)
            { throw new ArgumentNullException("filePath"); }

            try
            {
                Byte[] byteArray = null;

                using (FileStream fileStream = File.OpenRead(filePath))
                {
                    using (SHA256Managed sha256Managed = new SHA256Managed())
                    {
                        byteArray = sha256Managed.ComputeHash(fileStream);

                        fileStream.Close();
                    }
                }

                if (byteArray != null)
                { 
                    checksum = BitConverter.ToString(byteArray).Replace("-", String.Empty);

                    return true;
                }
            }
            catch (Exception exception)
            { Trace.WriteLine(exception); }

            MyTrace.WriteMethodError(System.Reflection.MethodBase.GetCurrentMethod());

            checksum = null;

            return false;
        }
    }
}