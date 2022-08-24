using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    public partial class TabanMed_20220824_115722 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "3e2c9b3b-1c5c-41a5-9fe6-9004dcd4b78b",
                column: "ConcurrencyStamp",
                value: "725000ad-13c1-4c5c-863c-c3254d8143df");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "6f4024b0-153e-4b8b-a851-5befbdb955f9",
                column: "ConcurrencyStamp",
                value: "b8817d69-45c7-43b6-9e5b-aef667186c23");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "04a76057-948a-4fd1-b9f0-ed36991fcaa5",
                columns: new[] { "ConcurrencyStamp", "Created", "SecurityStamp" },
                values: new object[] { "87a9709f-9bc1-44f7-a34b-53b68fad8292", new DateTime(2022, 8, 24, 7, 27, 44, 700, DateTimeKind.Utc).AddTicks(9796), "217e1f45-16f8-49ae-b5e9-419c044169bb" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "b0a39202-a221-47c7-9d34-dc4479ec33f2",
                columns: new[] { "ConcurrencyStamp", "Created", "SecurityStamp" },
                values: new object[] { "3908b5af-ce0b-4bd6-bcd2-a9a8e671deef", new DateTime(2022, 8, 24, 7, 27, 44, 700, DateTimeKind.Utc).AddTicks(9730), "2aa31d4d-3675-4bf6-b2a7-f4dae94c996d" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
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
    }
}
