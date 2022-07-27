using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    public partial class TabanMed_20220727_181213 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "EnName", "FaName" },
                values: new object[] { (short)1, "Iran", "ایران" });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "3e2c9b3b-1c5c-41a5-9fe6-9004dcd4b78b",
                column: "ConcurrencyStamp",
                value: "cc9302fe-8631-4ed4-906d-ef6b2961a4bc");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "6f4024b0-153e-4b8b-a851-5befbdb955f9",
                column: "ConcurrencyStamp",
                value: "f5384281-057e-4fbb-9896-3ad0a9fc19ac");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "04a76057-948a-4fd1-b9f0-ed36991fcaa5",
                columns: new[] { "ConcurrencyStamp", "Created", "SecurityStamp" },
                values: new object[] { "a6c10570-76c9-4ee7-849a-8148eee9c739", new DateTime(2022, 7, 27, 13, 42, 21, 535, DateTimeKind.Utc).AddTicks(8440), "3e3ca5c9-387e-431b-8b91-184d606c67b6" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "b0a39202-a221-47c7-9d34-dc4479ec33f2",
                columns: new[] { "ConcurrencyStamp", "Created", "SecurityStamp" },
                values: new object[] { "7aca6c88-9c88-45b0-b286-4061ebe5b1a2", new DateTime(2022, 7, 27, 13, 42, 21, 535, DateTimeKind.Utc).AddTicks(8407), "1ddc509a-222f-4a19-b5c2-92998d7d33a3" });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "Id", "CountryId", "EnName", "FaName" },
                values: new object[] { (short)1, (short)1, "Tehran", "تهران" });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "Id", "CountryId", "EnName", "FaName" },
                values: new object[] { (short)2, (short)1, "Mashhad", "مشهد" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: (short)1);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: (short)2);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: (short)1);

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "3e2c9b3b-1c5c-41a5-9fe6-9004dcd4b78b",
                column: "ConcurrencyStamp",
                value: "43b9894d-3b14-453f-b5bc-2b180560952e");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "6f4024b0-153e-4b8b-a851-5befbdb955f9",
                column: "ConcurrencyStamp",
                value: "0e1d3090-a611-4513-8ecf-ce6079daccfb");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "04a76057-948a-4fd1-b9f0-ed36991fcaa5",
                columns: new[] { "ConcurrencyStamp", "Created", "SecurityStamp" },
                values: new object[] { "f541ec0f-1dba-403e-bdd8-a03bc5a5a007", new DateTime(2022, 7, 27, 13, 34, 58, 970, DateTimeKind.Utc).AddTicks(2820), "87bf126d-55fc-4f17-9988-caa406276c63" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "b0a39202-a221-47c7-9d34-dc4479ec33f2",
                columns: new[] { "ConcurrencyStamp", "Created", "SecurityStamp" },
                values: new object[] { "25dddd98-13ff-4e7e-bda9-91e553e65524", new DateTime(2022, 7, 27, 13, 34, 58, 970, DateTimeKind.Utc).AddTicks(2763), "2615e0bd-5519-408c-90a7-14c47e6d6d78" });
        }
    }
}
