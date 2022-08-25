using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    public partial class TabanMed_20220825_115515 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TourServices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Price = table.Column<decimal>(type: "decimal(6,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TourServices", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TourServiceTranslations",
                columns: table => new
                {
                    TourServiceId = table.Column<int>(type: "int", nullable: false),
                    LanguageId = table.Column<short>(type: "smallint", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(3000)", maxLength: 3000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TourServiceTranslations", x => new { x.TourServiceId, x.LanguageId });
                    table.ForeignKey(
                        name: "FK_TourServiceTranslations_Languages_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Languages",
                        principalColumn: "Lcid");
                    table.ForeignKey(
                        name: "FK_TourServiceTranslations_TourServices_TourServiceId",
                        column: x => x.TourServiceId,
                        principalTable: "TourServices",
                        principalColumn: "Id");
                });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "3e2c9b3b-1c5c-41a5-9fe6-9004dcd4b78b",
                column: "ConcurrencyStamp",
                value: "6a8e6203-feb7-4a10-9848-1664857a1ae6");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "6f4024b0-153e-4b8b-a851-5befbdb955f9",
                column: "ConcurrencyStamp",
                value: "eea913c2-7a78-4606-87c7-bd39e29e600b");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "04a76057-948a-4fd1-b9f0-ed36991fcaa5",
                columns: new[] { "ConcurrencyStamp", "Created", "SecurityStamp" },
                values: new object[] { "a3f5eddf-c208-4ce6-b46f-72df8a03e2ca", new DateTime(2022, 8, 25, 7, 26, 10, 383, DateTimeKind.Utc).AddTicks(3211), "bdda7775-c8fe-4e41-b5c2-32fb4093c732" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "b0a39202-a221-47c7-9d34-dc4479ec33f2",
                columns: new[] { "ConcurrencyStamp", "Created", "SecurityStamp" },
                values: new object[] { "a65ba31e-1e74-4386-85ae-319a97dc2dd7", new DateTime(2022, 8, 25, 7, 26, 10, 383, DateTimeKind.Utc).AddTicks(3150), "96e29614-dfd5-415b-bc84-385f44daaabb" });

            migrationBuilder.CreateIndex(
                name: "IX_TourServiceTranslations_LanguageId",
                table: "TourServiceTranslations",
                column: "LanguageId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TourServiceTranslations");

            migrationBuilder.DropTable(
                name: "TourServices");

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
    }
}
