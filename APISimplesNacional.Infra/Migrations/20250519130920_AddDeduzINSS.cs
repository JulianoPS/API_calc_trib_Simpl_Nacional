using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APISimplesNacional.Infra.Migrations
{
    /// <inheritdoc />
    public partial class AddDeduzINSS : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Deducao",
                table: "TabelaINSS",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.UpdateData(
                table: "TabelaINSS",
                keyColumn: "Id",
                keyValue: 1,
                column: "Deducao",
                value: 0.00m);

            migrationBuilder.UpdateData(
                table: "TabelaINSS",
                keyColumn: "Id",
                keyValue: 2,
                column: "Deducao",
                value: 22.77m);

            migrationBuilder.UpdateData(
                table: "TabelaINSS",
                keyColumn: "Id",
                keyValue: 3,
                column: "Deducao",
                value: 106.59m);

            migrationBuilder.UpdateData(
                table: "TabelaINSS",
                keyColumn: "Id",
                keyValue: 4,
                column: "Deducao",
                value: 190.40m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Deducao",
                table: "TabelaINSS");
        }
    }
}
