using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Ostool.Infrastructure.Authentication
{
    public class GoogeUserInfo
    {
        public GoogeUserInfo(string email, string name)
        {
            Email = email;
            Name = name;
        }

        [JsonIgnore]
        public Guid Id { get; private set; } = Guid.NewGuid();

        [JsonPropertyName("id")]
        public string GoogleId { get; set; } = string.Empty;

        [JsonPropertyName("email")]
        public string Email { get; set; } = string.Empty;

        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("picture")]
        public string PictureUrl { get; set; } = string.Empty;
    }
}