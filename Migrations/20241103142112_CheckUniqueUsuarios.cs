using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sistema_ArgenMotos.Migrations
{
    /// <inheritdoc />
    public partial class CheckUniqueUsuarios : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Usuarios_VendedorId",
                table: "Usuarios");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_VendedorId",
                table: "Usuarios",
                column: "VendedorId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Usuarios_VendedorId",
                table: "Usuarios");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_VendedorId",
                table: "Usuarios",
                column: "VendedorId");
        }
    }
}
