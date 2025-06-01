using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Nacoes.Agendamentos.Infra.Migrations
{
    /// <inheritdoc />
    public partial class First : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "agenda",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    descricao = table.Column<string>(type: "text", nullable: false),
                    horario_data_inicial = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    horario_data_final = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    inactive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_agenda", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "ministerio",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    nome = table.Column<string>(type: "text", nullable: false),
                    descricao = table.Column<string>(type: "text", nullable: true),
                    cor = table.Column<string>(type: "text", nullable: false),
                    cor_tipo = table.Column<string>(type: "text", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    inactive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_ministerio", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "usuario",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    nome = table.Column<string>(type: "text", nullable: false),
                    email = table.Column<string>(type: "text", nullable: false),
                    email_confirmado = table.Column<bool>(type: "boolean", nullable: false),
                    email_codigo_confirmacao = table.Column<string>(type: "text", nullable: true),
                    email_data_expiracao_codigo_confirmacao = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    senha = table.Column<string>(type: "text", nullable: true),
                    celular_ddd = table.Column<string>(type: "text", nullable: false),
                    celular_numero = table.Column<string>(type: "text", nullable: false),
                    auth_type = table.Column<string>(type: "text", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    inactive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_usuario", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "voluntario",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    nome = table.Column<string>(type: "text", nullable: false),
                    email = table.Column<string>(type: "text", nullable: true),
                    email_confirmado = table.Column<bool>(type: "boolean", nullable: true),
                    email_codigo_confirmacao = table.Column<string>(type: "text", nullable: true),
                    email_data_expiracao_codigo_confirmacao = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    celular_ddd = table.Column<string>(type: "text", nullable: true),
                    celular_numero = table.Column<string>(type: "text", nullable: true),
                    cpf = table.Column<string>(type: "text", nullable: true),
                    data_nascimento = table.Column<DateOnly>(type: "date", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    inactive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_voluntario", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "voluntario_ministerio",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    voluntario_id = table.Column<Guid>(type: "uuid", nullable: false),
                    ministerio_id = table.Column<Guid>(type: "uuid", nullable: false),
                    status = table.Column<string>(type: "text", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    inactive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_voluntario_ministerio", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "atividade",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    nome = table.Column<string>(type: "text", nullable: false),
                    descricao = table.Column<string>(type: "text", nullable: false),
                    ministerio_id = table.Column<Guid>(type: "uuid", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    inactive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_atividade", x => x.id);
                    table.ForeignKey(
                        name: "fk_atividade_ministerio_ministerio_id",
                        column: x => x.ministerio_id,
                        principalTable: "ministerio",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "usuario_aprovacao",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    usuario_aprovador_id = table.Column<Guid>(type: "uuid", nullable: true),
                    status = table.Column<string>(type: "text", nullable: false),
                    data_aprovacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    usuario_solicitante_id = table.Column<Guid>(type: "uuid", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    inactive = table.Column<bool>(type: "boolean", nullable: false)
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
                name: "agendamento",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    voluntario_ministerio_id = table.Column<Guid>(type: "uuid", nullable: false),
                    atividade_id = table.Column<Guid>(type: "uuid", nullable: false),
                    status = table.Column<string>(type: "text", nullable: false),
                    origem = table.Column<string>(type: "text", nullable: false),
                    agenda_id = table.Column<Guid>(type: "uuid", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    inactive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_agendamento", x => x.id);
                    table.ForeignKey(
                        name: "fk_agendamento_agenda_agenda_id",
                        column: x => x.agenda_id,
                        principalTable: "agenda",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_agendamento_atividade_atividade_id",
                        column: x => x.atividade_id,
                        principalTable: "atividade",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_agendamento_voluntario_ministerio_voluntario_ministerio_id",
                        column: x => x.voluntario_ministerio_id,
                        principalTable: "voluntario_ministerio",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "usuario_aprovacao_ministerio",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    ministerio_id = table.Column<Guid>(type: "uuid", nullable: false),
                    aprovado = table.Column<bool>(type: "boolean", nullable: false),
                    usuario_aprovacao_id = table.Column<Guid>(type: "uuid", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    inactive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_usuario_aprovacao_ministerio", x => x.id);
                    table.ForeignKey(
                        name: "fk_usuario_aprovacao_ministerio_usuario_aprovacao_usuario_apro",
                        column: x => x.usuario_aprovacao_id,
                        principalTable: "usuario_aprovacao",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "ix_agenda_id_created_at",
                table: "agenda",
                columns: new[] { "id", "created_at" });

            migrationBuilder.CreateIndex(
                name: "ix_agendamento_agenda_id",
                table: "agendamento",
                column: "agenda_id");

            migrationBuilder.CreateIndex(
                name: "ix_agendamento_atividade_id",
                table: "agendamento",
                column: "atividade_id");

            migrationBuilder.CreateIndex(
                name: "ix_agendamento_id_created_at",
                table: "agendamento",
                columns: new[] { "id", "created_at" });

            migrationBuilder.CreateIndex(
                name: "ix_agendamento_voluntario_ministerio_id",
                table: "agendamento",
                column: "voluntario_ministerio_id");

            migrationBuilder.CreateIndex(
                name: "ix_atividade_id_created_at",
                table: "atividade",
                columns: new[] { "id", "created_at" });

            migrationBuilder.CreateIndex(
                name: "ix_atividade_ministerio_id",
                table: "atividade",
                column: "ministerio_id");

            migrationBuilder.CreateIndex(
                name: "ix_ministerio_id_created_at",
                table: "ministerio",
                columns: new[] { "id", "created_at" });

            migrationBuilder.CreateIndex(
                name: "ix_usuario_id_created_at",
                table: "usuario",
                columns: new[] { "id", "created_at" });

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
                name: "ix_usuario_aprovacao_ministerio_usuario_aprovacao_id",
                table: "usuario_aprovacao_ministerio",
                column: "usuario_aprovacao_id");

            migrationBuilder.CreateIndex(
                name: "ix_voluntario_id_created_at",
                table: "voluntario",
                columns: new[] { "id", "created_at" });

            migrationBuilder.CreateIndex(
                name: "ix_voluntario_ministerio_id_created_at",
                table: "voluntario_ministerio",
                columns: new[] { "id", "created_at" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "agendamento");

            migrationBuilder.DropTable(
                name: "usuario_aprovacao_ministerio");

            migrationBuilder.DropTable(
                name: "voluntario");

            migrationBuilder.DropTable(
                name: "agenda");

            migrationBuilder.DropTable(
                name: "atividade");

            migrationBuilder.DropTable(
                name: "voluntario_ministerio");

            migrationBuilder.DropTable(
                name: "usuario_aprovacao");

            migrationBuilder.DropTable(
                name: "ministerio");

            migrationBuilder.DropTable(
                name: "usuario");
        }
    }
}
