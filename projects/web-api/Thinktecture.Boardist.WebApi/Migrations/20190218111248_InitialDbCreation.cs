﻿using System;
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
        constraints: table => { table.PrimaryKey("PK_Persons", x => x.Id); });

      migrationBuilder.CreateTable(
        name: "Publishers",
        columns: table => new
        {
          Id = table.Column<Guid>(nullable: false),
          Name = table.Column<string>(nullable: true)
        },
        constraints: table => { table.PrimaryKey("PK_Publishers", x => x.Id); });

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
          PublisherId = table.Column<Guid>(nullable: true)
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
            onDelete: ReferentialAction.Restrict);
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
          table.PrimaryKey("PK_GameAuthor", x => new {x.GameId, x.AuthorId});
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
          table.PrimaryKey("PK_GameIllustrator", x => new {x.GameId, x.IllustratorId});
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
