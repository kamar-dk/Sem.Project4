using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FA_DB.Migrations
{
    /// <inheritdoc />
    public partial class UserWeight : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_bikeSessions_trantingData_traningDataEmail",
                table: "bikeSessions");

            migrationBuilder.DropForeignKey(
                name: "FK_runningSessions_trantingData_traningDataEmail",
                table: "runningSessions");

            migrationBuilder.DropForeignKey(
                name: "FK_trantingData_users_Email",
                table: "trantingData");

            migrationBuilder.DropPrimaryKey(
                name: "PK_trantingData",
                table: "trantingData");

            migrationBuilder.DropColumn(
                name: "Weight",
                table: "userDatas");

            migrationBuilder.RenameTable(
                name: "trantingData",
                newName: "traningData");

            migrationBuilder.AddPrimaryKey(
                name: "PK_traningData",
                table: "traningData",
                column: "Email");

            migrationBuilder.CreateTable(
                name: "UserWeights",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Weight = table.Column<float>(type: "real", nullable: false),
                    date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserDataEmail = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserWeights", x => x.ID);
                    table.ForeignKey(
                        name: "FK_UserWeights_userDatas_UserDataEmail",
                        column: x => x.UserDataEmail,
                        principalTable: "userDatas",
                        principalColumn: "Email",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserWeights_UserDataEmail",
                table: "UserWeights",
                column: "UserDataEmail");

            migrationBuilder.AddForeignKey(
                name: "FK_bikeSessions_traningData_traningDataEmail",
                table: "bikeSessions",
                column: "traningDataEmail",
                principalTable: "traningData",
                principalColumn: "Email",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_runningSessions_traningData_traningDataEmail",
                table: "runningSessions",
                column: "traningDataEmail",
                principalTable: "traningData",
                principalColumn: "Email",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_traningData_users_Email",
                table: "traningData",
                column: "Email",
                principalTable: "users",
                principalColumn: "Email",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_bikeSessions_traningData_traningDataEmail",
                table: "bikeSessions");

            migrationBuilder.DropForeignKey(
                name: "FK_runningSessions_traningData_traningDataEmail",
                table: "runningSessions");

            migrationBuilder.DropForeignKey(
                name: "FK_traningData_users_Email",
                table: "traningData");

            migrationBuilder.DropTable(
                name: "UserWeights");

            migrationBuilder.DropPrimaryKey(
                name: "PK_traningData",
                table: "traningData");

            migrationBuilder.RenameTable(
                name: "traningData",
                newName: "trantingData");

            migrationBuilder.AddColumn<float>(
                name: "Weight",
                table: "userDatas",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddPrimaryKey(
                name: "PK_trantingData",
                table: "trantingData",
                column: "Email");

            migrationBuilder.AddForeignKey(
                name: "FK_bikeSessions_trantingData_traningDataEmail",
                table: "bikeSessions",
                column: "traningDataEmail",
                principalTable: "trantingData",
                principalColumn: "Email",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_runningSessions_trantingData_traningDataEmail",
                table: "runningSessions",
                column: "traningDataEmail",
                principalTable: "trantingData",
                principalColumn: "Email",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_trantingData_users_Email",
                table: "trantingData",
                column: "Email",
                principalTable: "users",
                principalColumn: "Email",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
