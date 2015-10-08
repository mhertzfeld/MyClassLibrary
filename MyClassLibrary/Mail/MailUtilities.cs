using System;
using System.Diagnostics;
using System.Net.Mail;


namespace MyClassLibrary.Mail
{
    public static class MailUtilities
    {
        public static Boolean SendMailMessage(MailMessage mailMessage, String smtpHost)
        {
            try
            {
                SmtpClient smtpClient = new SmtpClient();
                smtpClient.Host = smtpHost;
                smtpClient.UseDefaultCredentials = true;
                smtpClient.Send(mailMessage);

                return true;
            }
            catch (Exception exception)
            { Trace.WriteLine(exception); }

            MyTrace.WriteMethodError(System.Reflection.MethodBase.GetCurrentMethod());

            return false;
        }
    }
}