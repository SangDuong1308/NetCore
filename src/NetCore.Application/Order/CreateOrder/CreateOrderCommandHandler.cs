﻿using NetCore.Application.Order.Shared;
using NetCore.Domain.Customers;
using NetCore.Domain.Orders;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace NetCore.Application.Order.CreateOrder
{

    public class CreateOrderCommandHandler : IConsumer<CreateOrderCommand>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ILogger<CreateOrderCommandHandler> _logger;

        public CreateOrderCommandHandler(IOrderRepository orderRepository, ILogger<CreateOrderCommandHandler> logger)
        {
            _orderRepository = orderRepository;
            _logger = logger;
        }


        public async Task Consume(ConsumeContext<CreateOrderCommand> command)
        {
            var order = Domain.Orders.Order.Create(
                new CustomerId(command.Message.CustomerId),
                new ShippingAddress(command.Message.Street, command.Message.PostalCode));

            foreach(var product in command.Message.Products) 
            {
                order.AddOrderItem(product.ProductId, product.ProductName, product.Price, product.Currency, product.Quantity);
            }
            await _orderRepository.AddAsync(order);
            await command.RespondAsync<OrderDto>(new OrderDto(order.OrderId.Value, order.OrderItems.ToDto()));

            _logger.LogInformation("Created an order: {OrderId} ", order.OrderId);
        }


    }
}
