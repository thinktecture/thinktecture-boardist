using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Thinktecture.Boardist.WebApi.Migrations
{
    public partial class AddCategoryRelationships : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("23d3a212-4996-4fe2-a3fa-d9fe8b975952"),
                column: "Name",
                value: "Roleplay Game");

            migrationBuilder.InsertData(
                table: "GameCategory",
                columns: new[] { "GameId", "CategoryId" },
                values: new object[,]
                {
                    { new Guid("7586c43c-ef14-499c-996b-05ad0ddecc67"), new Guid("75590c0d-46c6-4db3-a772-6614b6354c71") },
                    { new Guid("7586c43c-ef14-499c-996b-05ad0ddecc67"), new Guid("631caec4-088d-4cce-baa8-3356302b76da") },
                    { new Guid("7586c43c-ef14-499c-996b-05ad0ddecc67"), new Guid("537d56da-6a38-4da8-b872-ec462d5ef512") },
                    { new Guid("0dc94f91-dc0d-4071-91f1-ff67c80cda3a"), new Guid("75590c0d-46c6-4db3-a772-6614b6354c71") },
                    { new Guid("0dc94f91-dc0d-4071-91f1-ff67c80cda3a"), new Guid("631caec4-088d-4cce-baa8-3356302b76da") },
                    { new Guid("0dc94f91-dc0d-4071-91f1-ff67c80cda3a"), new Guid("537d56da-6a38-4da8-b872-ec462d5ef512") },
                    { new Guid("7e677287-e070-4ffb-b102-b45f3aeff158"), new Guid("23d3a212-4996-4fe2-a3fa-d9fe8b975952") }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "GameCategory",
                keyColumns: new[] { "GameId", "CategoryId" },
                keyValues: new object[] { new Guid("0dc94f91-dc0d-4071-91f1-ff67c80cda3a"), new Guid("537d56da-6a38-4da8-b872-ec462d5ef512") });

            migrationBuilder.DeleteData(
                table: "GameCategory",
                keyColumns: new[] { "GameId", "CategoryId" },
                keyValues: new object[] { new Guid("0dc94f91-dc0d-4071-91f1-ff67c80cda3a"), new Guid("631caec4-088d-4cce-baa8-3356302b76da") });

            migrationBuilder.DeleteData(
                table: "GameCategory",
                keyColumns: new[] { "GameId", "CategoryId" },
                keyValues: new object[] { new Guid("0dc94f91-dc0d-4071-91f1-ff67c80cda3a"), new Guid("75590c0d-46c6-4db3-a772-6614b6354c71") });

            migrationBuilder.DeleteData(
                table: "GameCategory",
                keyColumns: new[] { "GameId", "CategoryId" },
                keyValues: new object[] { new Guid("7586c43c-ef14-499c-996b-05ad0ddecc67"), new Guid("537d56da-6a38-4da8-b872-ec462d5ef512") });

            migrationBuilder.DeleteData(
                table: "GameCategory",
                keyColumns: new[] { "GameId", "CategoryId" },
                keyValues: new object[] { new Guid("7586c43c-ef14-499c-996b-05ad0ddecc67"), new Guid("631caec4-088d-4cce-baa8-3356302b76da") });

            migrationBuilder.DeleteData(
                table: "GameCategory",
                keyColumns: new[] { "GameId", "CategoryId" },
                keyValues: new object[] { new Guid("7586c43c-ef14-499c-996b-05ad0ddecc67"), new Guid("75590c0d-46c6-4db3-a772-6614b6354c71") });

            migrationBuilder.DeleteData(
                table: "GameCategory",
                keyColumns: new[] { "GameId", "CategoryId" },
                keyValues: new object[] { new Guid("7e677287-e070-4ffb-b102-b45f3aeff158"), new Guid("23d3a212-4996-4fe2-a3fa-d9fe8b975952") });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("23d3a212-4996-4fe2-a3fa-d9fe8b975952"),
                column: "Name",
                value: "Rollenspiel");
        }
    }
}
