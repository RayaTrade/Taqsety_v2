using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Raya.Taqsety.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updateMobileNumberToBeString : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_MobileNumberVerifications_MobileNumber",
                table: "MobileNumberVerifications");

            migrationBuilder.AlterColumn<string>(
                name: "MobileNumber",
                table: "MobileNumberVerifications",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_MobileNumberVerifications_MobileNumber",
                table: "MobileNumberVerifications",
                column: "MobileNumber",
                unique: true,
                filter: "[MobileNumber] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_MobileNumberVerifications_MobileNumber",
                table: "MobileNumberVerifications");

            migrationBuilder.AlterColumn<int>(
                name: "MobileNumber",
                table: "MobileNumberVerifications",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MobileNumberVerifications_MobileNumber",
                table: "MobileNumberVerifications",
                column: "MobileNumber",
                unique: true);
        }
    }
}
