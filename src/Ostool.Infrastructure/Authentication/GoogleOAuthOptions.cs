using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ostool.Infrastructure.Authentication
{
    public class GoogleOAuthOptions
    {
        public const string SectionName = "GoogleOAuthOptions";

        public string CallbackPath { get; set; } = string.Empty;
        public string AuthorizationEndpoint { get; set; } = string.Empty;
        public string TokenEndpoint { get; set; } = string.Empty;
        public string UserInformationEndpoint { get; set; } = string.Empty;
    }
}