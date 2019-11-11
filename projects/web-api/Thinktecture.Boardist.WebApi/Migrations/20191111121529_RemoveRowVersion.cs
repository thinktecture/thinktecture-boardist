using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Thinktecture.Boardist.WebApi.Migrations
{
    public partial class RemoveRowVersion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "Publishers",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "Persons",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "Mechanics",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "Games",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "Categories",
                rowVersion: true,
                nullable: true);
        }
    }
}
