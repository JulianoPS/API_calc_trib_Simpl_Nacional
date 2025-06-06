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
                    celular = table.Column<string>(type: "text", nullable: false),
                    email = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Empresas", x => x.Id);
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
                    Aliquota = table.Column<decimal>(type: "numeric", nullable: false)
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
                    VlrDeduzir = table.Column<decimal>(type: "numeric", nullable: false)
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
                columns: new[] { "Id", "Name", "celular", "email" },
                values: new object[] { 1, "JPS Technology in Development", "(62)99213-7872", "julianops79@gmail.com" });

            migrationBuilder.InsertData(
                table: "AnexoIII",
                columns: new[] { "Id", "Aliquota", "Faixa", "IdEmpresa", "LimiteFin", "LimiteInic", "VlrDeduzir" },
                values: new object[,]
                {
                    { 1, 0.06m, 1, 1, 180000m, 0m, 0m },
                    { 2, 0.112m, 2, 1, 360000m, 180000.01m, 9360m },
                    { 3, 0.135m, 3, 1, 720000m, 360000.01m, 17640m },
                    { 4, 0.16m, 4, 1, 1800000m, 720000.01m, 35640m },
                    { 5, 0.205m, 5, 1, 3600000m, 1800000.01m, 125640m },
                    { 6, 0.33m, 6, 1, 4800000m, 3600000.01m, 648000m }
                });

            migrationBuilder.InsertData(
                table: "AnexoV",
                columns: new[] { "Id", "Aliquota", "Faixa", "IdEmpresa", "LimiteFin", "LimiteInic", "VlrDeduzir" },
                values: new object[,]
                {
                    { 1, 0.155m, 1, 1, 180000m, 0m, 0m },
                    { 2, 0.180m, 2, 1, 360000m, 180000.01m, 4500m },
                    { 3, 0.195m, 3, 1, 720000m, 360000.01m, 9900m },
                    { 4, 0.205m, 4, 1, 1800000m, 720000.01m, 17100m },
                    { 5, 0.230m, 5, 1, 3600000m, 1800000.01m, 62100m },
                    { 6, 0.305m, 6, 1, 4800000m, 3600000.01m, 540m }
                });

            migrationBuilder.InsertData(
                table: "TabelaINSS",
                columns: new[] { "Id", "Aliquota", "Faixa", "IdEmpresa", "LimiteFin", "LimiteInic" },
                values: new object[,]
                {
                    { 1, 0.075m, 1, 1, 1518m, 0m },
                    { 2, 0.09m, 2, 1, 2793.88m, 1518.01m },
                    { 3, 0.12m, 3, 1, 4190.83m, 2793.89m },
                    { 4, 0.14m, 4, 1, 8157.41m, 4190.84m }
                });

            migrationBuilder.InsertData(
                table: "TabelaIR",
                columns: new[] { "Id", "Aliquota", "Faixa", "IdEmpresa", "LimiteFin", "LimiteInic", "VlrDeduzir" },
                values: new object[,]
                {
                    { 1, 0m, 1, 1, 0, 3036m, 0m },
                    { 2, 0.075m, 2, 1, 3036m, 3533.31m, 169.44m },
                    { 3, 0.15m, 3, 1, 3533.31m, 4688.85m, 381.44m },
                    { 4, 0.225m, 4, 1, 4688.85m, 5830.85m, 662.77m },
                    { 5, 0.275m, 5, 1, 5830.85m, 99999999m, 896m }
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
                name: "TabelaINSS");

            migrationBuilder.DropTable(
                name: "TabelaIR");

            migrationBuilder.DropTable(
                name: "Empresas");
        }
    }
}
