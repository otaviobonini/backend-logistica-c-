using System;
using Logistica_API.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Logistica_API.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20261010214245_CriarEntregas")]
    partial class CriarEntregas
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Logistica_API.Models.Entrega", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<DateOnly?>("DataConclusao")
                        .HasColumnType("date")
                        .HasColumnName("data_conclusao");

                    b.Property<DateOnly>("DataSaida")
                        .HasColumnType("date")
                        .HasColumnName("data_saida");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("descricao");

                    b.Property<string>("Destino")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("destino");

                    b.Property<long>("MotoristaId")
                        .HasColumnType("bigint")
                        .HasColumnName("motorista_id");

                    b.Property<decimal>("PesoKg")
                        .HasColumnType("numeric(10,2)")
                        .HasColumnName("peso_kg");

                    b.Property<long>("VeiculoId")
                        .HasColumnType("bigint")
                        .HasColumnName("veiculo_id");

                    b.HasKey("Id");

                    b.HasIndex("MotoristaId");

                    b.HasIndex("VeiculoId");

                    b.ToTable("entregas");
                });

            modelBuilder.Entity("Logistica_API.Models.Motorista", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("Cnh")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)")
                        .HasColumnName("cnh");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("character varying(150)")
                        .HasColumnName("nome");

                    b.Property<DateOnly>("ValidadeCnh")
                        .HasColumnType("date")
                        .HasColumnName("validade_cnh");

                    b.HasKey("Id");

                    b.ToTable("motoristas");
                });

            modelBuilder.Entity("Logistica_API.Models.Veiculo", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<decimal>("CapacidadeKg")
                        .HasColumnType("numeric(10,2)")
                        .HasColumnName("capacidade_kg");

                    b.Property<string>("Modelo")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("modelo");

                    b.Property<string>("Placa")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("character varying(10)")
                        .HasColumnName("placa");

                    b.HasKey("Id");

                    b.ToTable("veiculos");
                });

            modelBuilder.Entity("Logistica_API.Models.Entrega", b =>
                {
                    b.HasOne("Logistica_API.Models.Motorista", "Motorista")
                        .WithMany()
                        .HasForeignKey("MotoristaId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Logistica_API.Models.Veiculo", "Veiculo")
                        .WithMany()
                        .HasForeignKey("VeiculoId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Motorista");

                    b.Navigation("Veiculo");
                });
#pragma warning restore 612, 618
        }
    }
}
