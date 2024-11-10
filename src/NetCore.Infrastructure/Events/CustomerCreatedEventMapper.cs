﻿using NetCore.Application.Customer.CreateCustomer;
using NetCore.Application.Shared;
using NetCore.Domain;
using NetCore.Domain.Customers.DomainEvents;
using Newtonsoft.Json;

namespace NetCore.Infrastructure.Events
{
    public class CustomerCreatedEventMapper : IEventMapper
    {
        private readonly IDateTimeProvider _dateTimeProvider;

        public CustomerCreatedEventMapper(IDateTimeProvider dateTimeProvider)
        {
            _dateTimeProvider = dateTimeProvider;
        }
        public IntegrationEvent Map(IDomainEvent domainEvent)
        {

            var integrationEvent = new IntegrationEvent(
                Guid.NewGuid(),
                _dateTimeProvider.UtcNow,
                typeof(CustomerCreatedIntegrationEvent).FullName,
                typeof(CustomerCreatedIntegrationEvent).Assembly.GetName().Name,
                JsonConvert.SerializeObject(domainEvent as CustomerCreatedDomainEvent, new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.None }));

            return integrationEvent;

        }
    }
}