using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarRentalApi.Migrations
{
    /// <inheritdoc />
    public partial class CartTypeLoyaltyPoints : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LoyaltyPoints",
                table: "CarType",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "CarType",
                keyColumn: "Id",
                keyValue: 1,
                column: "LoyaltyPoints",
                value: 5);

            migrationBuilder.UpdateData(
                table: "CarType",
                keyColumn: "Id",
                keyValue: 2,
                column: "LoyaltyPoints",
                value: 3);

            migrationBuilder.UpdateData(
                table: "CarType",
                keyColumn: "Id",
                keyValue: 3,
                column: "LoyaltyPoints",
                value: 1);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LoyaltyPoints",
                table: "CarType");
        }
    }
}
