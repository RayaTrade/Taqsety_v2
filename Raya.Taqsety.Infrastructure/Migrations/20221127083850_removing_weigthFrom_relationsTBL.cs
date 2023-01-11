using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Raya.Taqsety.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class removingweigthFromrelationsTBL : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Weight",
                table: "Relations");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Weight",
                table: "Relations",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
