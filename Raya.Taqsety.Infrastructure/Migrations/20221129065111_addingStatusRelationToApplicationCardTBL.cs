using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Raya.Taqsety.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addingStatusRelationToApplicationCardTBL : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "StatusId",
                table: "InstallmentCards",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_InstallmentCards_StatusId",
                table: "InstallmentCards",
                column: "StatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_InstallmentCards_Statuses_StatusId",
                table: "InstallmentCards",
                column: "StatusId",
                principalTable: "Statuses",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InstallmentCards_Statuses_StatusId",
                table: "InstallmentCards");

            migrationBuilder.DropIndex(
                name: "IX_InstallmentCards_StatusId",
                table: "InstallmentCards");

            migrationBuilder.AlterColumn<int>(
                name: "StatusId",
                table: "InstallmentCards",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }
    }
}
