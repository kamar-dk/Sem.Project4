using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FA_DB.Migrations
{
    /// <inheritdoc />
    public partial class Inital : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "server",
                columns: table => new
                {
                    ServerID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_server", x => x.ServerID);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordHash = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    Salt = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    ServerID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.Email);
                    table.ForeignKey(
                        name: "FK_users_server_ServerID",
                        column: x => x.ServerID,
                        principalTable: "server",
                        principalColumn: "ServerID");
                });

            migrationBuilder.CreateTable(
                name: "favoriteTraningPrograms",
                columns: table => new
                {
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_favoriteTraningPrograms", x => x.Email);
                    table.ForeignKey(
                        name: "FK_favoriteTraningPrograms_users_Email",
                        column: x => x.Email,
                        principalTable: "users",
                        principalColumn: "Email",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "trantingData",
                columns: table => new
                {
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_trantingData", x => x.Email);
                    table.ForeignKey(
                        name: "FK_trantingData_users_Email",
                        column: x => x.Email,
                        principalTable: "users",
                        principalColumn: "Email",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "userDatas",
                columns: table => new
                {
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Weight = table.Column<float>(type: "real", nullable: false),
                    Height = table.Column<float>(type: "real", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DoB = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_userDatas", x => x.Email);
                    table.ForeignKey(
                        name: "FK_userDatas_users_Email",
                        column: x => x.Email,
                        principalTable: "users",
                        principalColumn: "Email",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "traningPrograms",
                columns: table => new
                {
                    TraningProgramID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FavoriteTraningProgramsEmail = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ServerID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_traningPrograms", x => x.TraningProgramID);
                    table.ForeignKey(
                        name: "FK_traningPrograms_favoriteTraningPrograms_FavoriteTraningProgramsEmail",
                        column: x => x.FavoriteTraningProgramsEmail,
                        principalTable: "favoriteTraningPrograms",
                        principalColumn: "Email");
                    table.ForeignKey(
                        name: "FK_traningPrograms_server_ServerID",
                        column: x => x.ServerID,
                        principalTable: "server",
                        principalColumn: "ServerID");
                });

            migrationBuilder.CreateTable(
                name: "bikeSessions",
                columns: table => new
                {
                    SessionID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Durration = table.Column<float>(type: "real", nullable: false),
                    Distance = table.Column<float>(type: "real", nullable: false),
                    AvgSpeed = table.Column<float>(type: "real", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    traningDataEmail = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_bikeSessions", x => x.SessionID);
                    table.ForeignKey(
                        name: "FK_bikeSessions_trantingData_traningDataEmail",
                        column: x => x.traningDataEmail,
                        principalTable: "trantingData",
                        principalColumn: "Email",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "runningSessions",
                columns: table => new
                {
                    SessionID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Durration = table.Column<float>(type: "real", nullable: false),
                    Distance = table.Column<float>(type: "real", nullable: false),
                    AvgSpeed = table.Column<float>(type: "real", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    traningDataEmail = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_runningSessions", x => x.SessionID);
                    table.ForeignKey(
                        name: "FK_runningSessions_trantingData_traningDataEmail",
                        column: x => x.traningDataEmail,
                        principalTable: "trantingData",
                        principalColumn: "Email",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_bikeSessions_traningDataEmail",
                table: "bikeSessions",
                column: "traningDataEmail");

            migrationBuilder.CreateIndex(
                name: "IX_runningSessions_traningDataEmail",
                table: "runningSessions",
                column: "traningDataEmail");

            migrationBuilder.CreateIndex(
                name: "IX_traningPrograms_FavoriteTraningProgramsEmail",
                table: "traningPrograms",
                column: "FavoriteTraningProgramsEmail");

            migrationBuilder.CreateIndex(
                name: "IX_traningPrograms_ServerID",
                table: "traningPrograms",
                column: "ServerID");

            migrationBuilder.CreateIndex(
                name: "IX_users_ServerID",
                table: "users",
                column: "ServerID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "bikeSessions");

            migrationBuilder.DropTable(
                name: "runningSessions");

            migrationBuilder.DropTable(
                name: "traningPrograms");

            migrationBuilder.DropTable(
                name: "userDatas");

            migrationBuilder.DropTable(
                name: "trantingData");

            migrationBuilder.DropTable(
                name: "favoriteTraningPrograms");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "server");
        }
    }
}
