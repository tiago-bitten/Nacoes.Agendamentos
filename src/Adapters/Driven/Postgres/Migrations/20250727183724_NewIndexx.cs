using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Postgres.Migrations
{
    /// <inheritdoc />
    public partial class NewIndexx : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "criado_em",
                table: "voluntario_ministerio",
                newName: "data_criacao");

            migrationBuilder.RenameColumn(
                name: "criado_em",
                table: "voluntario",
                newName: "data_criacao");

            migrationBuilder.RenameColumn(
                name: "criado_em",
                table: "usuario_convite",
                newName: "data_criacao");

            migrationBuilder.RenameColumn(
                name: "criado_em",
                table: "usuario",
                newName: "data_criacao");

            migrationBuilder.RenameColumn(
                name: "criado_em",
                table: "ministerio",
                newName: "data_criacao");

            migrationBuilder.RenameColumn(
                name: "criado_em",
                table: "historico",
                newName: "data_criacao");

            migrationBuilder.RenameColumn(
                name: "criado_em",
                table: "atividade",
                newName: "data_criacao");

            migrationBuilder.RenameColumn(
                name: "criado_em",
                table: "agendamento",
                newName: "data_criacao");

            migrationBuilder.RenameColumn(
                name: "criado_em",
                table: "agenda",
                newName: "data_criacao");

            migrationBuilder.CreateIndex(
                name: "ix_voluntarioministerio_data_criacao_id",
                table: "voluntario_ministerio",
                columns: new[] { "data_criacao", "id" },
                descending: new bool[0]);

            migrationBuilder.CreateIndex(
                name: "ix_voluntario_data_criacao_id",
                table: "voluntario",
                columns: new[] { "data_criacao", "id" },
                descending: new bool[0]);

            migrationBuilder.CreateIndex(
                name: "ix_usuarioconvite_data_criacao_id",
                table: "usuario_convite",
                columns: new[] { "data_criacao", "id" },
                descending: new bool[0]);

            migrationBuilder.CreateIndex(
                name: "ix_usuario_data_criacao_id",
                table: "usuario",
                columns: new[] { "data_criacao", "id" },
                descending: new bool[0]);

            migrationBuilder.CreateIndex(
                name: "ix_ministerio_data_criacao_id",
                table: "ministerio",
                columns: new[] { "data_criacao", "id" },
                descending: new bool[0]);

            migrationBuilder.CreateIndex(
                name: "ix_historico_data_criacao_id",
                table: "historico",
                columns: new[] { "data_criacao", "id" },
                descending: new bool[0]);

            migrationBuilder.CreateIndex(
                name: "ix_atividade_data_criacao_id",
                table: "atividade",
                columns: new[] { "data_criacao", "id" },
                descending: new bool[0]);

            migrationBuilder.CreateIndex(
                name: "ix_agendamento_data_criacao_id",
                table: "agendamento",
                columns: new[] { "data_criacao", "id" },
                descending: new bool[0]);

            migrationBuilder.CreateIndex(
                name: "ix_agenda_data_criacao_id",
                table: "agenda",
                columns: new[] { "data_criacao", "id" },
                descending: new bool[0]);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_voluntarioministerio_data_criacao_id",
                table: "voluntario_ministerio");

            migrationBuilder.DropIndex(
                name: "ix_voluntario_data_criacao_id",
                table: "voluntario");

            migrationBuilder.DropIndex(
                name: "ix_usuarioconvite_data_criacao_id",
                table: "usuario_convite");

            migrationBuilder.DropIndex(
                name: "ix_usuario_data_criacao_id",
                table: "usuario");

            migrationBuilder.DropIndex(
                name: "ix_ministerio_data_criacao_id",
                table: "ministerio");

            migrationBuilder.DropIndex(
                name: "ix_historico_data_criacao_id",
                table: "historico");

            migrationBuilder.DropIndex(
                name: "ix_atividade_data_criacao_id",
                table: "atividade");

            migrationBuilder.DropIndex(
                name: "ix_agendamento_data_criacao_id",
                table: "agendamento");

            migrationBuilder.DropIndex(
                name: "ix_agenda_data_criacao_id",
                table: "agenda");

            migrationBuilder.RenameColumn(
                name: "data_criacao",
                table: "voluntario_ministerio",
                newName: "criado_em");

            migrationBuilder.RenameColumn(
                name: "data_criacao",
                table: "voluntario",
                newName: "criado_em");

            migrationBuilder.RenameColumn(
                name: "data_criacao",
                table: "usuario_convite",
                newName: "criado_em");

            migrationBuilder.RenameColumn(
                name: "data_criacao",
                table: "usuario",
                newName: "criado_em");

            migrationBuilder.RenameColumn(
                name: "data_criacao",
                table: "ministerio",
                newName: "criado_em");

            migrationBuilder.RenameColumn(
                name: "data_criacao",
                table: "historico",
                newName: "criado_em");

            migrationBuilder.RenameColumn(
                name: "data_criacao",
                table: "atividade",
                newName: "criado_em");

            migrationBuilder.RenameColumn(
                name: "data_criacao",
                table: "agendamento",
                newName: "criado_em");

            migrationBuilder.RenameColumn(
                name: "data_criacao",
                table: "agenda",
                newName: "criado_em");
        }
    }
}
