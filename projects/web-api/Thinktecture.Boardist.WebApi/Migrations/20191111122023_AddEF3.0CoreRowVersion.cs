using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Thinktecture.Boardist.WebApi.Migrations
{
    public partial class AddEF30CoreRowVersion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "Publishers",
                type: "rowversion");

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "Persons",
                type: "rowversion");

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "Mechanics",
                type: "rowversion");

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "Games",
                type: "rowversion");

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "Categories",
                type: "rowversion");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "Publishers");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "Persons");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "Mechanics");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "Categories");
        }
    }
}
