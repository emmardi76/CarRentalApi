using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CarRentalApi.Migrations
{
    /// <inheritdoc />
    public partial class CarSeeding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Car",
                columns: new[] { "Id", "CarModelId", "CarTypeId", "PlateNumber" },
                values: new object[,]
                {
                    { 1, 1, 1, "ABC-1234" },
                    { 2, 2, 2, "DEF-5678" },
                    { 3, 4, 3, "GHI-9012" },
                    { 4, 5, 1, "JKL-3456" },
                    { 5, 3, 2, "MNO-7890" },
                    { 6, 6, 3, "PQR-2345" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Car",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Car",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Car",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Car",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Car",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Car",
                keyColumn: "Id",
                keyValue: 6);
        }
    }
}
