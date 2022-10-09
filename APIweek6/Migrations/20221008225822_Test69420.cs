using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APIweek6.Migrations
{
    public partial class Test69420 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Gender = table.Column<int>(type: "INTEGER", nullable: false),
                    UserName = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "INTEGER", nullable: false),
                    PasswordHash = table.Column<string>(type: "TEXT", nullable: true),
                    SecurityStamp = table.Column<string>(type: "TEXT", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "TEXT", nullable: true),
                    PhoneNumber = table.Column<string>(type: "TEXT", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "INTEGER", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "INTEGER", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "TEXT", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "INTEGER", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Attractie",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    name = table.Column<string>(type: "TEXT", nullable: false),
                    spooky = table.Column<int>(type: "INTEGER", nullable: false),
                    buildYeaar = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attractie", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RoleId = table.Column<string>(type: "TEXT", nullable: false),
                    ClaimType = table.Column<string>(type: "TEXT", nullable: true),
                    ClaimValue = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    ClaimType = table.Column<string>(type: "TEXT", nullable: true),
                    ClaimValue = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "TEXT", nullable: false),
                    ProviderKey = table.Column<string>(type: "TEXT", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "TEXT", nullable: true),
                    UserId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    RoleId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    LoginProvider = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Value = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LikedAttractie",
                columns: table => new
                {
                    AttractieId = table.Column<int>(type: "INTEGER", nullable: false),
                    UserId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LikedAttractie", x => new { x.AttractieId, x.UserId });
                    table.ForeignKey(
                        name: "FK_LikedAttractie_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LikedAttractie_Attractie_AttractieId",
                        column: x => x.AttractieId,
                        principalTable: "Attractie",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "6c279b39-5a0e-4d52-b9c0-184c928dbc44", "fc4dcfe2-2bee-4dbe-bd7b-15ea9f3bfc60", "Gast", "GAST" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "9171e04d-0c10-421d-9efd-4cbbbee0eb69", "4cb24b93-8cbe-480a-8048-84e6e5b42df6", "Medewerker", "MEDEWERKER" });

            migrationBuilder.InsertData(
                table: "Attractie",
                columns: new[] { "Id", "buildYeaar", "name", "spooky" },
                values: new object[] { 1, new DateTime(2001, 11, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), "Reuzenrat", 5 });

            migrationBuilder.InsertData(
                table: "Attractie",
                columns: new[] { "Id", "buildYeaar", "name", "spooky" },
                values: new object[] { 2, new DateTime(2005, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Splash", 20 });

            migrationBuilder.InsertData(
                table: "Attractie",
                columns: new[] { "Id", "buildYeaar", "name", "spooky" },
                values: new object[] { 3, new DateTime(666, 1, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "Spookhuis", 60 });

            migrationBuilder.InsertData(
                table: "Attractie",
                columns: new[] { "Id", "buildYeaar", "name", "spooky" },
                values: new object[] { 4, new DateTime(2002, 2, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), "Airborne", 50 });

            migrationBuilder.InsertData(
                table: "Attractie",
                columns: new[] { "Id", "buildYeaar", "name", "spooky" },
                values: new object[] { 5, new DateTime(1999, 8, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "Babyflug", 5 });

            migrationBuilder.InsertData(
                table: "Attractie",
                columns: new[] { "Id", "buildYeaar", "name", "spooky" },
                values: new object[] { 6, new DateTime(1985, 9, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), "Draaimolen", 5 });

            migrationBuilder.InsertData(
                table: "Attractie",
                columns: new[] { "Id", "buildYeaar", "name", "spooky" },
                values: new object[] { 7, new DateTime(2008, 4, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "Huricane", 45 });

            migrationBuilder.InsertData(
                table: "Attractie",
                columns: new[] { "Id", "buildYeaar", "name", "spooky" },
                values: new object[] { 8, new DateTime(2019, 3, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), "Tea cups", 10 });

            migrationBuilder.InsertData(
                table: "Attractie",
                columns: new[] { "Id", "buildYeaar", "name", "spooky" },
                values: new object[] { 9, new DateTime(2020, 9, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Pusher", 80 });

            migrationBuilder.InsertData(
                table: "Attractie",
                columns: new[] { "Id", "buildYeaar", "name", "spooky" },
                values: new object[] { 10, new DateTime(1996, 11, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Rups", 20 });

            migrationBuilder.InsertData(
                table: "Attractie",
                columns: new[] { "Id", "buildYeaar", "name", "spooky" },
                values: new object[] { 11, new DateTime(1998, 1, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "Cake Walk", 0 });

            migrationBuilder.InsertData(
                table: "Attractie",
                columns: new[] { "Id", "buildYeaar", "name", "spooky" },
                values: new object[] { 12, new DateTime(2016, 4, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), "Toxic", 95 });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LikedAttractie_UserId",
                table: "LikedAttractie",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "LikedAttractie");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Attractie");
        }
    }
}
