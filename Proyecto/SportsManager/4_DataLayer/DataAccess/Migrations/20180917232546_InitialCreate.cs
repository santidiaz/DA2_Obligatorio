using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Sports",
                columns: table => new
                {
                    SportOID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sports", x => x.SportOID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserOID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    UserName = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    IsAdmin = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserOID);
                });

            migrationBuilder.CreateTable(
                name: "Teams",
                columns: table => new
                {
                    TeamOID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Photo = table.Column<byte[]>(nullable: true),
                    SportOID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teams", x => x.TeamOID);
                    table.ForeignKey(
                        name: "FK_Teams_Sports_SportOID",
                        column: x => x.SportOID,
                        principalTable: "Sports",
                        principalColumn: "SportOID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    EventOID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Date = table.Column<DateTime>(nullable: false),
                    SportOID = table.Column<int>(nullable: true),
                    Team1TeamOID = table.Column<int>(nullable: true),
                    Team2TeamOID = table.Column<int>(nullable: true),
                    Comments = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.EventOID);
                    table.ForeignKey(
                        name: "FK_Events_Sports_SportOID",
                        column: x => x.SportOID,
                        principalTable: "Sports",
                        principalColumn: "SportOID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Events_Teams_Team1TeamOID",
                        column: x => x.Team1TeamOID,
                        principalTable: "Teams",
                        principalColumn: "TeamOID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Events_Teams_Team2TeamOID",
                        column: x => x.Team2TeamOID,
                        principalTable: "Teams",
                        principalColumn: "TeamOID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Events_SportOID",
                table: "Events",
                column: "SportOID");

            migrationBuilder.CreateIndex(
                name: "IX_Events_Team1TeamOID",
                table: "Events",
                column: "Team1TeamOID");

            migrationBuilder.CreateIndex(
                name: "IX_Events_Team2TeamOID",
                table: "Events",
                column: "Team2TeamOID");

            migrationBuilder.CreateIndex(
                name: "IX_Teams_SportOID",
                table: "Teams",
                column: "SportOID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Events");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Teams");

            migrationBuilder.DropTable(
                name: "Sports");
        }
    }
}
