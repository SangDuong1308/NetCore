﻿using NetCore.Application.Shared;
// using NetCore.Infrastructure.Persistance.MsSql;
using NetCore.Infrastructure.Persistance.PgSql;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Reflection;
using System.Text.Json;

namespace NetCore.Infrastructure.BackgroundTasks
{
    public class IntegrationEventsProcessor : BackgroundService
    {
        private readonly ILogger<IntegrationEventsProcessor> _logger;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private Dictionary<string, System.Reflection.Assembly> _assemblies = new();

        public IntegrationEventsProcessor(ILogger<IntegrationEventsProcessor> logger, IServiceScopeFactory serviceScopeFactory)
        {
            _logger = logger;
            _serviceScopeFactory = serviceScopeFactory;
        }

        /// <summary>
        /// TODO: Add comment
        /// </summary>
        /// <param name="stoppingToken"></param>
        /// <returns></returns>
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _assemblies = AppDomain.CurrentDomain.GetAssemblies()
                .ToDictionary(a => a.GetName().Name, a => a);

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    await ProcessIntegrationEvents(stoppingToken);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "An error occurred while executing the worker task.");
                }

                // await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
                await Task.Delay(TimeSpan.FromMinutes(3), stoppingToken);
            }
        }

        private async Task ProcessIntegrationEvents(CancellationToken stoppingToken)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                var publishEndpoint = scope.ServiceProvider.GetRequiredService<IPublishEndpoint>();

                var integrationEvents = context.Set<IntegrationEvent>().Where(x => x.PublishedAt == null).ToList();

                foreach (var @event in integrationEvents)
                {
                    var assembly = _assemblies.SingleOrDefault(assembly => assembly.Value.GetName().Name == @event.AssemblyName);
                    if (assembly is { })
                    {
                        var eventType = assembly.Value.GetType(@event.Type);
                        if (eventType != null)
                        {
                            var request = JsonSerializer.Deserialize(@event.Payload, eventType);

                            if (request != null)
                            {
                                await publishEndpoint.Publish(request);

                                context.Entry(@event).CurrentValues.SetValues(@event with { PublishedAt = DateTime.UtcNow });
                                context.SaveChanges();
                            }
                        }
                    }

                }
            }
        }
    }

}
