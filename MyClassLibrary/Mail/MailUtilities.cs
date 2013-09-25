using MyClassLibrary.Logging;
using System;
using System.Diagnostics;
using System.Net.Mail;


namespace MyClassLibrary.Mail
{
    public static class MailUtilities
    {
        public static Boolean SendMailMessage<T_LogWriter>(MailMessage mailMessage, String smtpHost)
            where T_LogWriter : Logging.ILogWriter, new ()
        {
            Boolean returnState = true;

            SmtpClient smtpClient = new SmtpClient();
            smtpClient.Host = smtpHost;
            smtpClient.UseDefaultCredentials = true;

            try
            {
                smtpClient.Send(mailMessage);
            }
            catch (Exception exception)
            {
                returnState = false;

                LoggingTools.WriteLogEntry<T_LogWriter>(exception);
            }

            smtpClient = null;

            return returnState;
        }
    }
}



//MyClassLibrary
//Copyright (C) 2013 Matthew Hertzfeld https://github.com/mhertzfeld
//This program is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version.
//This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU General Public License for more details.
//You should have received a copy of the GNU General Public License along with this program.  If not, see <http://www.gnu.org/licenses/>.