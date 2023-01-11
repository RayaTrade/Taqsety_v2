using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Raya.Taqsety.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class dataSeeding1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AttachmentTypes",
                keyColumn: "Id",
                keyValue: 1,
                column: "EnglishName",
                value: "Front NationalId");

            migrationBuilder.UpdateData(
                table: "AttachmentTypes",
                keyColumn: "Id",
                keyValue: 2,
                column: "EnglishName",
                value: "Back NationalId");

            migrationBuilder.UpdateData(
                table: "AttachmentTypes",
                keyColumn: "Id",
                keyValue: 3,
                column: "EnglishName",
                value: "Medical Card");

            migrationBuilder.UpdateData(
                table: "AttachmentTypes",
                keyColumn: "Id",
                keyValue: 4,
                column: "EnglishName",
                value: "Car License");

            migrationBuilder.UpdateData(
                table: "AttachmentTypes",
                keyColumn: "Id",
                keyValue: 5,
                column: "EnglishName",
                value: "Club Card");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AttachmentTypes",
                keyColumn: "Id",
                keyValue: 1,
                column: "EnglishName",
                value: "FrontNationalId");

            migrationBuilder.UpdateData(
                table: "AttachmentTypes",
                keyColumn: "Id",
                keyValue: 2,
                column: "EnglishName",
                value: "BackNationalId");

            migrationBuilder.UpdateData(
                table: "AttachmentTypes",
                keyColumn: "Id",
                keyValue: 3,
                column: "EnglishName",
                value: "MedicalCard");

            migrationBuilder.UpdateData(
                table: "AttachmentTypes",
                keyColumn: "Id",
                keyValue: 4,
                column: "EnglishName",
                value: "CarLicense");

            migrationBuilder.UpdateData(
                table: "AttachmentTypes",
                keyColumn: "Id",
                keyValue: 5,
                column: "EnglishName",
                value: "ClubCard");
        }
    }
}
