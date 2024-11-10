﻿using NetCore.Domain.Customers;
using NetCore.Domain.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace NetCore.Infrastructure.Persistance.Configuration.Domain.Orders
{
    internal class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(x => x.OrderId);
            builder.Property(x => x.OrderId).HasConversion(x => x.Value, v => new OrderId(v));
            builder.HasOne<Customer>().WithMany().HasForeignKey(x => x.CustomerId);
            builder.HasMany(x => x.OrderItems).WithOne().HasForeignKey(x => x.OrderId);
            builder.OwnsOne(x => x.ShippingAddress, shippingAddresBuilder =>
            {
                shippingAddresBuilder.Property(x => x.PostalCode).HasMaxLength(6);
                shippingAddresBuilder.Property(x => x.Street).HasMaxLength(255);
            });
            builder.Property<byte[]>("Version").IsRowVersion();
        }
    }
}
