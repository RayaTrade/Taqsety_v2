using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Raya.Taqsety.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class removeCustomerIdFromAddressDetails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AddressDetails_Customers_CustomerId",
                table: "AddressDetails");

            migrationBuilder.DropIndex(
                name: "IX_AddressDetails_CustomerId",
                table: "AddressDetails");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "AddressDetails");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CustomerId",
                table: "AddressDetails",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AddressDetails_CustomerId",
                table: "AddressDetails",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_AddressDetails_Customers_CustomerId",
                table: "AddressDetails",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id");
        }
    }
}
