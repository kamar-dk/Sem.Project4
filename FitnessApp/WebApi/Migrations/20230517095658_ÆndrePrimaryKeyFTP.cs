using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApi.Migrations
{
    /// <inheritdoc />
    public partial class ÆndrePrimaryKeyFTP : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_favoriteTraningPrograms",
                table: "favoriteTraningPrograms");

            migrationBuilder.DropIndex(
                name: "IX_favoriteTraningPrograms_TraningProgramID",
                table: "favoriteTraningPrograms");

            migrationBuilder.AddPrimaryKey(
                name: "PK_favoriteTraningPrograms",
                table: "favoriteTraningPrograms",
                column: "TraningProgramID");

            migrationBuilder.CreateIndex(
                name: "IX_favoriteTraningPrograms_Email",
                table: "favoriteTraningPrograms",
                column: "Email");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_favoriteTraningPrograms",
                table: "favoriteTraningPrograms");

            migrationBuilder.DropIndex(
                name: "IX_favoriteTraningPrograms_Email",
                table: "favoriteTraningPrograms");

            migrationBuilder.AddPrimaryKey(
                name: "PK_favoriteTraningPrograms",
                table: "favoriteTraningPrograms",
                column: "Email");

            migrationBuilder.CreateIndex(
                name: "IX_favoriteTraningPrograms_TraningProgramID",
                table: "favoriteTraningPrograms",
                column: "TraningProgramID");
        }
    }
}
