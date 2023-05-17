using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApi.Migrations
{
    /// <inheritdoc />
    public partial class UpdateFavoriteTraningprograms : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_traningPrograms_favoriteTraningPrograms_FavoriteTraningProgramsEmail",
                table: "traningPrograms");

            migrationBuilder.DropIndex(
                name: "IX_traningPrograms_FavoriteTraningProgramsEmail",
                table: "traningPrograms");

            migrationBuilder.DropColumn(
                name: "FavoriteTraningProgramsEmail",
                table: "traningPrograms");

            migrationBuilder.AddColumn<int>(
                name: "FavoriteTraningProgramsID",
                table: "favoriteTraningPrograms",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TraningProgramID",
                table: "favoriteTraningPrograms",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_favoriteTraningPrograms_TraningProgramID",
                table: "favoriteTraningPrograms",
                column: "TraningProgramID");

            migrationBuilder.AddForeignKey(
                name: "FK_favoriteTraningPrograms_traningPrograms_TraningProgramID",
                table: "favoriteTraningPrograms",
                column: "TraningProgramID",
                principalTable: "traningPrograms",
                principalColumn: "TraningProgramID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_favoriteTraningPrograms_traningPrograms_TraningProgramID",
                table: "favoriteTraningPrograms");

            migrationBuilder.DropIndex(
                name: "IX_favoriteTraningPrograms_TraningProgramID",
                table: "favoriteTraningPrograms");

            migrationBuilder.DropColumn(
                name: "FavoriteTraningProgramsID",
                table: "favoriteTraningPrograms");

            migrationBuilder.DropColumn(
                name: "TraningProgramID",
                table: "favoriteTraningPrograms");

            migrationBuilder.AddColumn<string>(
                name: "FavoriteTraningProgramsEmail",
                table: "traningPrograms",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_traningPrograms_FavoriteTraningProgramsEmail",
                table: "traningPrograms",
                column: "FavoriteTraningProgramsEmail");

            migrationBuilder.AddForeignKey(
                name: "FK_traningPrograms_favoriteTraningPrograms_FavoriteTraningProgramsEmail",
                table: "traningPrograms",
                column: "FavoriteTraningProgramsEmail",
                principalTable: "favoriteTraningPrograms",
                principalColumn: "Email");
        }
    }
}
