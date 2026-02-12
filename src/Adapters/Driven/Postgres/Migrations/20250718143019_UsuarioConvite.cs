using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Postgres.Migrations
{
    /// <inheritdoc />
    public partial class UsuarioConvite : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_agendamento_agenda_agenda_id",
                table: "agendamento");

            migrationBuilder.DropTable(
                name: "usuario_aprovacao_ministerio");

            migrationBuilder.DropTable(
                name: "usuario_aprovacao");

            migrationBuilder.DropIndex(
                name: "ix_agendamento_agenda_id",
                table: "agendamento");

            migrationBuilder.AlterColumn<Guid>(
                name: "voluntario_id",
                table: "voluntario_ministerio",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<string>(
                name: "celular_numero",
                table: "usuario",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "celular_ddd",
                table: "usuario",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "descricao",
                table: "atividade",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<Guid>(
                name: "agenda_id",
                table: "agendamento",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "agenda_id1",
                table: "agendamento",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "usuario_convite",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    nome = table.Column<string>(type: "text", nullable: false),
                    email = table.Column<string>(type: "text", nullable: false),
                    enviado_por_id = table.Column<Guid>(type: "uuid", nullable: false),
                    enviado_para_id = table.Column<Guid>(type: "uuid", nullable: true),
                    status = table.Column<string>(type: "text", nullable: false),
                    data_expiracao = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    token = table.Column<string>(type: "text", nullable: false),
                    created_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    inactive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_usuario_convite", x => x.id);
                    table.ForeignKey(
                        name: "fk_usuario_convite_usuario_enviado_para_id",
                        column: x => x.enviado_para_id,
                        principalTable: "usuario",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_usuario_convite_usuario_enviado_por_id",
                        column: x => x.enviado_por_id,
                        principalTable: "usuario",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_voluntario_ministerio_voluntario_id",
                table: "voluntario_ministerio",
                column: "voluntario_id");

            migrationBuilder.CreateIndex(
                name: "ix_agendamento_agenda_id",
                table: "agendamento",
                column: "agenda_id1");

            migrationBuilder.CreateIndex(
                name: "ix_usuario_convite_enviado_para_id",
                table: "usuario_convite",
                column: "enviado_para_id");

            migrationBuilder.CreateIndex(
                name: "ix_usuario_convite_enviado_por_id",
                table: "usuario_convite",
                column: "enviado_por_id");

            migrationBuilder.CreateIndex(
                name: "ix_usuario_convite_id_created_at",
                table: "usuario_convite",
                columns: new[] { "id", "created_at" });

            migrationBuilder.AddForeignKey(
                name: "fk_agendamento_agenda_agenda_id",
                table: "agendamento",
                column: "agenda_id1",
                principalTable: "agenda",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_voluntario_ministerio_voluntario_voluntario_id",
                table: "voluntario_ministerio",
                column: "voluntario_id",
                principalTable: "voluntario",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_agendamento_agenda_agenda_id",
                table: "agendamento");

            migrationBuilder.DropForeignKey(
                name: "fk_voluntario_ministerio_voluntario_voluntario_id",
                table: "voluntario_ministerio");

            migrationBuilder.DropTable(
                name: "usuario_convite");

            migrationBuilder.DropIndex(
                name: "ix_voluntario_ministerio_voluntario_id",
                table: "voluntario_ministerio");

            migrationBuilder.DropIndex(
                name: "ix_agendamento_agenda_id",
                table: "agendamento");

            migrationBuilder.DropColumn(
                name: "agenda_id1",
                table: "agendamento");

            migrationBuilder.AlterColumn<Guid>(
                name: "voluntario_id",
                table: "voluntario_ministerio",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "celular_numero",
                table: "usuario",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "celular_ddd",
                table: "usuario",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "descricao",
                table: "atividade",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "agenda_id",
                table: "agendamento",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.CreateTable(
                name: "usuario_aprovacao",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    usuario_aprovador_id = table.Column<Guid>(type: "uuid", nullable: true),
                    data_aprovacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    inactive = table.Column<bool>(type: "boolean", nullable: false),
                    status = table.Column<string>(type: "text", nullable: false),
                    usuario_solicitante_id = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_usuario_aprovacao", x => x.id);
                    table.ForeignKey(
                        name: "fk_usuario_aprovacao_usuario_usuario_aprovador_id",
                        column: x => x.usuario_aprovador_id,
                        principalTable: "usuario",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_usuario_aprovacao_usuario_usuario_solicitante_id",
                        column: x => x.usuario_solicitante_id,
                        principalTable: "usuario",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "usuario_aprovacao_ministerio",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    aprovado = table.Column<bool>(type: "boolean", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    inactive = table.Column<bool>(type: "boolean", nullable: false),
                    ministerio_id = table.Column<Guid>(type: "uuid", nullable: false),
                    usuario_aprovacao_id = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_usuario_aprovacao_ministerio", x => x.id);
                    table.ForeignKey(
                        name: "fk_usuario_aprovacao_ministerio_ministerio_ministerio_id",
                        column: x => x.ministerio_id,
                        principalTable: "ministerio",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_usuario_aprovacao_ministerio_usuario_aprovacao_usuario_apro",
                        column: x => x.usuario_aprovacao_id,
                        principalTable: "usuario_aprovacao",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "ix_agendamento_agenda_id",
                table: "agendamento",
                column: "agenda_id");

            migrationBuilder.CreateIndex(
                name: "ix_usuario_aprovacao_id_created_at",
                table: "usuario_aprovacao",
                columns: new[] { "id", "created_at" });

            migrationBuilder.CreateIndex(
                name: "ix_usuario_aprovacao_usuario_aprovador_id",
                table: "usuario_aprovacao",
                column: "usuario_aprovador_id");

            migrationBuilder.CreateIndex(
                name: "ix_usuario_aprovacao_usuario_solicitante_id",
                table: "usuario_aprovacao",
                column: "usuario_solicitante_id");

            migrationBuilder.CreateIndex(
                name: "ix_usuario_aprovacao_ministerio_id_created_at",
                table: "usuario_aprovacao_ministerio",
                columns: new[] { "id", "created_at" });

            migrationBuilder.CreateIndex(
                name: "ix_usuario_aprovacao_ministerio_ministerio_id",
                table: "usuario_aprovacao_ministerio",
                column: "ministerio_id");

            migrationBuilder.CreateIndex(
                name: "ix_usuario_aprovacao_ministerio_usuario_aprovacao_id",
                table: "usuario_aprovacao_ministerio",
                column: "usuario_aprovacao_id");

            migrationBuilder.AddForeignKey(
                name: "fk_agendamento_agenda_agenda_id",
                table: "agendamento",
                column: "agenda_id",
                principalTable: "agenda",
                principalColumn: "id");
        }
    }
}
