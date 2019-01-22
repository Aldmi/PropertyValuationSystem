﻿// <auto-generated />
using System;
using Digests.Data.EfCore.DbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Digests.Data.EfCore.Migrations
{
    [DbContext(typeof(Context))]
    [Migration("20190122185946_AddSharedItems")]
    partial class AddSharedItems
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("Digests.Data.EfCore.Entities._4Company.EfCompany", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Companys");
                });

            modelBuilder.Entity("Digests.Data.EfCore.Entities._4House.EfHouse", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("City");

                    b.Property<string>("District");

                    b.Property<string>("Geo");

                    b.Property<string>("MetroStation");

                    b.Property<string>("Number");

                    b.Property<string>("Street");

                    b.Property<int>("WallMaterialId");

                    b.Property<int?>("Year");

                    b.HasKey("Id");

                    b.HasIndex("WallMaterialId");

                    b.ToTable("Houses");
                });

            modelBuilder.Entity("Digests.Data.EfCore.Entities._4House.EfWallMaterial", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("EfSharedItemsId");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("EfSharedItemsId");

                    b.ToTable("WallMaterials");
                });

            modelBuilder.Entity("Digests.Data.EfCore.Entities.Shared.EfSharedItems", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.HasKey("Id");

                    b.ToTable("SharedItems");
                });

            modelBuilder.Entity("Digests.Data.EfCore.Entities._4House.EfHouse", b =>
                {
                    b.HasOne("Digests.Data.EfCore.Entities._4House.EfWallMaterial", "WallMaterial")
                        .WithMany("Houses")
                        .HasForeignKey("WallMaterialId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Digests.Data.EfCore.Entities._4House.EfWallMaterial", b =>
                {
                    b.HasOne("Digests.Data.EfCore.Entities.Shared.EfSharedItems")
                        .WithMany("WallMaterials")
                        .HasForeignKey("EfSharedItemsId");
                });
#pragma warning restore 612, 618
        }
    }
}
