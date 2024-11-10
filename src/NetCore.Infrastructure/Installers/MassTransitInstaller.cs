using NetCore.Application.Customer.CreateCustomer;
using NetCore.Domain;
using NetCore.Infrastructure.Filters.MassTransit;
using NetCore.Infrastructure.Settings;
using MassTransit;
using MassTransit.Internals;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using NetCore.Application.Customer.GetCustomer;
using NetCore.Infrastructure.Queries.GetCustomer;
using NetCore.Application.Order.GetOrder;
using NetCore.Infrastructure.Queries.GetOrder;

namespace NetCore.Infrastructure.Installers
{
    public static class MassTransitInstaller
    {
        public static void InstallMassTransit(this WebApplicationBuilder builder)
        {
            var rabbitMqSettings = builder.Configuration.GetSection(nameof(AppSettings)).Get<AppSettings>()!.RabbitMq;

            builder.Services.AddMediator(cfg =>
            {
                AddMediatorConsumersFromAssembly(cfg);

                // Register a Request Client for GetCustomerQuery, enabling it to send request/response messages
                cfg.AddRequestClient<GetCustomerQuery>();

                // Register GetCustomerQueryHandler as the consumer for handling GetCustomerQuery messages
                cfg.AddConsumer<GetCustomerQueryHandler>();

                cfg.AddRequestClient<GetOrderQuery>();
                cfg.AddConsumer<GetOrderQueryHandler>();

                cfg.ConfigureMediator((context, cfg) =>
                {
                    //The order of filter registration matters.

                    cfg.UseConsumeFilter(typeof(ValidationFilter<>), context, x => x.Include(type => !type.HasInterface<IDomainEvent>()));
                    cfg.UseConsumeFilter(typeof(LoggingFilter<>), context, x => x.Include(type => !type.HasInterface<IDomainEvent>()));
                    cfg.UseConsumeFilter(typeof(RedisFilter<>), context, x => x.Include(type => !type.HasInterface<IDomainEvent>()));
                    cfg.UseConsumeFilter(typeof(EventsFilter<>), context, x => x.Include(type => !type.HasInterface<IDomainEvent>()));
                    cfg.UseConsumeFilter(typeof(HtmlSanitizerFilter<>), context, x => x.Include(type => !type.HasInterface<IDomainEvent>()));


                    //cfg.UseConsumeFilter<GetCustomerQueryCacheFilter>(context);


                    //cfg.UseMessageRetry(x => x.Interval(3, TimeSpan.FromSeconds(15))); //causes long response to HTTP requests
                });
            });

            builder.Services.AddMassTransit(x =>
            {
                //below Consumers for RabbitMq
                x.AddConsumer<CustomerCreatedIntegrationEventHandler>();

                x.SetKebabCaseEndpointNameFormatter();

                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(rabbitMqSettings.Host);

                    cfg.ConfigureEndpoints(context);
                });
            });
        }

        private static void AddMediatorConsumersFromAssembly(IMediatorRegistrationConfigurator cfg)
        {
            cfg.AddConsumers(typeof(CreateCustomerCommandHandler).Assembly);
        }
    }

}
