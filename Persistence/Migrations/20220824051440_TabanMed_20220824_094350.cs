using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    public partial class TabanMed_20220824_094350 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MedicalServices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ParentId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicalServices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MedicalServices_MedicalServices_ParentId",
                        column: x => x.ParentId,
                        principalTable: "MedicalServices",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MedicalCenterMedicalServices",
                columns: table => new
                {
                    MedicalCenterId = table.Column<int>(type: "int", nullable: false),
                    MedicalServiceId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicalCenterMedicalServices", x => new { x.MedicalCenterId, x.MedicalServiceId });
                    table.ForeignKey(
                        name: "FK_MedicalCenterMedicalServices_MedicalCenters_MedicalCenterId",
                        column: x => x.MedicalCenterId,
                        principalTable: "MedicalCenters",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MedicalCenterMedicalServices_MedicalServices_MedicalServiceId",
                        column: x => x.MedicalServiceId,
                        principalTable: "MedicalServices",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MedicalServiceTranslations",
                columns: table => new
                {
                    MedicalServiceId = table.Column<int>(type: "int", nullable: false),
                    LanguageId = table.Column<short>(type: "smallint", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicalServiceTranslations", x => new { x.MedicalServiceId, x.LanguageId });
                    table.ForeignKey(
                        name: "FK_MedicalServiceTranslations_Languages_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Languages",
                        principalColumn: "Lcid");
                    table.ForeignKey(
                        name: "FK_MedicalServiceTranslations_MedicalServices_MedicalServiceId",
                        column: x => x.MedicalServiceId,
                        principalTable: "MedicalServices",
                        principalColumn: "Id");
                });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "3e2c9b3b-1c5c-41a5-9fe6-9004dcd4b78b",
                column: "ConcurrencyStamp",
                value: "56880d40-1240-442a-becf-b975d387f737");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "6f4024b0-153e-4b8b-a851-5befbdb955f9",
                column: "ConcurrencyStamp",
                value: "b7656762-07de-438e-9e2f-eee9dbe58bf9");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "04a76057-948a-4fd1-b9f0-ed36991fcaa5",
                columns: new[] { "ConcurrencyStamp", "Created", "SecurityStamp" },
                values: new object[] { "91898b30-04bf-4863-a6ac-1ebbb5a04dcc", new DateTime(2022, 8, 24, 5, 14, 36, 238, DateTimeKind.Utc).AddTicks(879), "65dd680e-75fe-4827-8a86-7e6bd9aebf99" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "b0a39202-a221-47c7-9d34-dc4479ec33f2",
                columns: new[] { "ConcurrencyStamp", "Created", "SecurityStamp" },
                values: new object[] { "1f5c2949-b3cb-47c4-978b-88c5f99fa0da", new DateTime(2022, 8, 24, 5, 14, 36, 238, DateTimeKind.Utc).AddTicks(816), "f821514e-eeaf-41d3-8da0-0f63b8584e1e" });

            migrationBuilder.CreateIndex(
                name: "IX_MedicalCenterMedicalServices_MedicalServiceId",
                table: "MedicalCenterMedicalServices",
                column: "MedicalServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalServices_ParentId",
                table: "MedicalServices",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalServiceTranslations_LanguageId",
                table: "MedicalServiceTranslations",
                column: "LanguageId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MedicalCenterMedicalServices");

            migrationBuilder.DropTable(
                name: "MedicalServiceTranslations");

            migrationBuilder.DropTable(
                name: "MedicalServices");

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
    }
}
