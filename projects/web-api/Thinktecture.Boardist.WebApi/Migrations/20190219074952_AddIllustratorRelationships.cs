using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Thinktecture.Boardist.WebApi.Migrations
{
    public partial class AddIllustratorRelationships : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "GameIllustrator",
                columns: new[] { "GameId", "IllustratorId" },
                values: new object[,]
                {
                    { new Guid("7586c43c-ef14-499c-996b-05ad0ddecc67"), new Guid("2202fe49-34ed-4e0e-9ffc-7e9ff8aca50d") },
                    { new Guid("7586c43c-ef14-499c-996b-05ad0ddecc67"), new Guid("2202fe49-34ed-4e0e-9ffc-7e9ff8aca50e") },
                    { new Guid("0dc94f91-dc0d-4071-91f1-ff67c80cda3a"), new Guid("2202fe49-34ed-4e0e-9ffc-7e9ff8aca50d") },
                    { new Guid("0dc94f91-dc0d-4071-91f1-ff67c80cda3a"), new Guid("2202fe49-34ed-4e0e-9ffc-7e9ff8aca50e") },
                    { new Guid("7e677287-e070-4ffb-b102-b45f3aeff158"), new Guid("2202fe49-34ed-4e0e-9ffc-7e9ff8aca50e") }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "GameIllustrator",
                keyColumns: new[] { "GameId", "IllustratorId" },
                keyValues: new object[] { new Guid("0dc94f91-dc0d-4071-91f1-ff67c80cda3a"), new Guid("2202fe49-34ed-4e0e-9ffc-7e9ff8aca50d") });

            migrationBuilder.DeleteData(
                table: "GameIllustrator",
                keyColumns: new[] { "GameId", "IllustratorId" },
                keyValues: new object[] { new Guid("0dc94f91-dc0d-4071-91f1-ff67c80cda3a"), new Guid("2202fe49-34ed-4e0e-9ffc-7e9ff8aca50e") });

            migrationBuilder.DeleteData(
                table: "GameIllustrator",
                keyColumns: new[] { "GameId", "IllustratorId" },
                keyValues: new object[] { new Guid("7586c43c-ef14-499c-996b-05ad0ddecc67"), new Guid("2202fe49-34ed-4e0e-9ffc-7e9ff8aca50d") });

            migrationBuilder.DeleteData(
                table: "GameIllustrator",
                keyColumns: new[] { "GameId", "IllustratorId" },
                keyValues: new object[] { new Guid("7586c43c-ef14-499c-996b-05ad0ddecc67"), new Guid("2202fe49-34ed-4e0e-9ffc-7e9ff8aca50e") });

            migrationBuilder.DeleteData(
                table: "GameIllustrator",
                keyColumns: new[] { "GameId", "IllustratorId" },
                keyValues: new object[] { new Guid("7e677287-e070-4ffb-b102-b45f3aeff158"), new Guid("2202fe49-34ed-4e0e-9ffc-7e9ff8aca50e") });
        }
    }
}
