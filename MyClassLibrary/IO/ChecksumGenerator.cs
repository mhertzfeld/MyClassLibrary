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
                LoggingTools.WriteLogEntry<T_LogWriter>(exception);

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



//MyClassLibrary
//Copyright (C) 2013 Matthew Hertzfeld https://github.com/mhertzfeld
//This program is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version.
//This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU General Public License for more details.
//You should have received a copy of the GNU General Public License along with this program.  If not, see <http://www.gnu.org/licenses/>.