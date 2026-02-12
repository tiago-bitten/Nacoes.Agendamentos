using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Postgres.Migrations
{
    /// <inheritdoc />
    public partial class EstabelecendoFk : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "ix_usuario_aprovacao_ministerio_ministerio_id",
                table: "usuario_aprovacao_ministerio",
                column: "ministerio_id");

            migrationBuilder.AddForeignKey(
                name: "fk_usuario_aprovacao_ministerio_ministerio_ministerio_id",
                table: "usuario_aprovacao_ministerio",
                column: "ministerio_id",
                principalTable: "ministerio",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_usuario_aprovacao_ministerio_ministerio_ministerio_id",
                table: "usuario_aprovacao_ministerio");

            migrationBuilder.DropIndex(
                name: "ix_usuario_aprovacao_ministerio_ministerio_id",
                table: "usuario_aprovacao_ministerio");
        }
    }
}
