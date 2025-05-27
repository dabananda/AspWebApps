using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AspWebApps.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class CompanyModelDataSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "Id", "City", "Name", "PhoneNumber", "PostalCode", "State", "StreetAddress" },
                values: new object[,]
                {
                    { 1, "Dhaka", "TechNova Ltd.", "+8801712345678", "1207", "Dhaka", "123 Main Street" },
                    { 2, "Chattogram", "GreenLeaf Organics", "+8801611223344", "4000", "Chattogram", "456 Garden Road" },
                    { 3, "Khulna", "FreshMart Enterprises", "+8801555667788", "9100", "Khulna", "789 Market Avenue" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
