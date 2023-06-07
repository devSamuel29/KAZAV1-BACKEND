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

                    b.Property<int>("UserId")
                        .HasColumnType("int")
                        .HasColumnName("UserId");

                    b.Property<int>("ZipCode")
                        .HasColumnType("int")
                        .HasColumnName("ZipCode");

                    b.HasKey("Id")
                        .HasName("PkAddressId");

                    b.HasIndex("UserId");

                    b.ToTable("Address", (string)null);
                });

            modelBuilder.Entity("kazariobranco_backend.Models.CartModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("UserId")
                        .HasColumnType("int")
                        .HasColumnName("UserId");

                    b.HasKey("Id")
                        .HasName("PkCartId");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Carts");
                });

            modelBuilder.Entity("kazariobranco_backend.Models.ContactModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

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

                    b.ToTable("Contacts", (string)null);
                });

            modelBuilder.Entity("kazariobranco_backend.Models.OrderModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CartId")
                        .HasColumnType("int");

                    b.HasKey("Id")
                        .HasName("PkAOrderId");

                    b.HasIndex("CartId");

                    b.ToTable("Orders", (string)null);
                });

            modelBuilder.Entity("kazariobranco_backend.Models.OrderProductModel", b =>
                {
                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.HasKey("OrderId", "ProductId");

                    b.HasIndex("ProductId");

                    b.ToTable("OrderProductModel");
                });

            modelBuilder.Entity("kazariobranco_backend.Models.ProductModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.HasKey("Id");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("kazariobranco_backend.Models.UserModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

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
                    b.HasOne("kazariobranco_backend.Models.UserModel", "User")
                        .WithMany("Addresses")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FkAddressUserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("kazariobranco_backend.Models.CartModel", b =>
                {
                    b.HasOne("kazariobranco_backend.Models.UserModel", "User")
                        .WithOne("Cart")
                        .HasForeignKey("kazariobranco_backend.Models.CartModel", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FkUserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("kazariobranco_backend.Models.OrderModel", b =>
                {
                    b.HasOne("kazariobranco_backend.Models.CartModel", "Cart")
                        .WithMany("Orders")
                        .HasForeignKey("CartId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cart");
                });

            modelBuilder.Entity("kazariobranco_backend.Models.OrderProductModel", b =>
                {
                    b.HasOne("kazariobranco_backend.Models.OrderModel", "Order")
                        .WithMany("OrderProducts")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("kazariobranco_backend.Models.ProductModel", "Product")
                        .WithMany("OrderProducts")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("kazariobranco_backend.Models.CartModel", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("kazariobranco_backend.Models.OrderModel", b =>
                {
                    b.Navigation("OrderProducts");
                });

            modelBuilder.Entity("kazariobranco_backend.Models.ProductModel", b =>
                {
                    b.Navigation("OrderProducts");
                });

            modelBuilder.Entity("kazariobranco_backend.Models.UserModel", b =>
                {
                    b.Navigation("Addresses");

                    b.Navigation("Cart");
                });
#pragma warning restore 612, 618
        }
    }
}
