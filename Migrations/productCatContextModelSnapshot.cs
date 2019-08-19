﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using prodCat.Models;

namespace prodCat.Migrations
{
    [DbContext(typeof(productCatContext))]
    partial class productCatContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("prodCat.Models.Association", b =>
                {
                    b.Property<int>("AssociationId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("GroupID");

                    b.Property<int>("ItemID");

                    b.HasKey("AssociationId");

                    b.HasIndex("GroupID");

                    b.HasIndex("ItemID");

                    b.ToTable("associations");
                });

            modelBuilder.Entity("prodCat.Models.Category", b =>
                {
                    b.Property<int>("CategoryId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedAt");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<DateTime>("UpdatedAt");

                    b.HasKey("CategoryId");

                    b.ToTable("categories");
                });

            modelBuilder.Entity("prodCat.Models.Product", b =>
                {
                    b.Property<int>("ProductId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedAt");

                    b.Property<string>("Description")
                        .IsRequired();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<decimal>("Price");

                    b.Property<DateTime>("UpdatedAt");

                    b.HasKey("ProductId");

                    b.ToTable("products");
                });

            modelBuilder.Entity("prodCat.Models.Association", b =>
                {
                    b.HasOne("prodCat.Models.Category", "Group")
                        .WithMany("Items")
                        .HasForeignKey("GroupID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("prodCat.Models.Product", "Item")
                        .WithMany("Groups")
                        .HasForeignKey("ItemID")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
