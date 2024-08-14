using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InventoryManagement.Migrations
{
    /// <inheritdoc />
    public partial class inventoryTablev2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Transactions_ProductId",
                table: "Transactions",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Products_ProductId",
                table: "Transactions",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Products_ProductId",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_ProductId",
                table: "Transactions");
        }
    }
}
