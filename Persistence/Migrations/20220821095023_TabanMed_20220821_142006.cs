using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    public partial class TabanMed_20220821_142006 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "AgentName",
                table: "MedicalCenterTranslations",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "3e2c9b3b-1c5c-41a5-9fe6-9004dcd4b78b",
                column: "ConcurrencyStamp",
                value: "80c30453-b591-4f49-aca2-e56ff234fe58");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "6f4024b0-153e-4b8b-a851-5befbdb955f9",
                column: "ConcurrencyStamp",
                value: "da5c3938-e311-48e4-a5b8-ab890c72b3d1");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "04a76057-948a-4fd1-b9f0-ed36991fcaa5",
                columns: new[] { "ConcurrencyStamp", "Created", "SecurityStamp" },
                values: new object[] { "2d984e45-c6bf-4f20-9604-ce311ee92fe4", new DateTime(2022, 8, 21, 9, 50, 22, 690, DateTimeKind.Utc).AddTicks(5396), "748daf90-a753-490b-b6ba-ed73fbe81446" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "b0a39202-a221-47c7-9d34-dc4479ec33f2",
                columns: new[] { "ConcurrencyStamp", "Created", "SecurityStamp" },
                values: new object[] { "fd0ffacc-84d4-4a81-b278-ad6eef4ef997", new DateTime(2022, 8, 21, 9, 50, 22, 690, DateTimeKind.Utc).AddTicks(5362), "24025d9e-56e9-4c6a-b646-6ac7d6223dae" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "AgentName",
                table: "MedicalCenterTranslations",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30,
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "3e2c9b3b-1c5c-41a5-9fe6-9004dcd4b78b",
                column: "ConcurrencyStamp",
                value: "3151f71a-f049-4a92-bf3e-0083cf67e255");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "6f4024b0-153e-4b8b-a851-5befbdb955f9",
                column: "ConcurrencyStamp",
                value: "ba697b8a-d255-4782-b885-2c845433dc97");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "04a76057-948a-4fd1-b9f0-ed36991fcaa5",
                columns: new[] { "ConcurrencyStamp", "Created", "SecurityStamp" },
                values: new object[] { "63e73e6f-d116-4c10-a5e1-681213e4cae9", new DateTime(2022, 8, 17, 11, 42, 52, 951, DateTimeKind.Utc).AddTicks(2854), "341151dc-38af-4a54-94f2-356625dc0ef5" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "b0a39202-a221-47c7-9d34-dc4479ec33f2",
                columns: new[] { "ConcurrencyStamp", "Created", "SecurityStamp" },
                values: new object[] { "6ae1250c-3903-4e0b-838e-3497280c8325", new DateTime(2022, 8, 17, 11, 42, 52, 951, DateTimeKind.Utc).AddTicks(2823), "f9383e82-395f-4661-a539-74d1915d577e" });
        }
    }
}
