using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APISimplesNacional.Infra.Migrations
{
    /// <inheritdoc />
    public partial class Inicial2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "TabelaIR",
                keyColumn: "Id",
                keyValue: 2,
                column: "LimiteFin",
                value: 3533.31m);

            migrationBuilder.UpdateData(
                table: "TabelaIR",
                keyColumn: "Id",
                keyValue: 3,
                column: "LimiteFin",
                value: 4688.85m);

            migrationBuilder.UpdateData(
                table: "TabelaIR",
                keyColumn: "Id",
                keyValue: 4,
                column: "LimiteFin",
                value: 5830.85m);

            migrationBuilder.UpdateData(
                table: "TabelaIR",
                keyColumn: "Id",
                keyValue: 5,
                column: "LimiteFin",
                value: 99999999m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "TabelaIR",
                keyColumn: "Id",
                keyValue: 2,
                column: "LimiteFin",
                value: 3036m);

            migrationBuilder.UpdateData(
                table: "TabelaIR",
                keyColumn: "Id",
                keyValue: 3,
                column: "LimiteFin",
                value: 3036m);

            migrationBuilder.UpdateData(
                table: "TabelaIR",
                keyColumn: "Id",
                keyValue: 4,
                column: "LimiteFin",
                value: 3036m);

            migrationBuilder.UpdateData(
                table: "TabelaIR",
                keyColumn: "Id",
                keyValue: 5,
                column: "LimiteFin",
                value: 3036m);
        }
    }
}
