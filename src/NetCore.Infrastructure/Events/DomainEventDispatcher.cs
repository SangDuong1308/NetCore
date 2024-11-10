using NetCore.Domain;
using MassTransit.Mediator;

namespace NetCore.Infrastructure.Events
{
    public class DomainEventDispatcher : IDomainEventDispatcher
    {
        private readonly IMediator _mediator;

        public DomainEventDispatcher(IMediator mediator)
        {
            _mediator = mediator;
        }
        public Task Dispatch(IDomainEvent domainEvent)
        {
            _mediator.Publish(domainEvent);
            return Task.CompletedTask;

        }
    }
}
