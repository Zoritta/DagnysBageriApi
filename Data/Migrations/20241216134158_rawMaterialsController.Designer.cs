﻿// <auto-generated />
using DagnysBageriApi.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DagnysBageriApi.Data.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20241216134158_rawMaterialsController")]
    partial class rawMaterialsController
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "9.0.0");

            modelBuilder.Entity("DagnysBageriApi.Entities.RawMaterial", b =>
                {
                    b.Property<int>("RawMaterialId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ItemNumber")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("RawMaterialId");

                    b.ToTable("RawMaterials");
                });

            modelBuilder.Entity("DagnysBageriApi.Entities.Supplier", b =>
                {
                    b.Property<int>("SupplierId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Address")
                        .HasColumnType("TEXT");

                    b.Property<string>("ContactPerson")
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("TEXT");

                    b.HasKey("SupplierId");

                    b.ToTable("Suppliers");
                });

            modelBuilder.Entity("DagnysBageriApi.Entities.SupplierMaterial", b =>
                {
                    b.Property<int>("SupplierId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("RawMaterialId")
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("PricePerKg")
                        .HasColumnType("TEXT");

                    b.HasKey("SupplierId", "RawMaterialId");

                    b.HasIndex("RawMaterialId");

                    b.ToTable("SupplierMaterials");
                });

            modelBuilder.Entity("DagnysBageriApi.Entities.SupplierMaterial", b =>
                {
                    b.HasOne("DagnysBageriApi.Entities.RawMaterial", "RawMaterial")
                        .WithMany("SupplierMaterials")
                        .HasForeignKey("RawMaterialId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DagnysBageriApi.Entities.Supplier", "Supplier")
                        .WithMany("SupplierMaterials")
                        .HasForeignKey("SupplierId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("RawMaterial");

                    b.Navigation("Supplier");
                });

            modelBuilder.Entity("DagnysBageriApi.Entities.RawMaterial", b =>
                {
                    b.Navigation("SupplierMaterials");
                });

            modelBuilder.Entity("DagnysBageriApi.Entities.Supplier", b =>
                {
                    b.Navigation("SupplierMaterials");
                });
#pragma warning restore 612, 618
        }
    }
}
