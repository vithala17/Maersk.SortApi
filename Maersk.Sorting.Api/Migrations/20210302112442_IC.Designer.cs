﻿// <auto-generated />
using Maersk.Sorting.Api.Data_Layer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Maersk.Sorting.Api.Migrations
{
    [DbContext(typeof(ApplicationDBContext))]
    [Migration("20210302112442_IC")]
    partial class IC
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.3")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Maersk.Sorting.Api.Data_Layer.SortJobEntity", b =>
                {
                    b.Property<string>("id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<long>("duration")
                        .HasColumnType("bigint");

                    b.Property<string>("input")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("output")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.ToTable("SortJobs");
                });
#pragma warning restore 612, 618
        }
    }
}
