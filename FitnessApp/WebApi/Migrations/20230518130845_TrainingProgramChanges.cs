using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApi.Migrations
{
    /// <inheritdoc />
    public partial class TrainingProgramChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Gender",
                table: "userDatas");

            migrationBuilder.AddColumn<int>(
                name: "FavoriteTraningProgramsID",
                table: "traningPrograms",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_traningPrograms_FavoriteTraningProgramsID",
                table: "traningPrograms",
                column: "FavoriteTraningProgramsID");

            migrationBuilder.AddForeignKey(
                name: "FK_traningPrograms_favoriteTraningPrograms_FavoriteTraningProgramsID",
                table: "traningPrograms",
                column: "FavoriteTraningProgramsID",
                principalTable: "favoriteTraningPrograms",
                principalColumn: "FavoriteTraningProgramsID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_traningPrograms_favoriteTraningPrograms_FavoriteTraningProgramsID",
                table: "traningPrograms");

            migrationBuilder.DropIndex(
                name: "IX_traningPrograms_FavoriteTraningProgramsID",
                table: "traningPrograms");

            migrationBuilder.DropColumn(
                name: "FavoriteTraningProgramsID",
                table: "traningPrograms");

            migrationBuilder.AddColumn<string>(
                name: "Gender",
                table: "userDatas",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
