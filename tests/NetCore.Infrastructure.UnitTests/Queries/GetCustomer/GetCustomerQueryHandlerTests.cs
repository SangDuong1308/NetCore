﻿using NetCore.Application.Customer.GetCustomer;
using NetCore.Application.Customer.Shared;
using NetCore.Application.Shared;
using NetCore.Domain.Customers;
using NetCore.Infrastructure.Persistance.PgSql;
using NetCore.Infrastructure.Queries.GetCustomer;
using MassTransit;
using MassTransit.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace NetCore.Infrastructure.UnitTests.Queries.GetCustomer
{
    public class GetCustomerQueryHandlerTests
    {
        private readonly Mock<ICacheService> _cacheServiceMock = new Mock<ICacheService>();
        private ServiceProvider _provider;
        private ITestHarness _harness;

        private Customer _customer = Customer.CreateCustomer(
                new CustomerId(Guid.NewGuid()),
                new FullName("Mikolaj Jankowski"),
                new Age(DateTime.UtcNow.AddYears(-20)),
                new Email("my-email@yahoo.com"),
                new Address("Fifth Avenue", "10A", "1", "USA", "10037"));

        private void SetupProviderAndHarness()
        {
            _provider = new ServiceCollection()
                .AddMassTransitTestHarness(x => x.AddConsumer<GetCustomerQueryHandler>())
                .AddSingleton(_cacheServiceMock.Object)
                .AddDbContext<IAppDbContext, AppDbContext>(options => options.UseInMemoryDatabase("TestDatabase"))
                .BuildServiceProvider(true);

            _harness = _provider.GetRequiredService<ITestHarness>();
        }

        [Fact]
        public async Task Should_Get_Customer_From_Cache()
        {
            //Arrange
            var sqlQueries = new List<string>();

            SetupProviderAndHarness();

            var harness = _provider.GetRequiredService<ITestHarness>();

            var expectedCustomer = new CustomerDto(Guid.NewGuid(), "Mikolaj Jankowski", 35, "mikolaj.jankowski@somedomain.com");

            _cacheServiceMock
                .Setup(repo => repo.GetAsync<CustomerDto>(NetCore.Application.Shared.CacheKeyBuilder.GetCustomerKey(expectedCustomer.Email)))
                .ReturnsAsync(expectedCustomer);

            await harness.Start();
            var query = new GetCustomerQuery(expectedCustomer.Email);


            var client = harness.GetRequestClient<GetCustomerQuery>();

            //Act
            var response = await client.GetResponse<CustomerDto>(query);

            //Assert
            Assert.True(await harness.Sent.Any<CustomerDto>());
            _cacheServiceMock.Verify(repo => repo.GetAsync<CustomerDto>(It.IsAny<string>()), Times.Exactly(1));
            Assert.Empty(sqlQueries);
            Assert.Equal(response.Message, expectedCustomer);

        }


        [Fact]
        public async Task Should_Get_Customer_From_Db_When_Not_Present_In_Cache()
        {
            //Arrange
            SetupProviderAndHarness();

            using (var scope = _provider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                context.Customers.Add(_customer);
                context.SaveChanges();
            }

            var harness = _provider.GetRequiredService<ITestHarness>();

            _cacheServiceMock
                .Setup(repo => repo.GetAsync<CustomerDto?>(NetCore.Application.Shared.CacheKeyBuilder.GetCustomerKey(_customer.Email.Value)))
                .ReturnsAsync((CustomerDto?)null);

            await harness.Start();

            var client = harness.GetRequestClient<GetCustomerQuery>();

            //Act
            var response = await client.GetResponse<CustomerDto>(new GetCustomerQuery(_customer.Email.Value));

            //Assert
            Assert.True(await harness.Sent.Any<CustomerDto>());
            _cacheServiceMock.Verify(repo => repo.GetAsync<CustomerDto>(It.IsAny<string>()), Times.Exactly(1));
            Assert.Equal(response.Message, _customer.ToDto());

        }

    }
}
