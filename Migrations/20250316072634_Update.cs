using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FilmProject.Migrations
{
    /// <inheritdoc />
    public partial class Update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_FilmDetails_UserId_MovieId",
                table: "FilmDetails");

            migrationBuilder.RenameColumn(
                name: "Guid",
                table: "User",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Films",
                newName: "FilmID");

            migrationBuilder.RenameColumn(
                name: "Guid",
                table: "FilmDetails",
                newName: "Id");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "FilmDetails",
                type: "uuid",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateAt",
                table: "FilmDetails",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_FilmDetails_MovieId",
                table: "FilmDetails",
                column: "MovieId");

            migrationBuilder.CreateIndex(
                name: "IX_FilmDetails_UserId",
                table: "FilmDetails",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_FilmDetails_Films_MovieId",
                table: "FilmDetails",
                column: "MovieId",
                principalTable: "Films",
                principalColumn: "FilmID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FilmDetails_User_UserId",
                table: "FilmDetails",
                column: "UserId",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FilmDetails_Films_MovieId",
                table: "FilmDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_FilmDetails_User_UserId",
                table: "FilmDetails");

            migrationBuilder.DropIndex(
                name: "IX_FilmDetails_MovieId",
                table: "FilmDetails");

            migrationBuilder.DropIndex(
                name: "IX_FilmDetails_UserId",
                table: "FilmDetails");

            migrationBuilder.DropColumn(
                name: "UpdateAt",
                table: "FilmDetails");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "User",
                newName: "Guid");

            migrationBuilder.RenameColumn(
                name: "FilmID",
                table: "Films",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "FilmDetails",
                newName: "Guid");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "FilmDetails",
                type: "integer",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.CreateIndex(
                name: "IX_FilmDetails_UserId_MovieId",
                table: "FilmDetails",
                columns: new[] { "UserId", "MovieId" },
                unique: true);
        }
    }
}
