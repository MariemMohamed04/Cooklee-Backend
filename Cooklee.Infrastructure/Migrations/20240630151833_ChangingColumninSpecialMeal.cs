using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cooklee.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangingColumninSpecialMeal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SpecialMeals_ChefPage_ChefPageId",
                table: "SpecialMeals");

            migrationBuilder.DropIndex(
                name: "IX_SpecialMeals_ChefPageId",
                table: "SpecialMeals");

            migrationBuilder.DropColumn(
                name: "ChefPageId",
                table: "SpecialMeals");

            migrationBuilder.AddColumn<int>(
                name: "ChefId",
                table: "SpecialMeals",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SpecialMeals_ChefId",
                table: "SpecialMeals",
                column: "ChefId");

            migrationBuilder.AddForeignKey(
                name: "FK_SpecialMeals_ChefPage_ChefId",
                table: "SpecialMeals",
                column: "ChefId",
                principalTable: "ChefPage",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SpecialMeals_ChefPage_ChefId",
                table: "SpecialMeals");

            migrationBuilder.DropIndex(
                name: "IX_SpecialMeals_ChefId",
                table: "SpecialMeals");

            migrationBuilder.DropColumn(
                name: "ChefId",
                table: "SpecialMeals");

            migrationBuilder.AddColumn<int>(
                name: "ChefPageId",
                table: "SpecialMeals",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_SpecialMeals_ChefPageId",
                table: "SpecialMeals",
                column: "ChefPageId");

            migrationBuilder.AddForeignKey(
                name: "FK_SpecialMeals_ChefPage_ChefPageId",
                table: "SpecialMeals",
                column: "ChefPageId",
                principalTable: "ChefPage",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
