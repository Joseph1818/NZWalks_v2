using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace NZWalks.API.Migrations
{
    /// <inheritdoc />
    public partial class seedingdifficutltiesandregions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "difficulties",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("6b4f8c2e-1d4d-4f3e-8b9d-1c2e3f4a5b6c"), "Easy" },
                    { new Guid("7c5f9d3f-2e5e-4f4f-9c0e-2d3f4f5a6b7c"), "Medium" },
                    { new Guid("8d6e0e4d-3f6f-4f5e-0d1f-3e4f5a6b7c8d"), "Hard" }
                });

            migrationBuilder.InsertData(
                table: "regions",
                columns: new[] { "Id", "Code", "Name", "RegionImageUrl" },
                values: new object[,]
                {
                    { new Guid("1a2b3c4d-5e6f-4a8b-9c0d-1e2f3a4b5c6d"), "AKL", "Auckland", "https://example.com/auckland.jpg" },
                    { new Guid("2b3c4d5e-6f7a-4b9c-0d1e-2f3a4b5c6d7e"), "WLG", "Wellington", "https://example.com/wellington.jpg" },
                    { new Guid("3c4d5e6f-7a8b-4c0d-1e2f-3a4b5c6d7e8f"), "CHC", "Christchurch", "https://example.com/christchurch.jpg" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "difficulties",
                keyColumn: "Id",
                keyValue: new Guid("6b4f8c2e-1d4d-4f3e-8b9d-1c2e3f4a5b6c"));

            migrationBuilder.DeleteData(
                table: "difficulties",
                keyColumn: "Id",
                keyValue: new Guid("7c5f9d3f-2e5e-4f4f-9c0e-2d3f4f5a6b7c"));

            migrationBuilder.DeleteData(
                table: "difficulties",
                keyColumn: "Id",
                keyValue: new Guid("8d6e0e4d-3f6f-4f5e-0d1f-3e4f5a6b7c8d"));

            migrationBuilder.DeleteData(
                table: "regions",
                keyColumn: "Id",
                keyValue: new Guid("1a2b3c4d-5e6f-4a8b-9c0d-1e2f3a4b5c6d"));

            migrationBuilder.DeleteData(
                table: "regions",
                keyColumn: "Id",
                keyValue: new Guid("2b3c4d5e-6f7a-4b9c-0d1e-2f3a4b5c6d7e"));

            migrationBuilder.DeleteData(
                table: "regions",
                keyColumn: "Id",
                keyValue: new Guid("3c4d5e6f-7a8b-4c0d-1e2f-3a4b5c6d7e8f"));
        }
    }
}
