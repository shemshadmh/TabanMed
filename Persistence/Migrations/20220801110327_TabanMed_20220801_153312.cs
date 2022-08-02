using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    public partial class TabanMed_20220801_153312 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Title",
                table: "HotelFacilities");

            migrationBuilder.CreateTable(
                name: "HotelFacilityTranslation",
                columns: table => new
                {
                    FacilityId = table.Column<int>(type: "int", nullable: false),
                    LanguageId = table.Column<byte>(type: "tinyint", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HotelFacilityTranslation", x => new { x.FacilityId, x.LanguageId });
                    table.ForeignKey(
                        name: "FK_HotelFacilityTranslation_HotelFacilities_FacilityId",
                        column: x => x.FacilityId,
                        principalTable: "HotelFacilities",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_HotelFacilityTranslation_Languages_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Languages",
                        principalColumn: "Id");
                });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "3e2c9b3b-1c5c-41a5-9fe6-9004dcd4b78b",
                column: "ConcurrencyStamp",
                value: "b2ef603b-6ae5-432e-b26a-a3b328e9199a");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "6f4024b0-153e-4b8b-a851-5befbdb955f9",
                column: "ConcurrencyStamp",
                value: "6d3c78d0-4722-4d15-98f3-355d9006f401");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "04a76057-948a-4fd1-b9f0-ed36991fcaa5",
                columns: new[] { "ConcurrencyStamp", "Created", "SecurityStamp" },
                values: new object[] { "15e9e2bb-1895-42ef-aee5-76f6d8f6d9de", new DateTime(2022, 8, 1, 11, 3, 27, 162, DateTimeKind.Utc).AddTicks(9951), "572a8450-4672-4408-9def-36e52f271421" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "b0a39202-a221-47c7-9d34-dc4479ec33f2",
                columns: new[] { "ConcurrencyStamp", "Created", "SecurityStamp" },
                values: new object[] { "9bf7813f-5dd3-4393-9c3c-c91bab23f11b", new DateTime(2022, 8, 1, 11, 3, 27, 162, DateTimeKind.Utc).AddTicks(9920), "af19cd7e-27fd-4dba-9f71-8df4058697f9" });

            migrationBuilder.CreateIndex(
                name: "IX_HotelFacilityTranslation_LanguageId",
                table: "HotelFacilityTranslation",
                column: "LanguageId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HotelFacilityTranslation");

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "HotelFacilities",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "3e2c9b3b-1c5c-41a5-9fe6-9004dcd4b78b",
                column: "ConcurrencyStamp",
                value: "902fed97-2107-43de-9228-ea246e76313a");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "6f4024b0-153e-4b8b-a851-5befbdb955f9",
                column: "ConcurrencyStamp",
                value: "7600b40d-8ae3-45e8-9d36-e0507059af0d");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "04a76057-948a-4fd1-b9f0-ed36991fcaa5",
                columns: new[] { "ConcurrencyStamp", "Created", "SecurityStamp" },
                values: new object[] { "cf2e42f5-c300-40e1-aedc-2660dfeaa3bf", new DateTime(2022, 8, 1, 10, 9, 39, 731, DateTimeKind.Utc).AddTicks(7477), "e949b70a-5c45-4913-8fe7-2027386e1372" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "b0a39202-a221-47c7-9d34-dc4479ec33f2",
                columns: new[] { "ConcurrencyStamp", "Created", "SecurityStamp" },
                values: new object[] { "8458cde1-9ff7-40e8-babc-c4242f8ac1cf", new DateTime(2022, 8, 1, 10, 9, 39, 731, DateTimeKind.Utc).AddTicks(7429), "c470eeee-38e5-41f7-9006-004d765271bd" });
        }
    }
}
