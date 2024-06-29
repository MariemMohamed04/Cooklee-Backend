﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cooklee.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddingColumnTo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsAccepted",
                table: "Meals",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAccepted",
                table: "Meals");
        }
    }
}
