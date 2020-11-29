﻿// <auto-generated />
using DatabaseParser.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
namespace DatabaseParser.Migrations
{
    [DbContext(typeof(EmployeeContext))]
    partial class EmployeeContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("DataBaseParser.Models.Employee", b =>
                {
                    b.Property<int>("EmployeeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Adress")
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Adress2")
                        .HasColumnType("varchar(50)");

                    b.Property<string>("DateOfBirth")
                        .HasColumnType("varchar(15)");

                    b.Property<string>("EmailHome")
                        .HasColumnType("varchar(50)");

                    b.Property<string>("ForeNames")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Mobile")
                        .HasColumnType("varchar(20)");

                    b.Property<string>("PayrollNumber")
                        .IsRequired()
                        .HasColumnType("varchar(10)");

                    b.Property<string>("PostCode")
                        .HasColumnType("varchar(15)");

                    b.Property<string>("StartDate")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<string>("SurName")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Telephone")
                        .HasColumnType("varchar(20)");

                    b.HasKey("EmployeeId");

                    b.ToTable("Employees");
                });
#pragma warning restore 612, 618
        }
    }
}
