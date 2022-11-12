using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using net5_job_schedule_with_email.Contracts;
using net5_job_schedule_with_email.Models;
using System;
using System.Threading.Tasks;

namespace net5_job_schedule_with_email.Repository
{
    public class EmailRepository : IEmailContracts
    {
        private MailConfiguration _mailConfiguration;
        public EmailRepository(MailConfiguration mailConfiguration)
        {
            _mailConfiguration = mailConfiguration;
        }
        /// <summary>
        /// To Send The Mail Purpose Of Job Scheduling
        /// </summary>
        /// <param name="emailAddress"></param>
        /// <returns></returns>
        public async Task SendMail(string emailAddress,string subject)
        {
           
            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse(_mailConfiguration.name);
            email.To.Add(MailboxAddress.Parse(emailAddress));
            email.Subject = subject;

            DateTime dateTime = DateTime.Now;
            string Message = string.Concat("(Minutes : (" + dateTime.Minute + "))", "(Seconds : (" + dateTime.Second + "))", "(MilliSeconds : (" + dateTime.Millisecond + "))");

            BodyBuilder builder = new BodyBuilder();
            builder.TextBody = Message;
            email.Body = builder.ToMessageBody();

            using var smtp = new SmtpClient();
            smtp.Connect(_mailConfiguration.host, _mailConfiguration.port, SecureSocketOptions.StartTls);
            smtp.Authenticate(_mailConfiguration.userName, _mailConfiguration.password);
            await smtp.SendAsync(email);
            smtp.Disconnect(true);
        }
    }
}
