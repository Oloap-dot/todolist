﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using todolist.Data;

#nullable disable

namespace todolist.Migrations
{
    [DbContext(typeof(todolistDBContext))]
    partial class todolistDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("todolist.Models.Activity", b =>
                {
                    b.Property<short>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("smallint");

                    b.Property<string>("Assegnatario")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<DateTime?>("DataFine")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("DataInizio")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Descrizione")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("varchar(1000)");

                    b.Property<int>("Stato")
                        .HasColumnType("int");

                    b.Property<string>("StatoString")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Titolo")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("varchar(200)");

                    b.HasKey("Id");

                    b.HasIndex("Descrizione")
                        .IsUnique();

                    b.HasIndex("Titolo")
                        .IsUnique();

                    b.ToTable("Activities");
                });
#pragma warning restore 612, 618
        }
    }
}
