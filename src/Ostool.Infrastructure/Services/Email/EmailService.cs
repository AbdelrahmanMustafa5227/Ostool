using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;
using Ostool.Application.Abstractions.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ostool.Infrastructure.Services.Email
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;
        private readonly EmailOptions _options;

        public EmailService(IConfiguration configuration, IOptions<EmailOptions> options)
        {
            _configuration = configuration;
            _options = options.Value;
        }

        public async Task Send(string subject, string to, string body)
        {
            var message = new MimeMessage();
            message.From.Add(MailboxAddress.Parse(_configuration["Ostool_Email"]));
            message.To.Add(MailboxAddress.Parse(to));
            message.Subject = subject;

            message.Body = new TextPart(TextFormat.Html)
            {
                Text = body
            };

            using var client = new SmtpClient();
            await client.ConnectAsync(_options.Host, _options.PortNumber, _options.UseSSL);
            await client.AuthenticateAsync(_configuration["Ostool_Email"], _configuration["Ostool_Email_AppPassword"]);
            await client.SendAsync(message);
            await client.DisconnectAsync(true);
        }
    }
}