using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Raya.Taqsety.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class attachmentTypesTwoLanuagesTBL : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AttachmentTypeName",
                table: "AttachmentTypes",
                newName: "EnglishName");

            migrationBuilder.AddColumn<string>(
                name: "ArabicName",
                table: "AttachmentTypes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AttachmentTypes",
                keyColumn: "Id",
                keyValue: 1,
                column: "ArabicName",
                value: "الصورة الأمامية للبطاقة");

            migrationBuilder.UpdateData(
                table: "AttachmentTypes",
                keyColumn: "Id",
                keyValue: 2,
                column: "ArabicName",
                value: "الصورة الخلفية للبطاقة");

            migrationBuilder.UpdateData(
                table: "AttachmentTypes",
                keyColumn: "Id",
                keyValue: 3,
                column: "ArabicName",
                value: " صورة بطاقة التامين الصحي ");

            migrationBuilder.UpdateData(
                table: "AttachmentTypes",
                keyColumn: "Id",
                keyValue: 4,
                column: "ArabicName",
                value: "صورة رخصة السيارة");

            migrationBuilder.UpdateData(
                table: "AttachmentTypes",
                keyColumn: "Id",
                keyValue: 5,
                column: "ArabicName",
                value: "صورة كارنيه عضوية النادي");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ArabicName",
                table: "AttachmentTypes");

            migrationBuilder.RenameColumn(
                name: "EnglishName",
                table: "AttachmentTypes",
                newName: "AttachmentTypeName");
        }
    }
}
