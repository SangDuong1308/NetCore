using NetCore.Application.Customer.GetCustomer;
using NetCore.Application.Customer.Shared;
using NetCore.Application.Exceptions;
using NetCore.Application.Shared;
using NetCore.Domain.Customers;
using NetCore.Infrastructure.Persistance.PgSql;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace NetCore.Infrastructure.Queries.GetCustomer
{
    public sealed class GetCustomerQueryHandler : IConsumer<GetCustomerQuery>
    {
        private readonly IAppDbContext _appDbContext;
        private readonly ICacheService _cacheService;

        public GetCustomerQueryHandler(IAppDbContext appDbContext, ICacheService cacheService)
        {
            _appDbContext = appDbContext;
            _cacheService = cacheService;
        }
        /// <summary>
        /// This handler demonstrates the usage of the Cache Aside Pattern.
        /// First, we check if the data is available in the cache (Redis). If not,
        /// we retrieve the data from the database and store it in the cache.
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        /// <exception cref="CustomerNotFoundApplicationException"></exception>
        public async Task Consume(ConsumeContext<GetCustomerQuery> query)
        {
            var cachedCustomerDto = await _cacheService.GetAsync<CustomerDto>(CacheKeyBuilder.GetCustomerKey(query.Message.Email));
            if (cachedCustomerDto is { })
            {
                await query.RespondAsync(cachedCustomerDto);
                return;
            }

            var email = query.Message.Email;
            var customer = await _appDbContext
                .Customers
                .TagWith(nameof(GetCustomerQueryHandler))
                .AsNoTracking()
                .FirstOrDefaultAsync(x=> ((string)x.Email) == email);

            if (customer is null)
            {
                throw new CustomerNotFoundApplicationException(email);
            }

            await _cacheService.SetAsync(CacheKeyBuilder.GetCustomerKey(query.Message.Email), customer.ToDto());
            await query.RespondAsync(customer.ToDto());
        }
    }
}
