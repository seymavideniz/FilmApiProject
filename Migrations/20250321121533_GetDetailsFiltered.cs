using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FilmProject.Migrations
{
    /// <inheritdoc />
    public partial class GetDetailsFiltered : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FilmDetails_Films_MovieId",
                table: "FilmDetails");

            migrationBuilder.RenameColumn(
                name: "UpdateAt",
                table: "FilmDetails",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "MovieId",
                table: "FilmDetails",
                newName: "FilmId");

            migrationBuilder.RenameIndex(
                name: "IX_FilmDetails_MovieId",
                table: "FilmDetails",
                newName: "IX_FilmDetails_FilmId");

            migrationBuilder.AddForeignKey(
                name: "FK_FilmDetails_Films_FilmId",
                table: "FilmDetails",
                column: "FilmId",
                principalTable: "Films",
                principalColumn: "FilmID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FilmDetails_Films_FilmId",
                table: "FilmDetails");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "FilmDetails",
                newName: "UpdateAt");

            migrationBuilder.RenameColumn(
                name: "FilmId",
                table: "FilmDetails",
                newName: "MovieId");

            migrationBuilder.RenameIndex(
                name: "IX_FilmDetails_FilmId",
                table: "FilmDetails",
                newName: "IX_FilmDetails_MovieId");

            migrationBuilder.AddForeignKey(
                name: "FK_FilmDetails_Films_MovieId",
                table: "FilmDetails",
                column: "MovieId",
                principalTable: "Films",
                principalColumn: "FilmID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
