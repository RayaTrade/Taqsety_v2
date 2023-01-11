using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Raya.Taqsety.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class mobileNumberVerification : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_MobileNumberVerifications_MobileNumber",
                table: "MobileNumberVerifications");

            migrationBuilder.CreateIndex(
                name: "IX_MobileNumberVerifications_MobileNumber",
                table: "MobileNumberVerifications",
                column: "MobileNumber");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_MobileNumberVerifications_MobileNumber",
                table: "MobileNumberVerifications");

            migrationBuilder.CreateIndex(
                name: "IX_MobileNumberVerifications_MobileNumber",
                table: "MobileNumberVerifications",
                column: "MobileNumber",
                unique: true,
                filter: "[MobileNumber] IS NOT NULL");
        }
    }
}
