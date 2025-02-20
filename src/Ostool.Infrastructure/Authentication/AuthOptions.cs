﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ostool.Infrastructure.Authentication
{
    public class AuthOptions
    {
        public const string SectionName = "AuthOptions";

        public string SecretKey { get; init; } = string.Empty;
        public string Issuer { get; init; } = string.Empty;
        public string Audience { get; init; } = string.Empty;
        public int AccessExpiresInMinutes { get; init; }
        public int RefreshExpiresInMinutes { get; init; }
    }
}