﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Persistence.DistributorManagment;

namespace Persistence.DistributorManagment.Migrations
{
    [DbContext(typeof(DistributorManagmentContext))]
    [Migration("20200707093912_AddingAudit")]
    partial class AddingAudit
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Domain.DistributorManagment.AggregatesModel.DistributorAggregate.Distributor", b =>
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

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Distributors");
                });

            modelBuilder.Entity("Domain.DistributorManagment.AggregatesModel.DistributorAggregate.DistributorUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AccountId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DistributorId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("DistributorId1")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FullName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("DistributorId1");

                    b.ToTable("DistributorUsers");
                });

            modelBuilder.Entity("Domain.DistributorManagment.AggregatesModel.DistributorAggregate.Distributor", b =>
                {
                    b.OwnsOne("Domain.SharedKernel.ValueObjects.Address", "Address", b1 =>
                        {
                            b1.Property<Guid>("DistributorId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Area")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("City")
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("DistributorId");

                            b1.ToTable("Distributors");

                            b1.WithOwner()
                                .HasForeignKey("DistributorId");
                        });
                });

            modelBuilder.Entity("Domain.DistributorManagment.AggregatesModel.DistributorAggregate.DistributorUser", b =>
                {
                    b.HasOne("Domain.DistributorManagment.AggregatesModel.DistributorAggregate.Distributor", "Distributor")
                        .WithMany("DistributorUsers")
                        .HasForeignKey("DistributorId1");
                });
#pragma warning restore 612, 618
        }
    }
}
