using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FA_DB.Migrations
{
    /// <inheritdoc />
    public partial class Weight : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropPrimaryKey(
                name: "PK_traningData",
                table: "traningData");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "traningData",
                newName: "UserEmail");

            migrationBuilder.RenameColumn(
                name: "traningDataEmail",
                table: "runningSessions",
                newName: "traningDataUserId");

            migrationBuilder.RenameIndex(
                name: "IX_runningSessions_traningDataEmail",
                table: "runningSessions",
                newName: "IX_runningSessions_traningDataUserId");

            migrationBuilder.RenameColumn(
                name: "traningDataEmail",
                table: "bikeSessions",
                newName: "traningDataUserId");

            migrationBuilder.RenameIndex(
                name: "IX_bikeSessions_traningDataEmail",
                table: "bikeSessions",
                newName: "IX_bikeSessions_traningDataUserId");

            migrationBuilder.AlterColumn<byte[]>(
                name: "Salt",
                table: "users",
                type: "varbinary(max)",
                nullable: true,
                oldClrType: typeof(byte[]),
                oldType: "varbinary(max)");

            migrationBuilder.AlterColumn<byte[]>(
                name: "PasswordHash",
                table: "users",
                type: "varbinary(max)",
                nullable: true,
                oldClrType: typeof(byte[]),
                oldType: "varbinary(max)");

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "users",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "users",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<float>(
                name: "Weight",
                table: "userDatas",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "traningData",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "AvgHeartRate",
                table: "traningData",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Calories",
                table: "traningData",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<float>(
                name: "Distance",
                table: "traningData",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<long>(
                name: "Id",
                table: "traningData",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<int>(
                name: "MaxHeartRate",
                table: "traningData",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MinHeartRate",
                table: "traningData",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "SessionDate",
                table: "traningData",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "SessionHourTime",
                table: "traningData",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SessionMinuteTime",
                table: "traningData",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SessionSecondTime",
                table: "traningData",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "TrainingType",
                table: "traningData",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<float>(
                name: "Vo2Max",
                table: "traningData",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddPrimaryKey(
                name: "PK_traningData",
                table: "traningData",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_traningData_UserEmail",
                table: "traningData",
                column: "UserEmail");

            migrationBuilder.AddForeignKey(
                name: "FK_bikeSessions_traningData_traningDataUserId",
                table: "bikeSessions",
                column: "traningDataUserId",
                principalTable: "traningData",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_runningSessions_traningData_traningDataUserId",
                table: "runningSessions",
                column: "traningDataUserId",
                principalTable: "traningData",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_traningData_users_UserEmail",
                table: "traningData",
                column: "UserEmail",
                principalTable: "users",
                principalColumn: "Email",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_bikeSessions_traningData_traningDataUserId",
                table: "bikeSessions");

            migrationBuilder.DropForeignKey(
                name: "FK_runningSessions_traningData_traningDataUserId",
                table: "runningSessions");

            migrationBuilder.DropForeignKey(
                name: "FK_traningData_users_UserEmail",
                table: "traningData");

            migrationBuilder.DropPrimaryKey(
                name: "PK_traningData",
                table: "traningData");

            migrationBuilder.DropIndex(
                name: "IX_traningData_UserEmail",
                table: "traningData");

            migrationBuilder.DropColumn(
                name: "Weight",
                table: "userDatas");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "traningData");

            migrationBuilder.DropColumn(
                name: "AvgHeartRate",
                table: "traningData");

            migrationBuilder.DropColumn(
                name: "Calories",
                table: "traningData");

            migrationBuilder.DropColumn(
                name: "Distance",
                table: "traningData");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "traningData");

            migrationBuilder.DropColumn(
                name: "MaxHeartRate",
                table: "traningData");

            migrationBuilder.DropColumn(
                name: "MinHeartRate",
                table: "traningData");

            migrationBuilder.DropColumn(
                name: "SessionDate",
                table: "traningData");

            migrationBuilder.DropColumn(
                name: "SessionHourTime",
                table: "traningData");

            migrationBuilder.DropColumn(
                name: "SessionMinuteTime",
                table: "traningData");

            migrationBuilder.DropColumn(
                name: "SessionSecondTime",
                table: "traningData");

            migrationBuilder.DropColumn(
                name: "TrainingType",
                table: "traningData");

            migrationBuilder.DropColumn(
                name: "Vo2Max",
                table: "traningData");

            migrationBuilder.RenameColumn(
                name: "UserEmail",
                table: "traningData",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "traningDataUserId",
                table: "runningSessions",
                newName: "traningDataEmail");

            migrationBuilder.RenameIndex(
                name: "IX_runningSessions_traningDataUserId",
                table: "runningSessions",
                newName: "IX_runningSessions_traningDataEmail");

            migrationBuilder.RenameColumn(
                name: "traningDataUserId",
                table: "bikeSessions",
                newName: "traningDataEmail");

            migrationBuilder.RenameIndex(
                name: "IX_bikeSessions_traningDataUserId",
                table: "bikeSessions",
                newName: "IX_bikeSessions_traningDataEmail");

            migrationBuilder.AlterColumn<byte[]>(
                name: "Salt",
                table: "users",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0],
                oldClrType: typeof(byte[]),
                oldType: "varbinary(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<byte[]>(
                name: "PasswordHash",
                table: "users",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0],
                oldClrType: typeof(byte[]),
                oldType: "varbinary(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_traningData",
                table: "traningData",
                column: "Email");

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
    }
}
