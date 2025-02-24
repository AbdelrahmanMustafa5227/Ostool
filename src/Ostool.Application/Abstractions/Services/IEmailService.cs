using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ostool.Application.Abstractions.Services
{
    public interface IEmailService
    {
        Task Send(string Subject, string To, string body);
    }
}