using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Postgres.Migrations
{
    /// <inheritdoc />
    public partial class Eventos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "agendamento");

            migrationBuilder.AddColumn<int>(
                name: "quantidade_maxima_reservas",
                table: "evento",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "quantidade_reservas",
                table: "evento",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "reserva",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    evento_id = table.Column<Guid>(type: "uuid", nullable: false),
                    voluntario_ministerio_id = table.Column<Guid>(type: "uuid", nullable: false),
                    atividade_id = table.Column<Guid>(type: "uuid", nullable: false),
                    status = table.Column<string>(type: "text", nullable: false),
                    origem = table.Column<string>(type: "text", nullable: false),
                    data_criacao = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    inativo = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_reserva", x => x.id);
                    table.ForeignKey(
                        name: "fk_reserva_atividade_atividade_id",
                        column: x => x.atividade_id,
                        principalTable: "atividade",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_reserva_evento_evento_id",
                        column: x => x.evento_id,
                        principalTable: "evento",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_reserva_voluntario_ministerio_voluntario_ministerio_id",
                        column: x => x.voluntario_ministerio_id,
                        principalTable: "voluntario_ministerio",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_reserva_atividade_id",
                table: "reserva",
                column: "atividade_id");

            migrationBuilder.CreateIndex(
                name: "ix_reserva_data_criacao_id",
                table: "reserva",
                columns: new[] { "data_criacao", "id" },
                descending: new bool[0]);

            migrationBuilder.CreateIndex(
                name: "ix_reserva_evento_id",
                table: "reserva",
                column: "evento_id");

            migrationBuilder.CreateIndex(
                name: "ix_reserva_voluntario_ministerio_id",
                table: "reserva",
                column: "voluntario_ministerio_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "reserva");

            migrationBuilder.DropColumn(
                name: "quantidade_maxima_reservas",
                table: "evento");

            migrationBuilder.DropColumn(
                name: "quantidade_reservas",
                table: "evento");

            migrationBuilder.CreateTable(
                name: "agendamento",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    evento_id = table.Column<Guid>(type: "uuid", nullable: false),
                    atividade_id = table.Column<Guid>(type: "uuid", nullable: false),
                    data_criacao = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    inativo = table.Column<bool>(type: "boolean", nullable: false),
                    origem = table.Column<string>(type: "text", nullable: false),
                    status = table.Column<string>(type: "text", nullable: false),
                    voluntario_ministerio_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_agendamento", x => x.id);
                    table.ForeignKey(
                        name: "fk_agendamento_atividade_atividade_id",
                        column: x => x.atividade_id,
                        principalTable: "atividade",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_agendamento_evento_evento_id",
                        column: x => x.evento_id,
                        principalTable: "evento",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_agendamento_voluntario_ministerio_voluntario_ministerio_id",
                        column: x => x.voluntario_ministerio_id,
                        principalTable: "voluntario_ministerio",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_agendamento_atividade_id",
                table: "agendamento",
                column: "atividade_id");

            migrationBuilder.CreateIndex(
                name: "ix_agendamento_data_criacao_id",
                table: "agendamento",
                columns: new[] { "data_criacao", "id" },
                descending: new bool[0]);

            migrationBuilder.CreateIndex(
                name: "ix_agendamento_evento_id",
                table: "agendamento",
                column: "evento_id");

            migrationBuilder.CreateIndex(
                name: "ix_agendamento_voluntario_ministerio_id",
                table: "agendamento",
                column: "voluntario_ministerio_id");
        }
    }
}
