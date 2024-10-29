using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sistema_ArgenMotos.Migrations
{
    /// <inheritdoc />
    public partial class ChangeRazonSocialAndAddLegajo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Legajo",
                table: "Vendedores",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "RazonSocial",
                table: "Proveedores",
                type: "text",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Legajo",
                table: "Vendedores");

            migrationBuilder.AlterColumn<int>(
                name: "RazonSocial",
                table: "Proveedores",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");
        }
    }
}
