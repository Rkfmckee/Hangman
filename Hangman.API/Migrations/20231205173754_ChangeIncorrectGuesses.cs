using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hangman.Migrations
{
    /// <inheritdoc />
    public partial class ChangeIncorrectGuesses : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IncorrectGuesses",
                table: "Game",
                newName: "IncorrectGuessesLeft");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IncorrectGuessesLeft",
                table: "Game",
                newName: "IncorrectGuesses");
        }
    }
}
