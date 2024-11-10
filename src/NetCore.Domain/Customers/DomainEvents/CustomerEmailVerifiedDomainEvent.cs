namespace NetCore.Domain.Customers.DomainEvents
{
    public sealed record CustomerEmailVerifiedDomainEvent(string NewEmailAddress) : IDomainEvent;
}
