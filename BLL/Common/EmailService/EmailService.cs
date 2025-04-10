using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace BLLServices.Common.EmailService
{
    public class EmailService : IEmailService
    {
        public void SendEmail(Email email)
        {
            var client = new SmtpClient("smtp.gmail.com", 587);
                
            client.EnableSsl = true;

            client.Credentials = new NetworkCredential("doctordotnet62@gmail.com", "dznbacdlebzliuuk");

            client.Send("doctordotnet62@gmail.com", email.To, email.Subject, email.Body);

        }
    }
}
