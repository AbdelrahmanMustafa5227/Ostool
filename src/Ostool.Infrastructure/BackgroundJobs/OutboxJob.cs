using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Ostool.Application.Abstractions.Repositories;
using Ostool.Application.Abstractions.Services;
using Ostool.Application.Outbox;
using Ostool.Application.Outbox.Events;
using Ostool.Infrastructure.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Ostool.Infrastructure.BackgroundJobs
{
    internal class OutboxJob : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<OutboxJob> _logger;

        public OutboxJob(IServiceProvider serviceProvider, ILogger<OutboxJob> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Outbox Job Started");

                using (var scope = _serviceProvider.CreateScope())
                {
                    var _outboxRepository = scope.ServiceProvider.GetRequiredService<IOutboxRepository>();
                    var messages = await _outboxRepository.GetUnprocessedMessages();

                    if (messages.Count > 0)
                    {
                        var _emailService = scope.ServiceProvider.GetRequiredService<IEmailService>();
                        var _unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

                        foreach (var message in messages)
                        {
                            if (message.EventType == OutboxEventType.Email)
                            {
                                var emailEvent = JsonSerializer.Deserialize<SendEmailOutboxEvent>(message.Content)!;
                                int result = 1;
                                try
                                {
                                    await _emailService.Send(emailEvent.Subject, emailEvent.Email, emailEvent.Body);
                                }
                                catch (Exception ex)
                                {
                                    _logger.LogError(ex, "Error While Processing Outbox Message {0}", message.Id);
                                    result = 0;
                                }

                                if (result == 1)
                                    message.Processed = true;
                            }
                        }

                        await _unitOfWork.SaveChangesAsync();
                    }

                }

                await Task.Delay(5000, stoppingToken);
            }
        }
    }
}