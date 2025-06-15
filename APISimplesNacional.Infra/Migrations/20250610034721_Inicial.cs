using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace APISimplesNacional.Infra.Migrations
{
    /// <inheritdoc />
    public partial class Inicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Empresas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nome = table.Column<string>(type: "text", nullable: false),
                    Celular = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    IrDependente = table.Column<decimal>(type: "numeric", nullable: false),
                    IrVlrIsento = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Empresas", x => x.Id);
                });

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

            migrationBuilder.CreateTable(
                name: "AnexoIII",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IdEmpresa = table.Column<int>(type: "integer", nullable: false),
                    Faixa = table.Column<int>(type: "integer", nullable: false),
                    LimiteInic = table.Column<decimal>(type: "numeric", nullable: false),
                    LimiteFin = table.Column<decimal>(type: "numeric", nullable: false),
                    Aliquota = table.Column<decimal>(type: "numeric", nullable: false),
                    VlrDeduzir = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnexoIII", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AnexoIII_Empresas_IdEmpresa",
                        column: x => x.IdEmpresa,
                        principalTable: "Empresas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AnexoV",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IdEmpresa = table.Column<int>(type: "integer", nullable: false),
                    Faixa = table.Column<int>(type: "integer", nullable: false),
                    LimiteInic = table.Column<decimal>(type: "numeric", nullable: false),
                    LimiteFin = table.Column<decimal>(type: "numeric", nullable: false),
                    Aliquota = table.Column<decimal>(type: "numeric", nullable: false),
                    VlrDeduzir = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnexoV", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AnexoV_Empresas_IdEmpresa",
                        column: x => x.IdEmpresa,
                        principalTable: "Empresas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TabelaINSS",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IdEmpresa = table.Column<int>(type: "integer", nullable: false),
                    Faixa = table.Column<int>(type: "integer", nullable: false),
                    LimiteInic = table.Column<decimal>(type: "numeric", nullable: false),
                    LimiteFin = table.Column<decimal>(type: "numeric", nullable: false),
                    Aliquota = table.Column<decimal>(type: "numeric", nullable: false),
                    Deducao = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TabelaINSS", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TabelaINSS_Empresas_IdEmpresa",
                        column: x => x.IdEmpresa,
                        principalTable: "Empresas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TabelaIR",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IdEmpresa = table.Column<int>(type: "integer", nullable: false),
                    Faixa = table.Column<int>(type: "integer", nullable: false),
                    LimiteInic = table.Column<decimal>(type: "numeric", nullable: false),
                    LimiteFin = table.Column<decimal>(type: "numeric", nullable: false),
                    Aliquota = table.Column<decimal>(type: "numeric", nullable: false),
                    VlrDeduzir = table.Column<decimal>(type: "numeric", nullable: false),
                    IrVlrIsento = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TabelaIR", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TabelaIR_Empresas_IdEmpresa",
                        column: x => x.IdEmpresa,
                        principalTable: "Empresas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Empresas",
                columns: new[] { "Id", "Celular", "Email", "IrDependente", "IrVlrIsento", "Nome" },
                values: new object[] { 1, "(62)99213-7872", "julianops79@gmail.com", 189.29m, 3036.00m, "JPS Technology in Development" });

            migrationBuilder.InsertData(
                table: "AnexoIII",
                columns: new[] { "Id", "Aliquota", "Faixa", "IdEmpresa", "LimiteFin", "LimiteInic", "VlrDeduzir" },
                values: new object[,]
                {
                    { 1, 6.0m, 1, 1, 180000m, 0m, 0m },
                    { 2, 11.2m, 2, 1, 360000m, 180000.01m, 9360m },
                    { 3, 13.5m, 3, 1, 720000m, 360000.01m, 17640m },
                    { 4, 16.0m, 4, 1, 1800000m, 720000.01m, 35640m },
                    { 5, 20.5m, 5, 1, 3600000m, 1800000.01m, 125640m },
                    { 6, 33.0m, 6, 1, 4800000m, 3600000.01m, 648000m }
                });

            migrationBuilder.InsertData(
                table: "AnexoV",
                columns: new[] { "Id", "Aliquota", "Faixa", "IdEmpresa", "LimiteFin", "LimiteInic", "VlrDeduzir" },
                values: new object[,]
                {
                    { 1, 15.5m, 1, 1, 180000m, 0m, 0m },
                    { 2, 18.0m, 2, 1, 360000m, 180000.01m, 4500m },
                    { 3, 19.5m, 3, 1, 720000m, 360000.01m, 9900m },
                    { 4, 20.5m, 4, 1, 1800000m, 720000.01m, 17100m },
                    { 5, 23.0m, 5, 1, 3600000m, 1800000.01m, 62100m },
                    { 6, 30.5m, 6, 1, 4800000m, 3600000.01m, 540m }
                });

            migrationBuilder.InsertData(
                table: "TabelaINSS",
                columns: new[] { "Id", "Aliquota", "Deducao", "Faixa", "IdEmpresa", "LimiteFin", "LimiteInic" },
                values: new object[,]
                {
                    { 1, 7.5m, 0.00m, 1, 1, 1518m, 0m },
                    { 2, 9m, 22.77m, 2, 1, 2793.88m, 1518.01m },
                    { 3, 12m, 106.59m, 3, 1, 4190.83m, 2793.89m },
                    { 4, 14m, 190.40m, 4, 1, 8157.41m, 4190.84m }
                });

            migrationBuilder.InsertData(
                table: "TabelaIR",
                columns: new[] { "Id", "Aliquota", "Faixa", "IdEmpresa", "IrVlrIsento", "LimiteFin", "LimiteInic", "VlrDeduzir" },
                values: new object[,]
                {
                    { 1, 0m, 1, 1, 0m, 2428.80m, 0m, 0m },
                    { 2, 7.5m, 2, 1, 0m, 2826.65m, 2428.01m, 182.16m },
                    { 3, 15.0m, 3, 1, 0m, 3751.05m, 2826.65m, 394.16m },
                    { 4, 22.5m, 4, 1, 0m, 4664.68m, 3751.06m, 675.49m },
                    { 5, 27.5m, 5, 1, 0m, 99999999m, 4664.69m, 908.73m }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AnexoIII_IdEmpresa",
                table: "AnexoIII",
                column: "IdEmpresa");

            migrationBuilder.CreateIndex(
                name: "IX_AnexoV_IdEmpresa",
                table: "AnexoV",
                column: "IdEmpresa");

            migrationBuilder.CreateIndex(
                name: "IX_TabelaINSS_IdEmpresa",
                table: "TabelaINSS",
                column: "IdEmpresa");

            migrationBuilder.CreateIndex(
                name: "IX_TabelaIR_IdEmpresa",
                table: "TabelaIR",
                column: "IdEmpresa");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnexoIII");

            migrationBuilder.DropTable(
                name: "AnexoV");

            migrationBuilder.DropTable(
                name: "ErrosLog");

            migrationBuilder.DropTable(
                name: "TabelaINSS");

            migrationBuilder.DropTable(
                name: "TabelaIR");

            migrationBuilder.DropTable(
                name: "Empresas");
        }
    }
}
