using System.Net.Mail;

namespace Service
{
    public class Mail
    {
        public static void Send(string to, string subject, string body)
        {
            var mail = new MailMessage("is@kangzen.com", to);
            var client = new SmtpClient
            {
                Port = 25,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Host = "10.10.2.31"
            };

            mail.Subject = subject;
            mail.Body = body;
            mail.IsBodyHtml = true;

            client.Send(mail);
        }
    }
}