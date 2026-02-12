using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Postgres.Migrations
{
    /// <inheritdoc />
    public partial class Recorrencia : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_agendamento_agenda_agenda_id",
                table: "agendamento");

            migrationBuilder.DropForeignKey(
                name: "fk_atividade_ministerio_ministerio_id",
                table: "atividade");

            migrationBuilder.DropTable(
                name: "agenda");

            migrationBuilder.DropColumn(
                name: "agenda_id",
                table: "agendamento");

            migrationBuilder.RenameColumn(
                name: "agenda_id1",
                table: "agendamento",
                newName: "evento_id");

            migrationBuilder.RenameIndex(
                name: "ix_agendamento_agenda_id",
                table: "agendamento",
                newName: "ix_agendamento_evento_id");

            migrationBuilder.AlterColumn<Guid>(
                name: "ministerio_id",
                table: "atividade",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "evento",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    descricao = table.Column<string>(type: "text", nullable: false),
                    horario_data_inicial = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    horario_data_final = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    status = table.Column<int>(type: "integer", nullable: false),
                    recorrencia_id = table.Column<Guid>(type: "uuid", nullable: true),
                    tipo_recorrencia = table.Column<int>(type: "integer", nullable: false),
                    intervalo_recorrencia = table.Column<int>(type: "integer", nullable: true),
                    data_final_recorrencia = table.Column<DateOnly>(type: "date", nullable: true),
                    data_criacao = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    inativo = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_evento", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "evento_suspensao",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    evento_id = table.Column<Guid>(type: "uuid", nullable: false),
                    data_encerramento = table.Column<DateOnly>(type: "date", nullable: true),
                    data_conclusao_encerramento = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    status = table.Column<int>(type: "integer", nullable: false),
                    data_criacao = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    inativo = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_evento_suspensao", x => x.id);
                    table.ForeignKey(
                        name: "fk_evento_suspensao_evento_evento_id",
                        column: x => x.evento_id,
                        principalTable: "evento",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_evento_data_criacao_id",
                table: "evento",
                columns: new[] { "data_criacao", "id" },
                descending: new bool[0]);

            migrationBuilder.CreateIndex(
                name: "ix_evento_suspensao_evento_id",
                table: "evento_suspensao",
                column: "evento_id");

            migrationBuilder.AddForeignKey(
                name: "fk_agendamento_evento_evento_id",
                table: "agendamento",
                column: "evento_id",
                principalTable: "evento",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_atividade_ministerio_ministerio_id",
                table: "atividade",
                column: "ministerio_id",
                principalTable: "ministerio",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_agendamento_evento_evento_id",
                table: "agendamento");

            migrationBuilder.DropForeignKey(
                name: "fk_atividade_ministerio_ministerio_id",
                table: "atividade");

            migrationBuilder.DropTable(
                name: "evento_suspensao");

            migrationBuilder.DropTable(
                name: "evento");

            migrationBuilder.RenameColumn(
                name: "evento_id",
                table: "agendamento",
                newName: "agenda_id1");

            migrationBuilder.RenameIndex(
                name: "ix_agendamento_evento_id",
                table: "agendamento",
                newName: "ix_agendamento_agenda_id");

            migrationBuilder.AlterColumn<Guid>(
                name: "ministerio_id",
                table: "atividade",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddColumn<Guid>(
                name: "agenda_id",
                table: "agendamento",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "agenda",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    data_criacao = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    descricao = table.Column<string>(type: "text", nullable: false),
                    inativo = table.Column<bool>(type: "boolean", nullable: false),
                    horario_data_final = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    horario_data_inicial = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_agenda", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "ix_agenda_data_criacao_id",
                table: "agenda",
                columns: new[] { "data_criacao", "id" },
                descending: new bool[0]);

            migrationBuilder.AddForeignKey(
                name: "fk_agendamento_agenda_agenda_id",
                table: "agendamento",
                column: "agenda_id1",
                principalTable: "agenda",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_atividade_ministerio_ministerio_id",
                table: "atividade",
                column: "ministerio_id",
                principalTable: "ministerio",
                principalColumn: "id");
        }
    }
}
