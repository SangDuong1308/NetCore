using NetCore.Application.Exceptions;
using NetCore.Application.Order.GetOrder;
using NetCore.Application.Order.Shared;
using NetCore.Application.Shared;
using NetCore.Infrastructure.Persistance.PgSql;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace NetCore.Infrastructure.Queries.GetOrder
{
    public sealed class GetOrderQueryHandler : IConsumer<GetOrderQuery>
    {
        private readonly IAppDbContext _appDbContext;
        private readonly ICacheService _cacheService;

        public GetOrderQueryHandler(IAppDbContext appDbContext, ICacheService cacheService)
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
        /// <exception cref="OrderNotFoundApplicationException"></exception>
        public async Task Consume(ConsumeContext<GetOrderQuery> query)
        {
            var cachedOder = await _cacheService.GetAsync<OrderDto>(CacheKeyBuilder.GetOrderKey(query.Message.Id));
            if (cachedOder is { })
            {
                await query.RespondAsync(cachedOder);
                return;
            }

            var id = query.Message.Id;
            var order = await _appDbContext
                .Orders
                .TagWith(nameof(GetOrderQueryHandler))
                .AsNoTracking()
                .AsSplitQuery()
                .Include(x => x.OrderItems)
                .Where(x => ((Guid)x.OrderId) == id)
                .FirstOrDefaultAsync();

            if (order == null)
            {
                throw new OrderNotFoundApplicationException(id);
            }

            await _cacheService.SetAsync(CacheKeyBuilder.GetOrderKey(query.Message.Id), order.ToDto());
            await query.RespondAsync(order.ToDto());
        }
    }
}
