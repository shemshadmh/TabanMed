using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    public partial class TabanMed_20220828_120451 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<short>(
                name: "Price",
                table: "TourServices",
                type: "smallint",
                nullable: false,
                defaultValue: (short)0);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "TourServices");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "3e2c9b3b-1c5c-41a5-9fe6-9004dcd4b78b",
                column: "ConcurrencyStamp",
                value: "bd87801b-f562-450b-866c-756c4ad616c9");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "6f4024b0-153e-4b8b-a851-5befbdb955f9",
                column: "ConcurrencyStamp",
                value: "a781aecc-0a18-4a6a-818c-9ade782a3119");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "04a76057-948a-4fd1-b9f0-ed36991fcaa5",
                columns: new[] { "ConcurrencyStamp", "Created", "SecurityStamp" },
                values: new object[] { "20f86e66-a55d-45b1-b6b0-2daf1559523d", new DateTime(2022, 8, 28, 6, 14, 45, 321, DateTimeKind.Utc).AddTicks(4450), "81ac0bd1-cb50-403f-9e10-0339964865fb" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "b0a39202-a221-47c7-9d34-dc4479ec33f2",
                columns: new[] { "ConcurrencyStamp", "Created", "SecurityStamp" },
                values: new object[] { "79d272d1-37de-42bc-a2ee-0ff77599fee8", new DateTime(2022, 8, 28, 6, 14, 45, 321, DateTimeKind.Utc).AddTicks(4385), "57e817be-00e2-4b1b-82e6-810212fc760f" });
        }
    }
}
