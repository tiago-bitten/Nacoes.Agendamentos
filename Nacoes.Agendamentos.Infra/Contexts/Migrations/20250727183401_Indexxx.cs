using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Nacoes.Agendamentos.Infra.Migrations
{
    /// <inheritdoc />
    public partial class Indexxx : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_voluntario_ministerio_voluntario_voluntario_id",
                table: "voluntario_ministerio");

            migrationBuilder.DropIndex(
                name: "ix_voluntario_ministerio_id_created_at",
                table: "voluntario_ministerio");

            migrationBuilder.DropIndex(
                name: "ix_voluntario_id_created_at",
                table: "voluntario");

            migrationBuilder.DropIndex(
                name: "ix_usuario_convite_id_created_at",
                table: "usuario_convite");

            migrationBuilder.DropIndex(
                name: "ix_usuario_id_created_at",
                table: "usuario");

            migrationBuilder.DropIndex(
                name: "ix_ministerio_id_created_at",
                table: "ministerio");

            migrationBuilder.DropIndex(
                name: "ix_historico_id_created_at",
                table: "historico");

            migrationBuilder.DropIndex(
                name: "ix_atividade_id_created_at",
                table: "atividade");

            migrationBuilder.DropIndex(
                name: "ix_agendamento_id_created_at",
                table: "agendamento");

            migrationBuilder.DropIndex(
                name: "ix_agenda_id_created_at",
                table: "agenda");

            migrationBuilder.RenameColumn(
                name: "inactive",
                table: "voluntario_ministerio",
                newName: "inativo");

            migrationBuilder.RenameColumn(
                name: "created_at",
                table: "voluntario_ministerio",
                newName: "criado_em");

            migrationBuilder.RenameColumn(
                name: "inactive",
                table: "voluntario",
                newName: "inativo");

            migrationBuilder.RenameColumn(
                name: "created_at",
                table: "voluntario",
                newName: "criado_em");

            migrationBuilder.RenameColumn(
                name: "inactive",
                table: "usuario_convite",
                newName: "inativo");

            migrationBuilder.RenameColumn(
                name: "created_at",
                table: "usuario_convite",
                newName: "criado_em");

            migrationBuilder.RenameColumn(
                name: "inactive",
                table: "usuario",
                newName: "inativo");

            migrationBuilder.RenameColumn(
                name: "created_at",
                table: "usuario",
                newName: "criado_em");

            migrationBuilder.RenameColumn(
                name: "inactive",
                table: "ministerio",
                newName: "inativo");

            migrationBuilder.RenameColumn(
                name: "created_at",
                table: "ministerio",
                newName: "criado_em");

            migrationBuilder.RenameColumn(
                name: "inactive",
                table: "historico",
                newName: "inativo");

            migrationBuilder.RenameColumn(
                name: "created_at",
                table: "historico",
                newName: "criado_em");

            migrationBuilder.RenameColumn(
                name: "inactive",
                table: "atividade",
                newName: "inativo");

            migrationBuilder.RenameColumn(
                name: "created_at",
                table: "atividade",
                newName: "criado_em");

            migrationBuilder.RenameColumn(
                name: "inactive",
                table: "agendamento",
                newName: "inativo");

            migrationBuilder.RenameColumn(
                name: "created_at",
                table: "agendamento",
                newName: "criado_em");

            migrationBuilder.RenameColumn(
                name: "inactive",
                table: "agenda",
                newName: "inativo");

            migrationBuilder.RenameColumn(
                name: "created_at",
                table: "agenda",
                newName: "criado_em");

            migrationBuilder.AlterColumn<Guid>(
                name: "voluntario_id",
                table: "voluntario_ministerio",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "voluntario_id1",
                table: "voluntario_ministerio",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "ix_voluntario_ministerio_ministerio_id",
                table: "voluntario_ministerio",
                column: "ministerio_id");

            migrationBuilder.CreateIndex(
                name: "ix_voluntario_ministerio_voluntario_id1",
                table: "voluntario_ministerio",
                column: "voluntario_id1");

            migrationBuilder.AddForeignKey(
                name: "fk_voluntario_ministerio_ministerio_ministerio_id",
                table: "voluntario_ministerio",
                column: "ministerio_id",
                principalTable: "ministerio",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_voluntario_ministerio_voluntario_voluntario_id",
                table: "voluntario_ministerio",
                column: "voluntario_id",
                principalTable: "voluntario",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_voluntario_ministerio_voluntario_voluntario_id1",
                table: "voluntario_ministerio",
                column: "voluntario_id1",
                principalTable: "voluntario",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_voluntario_ministerio_ministerio_ministerio_id",
                table: "voluntario_ministerio");

            migrationBuilder.DropForeignKey(
                name: "fk_voluntario_ministerio_voluntario_voluntario_id",
                table: "voluntario_ministerio");

            migrationBuilder.DropForeignKey(
                name: "fk_voluntario_ministerio_voluntario_voluntario_id1",
                table: "voluntario_ministerio");

            migrationBuilder.DropIndex(
                name: "ix_voluntario_ministerio_ministerio_id",
                table: "voluntario_ministerio");

            migrationBuilder.DropIndex(
                name: "ix_voluntario_ministerio_voluntario_id1",
                table: "voluntario_ministerio");

            migrationBuilder.DropColumn(
                name: "voluntario_id1",
                table: "voluntario_ministerio");

            migrationBuilder.RenameColumn(
                name: "inativo",
                table: "voluntario_ministerio",
                newName: "inactive");

            migrationBuilder.RenameColumn(
                name: "criado_em",
                table: "voluntario_ministerio",
                newName: "created_at");

            migrationBuilder.RenameColumn(
                name: "inativo",
                table: "voluntario",
                newName: "inactive");

            migrationBuilder.RenameColumn(
                name: "criado_em",
                table: "voluntario",
                newName: "created_at");

            migrationBuilder.RenameColumn(
                name: "inativo",
                table: "usuario_convite",
                newName: "inactive");

            migrationBuilder.RenameColumn(
                name: "criado_em",
                table: "usuario_convite",
                newName: "created_at");

            migrationBuilder.RenameColumn(
                name: "inativo",
                table: "usuario",
                newName: "inactive");

            migrationBuilder.RenameColumn(
                name: "criado_em",
                table: "usuario",
                newName: "created_at");

            migrationBuilder.RenameColumn(
                name: "inativo",
                table: "ministerio",
                newName: "inactive");

            migrationBuilder.RenameColumn(
                name: "criado_em",
                table: "ministerio",
                newName: "created_at");

            migrationBuilder.RenameColumn(
                name: "inativo",
                table: "historico",
                newName: "inactive");

            migrationBuilder.RenameColumn(
                name: "criado_em",
                table: "historico",
                newName: "created_at");

            migrationBuilder.RenameColumn(
                name: "inativo",
                table: "atividade",
                newName: "inactive");

            migrationBuilder.RenameColumn(
                name: "criado_em",
                table: "atividade",
                newName: "created_at");

            migrationBuilder.RenameColumn(
                name: "inativo",
                table: "agendamento",
                newName: "inactive");

            migrationBuilder.RenameColumn(
                name: "criado_em",
                table: "agendamento",
                newName: "created_at");

            migrationBuilder.RenameColumn(
                name: "inativo",
                table: "agenda",
                newName: "inactive");

            migrationBuilder.RenameColumn(
                name: "criado_em",
                table: "agenda",
                newName: "created_at");

            migrationBuilder.AlterColumn<Guid>(
                name: "voluntario_id",
                table: "voluntario_ministerio",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.CreateIndex(
                name: "ix_voluntario_ministerio_id_created_at",
                table: "voluntario_ministerio",
                columns: new[] { "id", "created_at" });

            migrationBuilder.CreateIndex(
                name: "ix_voluntario_id_created_at",
                table: "voluntario",
                columns: new[] { "id", "created_at" });

            migrationBuilder.CreateIndex(
                name: "ix_usuario_convite_id_created_at",
                table: "usuario_convite",
                columns: new[] { "id", "created_at" });

            migrationBuilder.CreateIndex(
                name: "ix_usuario_id_created_at",
                table: "usuario",
                columns: new[] { "id", "created_at" });

            migrationBuilder.CreateIndex(
                name: "ix_ministerio_id_created_at",
                table: "ministerio",
                columns: new[] { "id", "created_at" });

            migrationBuilder.CreateIndex(
                name: "ix_historico_id_created_at",
                table: "historico",
                columns: new[] { "id", "created_at" });

            migrationBuilder.CreateIndex(
                name: "ix_atividade_id_created_at",
                table: "atividade",
                columns: new[] { "id", "created_at" });

            migrationBuilder.CreateIndex(
                name: "ix_agendamento_id_created_at",
                table: "agendamento",
                columns: new[] { "id", "created_at" });

            migrationBuilder.CreateIndex(
                name: "ix_agenda_id_created_at",
                table: "agenda",
                columns: new[] { "id", "created_at" });

            migrationBuilder.AddForeignKey(
                name: "fk_voluntario_ministerio_voluntario_voluntario_id",
                table: "voluntario_ministerio",
                column: "voluntario_id",
                principalTable: "voluntario",
                principalColumn: "id");
        }
    }
}
