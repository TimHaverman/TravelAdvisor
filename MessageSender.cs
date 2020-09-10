using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace TravelAdvisor
{
  public static class MessageSender
    {
        private const string FromAddress = "TravelAdvisorsite@gmail.com";
        private const string Password = "zzeoiuvcuyhovlfy";
        public static void SendEmail(string subject, string body, params string[] toAddesses)
        {
            using(var client = new SmtpClient())
            using(var msg = new MailMessage())
            {
                
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential(FromAddress, Password);
                client.EnableSsl = true;
                client.Port = 587;
                client.Host = "smtp.gmail.com";

                msg.From = new MailAddress(FromAddress);
                foreach (var address in toAddesses)
                {
                    msg.To.Add(address);
                }
                msg.Body = body;
                //msg.IsBodyHtml = true; is ccs and style
                msg.Subject = subject;
                //msg.Attachments(new a)

                client.Send(msg);

            }
            // FromAddress / Username: TravelAdvisorsite@gmail.com
            // Password zzeoiuvcuyhovlfy
        }
    }
}