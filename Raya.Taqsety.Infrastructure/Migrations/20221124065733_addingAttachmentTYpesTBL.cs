using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Raya.Taqsety.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addingAttachmentTYpesTBL : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Weight",
                table: "JobTypes");

            migrationBuilder.DropColumn(
                name: "Weight",
                table: "JobDetails");

            migrationBuilder.AddColumn<int>(
                name: "AttachmentTypeId",
                table: "Attachments",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NearestSign",
                table: "AddressDetails",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "AttachmentTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AttachmentTypeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CtreatedBy = table.Column<int>(type: "int", nullable: false),
                    ModificationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttachmentTypes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Attachments_AttachmentTypeId",
                table: "Attachments",
                column: "AttachmentTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Attachments_AttachmentTypes_AttachmentTypeId",
                table: "Attachments",
                column: "AttachmentTypeId",
                principalTable: "AttachmentTypes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attachments_AttachmentTypes_AttachmentTypeId",
                table: "Attachments");

            migrationBuilder.DropTable(
                name: "AttachmentTypes");

            migrationBuilder.DropIndex(
                name: "IX_Attachments_AttachmentTypeId",
                table: "Attachments");

            migrationBuilder.DropColumn(
                name: "AttachmentTypeId",
                table: "Attachments");

            migrationBuilder.DropColumn(
                name: "NearestSign",
                table: "AddressDetails");

            migrationBuilder.AddColumn<int>(
                name: "Weight",
                table: "JobTypes",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Weight",
                table: "JobDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
