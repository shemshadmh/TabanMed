using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    public partial class TabanMed_20220828_104347 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

        protected override void Down(MigrationBuilder migrationBuilder)
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
                value: "de63f70e-4919-4895-9036-34a99d8ad2a9");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "6f4024b0-153e-4b8b-a851-5befbdb955f9",
                column: "ConcurrencyStamp",
                value: "f3c5a061-bcdf-4e7c-a58a-65780dbda806");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "04a76057-948a-4fd1-b9f0-ed36991fcaa5",
                columns: new[] { "ConcurrencyStamp", "Created", "SecurityStamp" },
                values: new object[] { "a7266488-0735-4be7-ae3a-4ccd829f20e0", new DateTime(2022, 8, 27, 14, 59, 19, 546, DateTimeKind.Utc).AddTicks(4609), "3ed01310-0c04-4a12-9beb-891f0ebe297d" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "b0a39202-a221-47c7-9d34-dc4479ec33f2",
                columns: new[] { "ConcurrencyStamp", "Created", "SecurityStamp" },
                values: new object[] { "b3b7b99a-6b65-4e41-919e-61bbfb5744a3", new DateTime(2022, 8, 27, 14, 59, 19, 546, DateTimeKind.Utc).AddTicks(4514), "fa8aabe9-1546-46cd-bd3c-a842316b308a" });
        }
    }
}
