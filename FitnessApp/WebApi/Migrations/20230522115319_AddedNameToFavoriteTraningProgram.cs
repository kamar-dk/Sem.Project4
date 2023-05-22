using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApi.Migrations
{
    /// <inheritdoc />
    public partial class AddedNameToFavoriteTraningProgram : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_traningData_users_UserId",
                table: "traningData");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "traningData",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "favoriteTraningPrograms",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_traningData_users_UserId",
                table: "traningData",
                column: "UserId",
                principalTable: "users",
                principalColumn: "Email",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_traningData_users_UserId",
                table: "traningData");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "favoriteTraningPrograms");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "traningData",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_traningData_users_UserId",
                table: "traningData",
                column: "UserId",
                principalTable: "users",
                principalColumn: "Email");
        }
    }
}
