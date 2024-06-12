using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cooklee.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updateChefaddDesaibled : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDesaibled",
                table: "ChefPage",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDesaibled",
                table: "ChefPage");
        }
    }
}
