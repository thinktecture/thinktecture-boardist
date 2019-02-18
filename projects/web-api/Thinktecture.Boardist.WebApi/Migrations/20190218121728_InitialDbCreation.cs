using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Thinktecture.Boardist.WebApi.Migrations
{
    public partial class InitialDbCreation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Persons",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persons", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Publishers",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Publishers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Games",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    MinPlayers = table.Column<int>(nullable: false),
                    MaxPlayers = table.Column<int>(nullable: false),
                    MinDuration = table.Column<int>(nullable: true),
                    MaxDuration = table.Column<int>(nullable: true),
                    PerPlayerDuration = table.Column<int>(nullable: true),
                    BuyPrice = table.Column<decimal>(type: "decimal(3,2)", nullable: true),
                    BuyDate = table.Column<DateTime>(nullable: true),
                    MainGameId = table.Column<Guid>(nullable: true),
                    PublisherId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Games", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Games_Games_MainGameId",
                        column: x => x.MainGameId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Games_Publishers_PublisherId",
                        column: x => x.PublisherId,
                        principalTable: "Publishers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    GameId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Categories_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GameAuthor",
                columns: table => new
                {
                    GameId = table.Column<Guid>(nullable: false),
                    AuthorId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameAuthor", x => new { x.GameId, x.AuthorId });
                    table.ForeignKey(
                        name: "FK_GameAuthor_Persons_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GameAuthor_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GameIllustrator",
                columns: table => new
                {
                    GameId = table.Column<Guid>(nullable: false),
                    IllustratorId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameIllustrator", x => new { x.GameId, x.IllustratorId });
                    table.ForeignKey(
                        name: "FK_GameIllustrator_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GameIllustrator_Persons_IllustratorId",
                        column: x => x.IllustratorId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Persons",
                columns: new[] { "Id", "FirstName", "LastName" },
                values: new object[,]
                {
                    { new Guid("6d6e4795-fd8d-4630-add0-eb80cc2c7fb2"), "Antoine", "Bauza" },
                    { new Guid("2202fe49-34ed-4e0e-9ffc-7e9ff8aca50c"), "Menzel", "Michael" }
                });

            migrationBuilder.InsertData(
                table: "Publishers",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("e6237d73-007a-4aa5-b068-bc909f0f9897"), "Asmodee" },
                    { new Guid("579176ab-5eaa-484b-87fb-33806252c214"), "Kosmos" }
                });

            migrationBuilder.InsertData(
                table: "Games",
                columns: new[] { "Id", "BuyDate", "BuyPrice", "MainGameId", "MaxDuration", "MaxPlayers", "MinDuration", "MinPlayers", "Name", "PerPlayerDuration", "PublisherId" },
                values: new object[] { new Guid("7586c43c-ef14-499c-996b-05ad0ddecc67"), null, null, null, null, 7, null, 3, "7 Wonders", 40, new Guid("e6237d73-007a-4aa5-b068-bc909f0f9897") });

            migrationBuilder.InsertData(
                table: "Games",
                columns: new[] { "Id", "BuyDate", "BuyPrice", "MainGameId", "MaxDuration", "MaxPlayers", "MinDuration", "MinPlayers", "Name", "PerPlayerDuration", "PublisherId" },
                values: new object[] { new Guid("7e677287-e070-4ffb-b102-b45f3aeff158"), null, null, null, null, 4, null, 2, "Die Legenden von Andor", 90, new Guid("579176ab-5eaa-484b-87fb-33806252c214") });

            migrationBuilder.InsertData(
                table: "GameAuthor",
                columns: new[] { "GameId", "AuthorId" },
                values: new object[] { new Guid("7586c43c-ef14-499c-996b-05ad0ddecc67"), new Guid("6d6e4795-fd8d-4630-add0-eb80cc2c7fb2") });

            migrationBuilder.InsertData(
                table: "GameAuthor",
                columns: new[] { "GameId", "AuthorId" },
                values: new object[] { new Guid("7e677287-e070-4ffb-b102-b45f3aeff158"), new Guid("2202fe49-34ed-4e0e-9ffc-7e9ff8aca50c") });

            migrationBuilder.InsertData(
                table: "Games",
                columns: new[] { "Id", "BuyDate", "BuyPrice", "MainGameId", "MaxDuration", "MaxPlayers", "MinDuration", "MinPlayers", "Name", "PerPlayerDuration", "PublisherId" },
                values: new object[] { new Guid("0dc94f91-dc0d-4071-91f1-ff67c80cda3a"), null, null, new Guid("7586c43c-ef14-499c-996b-05ad0ddecc67"), null, 7, null, 2, "7 Wonders - Babel", 40, new Guid("e6237d73-007a-4aa5-b068-bc909f0f9897") });

            migrationBuilder.InsertData(
                table: "GameAuthor",
                columns: new[] { "GameId", "AuthorId" },
                values: new object[] { new Guid("0dc94f91-dc0d-4071-91f1-ff67c80cda3a"), new Guid("6d6e4795-fd8d-4630-add0-eb80cc2c7fb2") });

            migrationBuilder.CreateIndex(
                name: "IX_Categories_GameId",
                table: "Categories",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_GameAuthor_AuthorId",
                table: "GameAuthor",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_GameIllustrator_IllustratorId",
                table: "GameIllustrator",
                column: "IllustratorId");

            migrationBuilder.CreateIndex(
                name: "IX_Games_MainGameId",
                table: "Games",
                column: "MainGameId",
                unique: true,
                filter: "[MainGameId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Games_PublisherId",
                table: "Games",
                column: "PublisherId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "GameAuthor");

            migrationBuilder.DropTable(
                name: "GameIllustrator");

            migrationBuilder.DropTable(
                name: "Games");

            migrationBuilder.DropTable(
                name: "Persons");

            migrationBuilder.DropTable(
                name: "Publishers");
        }
    }
}
