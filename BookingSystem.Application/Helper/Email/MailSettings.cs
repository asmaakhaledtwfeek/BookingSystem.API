using System.Net.Mail;
using System.Net;

namespace BookingSystem.Application.Helper.Email
{
    public static class MailSettings
    {
        public static void Send(string toEmail, string subject, string body)
        {
            string fromEmail = "";
            string fromPassword = "";

            MailMessage message = new MailMessage()
            {
                From = new MailAddress(fromEmail),
                Subject = subject,
                Body = body,
            };
            message.To.Add(new MailAddress(toEmail));

            var client = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(fromEmail, fromPassword),
                EnableSsl = true,
            };

            try
            {
                client.Send(message);

            }
            catch (SmtpException ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
