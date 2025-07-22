using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Nacoes.Agendamentos.Infra.Migrations
{
    /// <inheritdoc />
    public partial class Historico : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "origem_cadastro",
                table: "voluntario",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "historico",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    entidade_id = table.Column<string>(type: "text", nullable: true),
                    data = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    usuario_id = table.Column<string>(type: "text", nullable: true),
                    acao = table.Column<string>(type: "text", nullable: false),
                    tipo_acao = table.Column<string>(type: "text", nullable: false),
                    detalhes = table.Column<string>(type: "text", nullable: true),
                    created_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    inactive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_historico", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "ix_historico_id_created_at",
                table: "historico",
                columns: new[] { "id", "created_at" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "historico");

            migrationBuilder.DropColumn(
                name: "origem_cadastro",
                table: "voluntario");
        }
    }
}
