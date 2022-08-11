﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Persistence;

#nullable disable

namespace Persistence.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Domain.Entities.Destination.City", b =>
                {
                    b.Property<short>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("smallint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<short>("Id"), 1L, 1);

                    b.Property<short>("CountryId")
                        .HasColumnType("smallint");

                    b.Property<string>("EnName")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("varchar(30)");

                    b.Property<string>("FaName")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("Id");

                    b.HasIndex("CountryId");

                    b.ToTable("Cities");

                    b.HasData(
                        new
                        {
                            Id = (short)1,
                            CountryId = (short)1,
                            EnName = "Tehran",
                            FaName = "تهران"
                        },
                        new
                        {
                            Id = (short)2,
                            CountryId = (short)1,
                            EnName = "Mashhad",
                            FaName = "مشهد"
                        });
                });

            modelBuilder.Entity("Domain.Entities.Destination.Country", b =>
                {
                    b.Property<short>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("smallint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<short>("Id"), 1L, 1);

                    b.Property<string>("EnName")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("varchar(30)");

                    b.Property<string>("FaName")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("Id");

                    b.ToTable("Countries");

                    b.HasData(
                        new
                        {
                            Id = (short)1,
                            EnName = "Iran",
                            FaName = "ایران"
                        });
                });

            modelBuilder.Entity("Domain.Entities.Hotels.Hotel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("CallInformation")
                        .HasMaxLength(300)
                        .HasColumnType("varchar(300)");

                    b.Property<short>("CityId")
                        .HasColumnType("smallint");

                    b.Property<DateTime>("Created")
                        .HasColumnType("smalldatetime");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("DeletedBy")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<DateTime?>("DeletedOn")
                        .HasColumnType("smalldatetime");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("smalldatetime");

                    b.Property<string>("LastModifiedBy")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<double>("Lat")
                        .HasColumnType("float");

                    b.Property<double>("Lng")
                        .HasColumnType("float");

                    b.Property<byte>("Stars")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("tinyint")
                        .HasDefaultValue((byte)0);

                    b.Property<string>("WebsiteAddress")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<byte>("Zoom")
                        .HasColumnType("tinyint");

                    b.HasKey("Id");

                    b.HasIndex("CityId");

                    b.ToTable("Hotels");
                });

            modelBuilder.Entity("Domain.Entities.Hotels.HotelFacility", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("ParentId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ParentId");

                    b.ToTable("HotelFacilities");
                });

            modelBuilder.Entity("Domain.Entities.Hotels.HotelImage", b =>
                {
                    b.Property<string>("ImageUrl")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("HotelId")
                        .HasColumnType("int");

                    b.Property<string>("ImageAlt")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)");

                    b.HasKey("ImageUrl");

                    b.HasIndex("HotelId");

                    b.ToTable("HotelImages");
                });

            modelBuilder.Entity("Domain.Entities.Hotels.HotelSelectedFacility", b =>
                {
                    b.Property<int>("HotelId")
                        .HasColumnType("int");

                    b.Property<int>("HotelFacilityId")
                        .HasColumnType("int");

                    b.HasKey("HotelId", "HotelFacilityId");

                    b.HasIndex("HotelFacilityId");

                    b.ToTable("HotelSelectedFacilities");
                });

            modelBuilder.Entity("Domain.Entities.Hotels.Translation.HotelFacilityTranslation", b =>
                {
                    b.Property<int>("FacilityId")
                        .HasColumnType("int");

                    b.Property<byte>("LanguageId")
                        .HasColumnType("tinyint");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("FacilityId", "LanguageId");

                    b.HasIndex("LanguageId");

                    b.ToTable("HotelFacilityTranslations");
                });

            modelBuilder.Entity("Domain.Entities.Hotels.Translation.HotelTranslation", b =>
                {
                    b.Property<int>("HotelId")
                        .HasColumnType("int");

                    b.Property<byte>("LanguageId")
                        .HasColumnType("tinyint");

                    b.Property<string>("About")
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<string>("Address")
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("HotelId", "LanguageId");

                    b.HasIndex("LanguageId");

                    b.ToTable("HotelTranslations");
                });

            modelBuilder.Entity("Domain.Entities.Identity.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(40)
                        .HasColumnType("varchar(40)");

                    b.Property<byte>("AccessFailedCount")
                        .HasColumnType("tinyint");

                    b.Property<DateTime?>("BirthDay")
                        .HasColumnType("datetime2");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.Property<DateTime>("Created")
                        .HasColumnType("smalldatetime");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("DeletedBy")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<DateTime?>("DeletedOn")
                        .HasColumnType("smalldatetime");

                    b.Property<string>("Email")
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("Family")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<bool?>("Gender")
                        .HasColumnType("bit");

                    b.Property<string>("IpAddress")
                        .HasMaxLength(16)
                        .HasColumnType("varchar(16)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<bool>("IsOperator")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("smalldatetime");

                    b.Property<string>("LastModifiedBy")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("PasswordHash")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("PhoneNumber")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("ProfilePicture")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("SecurityStamp")
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("Users", (string)null);

                    b.HasData(
                        new
                        {
                            Id = "b0a39202-a221-47c7-9d34-dc4479ec33f2",
                            AccessFailedCount = (byte)0,
                            ConcurrencyStamp = "5c89d9d7-8749-442b-84bd-384dffa6f470",
                            Created = new DateTime(2022, 8, 11, 9, 53, 59, 940, DateTimeKind.Utc).AddTicks(7230),
                            CreatedBy = "Seed",
                            Email = "hatef@tabanmed.com",
                            EmailConfirmed = false,
                            Family = "شمشاد",
                            Gender = true,
                            IsDeleted = false,
                            IsOperator = true,
                            LockoutEnabled = true,
                            Name = "محمد هاتف",
                            NormalizedEmail = "HATEF@TABANMED.COM",
                            NormalizedUserName = "HATEFADMIN",
                            PasswordHash = "AQAAAAEAACcQAAAAEDCEjgFnVqs3jS+KYwhsCsNHoR7mV7tQ7/NUHc2bxUc9HjMuXSNCax/I5jPdFBGsVg==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "31ca9a74-8f93-4cc6-8e94-7fee2175f967",
                            TwoFactorEnabled = false,
                            UserName = "HatefAdmin"
                        },
                        new
                        {
                            Id = "04a76057-948a-4fd1-b9f0-ed36991fcaa5",
                            AccessFailedCount = (byte)0,
                            ConcurrencyStamp = "7a1699bd-9956-40e6-af15-d2d1991fe7c0",
                            Created = new DateTime(2022, 8, 11, 9, 53, 59, 940, DateTimeKind.Utc).AddTicks(7270),
                            CreatedBy = "Seed",
                            Email = "operator@tabanmed.com",
                            EmailConfirmed = false,
                            Family = "تابان",
                            IsDeleted = false,
                            IsOperator = true,
                            LockoutEnabled = true,
                            Name = "اپراتور",
                            NormalizedEmail = "OPERATOR@TABANMED.COM",
                            NormalizedUserName = "TABANMEDOPERATOR",
                            PasswordHash = "AQAAAAEAACcQAAAAEGO2+kmYpAenNWk5p1UYgYOMbU3/pUOoc4yRkUma3Zq2Hsc8g9HSWpztF3MozgJdig==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "877510d4-bb08-444a-a0eb-9b39648a6de6",
                            TwoFactorEnabled = false,
                            UserName = "tabanmedOperator"
                        });
                });

            modelBuilder.Entity("Domain.Entities.Identity.Role", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(40)
                        .HasColumnType("varchar(40)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DisplayName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("Roles", (string)null);

                    b.HasData(
                        new
                        {
                            Id = "3e2c9b3b-1c5c-41a5-9fe6-9004dcd4b78b",
                            ConcurrencyStamp = "d2b997f7-b624-4da8-b5a4-50062162a1a5",
                            DisplayName = "ادمین کل سیستم",
                            Name = "Administrator",
                            NormalizedName = "ADMINISTRATOR"
                        },
                        new
                        {
                            Id = "6f4024b0-153e-4b8b-a851-5befbdb955f9",
                            ConcurrencyStamp = "7e0c4ce5-6aa9-47bb-b18c-4b6fb21f32fc",
                            DisplayName = "اپراتور سیستم",
                            Name = "SystemOperator",
                            NormalizedName = "SYSTEMOPERATOR"
                        });
                });

            modelBuilder.Entity("Domain.Entities.Identity.RoleClaim", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("varchar(40)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("RoleClaims", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.Identity.UserClaim", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("varchar(40)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserClaims", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.Identity.UserLogin", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("varchar(40)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("UserLogins", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.Identity.UserRole", b =>
                {
                    b.Property<string>("UserId")
                        .HasMaxLength(40)
                        .HasColumnType("varchar(40)");

                    b.Property<string>("RoleId")
                        .HasColumnType("varchar(40)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("UserRoles", (string)null);

                    b.HasData(
                        new
                        {
                            UserId = "b0a39202-a221-47c7-9d34-dc4479ec33f2",
                            RoleId = "3e2c9b3b-1c5c-41a5-9fe6-9004dcd4b78b"
                        },
                        new
                        {
                            UserId = "04a76057-948a-4fd1-b9f0-ed36991fcaa5",
                            RoleId = "6f4024b0-153e-4b8b-a851-5befbdb955f9"
                        });
                });

            modelBuilder.Entity("Domain.Entities.Identity.UserToken", b =>
                {
                    b.Property<string>("UserId")
                        .HasMaxLength(40)
                        .HasColumnType("varchar(40)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("UserTokens", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.Localization.Language", b =>
                {
                    b.Property<byte>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("tinyint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<byte>("Id"), 1L, 1);

                    b.Property<string>("IsoName")
                        .IsRequired()
                        .HasMaxLength(7)
                        .HasColumnType("varchar(7)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.HasKey("Id");

                    b.ToTable("Languages");

                    b.HasData(
                        new
                        {
                            Id = (byte)1,
                            IsoName = "fa-IR",
                            Name = "فارسی"
                        },
                        new
                        {
                            Id = (byte)2,
                            IsoName = "en-US",
                            Name = "English"
                        },
                        new
                        {
                            Id = (byte)3,
                            IsoName = "ar-IQ",
                            Name = "العربیة"
                        },
                        new
                        {
                            Id = (byte)4,
                            IsoName = "prs-AF",
                            Name = "پشتو/دری"
                        });
                });

            modelBuilder.Entity("Domain.Entities.Permission.Permission", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Claim")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("varchar(80)");

                    b.Property<string>("DisplayText")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Permission");
                });

            modelBuilder.Entity("Domain.Entities.Destination.City", b =>
                {
                    b.HasOne("Domain.Entities.Destination.Country", "Country")
                        .WithMany("Cities")
                        .HasForeignKey("CountryId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Country");
                });

            modelBuilder.Entity("Domain.Entities.Hotels.Hotel", b =>
                {
                    b.HasOne("Domain.Entities.Destination.City", "City")
                        .WithMany("Hotels")
                        .HasForeignKey("CityId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("City");
                });

            modelBuilder.Entity("Domain.Entities.Hotels.HotelFacility", b =>
                {
                    b.HasOne("Domain.Entities.Hotels.HotelFacility", "Parent")
                        .WithMany("Children")
                        .HasForeignKey("ParentId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.Navigation("Parent");
                });

            modelBuilder.Entity("Domain.Entities.Hotels.HotelImage", b =>
                {
                    b.HasOne("Domain.Entities.Hotels.Hotel", "Hotel")
                        .WithMany("Gallery")
                        .HasForeignKey("HotelId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Hotel");
                });

            modelBuilder.Entity("Domain.Entities.Hotels.HotelSelectedFacility", b =>
                {
                    b.HasOne("Domain.Entities.Hotels.HotelFacility", "HotelFacility")
                        .WithMany("HotelSelectedFacilities")
                        .HasForeignKey("HotelFacilityId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Domain.Entities.Hotels.Hotel", "Hotel")
                        .WithMany("HotelSelectedFacilities")
                        .HasForeignKey("HotelId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Hotel");

                    b.Navigation("HotelFacility");
                });

            modelBuilder.Entity("Domain.Entities.Hotels.Translation.HotelFacilityTranslation", b =>
                {
                    b.HasOne("Domain.Entities.Hotels.HotelFacility", "HotelFacility")
                        .WithMany("HotelFacilityTranslations")
                        .HasForeignKey("FacilityId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Domain.Entities.Localization.Language", "Language")
                        .WithMany("HotelFacilityTranslations")
                        .HasForeignKey("LanguageId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("HotelFacility");

                    b.Navigation("Language");
                });

            modelBuilder.Entity("Domain.Entities.Hotels.Translation.HotelTranslation", b =>
                {
                    b.HasOne("Domain.Entities.Hotels.Hotel", "Hotel")
                        .WithMany("HotelTranslations")
                        .HasForeignKey("HotelId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Domain.Entities.Localization.Language", "Language")
                        .WithMany("HotelTranslations")
                        .HasForeignKey("LanguageId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Hotel");

                    b.Navigation("Language");
                });

            modelBuilder.Entity("Domain.Entities.Identity.RoleClaim", b =>
                {
                    b.HasOne("Domain.Entities.Identity.Role", "Role")
                        .WithMany("Claims")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("Domain.Entities.Identity.UserClaim", b =>
                {
                    b.HasOne("Domain.Entities.Identity.ApplicationUser", "User")
                        .WithMany("Claims")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Domain.Entities.Identity.UserLogin", b =>
                {
                    b.HasOne("Domain.Entities.Identity.ApplicationUser", "User")
                        .WithMany("Logins")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Domain.Entities.Identity.UserRole", b =>
                {
                    b.HasOne("Domain.Entities.Identity.Role", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.Identity.ApplicationUser", "User")
                        .WithMany("Roles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Domain.Entities.Identity.UserToken", b =>
                {
                    b.HasOne("Domain.Entities.Identity.ApplicationUser", "User")
                        .WithMany("Tokens")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Domain.Entities.Destination.City", b =>
                {
                    b.Navigation("Hotels");
                });

            modelBuilder.Entity("Domain.Entities.Destination.Country", b =>
                {
                    b.Navigation("Cities");
                });

            modelBuilder.Entity("Domain.Entities.Hotels.Hotel", b =>
                {
                    b.Navigation("Gallery");

                    b.Navigation("HotelSelectedFacilities");

                    b.Navigation("HotelTranslations");
                });

            modelBuilder.Entity("Domain.Entities.Hotels.HotelFacility", b =>
                {
                    b.Navigation("Children");

                    b.Navigation("HotelFacilityTranslations");

                    b.Navigation("HotelSelectedFacilities");
                });

            modelBuilder.Entity("Domain.Entities.Identity.ApplicationUser", b =>
                {
                    b.Navigation("Claims");

                    b.Navigation("Logins");

                    b.Navigation("Roles");

                    b.Navigation("Tokens");
                });

            modelBuilder.Entity("Domain.Entities.Identity.Role", b =>
                {
                    b.Navigation("Claims");

                    b.Navigation("Users");
                });

            modelBuilder.Entity("Domain.Entities.Localization.Language", b =>
                {
                    b.Navigation("HotelFacilityTranslations");

                    b.Navigation("HotelTranslations");
                });
#pragma warning restore 612, 618
        }
    }
}
