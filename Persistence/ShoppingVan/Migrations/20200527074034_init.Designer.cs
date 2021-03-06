// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Persistence.ShoppingVan;

namespace Persistence.ShoppingVan.Migrations
{
    [DbContext(typeof(ShoppingVanContext))]
    [Migration("20200527074034_init")]
    partial class init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Domain.ShoppingVanBoundedContext.AggregatesModel.ShoppingVanAggregate.ShoppingVan", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedDateUtc")
                        .HasColumnType("datetime2");

                    b.Property<string>("CustomerId")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ShoppingVans");
                });

            modelBuilder.Entity("Domain.ShoppingVanBoundedContext.AggregatesModel.ShoppingVanAggregate.ShoppingVanItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Amount")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDateUtc")
                        .HasColumnType("datetime2");

                    b.Property<string>("ProductId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("ShoppingVanId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("ShoppingVanId");

                    b.ToTable("ShoppingVanItems");
                });

            modelBuilder.Entity("Domain.ShoppingVanBoundedContext.AggregatesModel.ShoppingVanAggregate.ShoppingVanItem", b =>
                {
                    b.HasOne("Domain.ShoppingVanBoundedContext.AggregatesModel.ShoppingVanAggregate.ShoppingVan", null)
                        .WithMany("ShoppingVanItems")
                        .HasForeignKey("ShoppingVanId");
                });
#pragma warning restore 612, 618
        }
    }
}
