using NetCore.Application.Customer.Shared;
using NetCore.Application.Order.GetOrder;
using NetCore.Application.Order.Shared;
using NetCore.Application.Shared;
using NetCore.Domain.Customers;
using NetCore.Domain.Orders;
using NetCore.Infrastructure.Persistance.PgSql;
using NetCore.Infrastructure.Queries.GetOrder;
using MassTransit;
using MassTransit.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace NetCore.Infrastructure.UnitTests.Queries.GetOrder
{
    public class GetOrderQueryHandlerTests
    {
        [Fact]
        public async Task Should_Get_Order_From_Cache()
        {
            //Arrange
            var cacheServiceMock = new Mock<ICacheService>();
            var sqlQueries = new List<string>();

            await using var provider = new ServiceCollection()
            .AddMassTransitTestHarness(x =>
            {
                x.AddConsumer<GetOrderQueryHandler>();

            })
            .AddSingleton<ICacheService>(cacheServiceMock.Object)
            .AddDbContext<IAppDbContext,AppDbContext>()
            .BuildServiceProvider(true);

            var harness = provider.GetRequiredService<ITestHarness>();

            var customerId = new CustomerId(Guid.NewGuid());
            var shippingAddress = new ShippingAddress("Fifth Avenue 10A", "10037");
            var order = Order.Create(customerId, shippingAddress).ToDto();

            cacheServiceMock
                .Setup(repo => repo.GetAsync<OrderDto>(NetCore.Application.Shared.CacheKeyBuilder.GetOrderKey(order.OrderId)))
                .ReturnsAsync(order);

            await harness.Start();
            var query = new GetOrderQuery(order.OrderId);


            var client = harness.GetRequestClient<GetOrderQuery>();

            //Act
            var response = await client.GetResponse<OrderDto>(query);

            //Assert
            Assert.True(await harness.Sent.Any<OrderDto>());
            Assert.Empty(sqlQueries);
            Assert.Equal(response.Message.OrderId, order.OrderId);
            Assert.Equal(response.Message.OrderItems, order.OrderItems);
            cacheServiceMock.Verify(repo => repo.GetAsync<OrderDto>(It.IsAny<string>()), Times.Exactly(1));

        }
    }
}
