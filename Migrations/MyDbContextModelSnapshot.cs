﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using kazariobranco_backend.Database;

#nullable disable

namespace kazariobranco_backend.Migrations
{
    [DbContext(typeof(MyDbContext))]
    partial class MyDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("kazariobranco_backend.Models.AddressModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("varchar(40)")
                        .HasColumnName("Adress");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("varchar(20)")
                        .HasColumnName("City");

                    b.Property<string>("District")
                        .IsRequired()
                        .HasColumnType("varchar(30)")
                        .HasColumnName("District");

                    b.Property<int>("Number")
                        .HasColumnType("int")
                        .HasColumnName("Number");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasColumnType("varchar(20)")
                        .HasColumnName("State");

                    b.Property<int?>("UserModelId")
                        .HasColumnType("int");

                    b.Property<int>("ZipCode")
                        .HasColumnType("int")
                        .HasColumnName("ZipCode");

                    b.HasKey("Id")
                        .HasName("PkAddressId");

                    b.HasIndex("UserModelId");

                    b.ToTable("Addresses", (string)null);
                });

            modelBuilder.Entity("kazariobranco_backend.Models.CartModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.HasKey("Id");

                    b.ToTable("CartModel");
                });

            modelBuilder.Entity("kazariobranco_backend.Models.ContactModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("CartId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("date")
                        .HasColumnName("CreatedAt");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("Description");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("varchar(40)")
                        .HasColumnName("Email");

                    b.Property<DateTime>("EndedAt")
                        .HasColumnType("date")
                        .HasColumnName("EndedAt");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(40)")
                        .HasColumnName("Name");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("varchar(11)")
                        .HasColumnName("Phone");

                    b.Property<string>("Reason")
                        .IsRequired()
                        .HasColumnType("varchar(11)")
                        .HasColumnName("Reason");

                    b.HasKey("Id")
                        .HasName("pk_contact_id");

                    b.HasIndex("CartId");

                    b.ToTable("Contacts", (string)null);
                });

            modelBuilder.Entity("kazariobranco_backend.Models.ProductModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("CartModelId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CartModelId");

                    b.ToTable("ProductModel");
                });

            modelBuilder.Entity("kazariobranco_backend.Models.UserModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("CartId")
                        .HasColumnType("int");

                    b.Property<string>("Cpf")
                        .IsRequired()
                        .HasColumnType("varchar(84)")
                        .HasColumnName("Cpf");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("date")
                        .HasColumnName("CreatedAt");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("varchar(40)")
                        .HasColumnName("Email");

                    b.Property<string>("Firstname")
                        .IsRequired()
                        .HasColumnType("varchar(20)")
                        .HasColumnName("Firstname");

                    b.Property<string>("Lastname")
                        .IsRequired()
                        .HasColumnType("varchar(20)")
                        .HasColumnName("Lastname");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("varchar(85)")
                        .HasColumnName("Password");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("varchar(32)")
                        .HasColumnName("Phone");

                    b.Property<string>("Role")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(5)")
                        .HasColumnName("Role")
                        .HasDefaultValueSql("user");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("date")
                        .HasColumnName("UpdatedAt");

                    b.HasKey("Id")
                        .HasName("PkUserId");

                    b.HasIndex("CartId");

                    b.HasIndex("Cpf")
                        .IsUnique();

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("Phone")
                        .IsUnique();

                    b.ToTable("Users", (string)null);
                });

            modelBuilder.Entity("kazariobranco_backend.Models.AddressModel", b =>
                {
                    b.HasOne("kazariobranco_backend.Models.UserModel", null)
                        .WithMany("Addresses")
                        .HasForeignKey("UserModelId");
                });

            modelBuilder.Entity("kazariobranco_backend.Models.ContactModel", b =>
                {
                    b.HasOne("kazariobranco_backend.Models.CartModel", "Cart")
                        .WithMany()
                        .HasForeignKey("CartId");

                    b.Navigation("Cart");
                });

            modelBuilder.Entity("kazariobranco_backend.Models.ProductModel", b =>
                {
                    b.HasOne("kazariobranco_backend.Models.CartModel", null)
                        .WithMany("Products")
                        .HasForeignKey("CartModelId");
                });

            modelBuilder.Entity("kazariobranco_backend.Models.UserModel", b =>
                {
                    b.HasOne("kazariobranco_backend.Models.CartModel", "Cart")
                        .WithMany()
                        .HasForeignKey("CartId");

                    b.Navigation("Cart");
                });

            modelBuilder.Entity("kazariobranco_backend.Models.CartModel", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("kazariobranco_backend.Models.UserModel", b =>
                {
                    b.Navigation("Addresses");
                });
#pragma warning restore 612, 618
        }
    }
}
