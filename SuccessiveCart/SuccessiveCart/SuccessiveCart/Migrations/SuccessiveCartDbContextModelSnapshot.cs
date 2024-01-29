﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SuccessiveCart.Data;

#nullable disable

namespace SuccessiveCart.Migrations
{
    [DbContext(typeof(SuccessiveCartDbContext))]
    partial class SuccessiveCartDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("SuccessiveCart.Models.Domain.CartItem", b =>
                {
                    b.Property<int>("CartId")
                        .HasColumnType("int");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("ProductQuantity")
                        .HasColumnType("int");

                    b.HasKey("CartId");

                    b.ToTable("CartItems");
                });

            modelBuilder.Entity("SuccessiveCart.Models.Domain.Cateogry", b =>
                {
                    b.Property<int>("CateogryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CateogryId"));

                    b.Property<string>("CateogryName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CateogryPhoto")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CateogryId");

                    b.ToTable("Cateogries");
                });

            modelBuilder.Entity("SuccessiveCart.Models.Domain.Products", b =>
                {
                    b.Property<int>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProductId"));

                    b.Property<int?>("CartItemCartId")
                        .HasColumnType("int");

                    b.Property<int>("CateogryId")
                        .HasColumnType("int");

                    b.Property<bool>("IsAvailable")
                        .HasColumnType("bit");

                    b.Property<bool>("IsTrending")
                        .HasColumnType("bit");

                    b.Property<DateTime>("ProductCreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("ProductDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProductPhoto")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("ProductPrice")
                        .HasColumnType("float");

                    b.HasKey("ProductId");

                    b.HasIndex("CartItemCartId");

                    b.HasIndex("CateogryId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("SuccessiveCart.Models.Domain.Users", b =>
                {
                    b.Property<int>("UserID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserID"));

                    b.Property<string>("UserEmail")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserPassword")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("UserPhoneNo")
                        .HasColumnType("bigint");

                    b.Property<string>("UserRole")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserID");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("SuccessiveCart.Models.Domain.CartItem", b =>
                {
                    b.HasOne("SuccessiveCart.Models.Domain.Users", "Users")
                        .WithOne("CartItems")
                        .HasForeignKey("SuccessiveCart.Models.Domain.CartItem", "CartId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Users");
                });

            modelBuilder.Entity("SuccessiveCart.Models.Domain.Products", b =>
                {
                    b.HasOne("SuccessiveCart.Models.Domain.CartItem", null)
                        .WithMany("Products")
                        .HasForeignKey("CartItemCartId");

                    b.HasOne("SuccessiveCart.Models.Domain.Cateogry", "Cateogries")
                        .WithMany("Products")
                        .HasForeignKey("CateogryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cateogries");
                });

            modelBuilder.Entity("SuccessiveCart.Models.Domain.CartItem", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("SuccessiveCart.Models.Domain.Cateogry", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("SuccessiveCart.Models.Domain.Users", b =>
                {
                    b.Navigation("CartItems")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
