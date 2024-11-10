﻿using NetCore.Domain.Customers.DomainEvents;
using MassTransit;

namespace NetCore.Application.Customer.CreateCustomer.DomainEventHandlers
{
    public class CustomerCreatedDomainEventHandler : IConsumer<CustomerCreatedDomainEvent>
    {
        public Task Consume(ConsumeContext<CustomerCreatedDomainEvent> context)
        {
            //Here, you could send a welcome email to the newly created customer.

            // You could also include other logic here that should be part 
            // of the eventual consistency pattern.

            return Task.CompletedTask;
        }
    }
}
