using NetCore.Application.Shared;
using NetCore.Domain;
using NetCore.Domain.Customers;
using NetCore.Domain.Customers.DomainEvents;
using NetCore.Domain.Orders;
using NetCore.Infrastructure.BackgroundTasks;
using NetCore.Infrastructure.Events;
using NetCore.Infrastructure.Exceptions;
using NetCore.Infrastructure.Persistance.Configuration.Domain.Customers;
using NetCore.Infrastructure.Persistance.Configuration.Domain.Orders;
using NetCore.Infrastructure.Shared;
using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace NetCore.Infrastructure.Installers
{
    public static class DependencyInjectionInstaller
    {
        public static void InstallDependencyInjectionRegistrations(this WebApplicationBuilder builder)
        {
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
            builder.Services.AddScoped<IOrderRepository, OrderRepository>();
            builder.Services.AddTransient<IDateTimeProvider, DateTimeProvider>();
            builder.Services.AddTransient<IDomainEventDispatcher, DomainEventDispatcher>();
            builder.Services.AddHostedService<DomainEventsProcessor>();
            builder.Services.AddHostedService<IntegrationEventsProcessor>();

            builder.Services.AddTransient<CustomerCreatedEventMapper>();
            builder.Services.AddSingleton<EventMapperFactory>(provider =>
            {
                var mappers = new Dictionary<Type, IEventMapper>
                {
                    { typeof(CustomerCreatedDomainEvent), provider.GetRequiredService<CustomerCreatedEventMapper>() },
                };

                return new EventMapperFactory(mappers);
            });
            builder.Services.AddValidatorsFromAssemblyContaining<IApplicationValidator>(ServiceLifetime.Transient);
            builder.Services.AddProblemDetails();
            builder.Services.AddExceptionHandler<CommandValidationExceptionHandler>();
            builder.Services.AddSingleton<ICacheService, CacheService>();
        }

    }
}
