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

                LoggingUtilities.WriteLogEntry<T_LogWriter>(exception);
            }

            smtpClient = null;

            return returnState;
        }
    }
}