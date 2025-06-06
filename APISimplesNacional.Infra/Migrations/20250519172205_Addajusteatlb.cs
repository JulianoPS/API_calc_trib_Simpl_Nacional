using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APISimplesNacional.Infra.Migrations
{
    /// <inheritdoc />
    public partial class Addajusteatlb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "IrVlrIsento",
                table: "TabelaIR",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "IrVlrIsento",
                table: "Empresas",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.UpdateData(
                table: "AnexoIII",
                keyColumn: "Id",
                keyValue: 1,
                column: "Aliquota",
                value: 6.0m);

            migrationBuilder.UpdateData(
                table: "AnexoIII",
                keyColumn: "Id",
                keyValue: 2,
                column: "Aliquota",
                value: 11.2m);

            migrationBuilder.UpdateData(
                table: "AnexoIII",
                keyColumn: "Id",
                keyValue: 3,
                column: "Aliquota",
                value: 13.5m);

            migrationBuilder.UpdateData(
                table: "AnexoIII",
                keyColumn: "Id",
                keyValue: 4,
                column: "Aliquota",
                value: 16.0m);

            migrationBuilder.UpdateData(
                table: "AnexoIII",
                keyColumn: "Id",
                keyValue: 5,
                column: "Aliquota",
                value: 20.5m);

            migrationBuilder.UpdateData(
                table: "AnexoIII",
                keyColumn: "Id",
                keyValue: 6,
                column: "Aliquota",
                value: 33.0m);

            migrationBuilder.UpdateData(
                table: "AnexoV",
                keyColumn: "Id",
                keyValue: 1,
                column: "Aliquota",
                value: 15m);

            migrationBuilder.UpdateData(
                table: "AnexoV",
                keyColumn: "Id",
                keyValue: 2,
                column: "Aliquota",
                value: 18.0m);

            migrationBuilder.UpdateData(
                table: "AnexoV",
                keyColumn: "Id",
                keyValue: 3,
                column: "Aliquota",
                value: 19.5m);

            migrationBuilder.UpdateData(
                table: "AnexoV",
                keyColumn: "Id",
                keyValue: 4,
                column: "Aliquota",
                value: 20.5m);

            migrationBuilder.UpdateData(
                table: "AnexoV",
                keyColumn: "Id",
                keyValue: 5,
                column: "Aliquota",
                value: 23.0m);

            migrationBuilder.UpdateData(
                table: "AnexoV",
                keyColumn: "Id",
                keyValue: 6,
                column: "Aliquota",
                value: 30.5m);

            migrationBuilder.UpdateData(
                table: "Empresas",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "IrDependente", "IrVlrIsento" },
                values: new object[] { 189.29m, 3036.00m });

            migrationBuilder.UpdateData(
                table: "TabelaINSS",
                keyColumn: "Id",
                keyValue: 1,
                column: "Aliquota",
                value: 7.5m);

            migrationBuilder.UpdateData(
                table: "TabelaINSS",
                keyColumn: "Id",
                keyValue: 2,
                column: "Aliquota",
                value: 9m);

            migrationBuilder.UpdateData(
                table: "TabelaINSS",
                keyColumn: "Id",
                keyValue: 3,
                column: "Aliquota",
                value: 12m);

            migrationBuilder.UpdateData(
                table: "TabelaINSS",
                keyColumn: "Id",
                keyValue: 4,
                column: "Aliquota",
                value: 14m);

            migrationBuilder.UpdateData(
                table: "TabelaIR",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "IrVlrIsento", "LimiteFin" },
                values: new object[] { 0m, 2428.80m });

            migrationBuilder.UpdateData(
                table: "TabelaIR",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Aliquota", "IrVlrIsento", "LimiteFin", "LimiteInic", "VlrDeduzir" },
                values: new object[] { 7.5m, 0m, 2826.65m, 2428.01m, 182.16m });

            migrationBuilder.UpdateData(
                table: "TabelaIR",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Aliquota", "IrVlrIsento", "LimiteFin", "LimiteInic", "VlrDeduzir" },
                values: new object[] { 15.0m, 0m, 3751.05m, 2826.65m, 394.16m });

            migrationBuilder.UpdateData(
                table: "TabelaIR",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Aliquota", "IrVlrIsento", "LimiteFin", "LimiteInic", "VlrDeduzir" },
                values: new object[] { 22.5m, 0m, 4664.68m, 3751.06m, 675.49m });

            migrationBuilder.UpdateData(
                table: "TabelaIR",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Aliquota", "IrVlrIsento", "LimiteInic", "VlrDeduzir" },
                values: new object[] { 27.5m, 0m, 4664.69m, 908.73m });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IrVlrIsento",
                table: "TabelaIR");

            migrationBuilder.DropColumn(
                name: "IrVlrIsento",
                table: "Empresas");

            migrationBuilder.UpdateData(
                table: "AnexoIII",
                keyColumn: "Id",
                keyValue: 1,
                column: "Aliquota",
                value: 0.06m);

            migrationBuilder.UpdateData(
                table: "AnexoIII",
                keyColumn: "Id",
                keyValue: 2,
                column: "Aliquota",
                value: 0.112m);

            migrationBuilder.UpdateData(
                table: "AnexoIII",
                keyColumn: "Id",
                keyValue: 3,
                column: "Aliquota",
                value: 0.135m);

            migrationBuilder.UpdateData(
                table: "AnexoIII",
                keyColumn: "Id",
                keyValue: 4,
                column: "Aliquota",
                value: 0.16m);

            migrationBuilder.UpdateData(
                table: "AnexoIII",
                keyColumn: "Id",
                keyValue: 5,
                column: "Aliquota",
                value: 0.205m);

            migrationBuilder.UpdateData(
                table: "AnexoIII",
                keyColumn: "Id",
                keyValue: 6,
                column: "Aliquota",
                value: 0.33m);

            migrationBuilder.UpdateData(
                table: "AnexoV",
                keyColumn: "Id",
                keyValue: 1,
                column: "Aliquota",
                value: 0.15m);

            migrationBuilder.UpdateData(
                table: "AnexoV",
                keyColumn: "Id",
                keyValue: 2,
                column: "Aliquota",
                value: 0.18m);

            migrationBuilder.UpdateData(
                table: "AnexoV",
                keyColumn: "Id",
                keyValue: 3,
                column: "Aliquota",
                value: 0.195m);

            migrationBuilder.UpdateData(
                table: "AnexoV",
                keyColumn: "Id",
                keyValue: 4,
                column: "Aliquota",
                value: 0.205m);

            migrationBuilder.UpdateData(
                table: "AnexoV",
                keyColumn: "Id",
                keyValue: 5,
                column: "Aliquota",
                value: 0.23m);

            migrationBuilder.UpdateData(
                table: "AnexoV",
                keyColumn: "Id",
                keyValue: 6,
                column: "Aliquota",
                value: 0.305m);

            migrationBuilder.UpdateData(
                table: "Empresas",
                keyColumn: "Id",
                keyValue: 1,
                column: "IrDependente",
                value: 0m);

            migrationBuilder.UpdateData(
                table: "TabelaINSS",
                keyColumn: "Id",
                keyValue: 1,
                column: "Aliquota",
                value: 0.075m);

            migrationBuilder.UpdateData(
                table: "TabelaINSS",
                keyColumn: "Id",
                keyValue: 2,
                column: "Aliquota",
                value: 0.09m);

            migrationBuilder.UpdateData(
                table: "TabelaINSS",
                keyColumn: "Id",
                keyValue: 3,
                column: "Aliquota",
                value: 0.12m);

            migrationBuilder.UpdateData(
                table: "TabelaINSS",
                keyColumn: "Id",
                keyValue: 4,
                column: "Aliquota",
                value: 0.14m);

            migrationBuilder.UpdateData(
                table: "TabelaIR",
                keyColumn: "Id",
                keyValue: 1,
                column: "LimiteFin",
                value: 3036m);

            migrationBuilder.UpdateData(
                table: "TabelaIR",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Aliquota", "LimiteFin", "LimiteInic", "VlrDeduzir" },
                values: new object[] { 0.075m, 3533.31m, 3036m, 169.44m });

            migrationBuilder.UpdateData(
                table: "TabelaIR",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Aliquota", "LimiteFin", "LimiteInic", "VlrDeduzir" },
                values: new object[] { 0.15m, 4688.85m, 3533.31m, 381.44m });

            migrationBuilder.UpdateData(
                table: "TabelaIR",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Aliquota", "LimiteFin", "LimiteInic", "VlrDeduzir" },
                values: new object[] { 0.225m, 5830.85m, 4688.85m, 662.77m });

            migrationBuilder.UpdateData(
                table: "TabelaIR",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Aliquota", "LimiteInic", "VlrDeduzir" },
                values: new object[] { 0.275m, 5830.85m, 896m });
        }
    }
}
