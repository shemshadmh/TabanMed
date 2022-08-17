using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    public partial class TabanMed_20220817_155317 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MedicalCenters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImageUrl = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    PhoneNumber = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false),
                    AgentPhoneNumber = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true),
                    CityId = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicalCenters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MedicalCenters_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MedicalCenterTranslations",
                columns: table => new
                {
                    MedicalCenterId = table.Column<int>(type: "int", nullable: false),
                    LanguageId = table.Column<byte>(type: "tinyint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    AgentName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicalCenterTranslations", x => new { x.MedicalCenterId, x.LanguageId });
                    table.ForeignKey(
                        name: "FK_MedicalCenterTranslations_Languages_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Languages",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MedicalCenterTranslations_MedicalCenters_MedicalCenterId",
                        column: x => x.MedicalCenterId,
                        principalTable: "MedicalCenters",
                        principalColumn: "Id");
                });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "3e2c9b3b-1c5c-41a5-9fe6-9004dcd4b78b",
                column: "ConcurrencyStamp",
                value: "5fbd8bbb-5873-4aa3-9be7-cf825635b400");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "6f4024b0-153e-4b8b-a851-5befbdb955f9",
                column: "ConcurrencyStamp",
                value: "80c6cd0d-15b7-490e-9093-7ec689c1c1f5");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "04a76057-948a-4fd1-b9f0-ed36991fcaa5",
                columns: new[] { "ConcurrencyStamp", "Created", "SecurityStamp" },
                values: new object[] { "4cf981a3-ee4d-4c01-8c0e-691a727dc2a6", new DateTime(2022, 8, 17, 11, 24, 11, 598, DateTimeKind.Utc).AddTicks(7965), "46115aac-2435-430a-a5b2-454ebd739f19" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "b0a39202-a221-47c7-9d34-dc4479ec33f2",
                columns: new[] { "ConcurrencyStamp", "Created", "SecurityStamp" },
                values: new object[] { "b4deec65-338c-4808-b620-417ed983bb88", new DateTime(2022, 8, 17, 11, 24, 11, 598, DateTimeKind.Utc).AddTicks(7803), "64f571ee-5473-4c0b-97d4-c74074aae702" });

            migrationBuilder.CreateIndex(
                name: "IX_MedicalCenters_CityId",
                table: "MedicalCenters",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalCenterTranslations_LanguageId",
                table: "MedicalCenterTranslations",
                column: "LanguageId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MedicalCenterTranslations");

            migrationBuilder.DropTable(
                name: "MedicalCenters");

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
        }
    }
}
