using NetCore.Domain.Orders;
using NetCore.Infrastructure.Persistance.PgSql;

namespace NetCore.Infrastructure.Persistance.Configuration.Domain.Orders
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext _appDbContext;

        public OrderRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task AddAsync(Order order)
        {
            await _appDbContext.AddAsync(order);
        }
    }
}
