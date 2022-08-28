using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    public partial class TabanMed_20220828_123201 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<short>(
                name: "Price",
                table: "MedicalServices",
                type: "smallint",
                nullable: false,
                defaultValue: (short)0);

            migrationBuilder.AddColumn<short>(
                name: "Price",
                table: "Hotels",
                type: "smallint",
                nullable: false,
                defaultValue: (short)0);

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "3e2c9b3b-1c5c-41a5-9fe6-9004dcd4b78b",
                column: "ConcurrencyStamp",
                value: "2fdb48d8-915c-4547-bc1f-d0ef9e8d3145");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "6f4024b0-153e-4b8b-a851-5befbdb955f9",
                column: "ConcurrencyStamp",
                value: "195b210b-f8a7-4394-831e-9b52c70881bb");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "04a76057-948a-4fd1-b9f0-ed36991fcaa5",
                columns: new[] { "ConcurrencyStamp", "Created", "SecurityStamp" },
                values: new object[] { "1aa316de-335c-4d08-8d6c-0ae7ca2be589", new DateTime(2022, 8, 28, 8, 2, 47, 739, DateTimeKind.Utc).AddTicks(1584), "3feb5414-1bf6-4300-8e4f-630764182543" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "b0a39202-a221-47c7-9d34-dc4479ec33f2",
                columns: new[] { "ConcurrencyStamp", "Created", "SecurityStamp" },
                values: new object[] { "73521d71-e361-47e7-b35c-35f55daa0d92", new DateTime(2022, 8, 28, 8, 2, 47, 739, DateTimeKind.Utc).AddTicks(1516), "1387bae6-2771-4633-b697-4d7aeda59159" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "MedicalServices");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Hotels");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "3e2c9b3b-1c5c-41a5-9fe6-9004dcd4b78b",
                column: "ConcurrencyStamp",
                value: "66bb9921-f615-443e-819b-d38b620bb467");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "6f4024b0-153e-4b8b-a851-5befbdb955f9",
                column: "ConcurrencyStamp",
                value: "7d6b8202-441f-4de7-9d45-3c30975f0cbb");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "04a76057-948a-4fd1-b9f0-ed36991fcaa5",
                columns: new[] { "ConcurrencyStamp", "Created", "SecurityStamp" },
                values: new object[] { "2215d67b-7ed3-48d3-be01-48235eef7ac7", new DateTime(2022, 8, 28, 7, 35, 18, 89, DateTimeKind.Utc).AddTicks(5700), "9ae2ec6e-1fb4-4a64-9f89-39234d303cde" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "b0a39202-a221-47c7-9d34-dc4479ec33f2",
                columns: new[] { "ConcurrencyStamp", "Created", "SecurityStamp" },
                values: new object[] { "1393485c-eb25-41d5-ad01-6dfc0f285ab0", new DateTime(2022, 8, 28, 7, 35, 18, 89, DateTimeKind.Utc).AddTicks(5517), "a6741bc1-ee17-496d-b057-880993a0a209" });
        }
    }
}
