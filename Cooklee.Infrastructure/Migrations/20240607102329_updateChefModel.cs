using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cooklee.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updateChefModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DisplayName",
                table: "ChefPage",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DisplayName",
                table: "ChefPage");
        }
    }
}
