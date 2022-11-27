using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Model.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Name = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NormalizedName = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ConcurrencyStamp = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UserName = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NormalizedUserName = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NormalizedEmail = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    EmailConfirmed = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    PasswordHash = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SecurityStamp = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ConcurrencyStamp = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PhoneNumber = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PhoneNumberConfirmed = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "SETTINGS",
                columns: table => new
                {
                    STANDARD_VICTORY = table.Column<int>(type: "int", nullable: false),
                    TOTAL_VICTORY = table.Column<int>(type: "int", nullable: false),
                    DICE_MODE = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "UNITS_BT",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    MOVEMENT = table.Column<int>(type: "int", nullable: false),
                    UNIT_TYPE = table.Column<string>(type: "VARCHAR(45)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UNITS_BT", x => x.ID);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    RoleId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ClaimType = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ClaimValue = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
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
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ClaimType = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ClaimValue = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
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
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ProviderKey = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ProviderDisplayName = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UserId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
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
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    RoleId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
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
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LoginProvider = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Name = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Value = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
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
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "NATIONS",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    NAME = table.Column<string>(type: "VARCHAR(45)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TREASURY = table.Column<int>(type: "int", nullable: false),
                    PLAYER_ID = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NATIONS", x => x.ID);
                    table.ForeignKey(
                        name: "FK_NATIONS_AspNetUsers_PLAYER_ID",
                        column: x => x.PLAYER_ID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "REGIONS_BT",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    NAME = table.Column<string>(type: "VARCHAR(45)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    HAS_LANDING_STRIP = table.Column<sbyte>(type: "TINYINT", nullable: false),
                    OWNER_ID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_REGIONS_BT", x => x.ID);
                    table.ForeignKey(
                        name: "FK_REGIONS_BT_NATIONS_OWNER_ID",
                        column: x => x.OWNER_ID,
                        principalTable: "NATIONS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "LAND_REGION",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false),
                    INCOME = table.Column<int>(type: "int", nullable: false),
                    IS_CAPITAL = table.Column<sbyte>(type: "TINYINT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LAND_REGION", x => x.ID);
                    table.ForeignKey(
                        name: "FK_LAND_REGION_REGIONS_BT_ID",
                        column: x => x.ID,
                        principalTable: "REGIONS_BT",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "REGION_HAS_NEIGHBOURS_JT",
                columns: table => new
                {
                    REGION_ID = table.Column<int>(type: "int", nullable: false),
                    NEIGHBOUR_ID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_REGION_HAS_NEIGHBOURS_JT", x => new { x.NEIGHBOUR_ID, x.REGION_ID });
                    table.ForeignKey(
                        name: "FK_REGION_HAS_NEIGHBOURS_JT_REGIONS_BT_NEIGHBOUR_ID",
                        column: x => x.NEIGHBOUR_ID,
                        principalTable: "REGIONS_BT",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_REGION_HAS_NEIGHBOURS_JT_REGIONS_BT_REGION_ID",
                        column: x => x.REGION_ID,
                        principalTable: "REGIONS_BT",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "WATER_REGIONS",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WATER_REGIONS", x => x.ID);
                    table.ForeignKey(
                        name: "FK_WATER_REGIONS_REGIONS_BT_ID",
                        column: x => x.ID,
                        principalTable: "REGIONS_BT",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "FACTORIES",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    DAMAGE = table.Column<int>(type: "int", nullable: false),
                    REGION_ID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FACTORIES", x => x.ID);
                    table.ForeignKey(
                        name: "FK_FACTORIES_LAND_REGION_REGION_ID",
                        column: x => x.REGION_ID,
                        principalTable: "LAND_REGION",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "SHIPS_BT",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false),
                    LOCATION_ID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SHIPS_BT", x => x.ID);
                    table.ForeignKey(
                        name: "FK_SHIPS_BT_UNITS_BT_ID",
                        column: x => x.ID,
                        principalTable: "UNITS_BT",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SHIPS_BT_WATER_REGIONS_LOCATION_ID",
                        column: x => x.LOCATION_ID,
                        principalTable: "WATER_REGIONS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AIRCRAFT_CARRIER",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AIRCRAFT_CARRIER", x => x.ID);
                    table.ForeignKey(
                        name: "FK_AIRCRAFT_CARRIER_SHIPS_BT_ID",
                        column: x => x.ID,
                        principalTable: "SHIPS_BT",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "TRANSPORTER",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TRANSPORTER", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TRANSPORTER_SHIPS_BT_ID",
                        column: x => x.ID,
                        principalTable: "SHIPS_BT",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "PLANES",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false),
                    LOCATION_ID = table.Column<int>(type: "int", nullable: false),
                    AIRCRAFT_CARRIER_ID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PLANES", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PLANES_AIRCRAFT_CARRIER_AIRCRAFT_CARRIER_ID",
                        column: x => x.AIRCRAFT_CARRIER_ID,
                        principalTable: "AIRCRAFT_CARRIER",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_PLANES_REGIONS_BT_LOCATION_ID",
                        column: x => x.LOCATION_ID,
                        principalTable: "REGIONS_BT",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PLANES_UNITS_BT_ID",
                        column: x => x.ID,
                        principalTable: "UNITS_BT",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "LAND_UNITS",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false),
                    LOCATION_ID = table.Column<int>(type: "int", nullable: false),
                    TRANSPORT_ID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LAND_UNITS", x => x.ID);
                    table.ForeignKey(
                        name: "FK_LAND_UNITS_LAND_REGION_LOCATION_ID",
                        column: x => x.LOCATION_ID,
                        principalTable: "LAND_REGION",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LAND_UNITS_TRANSPORTER_TRANSPORT_ID",
                        column: x => x.TRANSPORT_ID,
                        principalTable: "TRANSPORTER",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_LAND_UNITS_UNITS_BT_ID",
                        column: x => x.ID,
                        principalTable: "UNITS_BT",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

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
                name: "IX_AspNetUsers_UserName_Email",
                table: "AspNetUsers",
                columns: new[] { "UserName", "Email" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FACTORIES_REGION_ID",
                table: "FACTORIES",
                column: "REGION_ID");

            migrationBuilder.CreateIndex(
                name: "IX_LAND_UNITS_LOCATION_ID",
                table: "LAND_UNITS",
                column: "LOCATION_ID");

            migrationBuilder.CreateIndex(
                name: "IX_LAND_UNITS_TRANSPORT_ID",
                table: "LAND_UNITS",
                column: "TRANSPORT_ID");

            migrationBuilder.CreateIndex(
                name: "IX_NATIONS_PLAYER_ID",
                table: "NATIONS",
                column: "PLAYER_ID");

            migrationBuilder.CreateIndex(
                name: "IX_PLANES_AIRCRAFT_CARRIER_ID",
                table: "PLANES",
                column: "AIRCRAFT_CARRIER_ID");

            migrationBuilder.CreateIndex(
                name: "IX_PLANES_LOCATION_ID",
                table: "PLANES",
                column: "LOCATION_ID");

            migrationBuilder.CreateIndex(
                name: "IX_REGION_HAS_NEIGHBOURS_JT_REGION_ID",
                table: "REGION_HAS_NEIGHBOURS_JT",
                column: "REGION_ID");

            migrationBuilder.CreateIndex(
                name: "IX_REGIONS_BT_OWNER_ID",
                table: "REGIONS_BT",
                column: "OWNER_ID");

            migrationBuilder.CreateIndex(
                name: "IX_SHIPS_BT_LOCATION_ID",
                table: "SHIPS_BT",
                column: "LOCATION_ID");
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
                name: "FACTORIES");

            migrationBuilder.DropTable(
                name: "LAND_UNITS");

            migrationBuilder.DropTable(
                name: "PLANES");

            migrationBuilder.DropTable(
                name: "REGION_HAS_NEIGHBOURS_JT");

            migrationBuilder.DropTable(
                name: "SETTINGS");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "LAND_REGION");

            migrationBuilder.DropTable(
                name: "TRANSPORTER");

            migrationBuilder.DropTable(
                name: "AIRCRAFT_CARRIER");

            migrationBuilder.DropTable(
                name: "SHIPS_BT");

            migrationBuilder.DropTable(
                name: "UNITS_BT");

            migrationBuilder.DropTable(
                name: "WATER_REGIONS");

            migrationBuilder.DropTable(
                name: "REGIONS_BT");

            migrationBuilder.DropTable(
                name: "NATIONS");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
