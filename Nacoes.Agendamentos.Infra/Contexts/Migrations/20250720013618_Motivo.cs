using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Nacoes.Agendamentos.Infra.Migrations
{
    /// <inheritdoc />
    public partial class Motivo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "motivo",
                table: "usuario_convite",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "motivo",
                table: "usuario_convite");
        }
    }
}
