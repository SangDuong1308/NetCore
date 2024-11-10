using NetCore.Domain;

namespace NetCore.Infrastructure.Events
{
    public interface IDomainEventDispatcher
    {
        Task Dispatch(IDomainEvent domainEvent);
    }
}
