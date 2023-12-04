using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hangman.Migrations
{
    /// <inheritdoc />
    public partial class AddCorrectLetters : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "GameState",
                table: "Game",
                newName: "GameStatus");

            migrationBuilder.AddColumn<string>(
                name: "CorrectLetters",
                table: "Game",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CorrectLetters",
                table: "Game");

            migrationBuilder.RenameColumn(
                name: "GameStatus",
                table: "Game",
                newName: "GameState");
        }
    }
}
