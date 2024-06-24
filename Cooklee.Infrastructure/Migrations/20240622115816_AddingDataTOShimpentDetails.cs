using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cooklee.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddingDataTOShimpentDetails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ShipmentDetails_Area",
                table: "Orders",
                newName: "ShipmentDetails_State");

            migrationBuilder.AddColumn<string>(
                name: "ShipmentDetails_Apartment",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ShipmentDetails_Building",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ShipmentDetails_City",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ShipmentDetails_Country",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ShipmentDetails_Email",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ShipmentDetails_Floor",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ShipmentDetails_PostalCode",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ShipmentDetails_ShippingMethod",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ShipmentDetails_Apartment",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ShipmentDetails_Building",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ShipmentDetails_City",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ShipmentDetails_Country",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ShipmentDetails_Email",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ShipmentDetails_Floor",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ShipmentDetails_PostalCode",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ShipmentDetails_ShippingMethod",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "ShipmentDetails_State",
                table: "Orders",
                newName: "ShipmentDetails_Area");
        }
    }
}
