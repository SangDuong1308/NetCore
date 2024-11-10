namespace NetCore.Domain.Orders
{
    public interface IOrderRepository
    {
        Task AddAsync(Order order);
    }
}
