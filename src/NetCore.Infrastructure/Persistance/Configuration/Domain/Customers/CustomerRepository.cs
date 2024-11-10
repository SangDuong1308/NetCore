using NetCore.Domain.Customers;
// using NetCore.Infrastructure.Persistance.MsSql;
using NetCore.Infrastructure.Persistance.PgSql;
using Microsoft.EntityFrameworkCore;

namespace NetCore.Infrastructure.Persistance.Configuration.Domain.Customers
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly AppDbContext _appDbContext;

        public CustomerRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task AddAsync(Customer customer, CancellationToken cancellationToken = default)
            => await _appDbContext.AddAsync(customer);

        public async Task<Customer?> GetAsync(string email, CancellationToken cancellationToken = default)
            => await _appDbContext.Set<Customer>().Where(x => ((string)x.Email).Contains(email)).SingleOrDefaultAsync();

    }
}
