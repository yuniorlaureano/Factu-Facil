using Microsoft.EntityFrameworkCore.Migrations;

namespace FactuFacil.Web.Migrations
{
    public partial class InventoryRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Inventorie_ProductId",
                table: "Inventorie");

            migrationBuilder.CreateIndex(
                name: "IX_Inventorie_ProductId",
                table: "Inventorie",
                column: "ProductId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Inventorie_ProductId",
                table: "Inventorie");

            migrationBuilder.CreateIndex(
                name: "IX_Inventorie_ProductId",
                table: "Inventorie",
                column: "ProductId");
        }
    }
}
