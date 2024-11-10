namespace NetCore.Domain.Orders.DomainEvents
{
    public sealed record OrderCreatedDomainEvent(Guid OrderId, Guid CustomerId) : IDomainEvent;
}
