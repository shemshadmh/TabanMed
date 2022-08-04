using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    public partial class TabanMed_20220803_122012 : Migration
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
                name: "HotelFacilities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ParentId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HotelFacilities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HotelFacilities_HotelFacilities_ParentId",
                        column: x => x.ParentId,
                        principalTable: "HotelFacilities",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Languages",
                columns: table => new
                {
                    Id = table.Column<byte>(type: "tinyint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    IsoName = table.Column<string>(type: "varchar(7)", maxLength: 7, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Languages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Permission",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Claim = table.Column<string>(type: "varchar(80)", maxLength: 80, nullable: false),
                    DisplayText = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permission", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(40)", maxLength: 40, nullable: false),
                    DisplayName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(40)", maxLength: 40, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Family = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Gender = table.Column<bool>(type: "bit", nullable: true),
                    BirthDay = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsOperator = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    ProfilePicture = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    IpAddress = table.Column<string>(type: "varchar(16)", maxLength: 16, nullable: true),
                    Created = table.Column<DateTime>(type: "smalldatetime", nullable: false),
                    CreatedBy = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    LastModified = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    DeletedBy = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<byte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
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

            migrationBuilder.CreateTable(
                name: "HotelFacilityTranslations",
                columns: table => new
                {
                    FacilityId = table.Column<int>(type: "int", nullable: false),
                    LanguageId = table.Column<byte>(type: "tinyint", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HotelFacilityTranslations", x => new { x.FacilityId, x.LanguageId });
                    table.ForeignKey(
                        name: "FK_HotelFacilityTranslations_HotelFacilities_FacilityId",
                        column: x => x.FacilityId,
                        principalTable: "HotelFacilities",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_HotelFacilityTranslations_Languages_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Languages",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "varchar(40)", maxLength: 40, nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoleClaims_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "varchar(40)", maxLength: 40, nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserClaims_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "varchar(40)", maxLength: 40, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_UserLogins_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "varchar(40)", maxLength: 40, nullable: false),
                    RoleId = table.Column<string>(type: "varchar(40)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_UserRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoles_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "varchar(40)", maxLength: 40, nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_UserTokens_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Hotels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FaName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    EnName = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    ImageUrl = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    About = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    Stars = table.Column<byte>(type: "tinyint", nullable: false, defaultValue: (byte)0),
                    Address = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    CallInformation = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    WebsiteAddress = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Lat = table.Column<double>(type: "float", nullable: false),
                    Lng = table.Column<double>(type: "float", nullable: false),
                    Zoom = table.Column<byte>(type: "tinyint", nullable: false),
                    CityId = table.Column<short>(type: "smallint", nullable: false),
                    CreatedBy = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    Created = table.Column<DateTime>(type: "smalldatetime", nullable: false),
                    LastModified = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    DeletedBy = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hotels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Hotels_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "HotelImages",
                columns: table => new
                {
                    ImageUrl = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ImageAlt = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false),
                    HotelId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HotelImages", x => x.ImageUrl);
                    table.ForeignKey(
                        name: "FK_HotelImages_Hotels_HotelId",
                        column: x => x.HotelId,
                        principalTable: "Hotels",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "HotelSelectedFacilities",
                columns: table => new
                {
                    HotelId = table.Column<int>(type: "int", nullable: false),
                    HotelFacilityId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HotelSelectedFacilities", x => new { x.HotelId, x.HotelFacilityId });
                    table.ForeignKey(
                        name: "FK_HotelSelectedFacilities_HotelFacilities_HotelFacilityId",
                        column: x => x.HotelFacilityId,
                        principalTable: "HotelFacilities",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_HotelSelectedFacilities_Hotels_HotelId",
                        column: x => x.HotelId,
                        principalTable: "Hotels",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "EnName", "FaName" },
                values: new object[] { (short)1, "Iran", "ایران" });

            migrationBuilder.InsertData(
                table: "Languages",
                columns: new[] { "Id", "IsoName", "Name" },
                values: new object[,]
                {
                    { (byte)1, "fa-IR", "فارسی" },
                    { (byte)2, "en-US", "English" },
                    { (byte)3, "ar-IQ", "العربیة" },
                    { (byte)4, "prs-AF", "پشتو/دری" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "DisplayName", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "3e2c9b3b-1c5c-41a5-9fe6-9004dcd4b78b", "07a8570b-f9a0-4c68-94e3-c60bfff7f732", "ادمین کل سیستم", "Administrator", "ADMINISTRATOR" },
                    { "6f4024b0-153e-4b8b-a851-5befbdb955f9", "83e8305d-0704-445c-9401-07c0342b915f", "اپراتور سیستم", "SystemOperator", "SYSTEMOPERATOR" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "BirthDay", "ConcurrencyStamp", "Created", "CreatedBy", "DeletedBy", "DeletedOn", "Email", "EmailConfirmed", "Family", "Gender", "IpAddress", "IsDeleted", "IsOperator", "LastModified", "LastModifiedBy", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "ProfilePicture", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "04a76057-948a-4fd1-b9f0-ed36991fcaa5", (byte)0, null, "3574a14e-d2da-4df5-84b0-e8ad540dab00", new DateTime(2022, 8, 3, 7, 50, 20, 156, DateTimeKind.Utc).AddTicks(5156), "Seed", null, null, "operator@tabanmed.com", false, "تابان", null, null, false, true, null, null, true, null, "اپراتور", "OPERATOR@TABANMED.COM", "TABANMEDOPERATOR", "AQAAAAEAACcQAAAAEGO2+kmYpAenNWk5p1UYgYOMbU3/pUOoc4yRkUma3Zq2Hsc8g9HSWpztF3MozgJdig==", null, false, null, "d337cea1-c079-4d7c-a19b-ec4d43d12ef9", false, "tabanmedOperator" },
                    { "b0a39202-a221-47c7-9d34-dc4479ec33f2", (byte)0, null, "e6f86475-664e-4925-8120-3d2bf148021f", new DateTime(2022, 8, 3, 7, 50, 20, 156, DateTimeKind.Utc).AddTicks(5125), "Seed", null, null, "hatef@tabanmed.com", false, "شمشاد", true, null, false, true, null, null, true, null, "محمد هاتف", "HATEF@TABANMED.COM", "HATEFADMIN", "AQAAAAEAACcQAAAAEDCEjgFnVqs3jS+KYwhsCsNHoR7mV7tQ7/NUHc2bxUc9HjMuXSNCax/I5jPdFBGsVg==", null, false, null, "7391ce6f-09bb-46d1-8efd-0263470105bd", false, "HatefAdmin" }
                });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "Id", "CountryId", "EnName", "FaName" },
                values: new object[,]
                {
                    { (short)1, (short)1, "Tehran", "تهران" },
                    { (short)2, (short)1, "Mashhad", "مشهد" }
                });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "6f4024b0-153e-4b8b-a851-5befbdb955f9", "04a76057-948a-4fd1-b9f0-ed36991fcaa5" },
                    { "3e2c9b3b-1c5c-41a5-9fe6-9004dcd4b78b", "b0a39202-a221-47c7-9d34-dc4479ec33f2" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cities_CountryId",
                table: "Cities",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_HotelFacilities_ParentId",
                table: "HotelFacilities",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_HotelFacilityTranslations_LanguageId",
                table: "HotelFacilityTranslations",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_HotelImages_HotelId",
                table: "HotelImages",
                column: "HotelId");

            migrationBuilder.CreateIndex(
                name: "IX_Hotels_CityId",
                table: "Hotels",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_HotelSelectedFacilities_HotelFacilityId",
                table: "HotelSelectedFacilities",
                column: "HotelFacilityId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleClaims_RoleId",
                table: "RoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "Roles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_UserClaims_UserId",
                table: "UserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserLogins_UserId",
                table: "UserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RoleId",
                table: "UserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "Users",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "Users",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HotelFacilityTranslations");

            migrationBuilder.DropTable(
                name: "HotelImages");

            migrationBuilder.DropTable(
                name: "HotelSelectedFacilities");

            migrationBuilder.DropTable(
                name: "Permission");

            migrationBuilder.DropTable(
                name: "RoleClaims");

            migrationBuilder.DropTable(
                name: "UserClaims");

            migrationBuilder.DropTable(
                name: "UserLogins");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "UserTokens");

            migrationBuilder.DropTable(
                name: "Languages");

            migrationBuilder.DropTable(
                name: "HotelFacilities");

            migrationBuilder.DropTable(
                name: "Hotels");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Cities");

            migrationBuilder.DropTable(
                name: "Countries");
        }
    }
}
