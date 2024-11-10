﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NetCore.Infrastructure.Persistance.PgSql;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace NetCore.Infrastructure.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("NetCore.Application.Shared.IntegrationEvent", b =>
                {
                    b.Property<Guid>("IntergrationEventId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("AssemblyName")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)");

                    b.Property<DateTime>("OccuredAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<string>("Payload")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime?>("PublishedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)");

                    b.HasKey("IntergrationEventId");

                    b.ToTable("IntegrationEvent");
                });

            modelBuilder.Entity("NetCore.Domain.Customers.Customer", b =>
                {
                    b.Property<Guid>("CustomerId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("Age")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(400)
                        .HasColumnType("character varying(400)");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<bool>("IsEmailVerified")
                        .HasColumnType("boolean");

                    b.Property<byte[]>("Version")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("bytea");

                    b.HasKey("CustomerId");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("NetCore.Domain.Orders.Order", b =>
                {
                    b.Property<Guid>("OrderId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("CustomerId")
                        .HasColumnType("uuid");

                    b.Property<byte[]>("Version")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("bytea");

                    b.HasKey("OrderId");

                    b.HasIndex("CustomerId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("NetCore.Domain.Orders.OrderItem", b =>
                {
                    b.Property<Guid>("OrderId")
                        .HasColumnType("uuid");

                    b.Property<decimal>("Discount")
                        .HasPrecision(18, 2)
                        .HasColumnType("numeric(18,2)");

                    b.Property<Guid?>("OrderItemId")
                        .HasColumnType("uuid");

                    b.Property<long>("ProductId")
                        .HasColumnType("bigint");

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<long>("Quantity")
                        .HasColumnType("bigint");

                    b.HasKey("OrderId");

                    b.ToTable("OrderItem");
                });

            modelBuilder.Entity("NetCore.Infrastructure.Persistance.Configuration.Infrastructure.DomainEvent", b =>
                {
                    b.Property<Guid>("DomainEventId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("AssemblyName")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)");

                    b.Property<DateTime?>("ComplatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("OccuredAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<string>("Payload")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)");

                    b.HasKey("DomainEventId");

                    b.ToTable("DomainEvent");
                });

            modelBuilder.Entity("NetCore.Domain.Customers.Customer", b =>
                {
                    b.OwnsOne("NetCore.Domain.Customers.Address", "Address", b1 =>
                        {
                            b1.Property<Guid>("CustomerId")
                                .HasColumnType("uuid");

                            b1.Property<string>("Country")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("character varying(100)");

                            b1.Property<string>("FlatNumber")
                                .IsRequired()
                                .HasMaxLength(15)
                                .HasColumnType("character varying(15)");

                            b1.Property<string>("HouseNumber")
                                .IsRequired()
                                .HasMaxLength(15)
                                .HasColumnType("character varying(15)");

                            b1.Property<string>("PostalCode")
                                .IsRequired()
                                .HasMaxLength(6)
                                .HasColumnType("character varying(6)");

                            b1.Property<string>("Street")
                                .IsRequired()
                                .HasMaxLength(255)
                                .HasColumnType("character varying(255)");

                            b1.HasKey("CustomerId");

                            b1.ToTable("Customers");

                            b1.WithOwner()
                                .HasForeignKey("CustomerId");
                        });

                    b.Navigation("Address")
                        .IsRequired();
                });

            modelBuilder.Entity("NetCore.Domain.Orders.Order", b =>
                {
                    b.HasOne("NetCore.Domain.Customers.Customer", null)
                        .WithMany()
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("NetCore.Domain.Orders.ShippingAddress", "ShippingAddress", b1 =>
                        {
                            b1.Property<Guid>("OrderId")
                                .HasColumnType("uuid");

                            b1.Property<string>("PostalCode")
                                .IsRequired()
                                .HasMaxLength(6)
                                .HasColumnType("character varying(6)");

                            b1.Property<string>("Street")
                                .IsRequired()
                                .HasMaxLength(255)
                                .HasColumnType("character varying(255)");

                            b1.HasKey("OrderId");

                            b1.ToTable("Orders");

                            b1.WithOwner()
                                .HasForeignKey("OrderId");
                        });

                    b.Navigation("ShippingAddress")
                        .IsRequired();
                });

            modelBuilder.Entity("NetCore.Domain.Orders.OrderItem", b =>
                {
                    b.HasOne("NetCore.Domain.Orders.Order", null)
                        .WithMany("OrderItems")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("NetCore.Domain.Orders.Money", "Price", b1 =>
                        {
                            b1.Property<Guid>("OrderItemOrderId")
                                .HasColumnType("uuid");

                            b1.Property<decimal>("Amount")
                                .HasPrecision(18, 2)
                                .HasColumnType("numeric(18,2)");

                            b1.Property<string>("Currency")
                                .IsRequired()
                                .HasColumnType("text");

                            b1.HasKey("OrderItemOrderId");

                            b1.ToTable("OrderItem");

                            b1.WithOwner()
                                .HasForeignKey("OrderItemOrderId");
                        });

                    b.Navigation("Price")
                        .IsRequired();
                });

            modelBuilder.Entity("NetCore.Domain.Orders.Order", b =>
                {
                    b.Navigation("OrderItems");
                });
#pragma warning restore 612, 618
        }
    }
}