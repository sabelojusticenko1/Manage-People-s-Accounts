﻿// <auto-generated />
using System;
using ManagePeople.ManangeDbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ManagePeople.Migrations
{
    [DbContext(typeof(ManageDbContext))]
    partial class ManageDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.27")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ManagePeople.Models.Accounts", b =>
                {
                    b.Property<int>("IdAccount")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AccountHolder")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AccountName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AccountNo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("IdPerson")
                        .HasColumnName("IdPerson")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("IdAccount");

                    b.HasIndex("IdPerson");

                    b.ToTable("TbAccounts");
                });

            modelBuilder.Entity("ManagePeople.Models.Person", b =>
                {
                    b.Property<int>("IdPerson")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Age")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("IDNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdPerson");

                    b.ToTable("TbPerson");
                });

            modelBuilder.Entity("ManagePeople.Models.Transations", b =>
                {
                    b.Property<int>("TransationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AccountHolder")
                        .IsRequired()
                        .HasColumnType("nvarchar(15)")
                        .HasMaxLength(15);

                    b.Property<int?>("IdAccount")
                        .HasColumnName("IdAccount")
                        .HasColumnType("int");

                    b.Property<DateTime>("TransationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("TransationDetails")
                        .IsRequired()
                        .HasColumnType("nvarchar(30)")
                        .HasMaxLength(30);

                    b.HasKey("TransationId");

                    b.HasIndex("IdAccount");

                    b.ToTable("TbTransations");
                });

            modelBuilder.Entity("ManagePeople.Models.Accounts", b =>
                {
                    b.HasOne("ManagePeople.Models.Person", "IdPersonNoNavigation")
                        .WithMany("Accounts")
                        .HasForeignKey("IdPerson");
                });

            modelBuilder.Entity("ManagePeople.Models.Transations", b =>
                {
                    b.HasOne("ManagePeople.Models.Accounts", "IdAccountsNoNavigation")
                        .WithMany("Transations")
                        .HasForeignKey("IdAccount");
                });
#pragma warning restore 612, 618
        }
    }
}