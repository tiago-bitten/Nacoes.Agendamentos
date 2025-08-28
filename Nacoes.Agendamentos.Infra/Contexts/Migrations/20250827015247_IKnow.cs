using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Nacoes.Agendamentos.Infra.Migrations
{
    /// <inheritdoc />
    public partial class IKnow : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "usuario_convite_ministerio",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v1()"),
                    convite_id = table.Column<Guid>(type: "uuid", nullable: false),
                    ministerio_id = table.Column<Guid>(type: "uuid", nullable: false),
                    data_criacao = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    inativo = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_usuario_convite_ministerio", x => x.id);
                    table.ForeignKey(
                        name: "fk_usuario_convite_ministerio_ministerio_ministerio_id",
                        column: x => x.ministerio_id,
                        principalTable: "ministerio",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_usuario_convite_ministerio_usuario_convite_convite_id",
                        column: x => x.convite_id,
                        principalTable: "usuario_convite",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "usuario_ministerio",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    usuario_id = table.Column<Guid>(type: "uuid", nullable: false),
                    ministerio_id = table.Column<Guid>(type: "uuid", nullable: false),
                    status = table.Column<int>(type: "integer", nullable: false),
                    data_criacao = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    inativo = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_usuario_ministerio", x => x.id);
                    table.ForeignKey(
                        name: "fk_usuario_ministerio_ministerio_ministerio_id",
                        column: x => x.ministerio_id,
                        principalTable: "ministerio",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_usuario_ministerio_usuario_usuario_id",
                        column: x => x.usuario_id,
                        principalTable: "usuario",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_usuario_convite_ministerio_convite_id",
                table: "usuario_convite_ministerio",
                column: "convite_id");

            migrationBuilder.CreateIndex(
                name: "ix_usuario_convite_ministerio_ministerio_id",
                table: "usuario_convite_ministerio",
                column: "ministerio_id");

            migrationBuilder.CreateIndex(
                name: "ix_usuarioconviteministerio_data_criacao_id",
                table: "usuario_convite_ministerio",
                columns: new[] { "data_criacao", "id" },
                descending: new bool[0]);

            migrationBuilder.CreateIndex(
                name: "ix_usuario_ministerio_ministerio_id",
                table: "usuario_ministerio",
                column: "ministerio_id");

            migrationBuilder.CreateIndex(
                name: "ix_usuario_ministerio_usuario_id",
                table: "usuario_ministerio",
                column: "usuario_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "usuario_convite_ministerio");

            migrationBuilder.DropTable(
                name: "usuario_ministerio");
        }
    }
}
