using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cooklee.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class update_meal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsAccepeted",
                table: "Meals",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAccepeted",
                table: "Meals");
        }
    }
}
