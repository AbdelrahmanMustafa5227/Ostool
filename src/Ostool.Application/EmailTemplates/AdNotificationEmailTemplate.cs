﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ostool.Application.EmailTemplates
{
    internal static class AdNotificationEmailTemplate
    {
        public static string Get(string carName, string vendorName, decimal price)
        {
            return template
                .Replace("{{CarName}}", carName)
                .Replace("{{VendorName}}", vendorName)
                .Replace("{{Price}}", price.ToString());
        }

        private const string template = """  
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
                            <h2>Check Out This new Offer !</h2>
                        </div>
                        <div class="content">
                            <h3>{{CarName}}</h3>
                            <h4>Is listed by {{VendorName}} at price of {{Price}}$</h4>
                        </div>
                        <div class="footer">
                            <p>&copy; 2025 Ostool. All rights reserved.</p>
                        </div>
                    </div>
                </body>
                </html>
                
                """;
    }
}