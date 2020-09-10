﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Persistence.OrderManagment;

namespace Persistence.OrderManagment.Migrations
{
    [DbContext(typeof(OrderContext))]
    partial class OrderContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("Relational:Sequence:shared.OrderNumbers", "'OrderNumbers', 'shared', '1000', '1', '', '', 'Int32', 'False'")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Domain.OrderManagment.AggregatesModel.OrderAggregate.Order", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CustomerArea")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CustomerCity")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CustomerCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CustomerId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CustomerLocationOnMap")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CustomerName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CustomerShopAddress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CustomerShopName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("OrderCanceledDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("OrderConfirmedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("OrderDeliveredDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("OrderNumber")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValueSql("NEXT VALUE FOR shared.OrderNumbers");

                    b.Property<DateTime>("OrderPlacedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("OrderShippedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("OrderStatus")
                        .HasColumnType("int");

                    b.Property<float>("TotalPrice")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("Domain.OrderManagment.AggregatesModel.OrderAggregate.OrderItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OrderId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("OrderId1")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("PhotoUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProductId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProductName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UnitCount")
                        .HasColumnType("int");

                    b.Property<string>("UnitId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UnitName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("UnitPrice")
                        .HasColumnType("real");

                    b.Property<float>("UnitSellingPrice")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.HasIndex("OrderId1");

                    b.ToTable("OrderItems");
                });

            modelBuilder.Entity("Domain.OrderManagment.AggregatesModel.OrderAggregate.OrderItem", b =>
                {
                    b.HasOne("Domain.OrderManagment.AggregatesModel.OrderAggregate.Order", "Order")
                        .WithMany("OrderItems")
                        .HasForeignKey("OrderId1");
                });
#pragma warning restore 612, 618
        }
    }
}
