using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApi.Migrations
{
    /// <inheritdoc />
    public partial class UniqueFTPkey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_favoriteTraningPrograms_FavoriteTraningProgramsID",
                table: "favoriteTraningPrograms",
                column: "FavoriteTraningProgramsID",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_favoriteTraningPrograms_FavoriteTraningProgramsID",
                table: "favoriteTraningPrograms");
        }
    }
}
