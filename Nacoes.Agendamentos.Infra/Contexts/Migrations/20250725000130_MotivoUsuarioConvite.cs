using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Nacoes.Agendamentos.Infra.Migrations
{
    /// <inheritdoc />
    public partial class MotivoUsuarioConvite : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "motivo",
                table: "usuario_convite",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "motivo",
                table: "usuario_convite",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);
        }
    }
}
