using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    public partial class TabanMed_20220816_171254 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CountryTranslation_Countries_CountryId",
                table: "CountryTranslation");

            migrationBuilder.DropForeignKey(
                name: "FK_CountryTranslation_Languages_LanguageId",
                table: "CountryTranslation");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CountryTranslation",
                table: "CountryTranslation");

            migrationBuilder.RenameTable(
                name: "CountryTranslation",
                newName: "CountriesTranslation");

            migrationBuilder.RenameIndex(
                name: "IX_CountryTranslation_LanguageId",
                table: "CountriesTranslation",
                newName: "IX_CountriesTranslation_LanguageId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CountriesTranslation",
                table: "CountriesTranslation",
                columns: new[] { "CountryId", "LanguageId" });

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
                name: "FK_CountriesTranslation_Countries_CountryId",
                table: "CountriesTranslation",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CountriesTranslation_Languages_LanguageId",
                table: "CountriesTranslation",
                column: "LanguageId",
                principalTable: "Languages",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CountriesTranslation_Countries_CountryId",
                table: "CountriesTranslation");

            migrationBuilder.DropForeignKey(
                name: "FK_CountriesTranslation_Languages_LanguageId",
                table: "CountriesTranslation");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CountriesTranslation",
                table: "CountriesTranslation");

            migrationBuilder.RenameTable(
                name: "CountriesTranslation",
                newName: "CountryTranslation");

            migrationBuilder.RenameIndex(
                name: "IX_CountriesTranslation_LanguageId",
                table: "CountryTranslation",
                newName: "IX_CountryTranslation_LanguageId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CountryTranslation",
                table: "CountryTranslation",
                columns: new[] { "CountryId", "LanguageId" });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "3e2c9b3b-1c5c-41a5-9fe6-9004dcd4b78b",
                column: "ConcurrencyStamp",
                value: "6e3e66c8-6c17-4125-a4be-3c775e024c04");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "6f4024b0-153e-4b8b-a851-5befbdb955f9",
                column: "ConcurrencyStamp",
                value: "278c24d8-e59a-4165-aa3f-b61806f03013");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "04a76057-948a-4fd1-b9f0-ed36991fcaa5",
                columns: new[] { "ConcurrencyStamp", "Created", "SecurityStamp" },
                values: new object[] { "09b06172-da8c-4ceb-8c8c-83d81d068961", new DateTime(2022, 8, 15, 15, 7, 50, 675, DateTimeKind.Utc).AddTicks(3262), "62c069ef-e318-46de-a1b2-70bbceecb1aa" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "b0a39202-a221-47c7-9d34-dc4479ec33f2",
                columns: new[] { "ConcurrencyStamp", "Created", "SecurityStamp" },
                values: new object[] { "f86dffff-0d1d-4b58-a972-e5a376d3e4ec", new DateTime(2022, 8, 15, 15, 7, 50, 675, DateTimeKind.Utc).AddTicks(3232), "3d108138-6864-40fa-9659-2399bfb26dfe" });

            migrationBuilder.AddForeignKey(
                name: "FK_CountryTranslation_Countries_CountryId",
                table: "CountryTranslation",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CountryTranslation_Languages_LanguageId",
                table: "CountryTranslation",
                column: "LanguageId",
                principalTable: "Languages",
                principalColumn: "Id");
        }
    }
}
