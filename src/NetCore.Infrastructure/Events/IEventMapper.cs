using NetCore.Application.Shared;
using NetCore.Domain;

namespace NetCore.Infrastructure.Events
{
    public interface IEventMapper
    {
        IntegrationEvent Map(IDomainEvent domainEvent);
    }
}
