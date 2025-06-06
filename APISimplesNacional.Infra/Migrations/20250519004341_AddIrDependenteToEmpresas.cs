using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APISimplesNacional.Infra.Migrations
{
    /// <inheritdoc />
    public partial class AddIrDependenteToEmpresas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "IrDependente",
                table: "Empresas",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.UpdateData(
                table: "Empresas",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Celular", "IrDependente" },
                values: new object[] { "(62)99213-7872", 0m });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IrDependente",
                table: "Empresas");

            migrationBuilder.UpdateData(
                table: "Empresas",
                keyColumn: "Id",
                keyValue: 1,
                column: "Celular",
                value: "(62) 99213-7872");
        }
    }
}
