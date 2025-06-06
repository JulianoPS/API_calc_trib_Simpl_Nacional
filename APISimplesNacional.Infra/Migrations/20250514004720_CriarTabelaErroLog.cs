using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace APISimplesNacional.Infra.Migrations
{
    /// <inheritdoc />
    public partial class CriarTabelaErroLog : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "email",
                table: "Empresas",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "celular",
                table: "Empresas",
                newName: "Celular");

            migrationBuilder.CreateTable(
                name: "ErrosLog",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DataOcorrencia = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Mensagem = table.Column<string>(type: "text", nullable: false),
                    StackTrace = table.Column<string>(type: "text", nullable: false),
                    Origem = table.Column<string>(type: "text", nullable: false),
                    StatusCode = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ErrosLog", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ErrosLog");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "Empresas",
                newName: "email");

            migrationBuilder.RenameColumn(
                name: "Celular",
                table: "Empresas",
                newName: "celular");
        }
    }
}
