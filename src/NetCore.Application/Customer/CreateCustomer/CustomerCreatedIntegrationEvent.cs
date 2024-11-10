using NetCore.Application.Shared;

namespace NetCore.Application.Customer.CreateCustomer
{
    public sealed record CustomerCreatedIntegrationEvent(Guid CustomerId);
}