// <auto-generated />
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
    [Migration("20200914134242_FixCityAndArea")]
    partial class FixCityAndArea
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Domain.DistributorManagment.AggregatesModel.DistributorAggregate.Area", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CityId")
                        .HasColumnName("CityId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CityId");

                    b.ToTable("DistributorsAreas");
                });

            modelBuilder.Entity("Domain.DistributorManagment.AggregatesModel.DistributorAggregate.City", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("DistributorsCities");
                });

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

            modelBuilder.Entity("Domain.DistributorManagment.AggregatesModel.DistributorAggregate.DistributorArea", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("AreaId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<Guid>("DistributorId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("AreaId");

                    b.HasIndex("DistributorId");

                    b.ToTable("DistributorArea");
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

            modelBuilder.Entity("Domain.DistributorManagment.AggregatesModel.DistributorAggregate.Area", b =>
                {
                    b.HasOne("Domain.DistributorManagment.AggregatesModel.DistributorAggregate.City", "City")
                        .WithMany("Areas")
                        .HasForeignKey("CityId");
                });

            modelBuilder.Entity("Domain.DistributorManagment.AggregatesModel.DistributorAggregate.DistributorArea", b =>
                {
                    b.HasOne("Domain.DistributorManagment.AggregatesModel.DistributorAggregate.Area", "Area")
                        .WithMany("DistributorAreas")
                        .HasForeignKey("AreaId");

                    b.HasOne("Domain.DistributorManagment.AggregatesModel.DistributorAggregate.Distributor", "Distributor")
                        .WithMany("DistributorAreas")
                        .HasForeignKey("DistributorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
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
