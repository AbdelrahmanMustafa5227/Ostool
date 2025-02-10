using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ostool.Application.Abstractions.Logging
{
    internal class TestableLogger<T> : ITestableLogger<T>
    {
        private readonly ILogger<T> _logger;

        public TestableLogger(ILogger<T> logger)
        {
            _logger = logger;
        }
        public void LogError(string? message, Exception? exception = null, params object?[] args)
        {
            _logger.LogError(exception, message, args);
        }

        public void LogInformation(string? message, params object?[] args)
        {
            _logger.LogInformation(message, args);
        }
    }
}