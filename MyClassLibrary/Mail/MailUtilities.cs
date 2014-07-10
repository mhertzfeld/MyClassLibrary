using System;
using System.Diagnostics;
using System.Net.Mail;


namespace MyClassLibrary.Mail
{
    public static class MailUtilities
    {
        public static Boolean SendMailMessage(MailMessage mailMessage, String smtpHost)
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

                Trace.WriteLine(exception);
            }

            smtpClient = null;

            return returnState;
        }
    }
}