using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Raya.Taqsety.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class removeGrantoIdFromAddressDetails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AddressDetails_Grantors_GrantorId",
                table: "AddressDetails");

            migrationBuilder.DropIndex(
                name: "IX_AddressDetails_GrantorId",
                table: "AddressDetails");

            migrationBuilder.DropColumn(
                name: "GrantorId",
                table: "AddressDetails");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GrantorId",
                table: "AddressDetails",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AddressDetails_GrantorId",
                table: "AddressDetails",
                column: "GrantorId");

            migrationBuilder.AddForeignKey(
                name: "FK_AddressDetails_Grantors_GrantorId",
                table: "AddressDetails",
                column: "GrantorId",
                principalTable: "Grantors",
                principalColumn: "Id");
        }
    }
}
