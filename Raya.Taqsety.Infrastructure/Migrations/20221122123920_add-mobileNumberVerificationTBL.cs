using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Raya.Taqsety.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addmobileNumberVerificationTBL : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropColumn(
            //    name: "JobName",
            //    table: "JobDetails");

            migrationBuilder.CreateTable(
                name: "MobileNumberVerifications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MobileNumber = table.Column<int>(type: "int", nullable: false),
                    Code = table.Column<int>(type: "int", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CtreatedBy = table.Column<int>(type: "int", nullable: false),
                    ModificationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MobileNumberVerifications", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MobileNumberVerifications_MobileNumber",
                table: "MobileNumberVerifications",
                column: "MobileNumber",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MobileNumberVerifications");

            //migrationBuilder.AddColumn<string>(
            //    name: "JobName",
            //    table: "JobDetails",
            //    type: "nvarchar(max)",
            //    nullable: false,
            //    defaultValue: "");
        }
    }
}
