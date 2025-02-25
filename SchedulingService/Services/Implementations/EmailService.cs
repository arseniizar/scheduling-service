using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using SchedulingService.Services.Interfaces;

namespace SchedulingService.Services.Implementations
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;
        private readonly string _host;
        private readonly int _port;
        private readonly bool _enableSsl;
        private readonly string _username;
        private readonly string _password;
        private readonly string _from;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));

            _host = configuration["Smtp:Host"] ??
                    throw new InvalidOperationException("Missing Smtp:Host configuration.");
            _port = int.Parse(configuration["Smtp:Port"] ??
                              throw new InvalidOperationException("Missing Smtp:Port configuration."));
            _enableSsl = bool.Parse(configuration["Smtp:EnableSsl"] ??
                                    throw new InvalidOperationException("Missing Smtp:EnableSsl configuration."));
            _username = configuration["Smtp:Username"] ??
                        throw new InvalidOperationException("Missing Smtp:Username configuration.");
            _password = configuration["Smtp:Password"] ??
                        throw new InvalidOperationException("Missing Smtp:Password configuration.");
            _from = configuration["Smtp:From"] ??
                    throw new InvalidOperationException("Missing Smtp:From configuration.");
        }

        public async Task SendEmailAsync(string to, string subject, string body)
        {
            using var smtpClient = new SmtpClient();
            smtpClient.Host = _host;
            smtpClient.Port = _port;
            smtpClient.EnableSsl = _enableSsl;
            smtpClient.Credentials = new NetworkCredential(_username, _password);

            var mailMessage = new MailMessage
            {
                From = new MailAddress(_from),
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            };
            mailMessage.To.Add(to);

            await smtpClient.SendMailAsync(mailMessage);
        }
    }
}