using NetCore.Domain.Customers;
using NetCore.Domain.Orders;
using NetCore.Domain.Orders.DomainEvents;
using NetCore.Domain.Orders.Exceptions;

namespace NetCore.Domain.UnitTests
{
    public class OrderTests
    {
        [Fact]
        internal void Should_Create_Order_For_Valid_Input_Data()
        {
            // Arrange
            var customerId = new CustomerId(Guid.NewGuid());
            var shippingAddress = new ShippingAddress("Fifth Avenue 10A", "10037");

            // Act
            var order = Order.Create(customerId, shippingAddress);

            // Assert
            var domainEvents = order.DomainEvents;
            Assert.Single(domainEvents.OfType<OrderCreatedDomainEvent>());
        }

        [Fact]
        internal void Should_Throw_Maximum_Quantity_Exceeded_Domain_Exception_When_Quantity_Is_Higher_Then_5()
        {
            // Arrange
            var customerId = new CustomerId(Guid.NewGuid());
            var shippingAddress = new ShippingAddress("Fifth Avenue 10A", "10037");

            // Act && Assert
            Assert.Throws<MaximumQuantityExceededDomainException>(() =>
            {
                Order.Create(customerId, shippingAddress).AddOrderItem(1, "Tent", 100, "USD", 6);
            });
        }
    }
}