using NetCore.Domain;
using NetCore.Infrastructure.Events;
using NetCore.Infrastructure.Persistance.Configuration.Infrastructure;
// using NetCore.Infrastructure.Persistance.MsSql;
using NetCore.Infrastructure.Persistance.PgSql;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace NetCore.Infrastructure.BackgroundTasks
{
    public class DomainEventsProcessor : BackgroundService
    {
        private readonly ILogger<DomainEventsProcessor> _logger;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly IDomainEventDispatcher _domainEventDispatcher;
        private Dictionary<string, System.Reflection.Assembly> _assemblies = new();

        public DomainEventsProcessor(
            ILogger<DomainEventsProcessor> logger,
            IServiceScopeFactory serviceScopeFactory,
            IDomainEventDispatcher domainEventDispatcher)
        {
            _logger = logger;
            _serviceScopeFactory = serviceScopeFactory;
            _domainEventDispatcher = domainEventDispatcher;
        }

        /// <summary>
        /// Domain Events aren't being dispatched in the same transaction as saving Aggregates.
        /// Hence they have to be dispatched here as part of eventuall consistency pattern.
        /// </summary>
        /// <param name="state"></param>
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _assemblies = AppDomain.CurrentDomain.GetAssemblies()
                .ToDictionary(a => a.GetName().Name, a => a);

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    await ProcessDomainEvents(stoppingToken);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "An error occurred while executing the worker task.");
                }

                // await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
                await Task.Delay(TimeSpan.FromMinutes(3), stoppingToken);
            }
        }

        private async Task ProcessDomainEvents(CancellationToken stoppingToken)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                var events = context.Set<DomainEvent>().Where(x => x.ComplatedAt == null).ToList();

                foreach (var @event in events)
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
                                await _domainEventDispatcher.Dispatch((IDomainEvent)request);

                                context.Entry(@event).CurrentValues.SetValues(@event with { ComplatedAt = DateTime.UtcNow });
                                context.SaveChanges();
                            }
                        }
                    }

                }
            }
        }
    }

}

