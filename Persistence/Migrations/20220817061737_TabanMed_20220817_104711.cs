using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    public partial class TabanMed_20220817_104711 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CityTranslation_Cities_CityId",
                table: "CityTranslation");

            migrationBuilder.DropForeignKey(
                name: "FK_CityTranslation_Languages_LanguageId",
                table: "CityTranslation");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CityTranslation",
                table: "CityTranslation");

            migrationBuilder.RenameTable(
                name: "CityTranslation",
                newName: "CityTranslations");

            migrationBuilder.RenameIndex(
                name: "IX_CityTranslation_LanguageId",
                table: "CityTranslations",
                newName: "IX_CityTranslations_LanguageId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CityTranslations",
                table: "CityTranslations",
                columns: new[] { "CityId", "LanguageId" });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "3e2c9b3b-1c5c-41a5-9fe6-9004dcd4b78b",
                column: "ConcurrencyStamp",
                value: "d086aec4-d4d8-4d4c-a763-eff901bb2d94");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "6f4024b0-153e-4b8b-a851-5befbdb955f9",
                column: "ConcurrencyStamp",
                value: "00abbd9e-5c2b-4a3f-b62f-1810c8cbf36c");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "04a76057-948a-4fd1-b9f0-ed36991fcaa5",
                columns: new[] { "ConcurrencyStamp", "Created", "SecurityStamp" },
                values: new object[] { "8e6d3b9c-9c36-469e-b6ce-5b57f4129056", new DateTime(2022, 8, 17, 6, 17, 35, 929, DateTimeKind.Utc).AddTicks(6097), "1cd5b53b-67d9-4fb1-91b4-d2d21589eed0" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "b0a39202-a221-47c7-9d34-dc4479ec33f2",
                columns: new[] { "ConcurrencyStamp", "Created", "SecurityStamp" },
                values: new object[] { "f9f8186d-a366-4572-bcbb-62a8b1c0e8f5", new DateTime(2022, 8, 17, 6, 17, 35, 929, DateTimeKind.Utc).AddTicks(6044), "75098838-a099-47ac-834c-cf1fd8c9c808" });

            migrationBuilder.AddForeignKey(
                name: "FK_CityTranslations_Cities_CityId",
                table: "CityTranslations",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CityTranslations_Languages_LanguageId",
                table: "CityTranslations",
                column: "LanguageId",
                principalTable: "Languages",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CityTranslations_Cities_CityId",
                table: "CityTranslations");

            migrationBuilder.DropForeignKey(
                name: "FK_CityTranslations_Languages_LanguageId",
                table: "CityTranslations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CityTranslations",
                table: "CityTranslations");

            migrationBuilder.RenameTable(
                name: "CityTranslations",
                newName: "CityTranslation");

            migrationBuilder.RenameIndex(
                name: "IX_CityTranslations_LanguageId",
                table: "CityTranslation",
                newName: "IX_CityTranslation_LanguageId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CityTranslation",
                table: "CityTranslation",
                columns: new[] { "CityId", "LanguageId" });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "3e2c9b3b-1c5c-41a5-9fe6-9004dcd4b78b",
                column: "ConcurrencyStamp",
                value: "37128188-b887-45dc-9bf1-d138ef7a2f3f");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "6f4024b0-153e-4b8b-a851-5befbdb955f9",
                column: "ConcurrencyStamp",
                value: "9ee523f2-9850-4a2d-a52a-7f8b4a989904");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "04a76057-948a-4fd1-b9f0-ed36991fcaa5",
                columns: new[] { "ConcurrencyStamp", "Created", "SecurityStamp" },
                values: new object[] { "9c78322a-beeb-4703-8f90-ac7e878cb82b", new DateTime(2022, 8, 16, 12, 43, 30, 629, DateTimeKind.Utc).AddTicks(6974), "9908be7b-780b-4422-8691-b5c91e933e31" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "b0a39202-a221-47c7-9d34-dc4479ec33f2",
                columns: new[] { "ConcurrencyStamp", "Created", "SecurityStamp" },
                values: new object[] { "ce493820-3c1d-4500-91e7-005857f51adb", new DateTime(2022, 8, 16, 12, 43, 30, 629, DateTimeKind.Utc).AddTicks(6833), "507da7d9-dbd3-4660-bb3c-2909d5f2bfd4" });

            migrationBuilder.AddForeignKey(
                name: "FK_CityTranslation_Cities_CityId",
                table: "CityTranslation",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CityTranslation_Languages_LanguageId",
                table: "CityTranslation",
                column: "LanguageId",
                principalTable: "Languages",
                principalColumn: "Id");
        }
    }
}
