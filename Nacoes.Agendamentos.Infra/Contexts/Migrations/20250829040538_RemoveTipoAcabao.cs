using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Nacoes.Agendamentos.Infra.Migrations
{
    /// <inheritdoc />
    public partial class RemoveTipoAcabao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "tipo_acao",
                table: "historico");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "tipo_acao",
                table: "historico",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
