using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sistema_ArgenMotos.Migrations
{
    /// <inheritdoc />
    public partial class CheckUniqueEmailVendedor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Vendedores_Email",
                table: "Vendedores",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Vendedores_Email",
                table: "Vendedores");
        }
    }
}
