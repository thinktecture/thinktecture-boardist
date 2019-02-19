using Microsoft.EntityFrameworkCore.Migrations;

namespace Thinktecture.Boardist.WebApi.Migrations
{
    public partial class ChangeRelationshipForMainGameToGame : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Games_MainGameId",
                table: "Games");

            migrationBuilder.CreateIndex(
                name: "IX_Games_MainGameId",
                table: "Games",
                column: "MainGameId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Games_MainGameId",
                table: "Games");

            migrationBuilder.CreateIndex(
                name: "IX_Games_MainGameId",
                table: "Games",
                column: "MainGameId",
                unique: true,
                filter: "[MainGameId] IS NOT NULL");
        }
    }
}
