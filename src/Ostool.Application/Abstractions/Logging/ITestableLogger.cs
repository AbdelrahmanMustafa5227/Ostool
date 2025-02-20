using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ostool.Application.Abstractions.Logging
{
    public interface ITestableLogger<T>
    {
        void LogInformation(string? message, params object?[] args);
        void LogError(string? message, Exception? exception = null, params object?[] args);
    }
}