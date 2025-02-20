using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ostool.Application.Abstractions.Authentication
{
    public class JwtToken
    {
        public JwtToken(string accessToken, Guid refreshToken)
        {
            AccessToken = accessToken;
            RefreshToken = refreshToken;
        }

        public string AccessToken { get; set; } = string.Empty;
        public Guid RefreshToken { get; set; }
    }
}