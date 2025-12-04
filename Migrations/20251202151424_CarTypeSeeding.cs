using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CarRentalApi.Migrations
{
    /// <inheritdoc />
    public partial class CarTypeSeeding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "CarModel",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "BMW 7" },
                    { 2, "Kia Sorento" },
                    { 3, "Nissan JuKe" },
                    { 4, "Seat Ibiza" },
                    { 5, "Audi A8" },
                    { 6, "Renault Clio" }
                });

            migrationBuilder.InsertData(
                table: "CarType",
                columns: new[] { "Id", "Name", "PricePerDay" },
                values: new object[,]
                {
                    { 1, "Premium", 300.00m },
                    { 2, "SUV", 150.00m },
                    { 3, "Small", 50.00m }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "CarModel",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "CarModel",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "CarModel",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "CarModel",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "CarModel",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "CarModel",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "CarType",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "CarType",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "CarType",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
