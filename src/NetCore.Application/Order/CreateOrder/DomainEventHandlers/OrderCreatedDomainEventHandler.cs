using NetCore.Domain.Orders.DomainEvents;
using MassTransit;

namespace NetCore.Application.Order.CreateOrder.DomainEventHandlers
{
    public sealed class OrderCreatedDomainEventHandler : IConsumer<OrderCreatedDomainEvent>
    {

        public OrderCreatedDomainEventHandler()
        {
        }
        public async Task Consume(ConsumeContext<OrderCreatedDomainEvent> context)
        {
            //Sending e-mail.
        }
    }
}
