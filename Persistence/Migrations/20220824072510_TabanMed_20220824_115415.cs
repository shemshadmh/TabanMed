using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    public partial class TabanMed_20220824_115415 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "3e2c9b3b-1c5c-41a5-9fe6-9004dcd4b78b",
                column: "ConcurrencyStamp",
                value: "0db282e0-dac3-4386-b3be-ff972d5140ed");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "6f4024b0-153e-4b8b-a851-5befbdb955f9",
                column: "ConcurrencyStamp",
                value: "ff0a5719-67b9-4f8e-acc6-c45a2b401f45");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "04a76057-948a-4fd1-b9f0-ed36991fcaa5",
                columns: new[] { "ConcurrencyStamp", "Created", "SecurityStamp" },
                values: new object[] { "c30cc6c5-8a2f-4d37-8d3f-04aacd20c94c", new DateTime(2022, 8, 24, 7, 25, 8, 345, DateTimeKind.Utc).AddTicks(3913), "f4c2b6f7-a179-4f3d-960c-5a8b5f0023ba" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "b0a39202-a221-47c7-9d34-dc4479ec33f2",
                columns: new[] { "ConcurrencyStamp", "Created", "SecurityStamp" },
                values: new object[] { "b27fd5fb-8753-4fd4-9b52-c462cfef71eb", new DateTime(2022, 8, 24, 7, 25, 8, 345, DateTimeKind.Utc).AddTicks(3785), "100984b0-de84-4c13-ad9f-22df013fd90c" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "3e2c9b3b-1c5c-41a5-9fe6-9004dcd4b78b",
                column: "ConcurrencyStamp",
                value: "56880d40-1240-442a-becf-b975d387f737");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "6f4024b0-153e-4b8b-a851-5befbdb955f9",
                column: "ConcurrencyStamp",
                value: "b7656762-07de-438e-9e2f-eee9dbe58bf9");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "04a76057-948a-4fd1-b9f0-ed36991fcaa5",
                columns: new[] { "ConcurrencyStamp", "Created", "SecurityStamp" },
                values: new object[] { "91898b30-04bf-4863-a6ac-1ebbb5a04dcc", new DateTime(2022, 8, 24, 5, 14, 36, 238, DateTimeKind.Utc).AddTicks(879), "65dd680e-75fe-4827-8a86-7e6bd9aebf99" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "b0a39202-a221-47c7-9d34-dc4479ec33f2",
                columns: new[] { "ConcurrencyStamp", "Created", "SecurityStamp" },
                values: new object[] { "1f5c2949-b3cb-47c4-978b-88c5f99fa0da", new DateTime(2022, 8, 24, 5, 14, 36, 238, DateTimeKind.Utc).AddTicks(816), "f821514e-eeaf-41d3-8da0-0f63b8584e1e" });
        }
    }
}
