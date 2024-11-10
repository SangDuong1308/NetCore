namespace NetCore.Domain.Customers.DomainEvents
{
    public sealed record CustomerEmailChangedDomainEvent(Guid CustomerId, string OldEmailAddress, string NewEmailAddress) : IDomainEvent;
}
