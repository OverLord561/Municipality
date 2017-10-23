using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Municipality.Service
{
    public class Manager
    {
        public Task SendMail(string userName,string title)
        {
            return Task.Factory.StartNew(() =>
            {
                var fromAddress = new MailAddress("municipality561@gmail.com", userName);
               
                System.Net.Mail.MailAddress toAddress = new MailAddress("serviceemployee561@gmail.com");
               
                const string fromPassword = "89ZXcvbNM";
                const string subject = "New incident";
                string body = title;

                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
                };
                using (var message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = subject,
                    Body = body
                })
                {
                    smtp.Send(message);

                }

            });
        }
    }
}
