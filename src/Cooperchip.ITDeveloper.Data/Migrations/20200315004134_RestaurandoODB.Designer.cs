﻿// <auto-generated />
using System;
using Cooperchip.ITDeveloper.Data.ORM;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Cooperchip.ITDeveloper.Data.Migrations
{
    [DbContext(typeof(ITDeveloperDbContext))]
    [Migration("20200315004134_RestaurandoODB")]
    partial class RestaurandoODB
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Cooperchip.ITDeveloper.Domain.Entities.Mural", b =>
                {
                    b.Property<int>("MuralId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Autor")
                        .HasColumnType("varchar(90)");

                    b.Property<string>("Aviso")
                        .HasColumnType("varchar(90)");

                    b.Property<DateTime>("Data")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasColumnType("varchar(90)");

                    b.Property<string>("Titulo")
                        .HasColumnType("varchar(90)");

                    b.HasKey("MuralId");

                    b.ToTable("Mural");
                });

            modelBuilder.Entity("Cooperchip.ITDeveloper.Domain.Models.EstadoPaciente", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnName("Descricao")
                        .HasColumnType("varchar(30)")
                        .HasMaxLength(20);

                    b.HasKey("Id");

                    b.ToTable("EstadoPaciente");
                });

            modelBuilder.Entity("Cooperchip.ITDeveloper.Domain.Models.Paciente", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Ativo")
                        .HasColumnType("bit");

                    b.Property<string>("Cpf")
                        .HasColumnName("Cpf")
                        .HasColumnType("varchar(11)")
                        .IsFixedLength(true)
                        .HasMaxLength(11);

                    b.Property<DateTime>("DataInternacao")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DataNascimento")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasColumnName("Email")
                        .HasColumnType("varchar(150)");

                    b.Property<Guid>("EstadoPacienteId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnName("Nome")
                        .HasColumnType("varchar(80)");

                    b.Property<string>("Rg")
                        .HasColumnName("Rg")
                        .HasColumnType("varchar(15)")
                        .HasMaxLength(15);

                    b.Property<DateTime>("RgDataEmissao")
                        .HasColumnType("datetime2");

                    b.Property<string>("RgOrgao")
                        .HasColumnName("RgOrgao")
                        .HasColumnType("varchar(10)");

                    b.Property<int>("Sexo")
                        .HasColumnType("int");

                    b.Property<int>("TipoPaciente")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("EstadoPacienteId");

                    b.ToTable("Paciente");
                });

            modelBuilder.Entity("Cooperchip.ITDeveloper.Domain.Models.Paciente", b =>
                {
                    b.HasOne("Cooperchip.ITDeveloper.Domain.Models.EstadoPaciente", "EstadoPaciente")
                        .WithMany("Paciente")
                        .HasForeignKey("EstadoPacienteId")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
