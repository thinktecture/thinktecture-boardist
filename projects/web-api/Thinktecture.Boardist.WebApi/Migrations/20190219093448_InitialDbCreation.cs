using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Thinktecture.Boardist.WebApi.Migrations
{
  public partial class InitialDbCreation : Migration
  {
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.CreateTable(
        name: "Categories",
        columns: table => new
        {
          Id = table.Column<Guid>(nullable: false),
          Name = table.Column<string>(maxLength: 250, nullable: true)
        },
        constraints: table => { table.PrimaryKey("PK_Categories", x => x.Id); });

      migrationBuilder.CreateTable(
        name: "Mechanics",
        columns: table => new
        {
          Id = table.Column<Guid>(nullable: false),
          Name = table.Column<string>(nullable: true)
        },
        constraints: table => { table.PrimaryKey("PK_Mechanics", x => x.Id); });

      migrationBuilder.CreateTable(
        name: "Persons",
        columns: table => new
        {
          Id = table.Column<Guid>(nullable: false),
          FirstName = table.Column<string>(maxLength: 250, nullable: true),
          LastName = table.Column<string>(maxLength: 250, nullable: true)
        },
        constraints: table => { table.PrimaryKey("PK_Persons", x => x.Id); });

      migrationBuilder.CreateTable(
        name: "Publishers",
        columns: table => new
        {
          Id = table.Column<Guid>(nullable: false),
          Name = table.Column<string>(maxLength: 250, nullable: true)
        },
        constraints: table => { table.PrimaryKey("PK_Publishers", x => x.Id); });

      migrationBuilder.CreateTable(
        name: "Games",
        columns: table => new
        {
          Id = table.Column<Guid>(nullable: false),
          Name = table.Column<string>(maxLength: 250, nullable: true),
          MinPlayers = table.Column<int>(nullable: false),
          MaxPlayers = table.Column<int>(nullable: false),
          MinAge = table.Column<int>(nullable: false),
          MinDuration = table.Column<int>(nullable: true),
          MaxDuration = table.Column<int>(nullable: true),
          BuyPrice = table.Column<decimal>(type: "decimal(3,2)", nullable: true),
          BuyDate = table.Column<DateTime>(nullable: true),
          BoardGameGeekId = table.Column<int>(nullable: true),
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
        name: "GameCategory",
        columns: table => new
        {
          GameId = table.Column<Guid>(nullable: false),
          CategoryId = table.Column<Guid>(nullable: false)
        },
        constraints: table =>
        {
          table.PrimaryKey("PK_GameCategory", x => new {x.GameId, x.CategoryId});
          table.ForeignKey(
            name: "FK_GameCategory_Categories_CategoryId",
            column: x => x.CategoryId,
            principalTable: "Categories",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);
          table.ForeignKey(
            name: "FK_GameCategory_Games_GameId",
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

      migrationBuilder.CreateTable(
        name: "GameMechanic",
        columns: table => new
        {
          GameId = table.Column<Guid>(nullable: false),
          MechanicId = table.Column<Guid>(nullable: false)
        },
        constraints: table =>
        {
          table.PrimaryKey("PK_GameMechanic", x => new {x.GameId, x.MechanicId});
          table.ForeignKey(
            name: "FK_GameMechanic_Games_GameId",
            column: x => x.GameId,
            principalTable: "Games",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);
          table.ForeignKey(
            name: "FK_GameMechanic_Mechanics_MechanicId",
            column: x => x.MechanicId,
            principalTable: "Mechanics",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);
        });

      migrationBuilder.CreateIndex(
        name: "IX_GameAuthor_AuthorId",
        table: "GameAuthor",
        column: "AuthorId");

      migrationBuilder.CreateIndex(
        name: "IX_GameCategory_CategoryId",
        table: "GameCategory",
        column: "CategoryId");

      migrationBuilder.CreateIndex(
        name: "IX_GameIllustrator_IllustratorId",
        table: "GameIllustrator",
        column: "IllustratorId");

      migrationBuilder.CreateIndex(
        name: "IX_GameMechanic_MechanicId",
        table: "GameMechanic",
        column: "MechanicId");

      migrationBuilder.CreateIndex(
        name: "IX_Games_MainGameId",
        table: "Games",
        column: "MainGameId");

      migrationBuilder.CreateIndex(
        name: "IX_Games_PublisherId",
        table: "Games",
        column: "PublisherId");

      migrationBuilder.Sql(@"
INSERT INTO Publishers (Id, Name) VALUES ('68A28E87-170C-4AC6-A77F-75B49DE67452', 'Abacusspiele');
INSERT INTO Publishers (Id, Name) VALUES ('FF19A341-8372-423A-B82F-2C36031681EA', 'Artipia Games');
INSERT INTO Publishers (Id, Name) VALUES ('8C8C8781-4926-403B-86AC-A4B61975FE31', 'Asmodee');
INSERT INTO Publishers (Id, Name) VALUES ('0AF6D8F6-D09B-4C12-9908-A4BB377117BA', 'Czech Games Edition ');
INSERT INTO Publishers (Id, Name) VALUES ('38AAC893-67DE-4FCD-9DA3-E94BDC024DBE', 'Days of Wonder');
INSERT INTO Publishers (Id, Name) VALUES ('FD455EAE-58C2-4123-9BFF-8877FFC9263F', 'eggertspiele');
INSERT INTO Publishers (Id, Name) VALUES ('EC30755A-C867-4ED3-936F-BF55FD1C47F2', 'Fantasy Flight Games');
INSERT INTO Publishers (Id, Name) VALUES ('1714A25E-66DF-4DEF-A5C4-D1C102E1BA43', 'Feuerland Spiele');
INSERT INTO Publishers (Id, Name) VALUES ('2C763E90-945D-4106-B841-6628A82219AC', 'Hans im Glück');
INSERT INTO Publishers (Id, Name) VALUES ('F849595C-801B-4D95-99B2-5DEBF824DE14', 'Heidelberger Spieleverlag');
INSERT INTO Publishers (Id, Name) VALUES ('EE826793-FA24-4020-8F8F-792E88F205F6', 'Iello');
INSERT INTO Publishers (Id, Name) VALUES ('72E8B7A0-1C25-4DEB-BD7E-2CB339DBC163', 'Kosmos');
INSERT INTO Publishers (Id, Name) VALUES ('5AFEAAEA-B451-413C-9AF4-C61C71E388D3', 'Lookout Games');
INSERT INTO Publishers (Id, Name) VALUES ('B69B5032-43A8-44FA-BD21-A4A7604BF5E9', 'Ludonaute');
INSERT INTO Publishers (Id, Name) VALUES ('767C317F-B4BE-4D84-A873-EB0BE1B41962', 'Matagot');
INSERT INTO Publishers (Id, Name) VALUES ('BAE5F53E-6873-4637-A07C-A40C6730ACC2', 'Pegasus Spiele');
INSERT INTO Publishers (Id, Name) VALUES ('F203898B-B16E-4B7C-BB93-4DA741DD5497', 'Queen Games');
INSERT INTO Publishers (Id, Name) VALUES ('6F7D65CB-0BF7-439E-A2E2-0DC70D37E179', 'Ravensburger');
INSERT INTO Publishers (Id, Name) VALUES ('5F647A2B-57B4-429D-8D82-449C385F0DFF', 'Schmidt Spiele');
INSERT INTO Publishers (Id, Name) VALUES ('579176AB-5EAA-484B-87FB-33806252C214', 'Unbekannt');
INSERT INTO Publishers (Id, Name) VALUES ('69209C44-22C9-40CE-AAF6-1E75C5A6C83E', 'WiWa Spiele');
");

      migrationBuilder.Sql(@"
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('322F3FCC-0D12-4D18-827F-1673BD6A1F5A', '7 Wonders', null, '8C8C8781-4926-403B-86AC-A4B61975FE31', 3, 7, 40, 40, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('0814C055-AD39-428A-B5E3-545C955D6FE8', '7 Wonders - Babel', '322F3FCC-0D12-4D18-827F-1673BD6A1F5A', '8C8C8781-4926-403B-86AC-A4B61975FE31', 2, 7, 40, 40, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('98634FC4-D569-4459-8503-FA3BDD377A42', '7 Wonders - Cities', '322F3FCC-0D12-4D18-827F-1673BD6A1F5A', '8C8C8781-4926-403B-86AC-A4B61975FE31', 2, 8, 40, 40, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('CEBF983E-0A43-437B-9A05-41A069F0D9DA', '7 Wonders - Leaders', '322F3FCC-0D12-4D18-827F-1673BD6A1F5A', '8C8C8781-4926-403B-86AC-A4B61975FE31', 3, 7, 40, 40, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('411E50AD-C5BD-47DF-BC02-ED5BF32B591D', '7 Wonders - Wonder Pack', '322F3FCC-0D12-4D18-827F-1673BD6A1F5A', '8C8C8781-4926-403B-86AC-A4B61975FE31', 3, 7, 40, 40, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('9F73EF15-F210-4C4C-84C1-437CB5A93883', '7 Wonders Duel', null, '8C8C8781-4926-403B-86AC-A4B61975FE31', 2, 2, 30, 30, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('4AEFAB27-1A5A-42E2-AA7D-3A552803EB7F', '7 Wonders Duel - Pantheon', '9F73EF15-F210-4C4C-84C1-437CB5A93883', '8C8C8781-4926-403B-86AC-A4B61975FE31', 2, 2, 30, 30, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('1030D528-9EA5-453E-9F3D-183E7F52E46B', 'Abluxxen', null, '6F7D65CB-0BF7-439E-A2E2-0DC70D37E179', 2, 5, 20, 20, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('B425095C-36BD-4E74-9DE6-204F1BBAB61A', 'Activity', null, '579176AB-5EAA-484B-87FB-33806252C214', 4, 16, null, null, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('CEDD8157-8CFA-448E-A301-E2DB916212E7', 'Adrenalin', null, '0AF6D8F6-D09B-4C12-9908-A4BB377117BA', 3, 5, 60, 60, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('9082D6FC-C0AF-4762-AE5E-9305D8526636', 'Agricola - Die Bauern und das liebe Vieh', null, '5AFEAAEA-B451-413C-9AF4-C61C71E388D3', 2, 2, 30, 30, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('39638CAE-BBEF-45B1-86C8-3CABE626F170', 'Agricola (Familienspiel)', null, '5AFEAAEA-B451-413C-9AF4-C61C71E388D3', 1, 4, 60, 60, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('5986B3A2-BF4D-4EC0-A65D-79E5771F9404', 'Agricola (Kennerspiel)', null, '5AFEAAEA-B451-413C-9AF4-C61C71E388D3', 1, 4, 90, 90, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('670DE3E8-DB58-4CF7-9994-274CCBA767E8', 'Agricola (Kennerspiel) - 5-6 Spieler', '5986B3A2-BF4D-4EC0-A65D-79E5771F9404', '5AFEAAEA-B451-413C-9AF4-C61C71E388D3', 2, 6, 150, 150, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('CCE27FE9-C5AC-4A0A-B82A-EC26303968B2', 'Alhambra', null, 'F203898B-B16E-4B7C-BB93-4DA741DD5497', 2, 6, 60, 60, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('11BE64CC-1C4E-409F-9A7A-81EF6E321359', 'Amerigo', null, 'F203898B-B16E-4B7C-BB93-4DA741DD5497', 2, 4, 75, 75, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('9413EC6D-1116-483D-B2F7-E993B82013C4', 'Arkham Horror - Das Kartenspiel', null, 'EC30755A-C867-4ED3-936F-BF55FD1C47F2', 1, 2, 120, 120, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('33039BE9-31D3-4FA8-8D9A-7B7B855B0F8D', 'Arkham Horror - Das Kartenspiel - Blut auf dem Altar (Mythos-Pack Dunwich 3)', '9413EC6D-1116-483D-B2F7-E993B82013C4', 'EC30755A-C867-4ED3-936F-BF55FD1C47F2', 1, 2, 120, 120, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('3EC097A1-B0BB-470B-958B-DBBC60EF93F7', 'Arkham Horror - Das Kartenspiel - Das Miskatonic-Museum (Mythos-Pack Dunwich 1)', '9413EC6D-1116-483D-B2F7-E993B82013C4', 'EC30755A-C867-4ED3-936F-BF55FD1C47F2', 1, 2, 120, 120, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('83F314E6-760D-46DF-9559-03F7E92FBD4A', 'Arkham Horror - Das Kartenspiel - Das Vermächtnis von Dunwich', '9413EC6D-1116-483D-B2F7-E993B82013C4', 'EC30755A-C867-4ED3-936F-BF55FD1C47F2', 1, 2, 120, 120, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('D7DC077B-23D4-49DC-B6EF-F924C6B2C125', 'Arkham Horror - Das Kartenspiel - Der Essex-County-Express (Mythos-Pack Dunwich 2)', '9413EC6D-1116-483D-B2F7-E993B82013C4', 'EC30755A-C867-4ED3-936F-BF55FD1C47F2', 1, 2, 120, 120, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('8CDB3979-6615-4FD1-90C5-699AFAA6D68D', 'Arkham Horror - Das Kartenspiel - Der Pfad nach Carcosa', '9413EC6D-1116-483D-B2F7-E993B82013C4', 'EC30755A-C867-4ED3-936F-BF55FD1C47F2', 1, 2, 120, 120, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('5B4FFE3E-4D49-4224-811F-8882022036F7', 'Arkham Horror - Das Kartenspiel - Der unausprechliche Eid (Mythos-Pack Carcosa 2)', '9413EC6D-1116-483D-B2F7-E993B82013C4', 'EC30755A-C867-4ED3-936F-BF55FD1C47F2', 1, 2, 120, 120, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('B941438B-1190-45CD-96CE-A7F236CC235D', 'Arkham Horror - Das Kartenspiel - Gestaltlos und unsichtbar (Mythos-Pack Dunwich 4)', '9413EC6D-1116-483D-B2F7-E993B82013C4', 'EC30755A-C867-4ED3-936F-BF55FD1C47F2', 1, 2, 120, 120, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('F012A3D8-07C5-4DE1-AC17-D5107A91BCDC', 'Arkham Horror - Das Kartenspiel - Verloren in Zeit und Raum (Mythos-Pack Dunwich 6)', '9413EC6D-1116-483D-B2F7-E993B82013C4', 'EC30755A-C867-4ED3-936F-BF55FD1C47F2', 1, 2, 120, 120, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('C4ECF35C-684C-4396-A743-7E8CBD3BEF54', 'Arkham Horror - Das Kartenspiel - Widerhall aus der Vergangenheit (Mythos-Pack Carcosa 1)', '9413EC6D-1116-483D-B2F7-E993B82013C4', 'EC30755A-C867-4ED3-936F-BF55FD1C47F2', 1, 2, 120, 120, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('F1868BD0-7BBA-4611-A5B8-5097523BAD56', 'Arkham Horror - Das Kartenspiel - Wo das Verderben wartet (Mythos-Pack Dunwich 5)', '9413EC6D-1116-483D-B2F7-E993B82013C4', 'EC30755A-C867-4ED3-936F-BF55FD1C47F2', 1, 2, 120, 120, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('585A13F8-EA52-414A-8CBF-FE172823281F', 'Armageddon', null, 'F203898B-B16E-4B7C-BB93-4DA741DD5497', 3, 4, 90, 90, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('1817DEB6-D889-4462-8316-0F589B7F728C', 'Auf den Spuren von Marco Polo', null, '2C763E90-945D-4106-B841-6628A82219AC', 2, 4, 90, 90, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('4A78E006-5651-4713-8D7A-CE9F0D9F826A', 'Azul', null, '8C8C8781-4926-403B-86AC-A4B61975FE31', 2, 4, 45, 45, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('B601B7F4-A99B-4272-ADAC-DEEA3732CF47', 'Bang!', null, '68A28E87-170C-4AC6-A77F-75B49DE67452', 4, 7, 30, 30, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('43A3FC6E-EC69-414D-9B0E-3906D1A37032', 'Barragoon', null, '69209C44-22C9-40CE-AAF6-1E75C5A6C83E', 2, 2, 90, 90, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('35E6583D-6AC0-4C03-B744-7CD876896746', 'Battlestar Galactica', null, 'F849595C-801B-4D95-99B2-5DEBF824DE14', 3, 6, 180, 180, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('DBC6FBB7-FEF0-422C-8DCA-049AA98AF2A9', 'Berge des Wahnsinns', null, 'EE826793-FA24-4020-8F8F-792E88F205F6', 3, 5, 60, 60, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('ADD19568-F7DD-4D39-9074-3DE36255CB36', 'Boss Monster', null, 'BAE5F53E-6873-4637-A07C-A40C6730ACC2', 2, 4, 60, 60, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('7C46E7F3-B827-41ED-8989-1E35063CE47A', 'Camel Up', null, 'FD455EAE-58C2-4123-9BFF-8877FFC9263F', 2, 8, 30, 30, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('C3183FAE-FD59-45F5-9B1A-9293B2A7235E', 'Camel Up - Supercup', '7C46E7F3-B827-41ED-8989-1E35063CE47A', 'FD455EAE-58C2-4123-9BFF-8877FFC9263F', 2, 10, 30, 30, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('68EDD0A8-CE28-4040-A069-DD3622B971DB', 'Captain Sonar', null, '767C317F-B4BE-4D84-A873-EB0BE1B41962', 2, 8, 60, 60, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('BCC0A9D0-87E9-4832-BFD2-8DB38D1C8AE4', 'Captain Sonar - Volles Rohr', '68EDD0A8-CE28-4040-A069-DD3622B971DB', '767C317F-B4BE-4D84-A873-EB0BE1B41962', 2, 8, 60, 60, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('883D0F36-BA48-475B-A64D-430C6DCEDDA0', 'Carcassonne', null, '2C763E90-945D-4106-B841-6628A82219AC', 2, 5, 30, 30, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('B8BE2745-CB60-44E5-883A-EF3C9F866E10', 'Carcassonne - Händler und Baumeister', '883D0F36-BA48-475B-A64D-430C6DCEDDA0', '2C763E90-945D-4106-B841-6628A82219AC', 2, 6, 40, 40, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('29CF6E49-8B34-4457-BE0A-260BF0BC6E7C', 'Carcassonne - Wirtshäuser und Kathedralen', '883D0F36-BA48-475B-A64D-430C6DCEDDA0', '2C763E90-945D-4106-B841-6628A82219AC', 2, 5, 30, 30, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('C7DDBEE7-2540-42ED-BEB9-C967012D8552', 'Carta Impera Victoria', null, 'B69B5032-43A8-44FA-BD21-A4A7604BF5E9', 2, 4, 20, 20, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('06FAA74C-AC79-4D93-82EF-75E490D5FA1C', 'Catan - Das Duell', null, '72E8B7A0-1C25-4DEB-BD7E-2CB339DBC163', 2, 2, 45, 45, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('4721FAB4-B8E9-432C-A6A6-02741EE71E45', 'Caverna - Höhle gegen Höhle', null, '5AFEAAEA-B451-413C-9AF4-C61C71E388D3', 1, 2, 40, 40, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('5E6773A8-620B-4489-8AF5-3C6997C097C3', 'Chicago Express', null, '579176AB-5EAA-484B-87FB-33806252C214', 2, 6, 60, 60, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('388131C6-4120-4DF7-85E1-47EE2B64EE89', 'Codenames', null, '579176AB-5EAA-484B-87FB-33806252C214', 2, 8, 15, 15, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('AA24A784-0644-4880-9A21-1883C7F1A814', 'Colt Express', null, 'B69B5032-43A8-44FA-BD21-A4A7604BF5E9', 2, 6, 30, 30, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('AAAD0FD6-1ABB-4565-8C00-850C01A5DD4E', 'Colt Express - Marshal & Gefangene', 'AA24A784-0644-4880-9A21-1883C7F1A814', 'B69B5032-43A8-44FA-BD21-A4A7604BF5E9', 3, 8, 40, 40, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('6BFD7371-9363-47F0-97F6-26D0CCD55683', 'Colt Express - Postkutsche & Pferde', 'AA24A784-0644-4880-9A21-1883C7F1A814', 'B69B5032-43A8-44FA-BD21-A4A7604BF5E9', 2, 6, 40, 40, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('7F3E6FB1-AC6F-49C6-B569-11E897677DA3', 'Concept', null, '579176AB-5EAA-484B-87FB-33806252C214', 4, 12, 40, 40, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('6CDD9868-4FD6-4485-B38A-F3940B5CAFBC', 'Cottage Garden', null, '579176AB-5EAA-484B-87FB-33806252C214', 1, 4, 60, 60, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('A449F162-96FB-48C0-9010-5DBC50772C02', 'Crazy Race', null, '579176AB-5EAA-484B-87FB-33806252C214', 2, 5, 60, 60, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('36547083-0D1D-4ECE-83D6-8F77A5E6C69B', 'Das Labyrinth der Meister', null, '579176AB-5EAA-484B-87FB-33806252C214', 2, 4, 30, 30, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('78BCEDE3-DF00-42EF-9EBE-5BA821203DFA', 'Das MAD Spiel', null, '579176AB-5EAA-484B-87FB-33806252C214', 2, 4, 60, 60, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('A209B7C8-1EB8-4E57-BEFA-D65EA9095410', 'Das Orakel von Delphi', null, '579176AB-5EAA-484B-87FB-33806252C214', 2, 4, 100, 100, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('939C6FBA-4C00-4168-92FC-F946C4292AD7', 'Decrypto', null, '579176AB-5EAA-484B-87FB-33806252C214', 3, 8, 30, 30, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('11E054D8-33FE-4317-A34B-DBA8D7C82BCB', 'Die Alchemisten', null, '0AF6D8F6-D09B-4C12-9908-A4BB377117BA', 2, 4, 120, 120, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('A754D44B-EDD2-4586-B195-2F6F85DB92C6', 'Die blutige Herberge', null, '579176AB-5EAA-484B-87FB-33806252C214', 1, 4, 60, 60, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('64EDCCE8-11CC-4EAC-BA1A-138E2F9F99EB', 'Die Grimoire des Wahnsinns', null, '579176AB-5EAA-484B-87FB-33806252C214', 2, 5, 90, 90, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('56AEC2B6-B022-4A91-B58F-C4B05A6F0592', 'Die holde Isolde', null, '579176AB-5EAA-484B-87FB-33806252C214', 2, 5, 30, 30, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('0DCA2D1A-6845-4375-A488-C0E867EA90F8', 'Die Legenden von Andor', null, '72E8B7A0-1C25-4DEB-BD7E-2CB339DBC163', 2, 4, 90, 90, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('1A76B5DD-B9E8-4B54-8A43-ACCE111605C2', 'Die Legenden von Andor - Chada und Thorn', '0DCA2D1A-6845-4375-A488-C0E867EA90F8', '72E8B7A0-1C25-4DEB-BD7E-2CB339DBC163', 2, 2, 45, 45, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('8F089DB6-1B79-4E1D-B022-96985CD9F18A', 'Die Legenden von Andor - Der Sternenschild', '0DCA2D1A-6845-4375-A488-C0E867EA90F8', '72E8B7A0-1C25-4DEB-BD7E-2CB339DBC163', 2, 4, 90, 90, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('D5E07C29-0992-4701-9412-D2B53449F4EE', 'Die Legenden von Andor - Die Reise in den Norden', '0DCA2D1A-6845-4375-A488-C0E867EA90F8', '72E8B7A0-1C25-4DEB-BD7E-2CB339DBC163', 2, 4, 60, 60, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('D2728977-3F57-4822-A006-D842A501A86D', 'Die Legenden von Andor - Neue Helden', '0DCA2D1A-6845-4375-A488-C0E867EA90F8', '72E8B7A0-1C25-4DEB-BD7E-2CB339DBC163', 5, 6, 75, 75, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('6514AB74-013D-4DA8-86A9-3DC949F77AED', 'Die Portale von Molthar', null, '579176AB-5EAA-484B-87FB-33806252C214', 2, 5, 45, 45, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('E04D568C-C583-4888-9F4E-377094434063', 'Die Quacksalber von Quedlinburg', null, '5F647A2B-57B4-429D-8D82-449C385F0DFF', 2, 4, 45, 45, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('B895B5A3-2BD7-49F6-96E8-AC6BF916EDDA', 'Die Siedler von Catan', null, '72E8B7A0-1C25-4DEB-BD7E-2CB339DBC163', 3, 4, 75, 75, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('740252B3-6E4B-4E79-973C-A5DE36530693', 'Die Siedler von Catan - 5-6 Spieler', 'B895B5A3-2BD7-49F6-96E8-AC6BF916EDDA', '72E8B7A0-1C25-4DEB-BD7E-2CB339DBC163', 3, 6, 90, 90, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('C2B83001-E8C9-4F2D-90D3-74B0A6BA8F99', 'Die verbotene Insel', null, '579176AB-5EAA-484B-87FB-33806252C214', 2, 4, 30, 30, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('E004F1AD-E404-456D-82E9-2EE754CA2B02', 'Die Zwerge - Das Duell', null, '579176AB-5EAA-484B-87FB-33806252C214', 2, 2, 20, 20, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('2DDD0247-0973-4771-9254-B3F1D75C8A84', 'Dixit', null, '579176AB-5EAA-484B-87FB-33806252C214', 3, 6, 30, 30, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('80165333-CD21-4FD6-B4FF-92339023DF5A', 'Do De Li Do', null, '579176AB-5EAA-484B-87FB-33806252C214', 2, 6, 20, 20, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('02BCE846-0407-458D-9D11-FE471CAB204A', 'Dominion', null, '2C763E90-945D-4106-B841-6628A82219AC', 2, 4, 60, 60, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('C119A2C9-0564-49E4-A5A2-DE580F2D4BE8', 'Drecksau', null, '579176AB-5EAA-484B-87FB-33806252C214', 2, 4, 20, 20, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('CD0EA0B7-9B8D-4DB1-B203-7005022234B5', 'Dungeon Lords', null, '0AF6D8F6-D09B-4C12-9908-A4BB377117BA', 2, 4, 90, 90, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('2FFF25B9-A5BC-46D4-B1E8-22CCE37A1E03', 'Ein Fest für Odin', null, '579176AB-5EAA-484B-87FB-33806252C214', 1, 4, 120, 120, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('D970C12F-BA3C-43D9-8843-12B0DEBD7FE9', 'ekö', null, '579176AB-5EAA-484B-87FB-33806252C214', 2, 4, null, null, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('1D3EE804-3650-48E6-B0FC-9C23EEC9095A', 'Elfer Raus', null, '579176AB-5EAA-484B-87FB-33806252C214', 2, 6, 20, 20, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('11695870-B396-48BD-B1D7-278B720D9821', 'Elysium', null, '579176AB-5EAA-484B-87FB-33806252C214', 2, 4, null, null, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('8A6BABDE-AF95-4F12-96AE-64F21DBEDAA3', 'Epic PvP', null, 'BAE5F53E-6873-4637-A07C-A40C6730ACC2', 2, 2, 30, 30, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('DBDF1C0B-F813-45DA-B66C-00632E2B113F', 'Epic PvP - Dunkelelf, Ork, Barbar, Mönch', '8A6BABDE-AF95-4F12-96AE-64F21DBEDAA3', 'BAE5F53E-6873-4637-A07C-A40C6730ACC2', 2, 2, 30, 30, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('2388E667-2ECA-4967-BEB0-88DAC67B5F56', 'Epic PvP - Halbling, Katzenmensch, Dunkler Ritter, Kleriker', '8A6BABDE-AF95-4F12-96AE-64F21DBEDAA3', 'BAE5F53E-6873-4637-A07C-A40C6730ACC2', 2, 2, 30, 30, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('0C09626F-CEE1-4CE4-BABA-B2FE602DE46F', 'Escape - Der Fluch des Tempels', null, 'F203898B-B16E-4B7C-BB93-4DA741DD5497', 2, 5, 10, 10, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('FC16DF4B-DED4-4429-A275-12E25F080B65', 'Escape - Zombie City', null, 'F203898B-B16E-4B7C-BB93-4DA741DD5497', 2, 4, 20, 20, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('019EBA74-3D95-482A-A981-A86776CE28BE', 'Exploding Kittens', null, '579176AB-5EAA-484B-87FB-33806252C214', 2, 5, 15, 15, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('5FA611EF-3C93-432D-AC03-37A345A6678D', 'Exploding Kittens - NSFW Edition', null, '579176AB-5EAA-484B-87FB-33806252C214', 2, 5, 15, 15, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('07BC6BF1-F269-4C2F-A2CF-AC4100E61B23', 'Fiese Freunde Fette Feten', null, '579176AB-5EAA-484B-87FB-33806252C214', 2, 6, null, null, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('010F1367-CDF5-4593-8769-A94F84F85719', 'Food Chain Magnate', null, '579176AB-5EAA-484B-87FB-33806252C214', 2, 5, 240, 240, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('478C650A-A103-4870-A079-A0C9FD7B8AA6', 'Fresko', null, '579176AB-5EAA-484B-87FB-33806252C214', 2, 4, 60, 60, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('829482D0-A6BA-417C-82EF-7C22E099A2A4', 'Funkenschlag Fabrikmanager', null, '579176AB-5EAA-484B-87FB-33806252C214', 2, 5, 60, 60, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('E41AAA87-6337-4D68-9B96-5037E75B503C', 'Fußball-Duell', null, '579176AB-5EAA-484B-87FB-33806252C214', 2, 2, 40, 40, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('C7F2D69C-EDBC-42D2-AB0E-B48D61F826BA', 'Futschikato', null, '579176AB-5EAA-484B-87FB-33806252C214', 3, 8, 15, 15, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('073BCF86-63ED-49C5-ABC8-6B018B598D41', 'Gaia', null, '579176AB-5EAA-484B-87FB-33806252C214', 2, 5, 30, 30, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('66025CBC-A5BE-4925-959B-CA74DE5E3088', 'Galaxy Trucker', null, '579176AB-5EAA-484B-87FB-33806252C214', 2, 4, 60, 60, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('35FFB2E6-898C-48F8-8667-872AC39C13F1', 'Galileo - The Game', null, '579176AB-5EAA-484B-87FB-33806252C214', 2, 6, 30, 30, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('049523D7-8C43-42BF-91E5-E1BB93115EF6', 'Game of Trains', null, '579176AB-5EAA-484B-87FB-33806252C214', 2, 4, 20, 20, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('B29B4106-4DB4-4049-A66B-2ED60E5670E1', 'Ganz schön clever', null, '5F647A2B-57B4-429D-8D82-449C385F0DFF', 1, 4, 20, 20, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('68CBAF3B-68D3-4EF3-879B-E5569F2276F5', 'Gentes', null, '579176AB-5EAA-484B-87FB-33806252C214', 1, 4, 90, 90, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('07C6D4B4-76E3-422E-8F0E-494D78FF67CA', 'Giants', null, '579176AB-5EAA-484B-87FB-33806252C214', 3, 5, 60, 60, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('CEB0E64B-B269-4E4C-BD59-F7ED8BB90A03', 'Great Western Trail', null, 'BAE5F53E-6873-4637-A07C-A40C6730ACC2', 2, 4, 150, 150, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('F1FA4B3F-E47E-4B9D-9DA2-28E97A3852AB', 'Great Western Trail - Rail to the North', 'CEB0E64B-B269-4E4C-BD59-F7ED8BB90A03', 'BAE5F53E-6873-4637-A07C-A40C6730ACC2', 2, 4, 150, 150, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('13B852A0-8256-4243-A3A9-1B551FF9411B', 'Gùgōng', null, '579176AB-5EAA-484B-87FB-33806252C214', 1, 5, 90, 90, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('6E9E9C7F-2222-4B6C-9074-DBC5E337874D', 'Halt mal kurz', null, '579176AB-5EAA-484B-87FB-33806252C214', 3, 6, 20, 20, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('A3FD5543-611E-4900-9891-714AE7FA8C96', 'Haus der Sonne', null, '579176AB-5EAA-484B-87FB-33806252C214', 2, 2, 40, 40, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('0B341063-0DE9-4FF3-BD9A-F460B527FE6E', 'Heaven & Ale', null, 'FD455EAE-58C2-4123-9BFF-8877FFC9263F', 2, 4, 90, 90, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('22E94F0A-11AB-43FE-9E10-53D591766311', 'Heckmeck am Bratwurmeck', null, '579176AB-5EAA-484B-87FB-33806252C214', 2, 7, 30, 30, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('E1D47477-A75C-41C7-9EB0-F3E12D9EC004', 'Hit Z Road', null, '579176AB-5EAA-484B-87FB-33806252C214', 1, 4, 60, 60, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('43A2BBD5-1D5D-4ABC-8C92-4CADA3A5729E', 'Hoftheater', null, '579176AB-5EAA-484B-87FB-33806252C214', 2, 5, 40, 40, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('29051000-9D56-41D1-B4D5-DB4323AA63DD', 'Imaginarium', null, '579176AB-5EAA-484B-87FB-33806252C214', 2, 5, 90, 90, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('578AA2AB-AF21-49D2-BB82-F93E99797F10', 'Imhotep', null, '72E8B7A0-1C25-4DEB-BD7E-2CB339DBC163', 2, 4, 40, 40, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('1FF967CE-B4F7-4C13-A1E4-AC6CF7519EB1', 'Imhotep - Eine neue Dynastie', null, '72E8B7A0-1C25-4DEB-BD7E-2CB339DBC163', 2, 4, 50, 50, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('69187B47-09A2-4EB7-923E-E0CF57290FB6', 'Inis', null, '579176AB-5EAA-484B-87FB-33806252C214', 2, 4, 60, 60, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('8C0E584E-5B48-42AD-8C04-DBB68E434D6E', 'Isle of Skye', null, '5AFEAAEA-B451-413C-9AF4-C61C71E388D3', 2, 5, 60, 60, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('0832A6B3-ED5E-4C83-93B8-D55C104D88EF', 'Isle of Skye - Der Wanderer', '8C0E584E-5B48-42AD-8C04-DBB68E434D6E', '5AFEAAEA-B451-413C-9AF4-C61C71E388D3', 2, 5, 60, 60, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('F8E3A417-6C7C-4A98-9203-0EE5F4596A35', 'Isola', null, '579176AB-5EAA-484B-87FB-33806252C214', 2, 2, 15, 15, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('285F5601-1535-468B-A443-4589ABC5F800', 'Istanbul', null, 'BAE5F53E-6873-4637-A07C-A40C6730ACC2', 2, 5, 50, 50, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('974D1366-202B-4C2E-8950-526434508858', 'Istanbul - Brief & Siegel', '285F5601-1535-468B-A443-4589ABC5F800', 'BAE5F53E-6873-4637-A07C-A40C6730ACC2', 2, 5, 50, 50, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('CD110B0C-00A2-4F62-B2F6-A2FE68311CA3', 'Istanbul - Das Würfelspiel', null, 'BAE5F53E-6873-4637-A07C-A40C6730ACC2', 2, 4, 40, 40, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('241F305A-258C-4DF7-BD46-B43D8700892C', 'Istanbul - Mokka & Backschich', '285F5601-1535-468B-A443-4589ABC5F800', 'BAE5F53E-6873-4637-A07C-A40C6730ACC2', 2, 5, 50, 50, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('96D146E3-22D9-49CF-BA10-12EEF2C72FDB', 'Jäger + Späher', null, '579176AB-5EAA-484B-87FB-33806252C214', 2, 2, 60, 60, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('CA927B7D-F5F5-43EE-BDE1-AB8E8DD293FA', 'Jenseits von Theben', null, '579176AB-5EAA-484B-87FB-33806252C214', 2, 4, 60, 60, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('ABB9F35C-93B0-4E63-BD8F-7C0896C5B8D2', 'Junta', null, '579176AB-5EAA-484B-87FB-33806252C214', 2, 7, 120, 120, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('7453CB53-2DEB-4A8C-B7EF-F9A4F12E9710', 'Kahuna', null, '579176AB-5EAA-484B-87FB-33806252C214', 2, 2, 40, 40, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('13D45015-C762-4BA5-ADF9-11E9E8EE90BC', 'Kampf um den Olymp', null, '579176AB-5EAA-484B-87FB-33806252C214', 2, 2, 30, 30, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('8314DE0E-D496-49E5-8709-9CACB4908E9B', 'Kanban - Automotive Revolution', null, '579176AB-5EAA-484B-87FB-33806252C214', 2, 4, 120, 120, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('FACAB68C-B4F1-4254-978B-A33076E1E222', 'Karuba', null, '579176AB-5EAA-484B-87FB-33806252C214', 2, 4, 40, 40, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('F16908F8-8811-4DD9-9773-0E69A096FE15', 'Kingdom Builder', null, '579176AB-5EAA-484B-87FB-33806252C214', 2, 4, 45, 45, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('65CCAD52-ACD9-4BDD-9208-4B9743E250ED', 'Kingdom Builder - Nomads', 'F16908F8-8811-4DD9-9773-0E69A096FE15', '579176AB-5EAA-484B-87FB-33806252C214', 2, 5, 45, 45, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('BB943E77-DDCD-46DB-9A0E-C8A8CE82F4A5', 'Kingdomino', null, '579176AB-5EAA-484B-87FB-33806252C214', 2, 4, 30, 30, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('EFC8A59F-816E-4838-B174-5FA69E6E1341', 'Klong!', null, '579176AB-5EAA-484B-87FB-33806252C214', 2, 4, null, null, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('C8E74AB9-41F2-47E7-BB17-8E0ED30A53A9', 'Labyrinth - Das Duell', null, '579176AB-5EAA-484B-87FB-33806252C214', 2, 2, 20, 20, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('81D1B407-7922-4804-B052-AF83AB6B4CE2', 'Leaders', null, '579176AB-5EAA-484B-87FB-33806252C214', 2, 6, 180, 180, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('4674E54B-63AA-4D63-A44C-FC1A63C7C631', 'Les Poilus', null, '579176AB-5EAA-484B-87FB-33806252C214', 2, 5, 30, 30, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('61E7FC15-611A-484B-BF91-FE69C7468FE3', 'Loot Island', null, '579176AB-5EAA-484B-87FB-33806252C214', 2, 5, 60, 60, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('70C4BF5B-774C-4A2D-A8E1-388459044852', 'Lost Cities', null, '579176AB-5EAA-484B-87FB-33806252C214', 2, 2, 40, 40, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('F88AFDF1-645B-426D-9A26-AFE9007E6BCF', 'Lost Galaxy', null, '579176AB-5EAA-484B-87FB-33806252C214', 2, 4, 15, 15, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('593B1B0B-57E8-4C2D-8AEC-8DF73B91C258', 'Love Letter', null, '579176AB-5EAA-484B-87FB-33806252C214', 2, 4, 20, 20, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('667236D6-207B-4C88-8F8B-A6B1DCD908C4', 'Luxor', null, '579176AB-5EAA-484B-87FB-33806252C214', 2, 4, 45, 45, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('F6672718-39D9-46C5-9211-45544B135723', 'Machi Koro', null, '72E8B7A0-1C25-4DEB-BD7E-2CB339DBC163', 2, 4, 30, 30, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('4EC2FD40-372D-4C58-A599-B28AB39FAE3D', 'Machi Koro - Großstadt', 'F6672718-39D9-46C5-9211-45544B135723', '72E8B7A0-1C25-4DEB-BD7E-2CB339DBC163', 2, 4, 40, 40, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('6D3176EF-D51A-4629-B74D-7E5EC514BF87', 'Magic Maze', null, '579176AB-5EAA-484B-87FB-33806252C214', 1, 8, 15, 15, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('20263A53-C360-4512-A282-7CB8F15D30AB', 'Maharani - Mosaic Palace', null, '579176AB-5EAA-484B-87FB-33806252C214', 2, 4, 40, 40, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('CDA55509-4E8D-4F9B-AFB1-4D6CFAFA08C1', 'Majesty - Deine Krone, dein Königreich', null, '579176AB-5EAA-484B-87FB-33806252C214', 2, 4, 30, 30, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('4EC2ADC6-51BE-4B7B-BB2A-E7282192AE61', 'Memo Dice', null, '579176AB-5EAA-484B-87FB-33806252C214', 2, 4, 10, 10, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('470D38EE-0124-4C01-9237-5BA2355839CC', 'Memoarrr!', null, '579176AB-5EAA-484B-87FB-33806252C214', 2, 4, 20, 20, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('ADB16C86-8F08-44E7-B25B-42CFF53DAEE3', 'Menara', null, '579176AB-5EAA-484B-87FB-33806252C214', 1, 4, 45, 45, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('A9A3FA85-7AC8-4EF1-88BF-BBEEBA4D797E', 'Mercado', null, '579176AB-5EAA-484B-87FB-33806252C214', 2, 4, 30, 30, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('2F34A425-D9E2-4B2B-8974-90A680D49F08', 'Metro', null, '579176AB-5EAA-484B-87FB-33806252C214', 2, 6, 45, 45, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('11E15132-6F75-4D3C-A43D-9A17CB8952FC', 'Mixmo', null, '579176AB-5EAA-484B-87FB-33806252C214', 2, 6, 15, 15, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('12230528-C9FB-4511-B23C-3DDBBE793B1A', 'Mombasa', null, '579176AB-5EAA-484B-87FB-33806252C214', 2, 4, 150, 150, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('8D46FD79-B15B-437C-AA9B-4C654DD35026', 'Monopoly', null, '579176AB-5EAA-484B-87FB-33806252C214', 2, 8, 240, 240, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('229BE380-7129-47F7-B6CC-1C1F21B843C8', 'Moorea', null, '579176AB-5EAA-484B-87FB-33806252C214', 2, 5, 30, 30, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('FE177D2B-0B5D-46EB-A113-81CD9FCE4775', 'Munchkin', null, 'BAE5F53E-6873-4637-A07C-A40C6730ACC2', 3, 6, 60, 60, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('E85C892A-AFB1-46AA-A2AB-F67378CE5819', 'Munchkin 2: Abartige Axt', 'FE177D2B-0B5D-46EB-A113-81CD9FCE4775', 'BAE5F53E-6873-4637-A07C-A40C6730ACC2', 3, 6, 60, 60, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('EECC55EA-26F6-45AA-8F6A-99025044BF91', 'Munchkin 3: Beschwörungsfehler', 'FE177D2B-0B5D-46EB-A113-81CD9FCE4775', 'BAE5F53E-6873-4637-A07C-A40C6730ACC2', 3, 6, 60, 60, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('36F84A54-4971-4428-8E3E-E899B7D04BAD', 'Munchkin 4: Rasende Rösser', 'FE177D2B-0B5D-46EB-A113-81CD9FCE4775', 'BAE5F53E-6873-4637-A07C-A40C6730ACC2', 3, 6, 60, 60, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('13D52346-B5FE-471F-8B22-2EE62C6040A1', 'Munchkin 5: Wirre Waldläufer', 'FE177D2B-0B5D-46EB-A113-81CD9FCE4775', 'BAE5F53E-6873-4637-A07C-A40C6730ACC2', 3, 6, 60, 60, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('FCD61FF3-F6A7-4A55-B8B4-4EE879A3AEC4', 'Munchkin 6.5: Grausige Grüfte', 'FE177D2B-0B5D-46EB-A113-81CD9FCE4775', 'BAE5F53E-6873-4637-A07C-A40C6730ACC2', 3, 6, 60, 60, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('7B93D12D-A676-42DF-9217-3B2118659A1D', 'Munchkin 6: Durchgeknallte Dungeons', 'FE177D2B-0B5D-46EB-A113-81CD9FCE4775', 'BAE5F53E-6873-4637-A07C-A40C6730ACC2', 3, 6, 60, 60, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('1E0BD59B-CDBA-45CE-9568-76B47E180231', 'Munchkin 7: Mit beiden Händen schummeln', 'FE177D2B-0B5D-46EB-A113-81CD9FCE4775', 'BAE5F53E-6873-4637-A07C-A40C6730ACC2', 3, 6, 60, 60, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('2577C0DE-CC96-426E-96F3-756B36412C86', 'Munchkin 8: Echsenmenschen und Zentauren', 'FE177D2B-0B5D-46EB-A113-81CD9FCE4775', 'BAE5F53E-6873-4637-A07C-A40C6730ACC2', 3, 6, 60, 60, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('DCCE13C2-6EBE-4F55-9375-66A61A2A567A', 'Munchkin Panic', null, 'BAE5F53E-6873-4637-A07C-A40C6730ACC2', 1, 6, 60, 60, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('73AEA4C3-67A6-44CD-963F-2E9FB80C8EAB', 'Munchkin Quest', null, 'BAE5F53E-6873-4637-A07C-A40C6730ACC2', 2, 4, 180, 180, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('15549408-2B28-496A-B1E0-8EFC9A2FDF9E', 'Mythotopia', null, '579176AB-5EAA-484B-87FB-33806252C214', 2, 4, 60, 60, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('E33EC3F8-2851-456D-89EE-D1D25208AB3B', 'Nmbr9', null, '579176AB-5EAA-484B-87FB-33806252C214', 1, 4, 20, 20, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('15B40C76-5B37-4A2B-86E0-F4C637D39F3C', 'Nobody is Perfect', null, '579176AB-5EAA-484B-87FB-33806252C214', 3, 10, 60, 60, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('A074F3F3-96D0-4031-8008-4FE420BB4713', 'Noria', null, '579176AB-5EAA-484B-87FB-33806252C214', 2, 4, 120, 120, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('0E043E67-7B3C-4647-9001-487F20D5A669', 'Not Alone', null, '579176AB-5EAA-484B-87FB-33806252C214', 2, 7, 45, 45, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('44210C39-85B6-46C3-899E-F2CA6A560530', 'Numeri', null, '579176AB-5EAA-484B-87FB-33806252C214', 2, 4, 30, 30, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('11DC8C7B-928E-4B6B-990E-21B35FA4EC01', 'Ohne Furcht und Adel', null, '579176AB-5EAA-484B-87FB-33806252C214', 3, 7, 60, 60, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('1A667C5F-4705-464A-A243-B97D570198F5', 'Ohne Furcht und Adel - Die dunklen Lande', '11DC8C7B-928E-4B6B-990E-21B35FA4EC01', '579176AB-5EAA-484B-87FB-33806252C214', 3, 7, 60, 60, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('3FD38E60-3635-4487-9F1A-D9314AA0E8E9', 'Orléans', null, '579176AB-5EAA-484B-87FB-33806252C214', 2, 4, 90, 90, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('9AB949EC-CA83-4BF1-A833-98A8688684AC', 'Otys', null, '579176AB-5EAA-484B-87FB-33806252C214', 2, 4, null, null, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('B5344F07-6BB8-4054-ADF5-FD5DE1B1B9B8', 'Outlive', null, '579176AB-5EAA-484B-87FB-33806252C214', 2, 4, 90, 90, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('D9551C9B-951D-4EE3-A2AD-05515CDCB676', 'P. I.', null, '579176AB-5EAA-484B-87FB-33806252C214', 2, 5, 45, 45, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('05FE0E56-E514-4077-9285-D2D7EACE5303', 'Paku Paku', null, '579176AB-5EAA-484B-87FB-33806252C214', 2, 8, 10, 10, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('BF6AFD5E-3EDB-4886-9374-4A94A569266B', 'Pandemie', null, '579176AB-5EAA-484B-87FB-33806252C214', 2, 4, 45, 45, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('C0E0264A-6FC4-4280-8181-6FC55ED0D5CE', 'Phase 10 - Das Brettspiel', null, '579176AB-5EAA-484B-87FB-33806252C214', 2, 6, 60, 60, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('88555477-A992-4289-BD2C-4F061CC36B47', 'Photosynthese', null, '579176AB-5EAA-484B-87FB-33806252C214', 2, 4, 60, 60, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('155E4F6C-368C-4FA2-B388-4D4FCE8A359C', 'Port Royal', null, 'BAE5F53E-6873-4637-A07C-A40C6730ACC2', 2, 4, 40, 40, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('822F3D4E-92BF-4C9E-ABA8-82532EF4B486', 'Port Royal - Das Abenteuer beginnt...', '155E4F6C-368C-4FA2-B388-4D4FCE8A359C', 'BAE5F53E-6873-4637-A07C-A40C6730ACC2', 2, 4, 40, 40, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('AB99BD1E-44E8-4572-8A76-F64774D2B252', 'Port Royal - Ein Auftrag geht noch...', '155E4F6C-368C-4FA2-B388-4D4FCE8A359C', 'BAE5F53E-6873-4637-A07C-A40C6730ACC2', 2, 4, 40, 40, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('DEB3BDC9-641D-4901-BB24-391BE1C51739', 'Privacy 2', null, '579176AB-5EAA-484B-87FB-33806252C214', 5, 12, 45, 45, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('BFAB2E65-3973-4B9D-A2F0-0761D70E9AAF', 'Queendomino', null, '579176AB-5EAA-484B-87FB-33806252C214', 2, 4, 40, 40, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('8C9FDDCD-A3C4-41B4-BFD4-204F32BF0861', 'Qwixx', null, '579176AB-5EAA-484B-87FB-33806252C214', 2, 5, 20, 20, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('96F9374E-8877-4727-9D79-D58212B13B9F', 'Race for the Galaxy', null, '579176AB-5EAA-484B-87FB-33806252C214', 2, 4, 60, 60, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('22C47D49-FFAB-49F7-9D34-9B0E91E90F4A', 'Rajas of the Ganges', null, '579176AB-5EAA-484B-87FB-33806252C214', 2, 4, 75, 75, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('E250CA4F-C4EE-4116-B628-DD8D05D19AE9', 'Raptor', null, '579176AB-5EAA-484B-87FB-33806252C214', 2, 2, 30, 30, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('35D65DAD-36E4-42DF-9EA5-4208771D4FE4', 'Räuber der Nordsee', null, '579176AB-5EAA-484B-87FB-33806252C214', 2, 4, 80, 80, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('969CEAA9-FFC5-4D74-96BA-29D780DB7A71', 'Raxxon', null, '579176AB-5EAA-484B-87FB-33806252C214', 1, 4, null, null, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('D01CA390-8F5D-4AFE-8F7B-F616FBC6275A', 'Risiko', null, '579176AB-5EAA-484B-87FB-33806252C214', 2, 5, 90, 90, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('234D4745-2D22-4AFF-8291-8479C3928021', 'Robinson Crusoe', null, 'BAE5F53E-6873-4637-A07C-A40C6730ACC2', 1, 4, 120, 120, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('7C5F18E4-EB0E-402E-A518-FB4B9F485380', 'Robinson Crusoe - Die Fahrt der Beagle', '234D4745-2D22-4AFF-8291-8479C3928021', 'BAE5F53E-6873-4637-A07C-A40C6730ACC2', 1, 4, 120, 120, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('CDC87B86-6189-460F-8F0F-5C1DD369B5FD', 'Rosenkönig', null, '579176AB-5EAA-484B-87FB-33806252C214', 2, 2, 40, 40, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('C555835D-8676-4DDF-9034-1D88197175FE', 'Rumms - Voll auf die Krone', null, '579176AB-5EAA-484B-87FB-33806252C214', 2, 4, 20, 20, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('BCEBA3EE-5CAC-4000-8B9C-086E80F5DC27', 'Santorini', null, '579176AB-5EAA-484B-87FB-33806252C214', 2, 4, null, null, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('C77731A0-55DE-4650-81E2-0A585A94E813', 'SchatzJäger', null, '579176AB-5EAA-484B-87FB-33806252C214', 2, 6, 45, 45, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('3BDAAA7E-B793-4E1B-B0B5-64FA1B3B3CC7', 'Scotland Yard', null, '6F7D65CB-0BF7-439E-A2E2-0DC70D37E179', 2, 6, 45, 45, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('4DAA3A03-3395-4F87-BDF8-15416D4B93AE', 'Scrabble', null, '579176AB-5EAA-484B-87FB-33806252C214', 2, 4, 90, 90, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('025D5EC1-A371-4832-BE45-40DD21E50D30', 'Scythe', null, '1714A25E-66DF-4DEF-A5C4-D1C102E1BA43', 1, 5, 115, 115, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('551BC8E5-924F-415E-89FC-9481CB0BD3D5', 'Scythe - Invasoren aus der Ferne', '025D5EC1-A371-4832-BE45-40DD21E50D30', '1714A25E-66DF-4DEF-A5C4-D1C102E1BA43', 1, 7, 120, 120, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('722E81EB-FA93-48AA-B41E-A7E295FD79E4', 'Scythe - Kolosse der Lüfte', '025D5EC1-A371-4832-BE45-40DD21E50D30', '1714A25E-66DF-4DEF-A5C4-D1C102E1BA43', 1, 7, 120, 120, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('F66FA62C-FAAC-44BD-BF77-FA10EEFA62C3', 'Sea of Clouds', null, 'EE826793-FA24-4020-8F8F-792E88F205F6', 2, 4, 40, 40, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('88894445-48C5-41BE-855E-2349B49EAA32', 'Sequence', null, '579176AB-5EAA-484B-87FB-33806252C214', 2, 12, 45, 45, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('174D35D4-F38E-4AC8-903E-856549742A18', 'Shogun', null, '579176AB-5EAA-484B-87FB-33806252C214', 3, 5, 240, 240, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('F7602D80-74AA-4AFC-A3BD-14E158369E5B', 'Spiel des Wissens', null, '579176AB-5EAA-484B-87FB-33806252C214', 2, 6, 90, 90, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('40303998-2DC6-4A8B-BC8D-CC4ACAD4F34C', 'St. Petersburg', null, '2C763E90-945D-4106-B841-6628A82219AC', 2, 4, 60, 60, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('E8207978-40D1-4C71-9A48-D3544EC3C720', 'St. Petersburg - In bester Gesellschaft - Das Bankett', '40303998-2DC6-4A8B-BC8D-CC4ACAD4F34C', '2C763E90-945D-4106-B841-6628A82219AC', 2, 5, 30, 30, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('DD6FD171-28B8-48B5-A92D-04C7DE4F2D9A', 'Star Wars: Rebellion', null, 'EC30755A-C867-4ED3-936F-BF55FD1C47F2', 2, 4, null, null, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('F9CBCDE5-F1BE-4099-A052-394E8751A68A', 'Stratego', null, '579176AB-5EAA-484B-87FB-33806252C214', 2, 2, 45, 45, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('073EC751-EB82-48C6-A030-C0AB5B4E659B', 'Sushi Dice', null, '579176AB-5EAA-484B-87FB-33806252C214', 2, 6, 20, 20, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('A7937D54-AD31-4085-B9E9-B8CF00C2992D', 'Tabu', null, '579176AB-5EAA-484B-87FB-33806252C214', 4, 99, 90, 90, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('9EE3159D-8E3C-4BF8-9E3A-3C61F8907A18', 'Ta-Ke', null, '579176AB-5EAA-484B-87FB-33806252C214', 2, 2, 30, 30, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('C8BD31AD-1C07-44B2-8AB0-347473CB5ECC', 'Takenoko', null, '579176AB-5EAA-484B-87FB-33806252C214', 2, 4, 60, 60, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('AEEDD075-A8BF-4CD8-9379-CAB56064A2B8', 'Targi', null, '72E8B7A0-1C25-4DEB-BD7E-2CB339DBC163', 2, 2, 60, 60, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('B6AC92BD-8E0E-4C62-9F22-ED3BAA2B1AEA', 'Targi - Die Erweiterung', 'AEEDD075-A8BF-4CD8-9379-CAB56064A2B8', '72E8B7A0-1C25-4DEB-BD7E-2CB339DBC163', 2, 2, 60, 60, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('DE3171AA-6C46-47C0-BC02-D0C475C9506C', 'Tempel des Schreckens', null, '579176AB-5EAA-484B-87FB-33806252C214', 3, 10, 15, 15, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('19455DA1-7D53-4146-B9F0-16B3A1216A4D', 'Terraforming Mars', null, '579176AB-5EAA-484B-87FB-33806252C214', 1, 5, 120, 120, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('D110DC93-BEC0-4776-8B6F-DD2B6DABC070', 'The Cave', null, 'BAE5F53E-6873-4637-A07C-A40C6730ACC2', 2, 5, 60, 60, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('07AF3FFB-1866-45E2-AD91-47DC71F4A1C1', 'The Game', null, '579176AB-5EAA-484B-87FB-33806252C214', 1, 5, 20, 20, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('F046051F-4086-4421-9A75-8522C593E9C3', 'The Lost Expedition', null, '579176AB-5EAA-484B-87FB-33806252C214', 1, 5, 50, 50, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('4A5C55C4-DF87-4D5F-9BC0-A72A8AD8979C', 'The Lost Expedition - The Fountain of Youth', 'F046051F-4086-4421-9A75-8522C593E9C3', '579176AB-5EAA-484B-87FB-33806252C214', 1, 5, 50, 50, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('4EBEAA88-D3AB-4932-AB6F-E68B2CA9A4A6', 'The Manhattan Project', null, '579176AB-5EAA-484B-87FB-33806252C214', 2, 5, 90, 90, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('9084D1B8-56F2-4238-BF6B-8169710526F4', 'The Pursuit of Happiness', null, 'FF19A341-8372-423A-B82F-2C36031681EA', 1, 4, 90, 90, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('59451AA4-DA4A-4F8F-A076-6DB8D0DC7B03', 'This War of Mine - Das Brettspiel', null, '579176AB-5EAA-484B-87FB-33806252C214', 1, 6, 120, 120, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('960DEF58-0405-40C1-9A19-DE78B950C4EE', 'Thurn und Taxis', null, '579176AB-5EAA-484B-87FB-33806252C214', 2, 4, 50, 50, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('1E852C6B-FB0C-4C49-B162-9C64E2BA2B89', 'Tichu', null, '579176AB-5EAA-484B-87FB-33806252C214', 3, 10, 30, 30, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('400682B7-0017-4BE2-9090-FCDC6C454363', 'Tiefseeabenteuer', null, '579176AB-5EAA-484B-87FB-33806252C214', 3, 6, 30, 30, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('A3F5432F-7FD3-4809-9EA7-0D1C3A428281', 'Time Arena', null, '579176AB-5EAA-484B-87FB-33806252C214', 2, 4, 10, 10, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('8946FAF9-6C9A-4D13-AC3F-9F672821EB23', 'Tobago', null, '579176AB-5EAA-484B-87FB-33806252C214', 2, 4, 60, 60, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('0049B042-E0F7-4BFA-9EEF-3111AF621F0B', 'Tokaido', null, '579176AB-5EAA-484B-87FB-33806252C214', 2, 5, 45, 45, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('AAE9FAFA-7D1C-4BFB-9878-A311D3D87335', 'Trans America', null, '579176AB-5EAA-484B-87FB-33806252C214', 2, 6, 30, 30, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('ECD65D28-4808-4C05-8E48-BDEBE7923ED3', 'Trivial Pursuit Genus Edition', null, '579176AB-5EAA-484B-87FB-33806252C214', 2, 6, 60, 60, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('A85F0C32-A277-4DFD-8247-B7AABB369FC0', 'Tumult Royal', null, '579176AB-5EAA-484B-87FB-33806252C214', 2, 4, 40, 40, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('EF49DDC6-76C7-4DE6-A23B-310555047D16', 'Unfair', null, '579176AB-5EAA-484B-87FB-33806252C214', 2, 5, 125, 125, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('3EFAFF16-5853-4764-8A9E-7BCF185B1ACE', 'Uno Rummy', null, '579176AB-5EAA-484B-87FB-33806252C214', 2, 4, 45, 45, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('B683BD59-494D-40F4-8A65-AFFF8E997035', 'Village', null, '579176AB-5EAA-484B-87FB-33806252C214', 2, 4, 90, 90, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('95C8A642-3650-4AAF-A1E0-A93127EDCA01', 'Vor den Toren von Loyang', null, '579176AB-5EAA-484B-87FB-33806252C214', 1, 4, 120, 120, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('1A213CCF-1948-4F3A-9329-8E7AF6243BC5', 'Wanted 7', null, '579176AB-5EAA-484B-87FB-33806252C214', 2, 6, 20, 20, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('69E4AD75-32A0-4C57-919D-533E7BB3AF21', 'Was kostet die Welt?', null, '579176AB-5EAA-484B-87FB-33806252C214', 2, 6, 120, 120, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('945D2891-B7F4-4A92-BBE0-07223F0C397D', 'Was ''ne Frage', null, '579176AB-5EAA-484B-87FB-33806252C214', 3, 6, 30, 30, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('23AF2D0D-E19C-406D-9FAE-2D09E1A75408', 'We will Wok you', null, '579176AB-5EAA-484B-87FB-33806252C214', 2, 4, 40, 40, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('6F27F128-6CC5-4F53-850E-3564AC56BE34', 'Wettlauf nach El Dorado', null, '579176AB-5EAA-484B-87FB-33806252C214', 2, 4, 60, 60, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('25FBFB5E-7D3D-46A1-A7E9-F9CFC5C83FD3', 'When I Dream', null, '579176AB-5EAA-484B-87FB-33806252C214', 4, 10, 30, 30, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('89A70D5B-533F-48A1-A428-D54E6B026B8D', 'Wizard', null, '579176AB-5EAA-484B-87FB-33806252C214', 3, 6, 20, 20, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('6B007B51-FDB7-4C6C-9634-0653E55A40FB', 'Würfelkönig', null, '579176AB-5EAA-484B-87FB-33806252C214', 2, 5, null, null, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('D9F32603-ED77-49C2-A327-0026748224F5', 'Xalapa', null, '579176AB-5EAA-484B-87FB-33806252C214', 1, 6, 30, 30, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('356E7929-34A6-44EC-A3C7-9F9421080185', 'Zombicide', null, '579176AB-5EAA-484B-87FB-33806252C214', 2, 6, 180, 180, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('00B1EA68-70E0-4E4C-9594-27C7B6222F89', 'Zombie 15', null, '579176AB-5EAA-484B-87FB-33806252C214', 2, 4, null, null, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('4BCFA1EE-1665-449B-9C17-9D6D84E96459', 'Zooloretto', null, '579176AB-5EAA-484B-87FB-33806252C214', 2, 5, 45, 45, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('EBD04584-4B9B-4D2B-B37B-60E921441971', 'Zooloretto Duell', null, '579176AB-5EAA-484B-87FB-33806252C214', 2, 2, 20, 20, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('1409E0D0-5CC2-40F4-B272-46852CBF0423', 'Zug um Zug', null, '38AAC893-67DE-4FCD-9DA3-E94BDC024DBE', 2, 5, 60, 60, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('CA97F406-4EC3-4EFD-B1A2-74DB3CDE1BE6', 'Zug um Zug - Deutschland', null, '38AAC893-67DE-4FCD-9DA3-E94BDC024DBE', 2, 5, 60, 60, null, null, 0);
INSERT INTO Games (Id, Name, MainGameId, PublisherId, MinPlayers, MaxPlayers, MinDuration, MaxDuration, BuyPrice, BuyDate, MinAge) VALUES ('79F5544A-A0A1-46F8-AAF2-0EC2EE3FD146', 'Zug um Zug - Europa', null, '38AAC893-67DE-4FCD-9DA3-E94BDC024DBE', 2, 5, 60, 60, null, null, 0);
");
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.DropTable(
        name: "GameAuthor");

      migrationBuilder.DropTable(
        name: "GameCategory");

      migrationBuilder.DropTable(
        name: "GameIllustrator");

      migrationBuilder.DropTable(
        name: "GameMechanic");

      migrationBuilder.DropTable(
        name: "Categories");

      migrationBuilder.DropTable(
        name: "Persons");

      migrationBuilder.DropTable(
        name: "Games");

      migrationBuilder.DropTable(
        name: "Mechanics");

      migrationBuilder.DropTable(
        name: "Publishers");
    }
  }
}
