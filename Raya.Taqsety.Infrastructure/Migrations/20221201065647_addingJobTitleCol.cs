using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Raya.Taqsety.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addingJobTitleCol : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "JobTitle",
                table: "JobDetails",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "JobTitle",
                table: "JobDetails");
        }
    }
}
