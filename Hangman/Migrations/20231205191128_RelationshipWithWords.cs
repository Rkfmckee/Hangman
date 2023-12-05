using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hangman.Migrations
{
    /// <inheritdoc />
    public partial class RelationshipWithWords : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Word",
                table: "Game");

            migrationBuilder.AddColumn<Guid>(
                name: "ChosenWordId",
                table: "Game",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Game_ChosenWordId",
                table: "Game",
                column: "ChosenWordId");

            migrationBuilder.AddForeignKey(
                name: "FK_Game_Words_ChosenWordId",
                table: "Game",
                column: "ChosenWordId",
                principalTable: "Words",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Game_Words_ChosenWordId",
                table: "Game");

            migrationBuilder.DropIndex(
                name: "IX_Game_ChosenWordId",
                table: "Game");

            migrationBuilder.DropColumn(
                name: "ChosenWordId",
                table: "Game");

            migrationBuilder.AddColumn<string>(
                name: "Word",
                table: "Game",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
