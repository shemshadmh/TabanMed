using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    public partial class TabanMed_20220801_143921 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Languages",
                columns: table => new
                {
                    Id = table.Column<byte>(type: "tinyint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    IsoName = table.Column<string>(type: "varchar(5)", maxLength: 5, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Languages", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Languages",
                columns: new[] { "Id", "IsoName", "Name" },
                values: new object[,]
                {
                    { (byte)1, "fa", "فارسی" },
                    { (byte)2, "en", "English" },
                    { (byte)3, "ar", "العربیة" },
                    { (byte)4, "ps", "پشتو/دری" }
                });

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Languages");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "3e2c9b3b-1c5c-41a5-9fe6-9004dcd4b78b",
                column: "ConcurrencyStamp",
                value: "60863d5f-7a79-49eb-b7e3-bdf620b7204d");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "6f4024b0-153e-4b8b-a851-5befbdb955f9",
                column: "ConcurrencyStamp",
                value: "414bd14e-1908-40db-a4e2-6b29a987dda8");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "04a76057-948a-4fd1-b9f0-ed36991fcaa5",
                columns: new[] { "ConcurrencyStamp", "Created", "SecurityStamp" },
                values: new object[] { "eacadb04-ab09-4615-bcc8-e03af8671a46", new DateTime(2022, 7, 30, 7, 53, 49, 266, DateTimeKind.Utc).AddTicks(7289), "d3644d64-cd48-465c-929c-fcbc7152df47" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "b0a39202-a221-47c7-9d34-dc4479ec33f2",
                columns: new[] { "ConcurrencyStamp", "Created", "SecurityStamp" },
                values: new object[] { "5ba5a743-2b43-4794-800b-cdd85d6df249", new DateTime(2022, 7, 30, 7, 53, 49, 266, DateTimeKind.Utc).AddTicks(7258), "47bd8464-30f5-4ec6-898c-f28fd08bef25" });
        }
    }
}
