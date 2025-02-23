using MailKit.Net.Smtp;
using MimeKit;
using MimeKit.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ostool.Infrastructure.Services
{
    public class EmailService
    {
        public EmailService()
        {

        }


        public void Send(string Subject, string To)
        {
            var message = new MimeMessage();
            message.From.Add(MailboxAddress.Parse("sulabeexsulabee@gmail.com"));
            message.To.Add(MailboxAddress.Parse(To));
            message.Subject = Subject;

            message.Body = new TextPart(TextFormat.Html)
            {
                Text = """  
                        <!DOCTYPE html>
                <html>
                <head>
                    <meta charset="UTF-8">
                    <meta name="viewport" content="width=device-width, initial-scale=1.0">
                    <title>Email Template</title>
                    <style>
                        body {
                            font-family: Arial, sans-serif;
                            background-color: #f4f4f4;
                            margin: 0;
                            padding: 0;
                        }
                        .container {
                            max-width: 600px;
                            margin: 20px auto;
                            background: #ffffff;
                            padding: 20px;
                            border-radius: 8px;
                            box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
                        }
                        .header {
                            text-align: center;
                            background: #007bff;
                            color: #ffffff;
                            padding: 15px;
                            border-radius: 8px 8px 0 0;
                        }
                        .content {
                            padding: 20px;
                            text-align: center;
                        }
                        .button {
                            display: inline-block;
                            padding: 12px 20px;
                            margin-top: 15px;
                            color: white;
                            background: #007bff;
                            text-decoration: none;
                            font-weight: bold;
                            border-radius: 5px;
                        }
                        .footer {
                            text-align: center;
                            font-size: 12px;
                            color: #777;
                            margin-top: 20px;
                            padding: 10px;
                            border-top: 1px solid #ddd;
                        }
                    </style>
                </head>
                <body>
                    <div class="container">
                        <div class="header">
                            <h2>Welcome to Our Service</h2>
                        </div>
                        <div class="content">
                            <p>Hello <strong>{{Name}}</strong>,</p>
                            <p>Thank you for signing up! Click the button below to verify your email:</p>
                            <a href="{{VerificationLink}}" class="button">Verify Email</a>
                        </div>
                        <div class="footer">
                            <p>If you didn't sign up, please ignore this email.</p>
                            <p>&copy; 2025 Your Company. All rights reserved.</p>
                        </div>
                    </div>
                </body>
                </html>
                
                """
            };

            using var client = new SmtpClient();
            client.Connect("smtp.gmail.com", 465, true);
            client.Authenticate("sulabeexsulabee@gmail.com", "dbms uehb qpbv vvvu");
            client.Send(message);
            client.Disconnect(true);
        }
    }
}