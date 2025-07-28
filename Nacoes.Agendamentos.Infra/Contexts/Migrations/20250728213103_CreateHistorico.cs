using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Nacoes.Agendamentos.Infra.Migrations
{
    /// <inheritdoc />
    public partial class CreateHistorico : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_agendamento_agenda_agenda_id",
                table: "agendamento");

            migrationBuilder.DropForeignKey(
                name: "fk_voluntario_ministerio_voluntario_voluntario_id1",
                table: "voluntario_ministerio");

            migrationBuilder.DropIndex(
                name: "ix_historico_data_criacao_id",
                table: "historico");

            migrationBuilder.DropColumn(
                name: "data_criacao",
                table: "historico");

            migrationBuilder.DropColumn(
                name: "inativo",
                table: "historico");
            
            migrationBuilder.Sql("ALTER TABLE historico ALTER COLUMN usuario_id TYPE uuid USING usuario_id::uuid;");

            migrationBuilder.Sql("ALTER TABLE historico ALTER COLUMN entidade_id TYPE uuid USING entidade_id::uuid;");

            migrationBuilder.AlterColumn<Guid>(
                name: "voluntario_id1",
                table: "voluntario_ministerio",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "usuario_id",
                table: "historico",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "entidade_id",
                table: "historico",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "usuario_acao",
                table: "historico",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<Guid>(
                name: "agenda_id1",
                table: "agendamento",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "fk_agendamento_agenda_agenda_id",
                table: "agendamento",
                column: "agenda_id1",
                principalTable: "agenda",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_voluntario_ministerio_voluntario_voluntario_id1",
                table: "voluntario_ministerio",
                column: "voluntario_id1",
                principalTable: "voluntario",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_agendamento_agenda_agenda_id",
                table: "agendamento");

            migrationBuilder.DropForeignKey(
                name: "fk_voluntario_ministerio_voluntario_voluntario_id1",
                table: "voluntario_ministerio");

            migrationBuilder.DropColumn(
                name: "usuario_acao",
                table: "historico");

            migrationBuilder.AlterColumn<Guid>(
                name: "voluntario_id1",
                table: "voluntario_ministerio",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");
            
            migrationBuilder.Sql("ALTER TABLE historico ALTER COLUMN usuario_id TYPE text USING usuario_id::text;");

            migrationBuilder.Sql("ALTER TABLE historico ALTER COLUMN entidade_id TYPE text USING entidade_id::text;");

            migrationBuilder.AlterColumn<string>(
                name: "usuario_id",
                table: "historico",
                type: "text",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "entidade_id",
                table: "historico",
                type: "text",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "data_criacao",
                table: "historico",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<bool>(
                name: "inativo",
                table: "historico",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<Guid>(
                name: "agenda_id1",
                table: "agendamento",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.CreateIndex(
                name: "ix_historico_data_criacao_id",
                table: "historico",
                columns: new[] { "data_criacao", "id" },
                descending: new bool[0]);

            migrationBuilder.AddForeignKey(
                name: "fk_agendamento_agenda_agenda_id",
                table: "agendamento",
                column: "agenda_id1",
                principalTable: "agenda",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_voluntario_ministerio_voluntario_voluntario_id1",
                table: "voluntario_ministerio",
                column: "voluntario_id1",
                principalTable: "voluntario",
                principalColumn: "id");
        }
    }
}
