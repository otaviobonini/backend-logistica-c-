using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Logistica_API.Migrations
{
    public partial class CriarEntregas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "entregas",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    descricao = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    destino = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    peso_kg = table.Column<decimal>(type: "numeric(10,2)", nullable: false),
                    data_saida = table.Column<DateOnly>(type: "date", nullable: false),
                    data_conclusao = table.Column<DateOnly>(type: "date", nullable: true),
                    veiculo_id = table.Column<long>(type: "bigint", nullable: false),
                    motorista_id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_entregas", x => x.id);
                    table.ForeignKey(
                        name: "FK_entregas_motoristas_motorista_id",
                        column: x => x.motorista_id,
                        principalTable: "motoristas",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_entregas_veiculos_veiculo_id",
                        column: x => x.veiculo_id,
                        principalTable: "veiculos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_entregas_motorista_id",
                table: "entregas",
                column: "motorista_id");

            migrationBuilder.CreateIndex(
                name: "IX_entregas_veiculo_id",
                table: "entregas",
                column: "veiculo_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "entregas");
        }
    }
}
