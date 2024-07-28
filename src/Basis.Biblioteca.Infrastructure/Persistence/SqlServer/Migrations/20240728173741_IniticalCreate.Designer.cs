﻿// <auto-generated />
using System;
using Basis.Biblioteca.Infrastructure.Persistence.SqlServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Basis.Biblioteca.Infrastructure.Persistence.SqlServer.Migrations
{
    [DbContext(typeof(BibliotecaContext))]
    [Migration("20240728173741_IniticalCreate")]
    partial class IniticalCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Basis.Biblioteca.Domain.Entities.Assunto", b =>
                {
                    b.Property<int>("CodAs")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CodAs"));

                    b.Property<DateTime?>("DataAtualizacao")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DataCriacao")
                        .HasColumnType("datetime2");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("CodAs");

                    b.ToTable("Assunto", (string)null);
                });

            modelBuilder.Entity("Basis.Biblioteca.Domain.Entities.Autor", b =>
                {
                    b.Property<int>("CodAu")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CodAu"));

                    b.Property<DateTime?>("DataAtualizacao")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DataCriacao")
                        .HasColumnType("datetime2");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.HasKey("CodAu");

                    b.ToTable("Autor", (string)null);
                });

            modelBuilder.Entity("Basis.Biblioteca.Domain.Entities.Livro", b =>
                {
                    b.Property<int>("CodL")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CodL"));

                    b.Property<string>("AnoPublicacao")
                        .IsRequired()
                        .HasMaxLength(4)
                        .HasColumnType("nvarchar(4)");

                    b.Property<DateTime?>("DataAtualizacao")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DataCriacao")
                        .HasColumnType("datetime2");

                    b.Property<int>("Edicao")
                        .HasColumnType("int");

                    b.Property<string>("Editora")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.Property<string>("Titulo")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.HasKey("CodL");

                    b.ToTable("Livro", (string)null);
                });

            modelBuilder.Entity("Basis.Biblioteca.Domain.Entities.PrecoVenda", b =>
                {
                    b.Property<int>("CodP")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CodP"));

                    b.Property<int?>("CodL")
                        .HasColumnType("int");

                    b.Property<DateTime?>("DataAtualizacao")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DataCriacao")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("Preco")
                        .HasPrecision(9, 2)
                        .HasColumnType("decimal(9,2)");

                    b.Property<int>("TipoDeVenda")
                        .HasColumnType("int");

                    b.HasKey("CodP");

                    b.HasIndex("CodL");

                    b.ToTable("PrecoVenda", (string)null);
                });

            modelBuilder.Entity("Livro_Assunto", b =>
                {
                    b.Property<int>("CodAs")
                        .HasColumnType("int");

                    b.Property<int>("CodL")
                        .HasColumnType("int");

                    b.HasKey("CodAs", "CodL");

                    b.HasIndex("CodL");

                    b.ToTable("Livro_Assunto");
                });

            modelBuilder.Entity("Livro_Autor", b =>
                {
                    b.Property<int>("CodAu")
                        .HasColumnType("int");

                    b.Property<int>("CodL")
                        .HasColumnType("int");

                    b.HasKey("CodAu", "CodL");

                    b.HasIndex("CodL");

                    b.ToTable("Livro_Autor");
                });

            modelBuilder.Entity("Basis.Biblioteca.Domain.Entities.PrecoVenda", b =>
                {
                    b.HasOne("Basis.Biblioteca.Domain.Entities.Livro", null)
                        .WithMany("Precos")
                        .HasForeignKey("CodL");
                });

            modelBuilder.Entity("Livro_Assunto", b =>
                {
                    b.HasOne("Basis.Biblioteca.Domain.Entities.Assunto", null)
                        .WithMany()
                        .HasForeignKey("CodAs")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Basis.Biblioteca.Domain.Entities.Livro", null)
                        .WithMany()
                        .HasForeignKey("CodL")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Livro_Autor", b =>
                {
                    b.HasOne("Basis.Biblioteca.Domain.Entities.Autor", null)
                        .WithMany()
                        .HasForeignKey("CodAu")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Basis.Biblioteca.Domain.Entities.Livro", null)
                        .WithMany()
                        .HasForeignKey("CodL")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Basis.Biblioteca.Domain.Entities.Livro", b =>
                {
                    b.Navigation("Precos");
                });
#pragma warning restore 612, 618
        }
    }
}
