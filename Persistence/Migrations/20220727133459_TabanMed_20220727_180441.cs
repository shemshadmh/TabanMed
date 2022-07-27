using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    public partial class TabanMed_20220727_180441 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    Id = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FaName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    EnName = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    Id = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FaName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    EnName = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false),
                    CountryId = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cities_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "Id");
                });

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
                columns: new[] { "ConcurrencyStamp", "Created", "Family", "SecurityStamp" },
                values: new object[] { "f541ec0f-1dba-403e-bdd8-a03bc5a5a007", new DateTime(2022, 7, 27, 13, 34, 58, 970, DateTimeKind.Utc).AddTicks(2820), "تابان", "87bf126d-55fc-4f17-9988-caa406276c63" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "b0a39202-a221-47c7-9d34-dc4479ec33f2",
                columns: new[] { "ConcurrencyStamp", "Created", "SecurityStamp" },
                values: new object[] { "25dddd98-13ff-4e7e-bda9-91e553e65524", new DateTime(2022, 7, 27, 13, 34, 58, 970, DateTimeKind.Utc).AddTicks(2763), "2615e0bd-5519-408c-90a7-14c47e6d6d78" });

            migrationBuilder.CreateIndex(
                name: "IX_Cities_CountryId",
                table: "Cities",
                column: "CountryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cities");

            migrationBuilder.DropTable(
                name: "Countries");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "3e2c9b3b-1c5c-41a5-9fe6-9004dcd4b78b",
                column: "ConcurrencyStamp",
                value: "ac095398-f75d-41f4-8d9d-c9c9de6aadce");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "6f4024b0-153e-4b8b-a851-5befbdb955f9",
                column: "ConcurrencyStamp",
                value: "404af38e-f24e-4135-aaeb-74e405f8d704");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "04a76057-948a-4fd1-b9f0-ed36991fcaa5",
                columns: new[] { "ConcurrencyStamp", "Created", "Family", "SecurityStamp" },
                values: new object[] { "f81071fe-cff4-4d64-a869-dc24323294ef", new DateTime(2022, 7, 26, 10, 21, 20, 480, DateTimeKind.Utc).AddTicks(7961), "فیباتو", "7dafaabe-b828-4a0f-a0cd-3afff3b97e2f" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "b0a39202-a221-47c7-9d34-dc4479ec33f2",
                columns: new[] { "ConcurrencyStamp", "Created", "SecurityStamp" },
                values: new object[] { "7a9e7728-e0c2-4baa-ac2d-e72539a23c8b", new DateTime(2022, 7, 26, 10, 21, 20, 480, DateTimeKind.Utc).AddTicks(7931), "91b8b005-9cd3-40c3-957b-c72eb137281a" });
        }
    }
}
