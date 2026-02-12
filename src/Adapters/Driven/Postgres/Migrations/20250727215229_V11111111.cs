using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Postgres.Migrations
{
    /// <inheritdoc />
    public partial class V11111111 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "id",
                table: "voluntario_ministerio",
                type: "uuid",
                nullable: false,
                defaultValueSql: "uuid_generate_v1()",
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldDefaultValueSql: "gen_random_uuid_v7()");

            migrationBuilder.AlterColumn<Guid>(
                name: "id",
                table: "voluntario",
                type: "uuid",
                nullable: false,
                defaultValueSql: "uuid_generate_v1()",
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldDefaultValueSql: "gen_random_uuid_v7()");

            migrationBuilder.AlterColumn<Guid>(
                name: "id",
                table: "usuario_convite",
                type: "uuid",
                nullable: false,
                defaultValueSql: "uuid_generate_v1()",
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldDefaultValueSql: "gen_random_uuid_v7()");

            migrationBuilder.AlterColumn<Guid>(
                name: "id",
                table: "usuario",
                type: "uuid",
                nullable: false,
                defaultValueSql: "uuid_generate_v1()",
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldDefaultValueSql: "gen_random_uuid_v7()");

            migrationBuilder.AlterColumn<Guid>(
                name: "id",
                table: "ministerio",
                type: "uuid",
                nullable: false,
                defaultValueSql: "uuid_generate_v1()",
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldDefaultValueSql: "gen_random_uuid_v7()");

            migrationBuilder.AlterColumn<Guid>(
                name: "id",
                table: "historico",
                type: "uuid",
                nullable: false,
                defaultValueSql: "uuid_generate_v1()",
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldDefaultValueSql: "gen_random_uuid_v7()");

            migrationBuilder.AlterColumn<Guid>(
                name: "id",
                table: "atividade",
                type: "uuid",
                nullable: false,
                defaultValueSql: "uuid_generate_v1()",
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldDefaultValueSql: "gen_random_uuid_v7()");

            migrationBuilder.AlterColumn<Guid>(
                name: "id",
                table: "agendamento",
                type: "uuid",
                nullable: false,
                defaultValueSql: "uuid_generate_v1()",
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldDefaultValueSql: "gen_random_uuid_v7()");

            migrationBuilder.AlterColumn<Guid>(
                name: "id",
                table: "agenda",
                type: "uuid",
                nullable: false,
                defaultValueSql: "uuid_generate_v1()",
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldDefaultValueSql: "gen_random_uuid_v7()");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "id",
                table: "voluntario_ministerio",
                type: "uuid",
                nullable: false,
                defaultValueSql: "gen_random_uuid_v7()",
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldDefaultValueSql: "uuid_generate_v1()");

            migrationBuilder.AlterColumn<Guid>(
                name: "id",
                table: "voluntario",
                type: "uuid",
                nullable: false,
                defaultValueSql: "gen_random_uuid_v7()",
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldDefaultValueSql: "uuid_generate_v1()");

            migrationBuilder.AlterColumn<Guid>(
                name: "id",
                table: "usuario_convite",
                type: "uuid",
                nullable: false,
                defaultValueSql: "gen_random_uuid_v7()",
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldDefaultValueSql: "uuid_generate_v1()");

            migrationBuilder.AlterColumn<Guid>(
                name: "id",
                table: "usuario",
                type: "uuid",
                nullable: false,
                defaultValueSql: "gen_random_uuid_v7()",
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldDefaultValueSql: "uuid_generate_v1()");

            migrationBuilder.AlterColumn<Guid>(
                name: "id",
                table: "ministerio",
                type: "uuid",
                nullable: false,
                defaultValueSql: "gen_random_uuid_v7()",
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldDefaultValueSql: "uuid_generate_v1()");

            migrationBuilder.AlterColumn<Guid>(
                name: "id",
                table: "historico",
                type: "uuid",
                nullable: false,
                defaultValueSql: "gen_random_uuid_v7()",
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldDefaultValueSql: "uuid_generate_v1()");

            migrationBuilder.AlterColumn<Guid>(
                name: "id",
                table: "atividade",
                type: "uuid",
                nullable: false,
                defaultValueSql: "gen_random_uuid_v7()",
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldDefaultValueSql: "uuid_generate_v1()");

            migrationBuilder.AlterColumn<Guid>(
                name: "id",
                table: "agendamento",
                type: "uuid",
                nullable: false,
                defaultValueSql: "gen_random_uuid_v7()",
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldDefaultValueSql: "uuid_generate_v1()");

            migrationBuilder.AlterColumn<Guid>(
                name: "id",
                table: "agenda",
                type: "uuid",
                nullable: false,
                defaultValueSql: "gen_random_uuid_v7()",
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldDefaultValueSql: "uuid_generate_v1()");
        }
    }
}
