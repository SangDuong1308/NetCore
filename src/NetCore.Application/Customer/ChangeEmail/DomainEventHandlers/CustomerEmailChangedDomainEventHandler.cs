﻿using NetCore.Application.Customer.Shared;
using NetCore.Application.Shared;
using NetCore.Domain.Customers.DomainEvents;
using MassTransit;

namespace NetCore.Application.Customer.ChangeEmail.DomainEventHandlers
{
    public class CustomerEmailChangedDomainEventHandler : IConsumer<CustomerEmailChangedDomainEvent>
    {
        private readonly ICacheService _cacheService;

        public CustomerEmailChangedDomainEventHandler(ICacheService cacheService)
        {
            _cacheService = cacheService;
        }
        public async Task Consume(ConsumeContext<CustomerEmailChangedDomainEvent> context)
        {
            //Here, you could send an emails to old and new e-email addresses
            //informing about the correct change of the email address.

            // You could also include other logic here that should be part 
            // of the eventual consistency pattern.

            var customerDto = await _cacheService.GetAsync<CustomerDto>(CacheKeyBuilder.GetCustomerKey(context.Message.OldEmailAddress));
            if(customerDto is { })
            {
                await _cacheService.RemoveAsync(CacheKeyBuilder.GetCustomerKey(context.Message.OldEmailAddress));
            }
        }
    }
}