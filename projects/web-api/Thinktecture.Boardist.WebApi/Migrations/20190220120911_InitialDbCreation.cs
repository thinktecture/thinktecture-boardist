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
                    Name = table.Column<string>(maxLength: 250, nullable: true),
                    BoardGameGeekId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Mechanics",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    BoardGameGeekId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mechanics", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Persons",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 250, nullable: true),
                    BoardGameGeekId = table.Column<int>(nullable: true)
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
                    Name = table.Column<string>(maxLength: 250, nullable: true),
                    Priority = table.Column<int>(nullable: false),
                    BoardGameGeekId = table.Column<int>(nullable: true)
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
                        onDelete: ReferentialAction.SetNull);
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
                name: "GameCategory",
                columns: table => new
                {
                    GameId = table.Column<Guid>(nullable: false),
                    CategoryId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameCategory", x => new { x.GameId, x.CategoryId });
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

            migrationBuilder.CreateTable(
                name: "GameMechanic",
                columns: table => new
                {
                    GameId = table.Column<Guid>(nullable: false),
                    MechanicId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameMechanic", x => new { x.GameId, x.MechanicId });
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
EXEC sp_MSforeachtable ""ALTER TABLE ? NOCHECK CONSTRAINT all""

INSERT INTO Categories (Id, Name, BoardGameGeekId) VALUES ('68C2FDF3-9ECC-4991-9E41-069EDFA11CA8', 'Educational', 1094);
INSERT INTO Categories (Id, Name, BoardGameGeekId) VALUES ('8318BE2E-CDB5-4561-92BB-06A48C433EE4', 'Economic', 1021);
INSERT INTO Categories (Id, Name, BoardGameGeekId) VALUES ('852264C9-A989-41FC-A59C-09AB003DFE16', 'Bluffing', 1023);
INSERT INTO Categories (Id, Name, BoardGameGeekId) VALUES ('3AFEA2B4-BCC0-443D-8D8E-0A3E67CF9E7D', 'Card Game', 1002);
INSERT INTO Categories (Id, Name, BoardGameGeekId) VALUES ('9B2D55D8-0B2C-482C-A2D0-13D93EAD0957', 'Travel', 1097);
INSERT INTO Categories (Id, Name, BoardGameGeekId) VALUES ('831F8AB6-1EA7-4FEC-9401-2BA77D340807', 'Real-time', 1037);
INSERT INTO Categories (Id, Name, BoardGameGeekId) VALUES ('51A4B116-ADCD-442A-A0F7-2C62166CB48B', 'Fantasy', 1010);
INSERT INTO Categories (Id, Name, BoardGameGeekId) VALUES ('21B190D2-41DF-4BF4-A142-332DC0052854', 'Farming', 1013);
INSERT INTO Categories (Id, Name, BoardGameGeekId) VALUES ('FC8BC31F-40F1-4D9A-A86B-34786361656C', 'Horror', 1024);
INSERT INTO Categories (Id, Name, BoardGameGeekId) VALUES ('FFB0DB97-A147-42CF-A3F0-37E11E737B9C', 'World War I', 1065);
INSERT INTO Categories (Id, Name, BoardGameGeekId) VALUES ('3138700C-64F6-4305-B7EF-3D57A0648A05', 'Industry / Manufacturing', 1088);
INSERT INTO Categories (Id, Name, BoardGameGeekId) VALUES ('5A91EC0C-AC9B-4723-8B8D-3D93DABBB2D9', 'Puzzle', 1028);
INSERT INTO Categories (Id, Name, BoardGameGeekId) VALUES ('90AB3A59-853D-49EF-90E5-3D9C0BB119B8', 'Deduction', 1039);
INSERT INTO Categories (Id, Name, BoardGameGeekId) VALUES ('3FC2F310-FC3B-4600-9C3D-44418D01164A', 'Territory Building', 1086);
INSERT INTO Categories (Id, Name, BoardGameGeekId) VALUES ('6435A986-7D40-4407-8C56-48DFACF2BE57', 'Expansion for Base-game', 1042);
INSERT INTO Categories (Id, Name, BoardGameGeekId) VALUES ('C831D482-FD1C-4E80-B8A2-4A3D347822A3', 'Dice', 1017);
INSERT INTO Categories (Id, Name, BoardGameGeekId) VALUES ('E81A0F07-E2CF-4010-A623-4C816E2A65EE', 'Nautical', 1008);
INSERT INTO Categories (Id, Name, BoardGameGeekId) VALUES ('44C73A39-4DCF-482E-BAF5-4E53C0B1425C', 'Transportation', 1011);
INSERT INTO Categories (Id, Name, BoardGameGeekId) VALUES ('3D180F26-DC72-4CB4-B477-54AAC47C4642', 'Renaissance', 1070);
INSERT INTO Categories (Id, Name, BoardGameGeekId) VALUES ('0B7A2178-C10F-42E4-A85E-5BE633A1A38A', 'Spies/Secret Agents', 1081);
INSERT INTO Categories (Id, Name, BoardGameGeekId) VALUES ('00470BAC-69A8-4474-8974-5D3030B3619A', 'Adventure', 1022);
INSERT INTO Categories (Id, Name, BoardGameGeekId) VALUES ('043C334D-0D18-4F51-B2FE-6358461A6E00', 'Pirates', 1090);
INSERT INTO Categories (Id, Name, BoardGameGeekId) VALUES ('0262E208-C2A6-44CB-85EB-675C4C2C5688', 'City Building', 1029);
INSERT INTO Categories (Id, Name, BoardGameGeekId) VALUES ('524187E8-85E4-452C-9FC2-7666F9C1B233', 'Medieval', 1035);
INSERT INTO Categories (Id, Name, BoardGameGeekId) VALUES ('DF321D72-46A4-4AB3-93E4-79A624C7759F', 'Party Game', 1030);
INSERT INTO Categories (Id, Name, BoardGameGeekId) VALUES ('B66B7A85-346F-47DD-ADFD-79AF41EAE274', 'Negotiation', 1026);
INSERT INTO Categories (Id, Name, BoardGameGeekId) VALUES ('521FF3FF-E692-451A-B72E-897456CCFE31', 'Word Game', 1025);
INSERT INTO Categories (Id, Name, BoardGameGeekId) VALUES ('4D7DBA0C-1549-402C-BD3F-8AF8842EC659', 'Medical', 2145);
INSERT INTO Categories (Id, Name, BoardGameGeekId) VALUES ('27AE421C-A74A-40EF-BE6A-8B8AE11A6586', 'Fighting', 1046);
INSERT INTO Categories (Id, Name, BoardGameGeekId) VALUES ('DE57D2F0-7785-4744-94A0-8E427FA33458', 'Maze', 1059);
INSERT INTO Categories (Id, Name, BoardGameGeekId) VALUES ('CB14BA0D-BCDB-4E91-B0A6-90B5DCA211A9', 'Civilization', 1015);
INSERT INTO Categories (Id, Name, BoardGameGeekId) VALUES ('09CF3354-2D15-4EF3-900B-9788B4AC2E99', 'Animals', 1089);
INSERT INTO Categories (Id, Name, BoardGameGeekId) VALUES ('CE2739D4-CAF8-4C56-B0DF-A33879641F1E', 'Mythology', 1082);
INSERT INTO Categories (Id, Name, BoardGameGeekId) VALUES ('F5C8EC29-DFB3-4125-A6BB-A8378616C60E', 'Wargame', 1019);
INSERT INTO Categories (Id, Name, BoardGameGeekId) VALUES ('B3B69F88-6B0B-4095-A6AC-A926354998AE', 'Ancient', 1050);
INSERT INTO Categories (Id, Name, BoardGameGeekId) VALUES ('744A8FC4-9A96-44F1-A27B-AC5275B51C22', 'Murder/Mystery', 1040);
INSERT INTO Categories (Id, Name, BoardGameGeekId) VALUES ('B16790EB-2BB9-456C-BF7A-B66A0B20B060', 'Environmental', 1084);
INSERT INTO Categories (Id, Name, BoardGameGeekId) VALUES ('29763546-335E-4F49-B353-BB49402FF171', 'Sports', 1038);
INSERT INTO Categories (Id, Name, BoardGameGeekId) VALUES ('1E96AC37-A1AF-49B9-9723-C06921D882A7', 'Novel-based', 1093);
INSERT INTO Categories (Id, Name, BoardGameGeekId) VALUES ('547802C2-56E5-4753-BCC2-C4E626115C85', 'Action / Dexterity', 1032);
INSERT INTO Categories (Id, Name, BoardGameGeekId) VALUES ('1E77B631-88B4-42FD-B829-CF6944DA8035', 'Abstract Strategy', 1009);
INSERT INTO Categories (Id, Name, BoardGameGeekId) VALUES ('880FF14B-D679-4AB3-97DD-D1E5C2B5E2A6', 'Video Game Theme', 1101);
INSERT INTO Categories (Id, Name, BoardGameGeekId) VALUES ('AA4F9245-C30E-4378-BB42-D44B1EAC0F81', 'Number', 1098);
INSERT INTO Categories (Id, Name, BoardGameGeekId) VALUES ('19D81AE3-B3B1-4452-938E-D504975A23A7', 'Miniatures', 1047);
INSERT INTO Categories (Id, Name, BoardGameGeekId) VALUES ('F9E27215-A485-4208-BB99-D7300EE25625', 'Movies / TV / Radio theme', 1064);
INSERT INTO Categories (Id, Name, BoardGameGeekId) VALUES ('36D0D4D0-1EFB-4A6F-9690-D8DDA743BCCE', 'Trains', 1034);
INSERT INTO Categories (Id, Name, BoardGameGeekId) VALUES ('69815AF8-21B4-4CBD-81EA-DA860B5951F9', 'Humor', 1079);
INSERT INTO Categories (Id, Name, BoardGameGeekId) VALUES ('35A5EAC9-DBEB-4305-A55F-EE0490CA3078', 'Science Fiction', 1016);
INSERT INTO Mechanics (Id, Name, BoardGameGeekId) VALUES ('AD43DD09-0AD2-4E59-8071-0601871B0B41', 'Deck / Pool Building', 2664);
INSERT INTO Mechanics (Id, Name, BoardGameGeekId) VALUES ('8E8CEB06-8EE1-47A3-9EB4-11458C4735D6', 'Betting/Wagering', 2014);
INSERT INTO Mechanics (Id, Name, BoardGameGeekId) VALUES ('E649EF7C-1C48-449B-8B59-160F6518B07D', 'Commodity Speculation', 2013);
INSERT INTO Mechanics (Id, Name, BoardGameGeekId) VALUES ('F7D2DCC4-703A-45FA-B17B-26E06087A9B6', 'Storytelling', 2027);
INSERT INTO Mechanics (Id, Name, BoardGameGeekId) VALUES ('67809247-1524-4F47-9E52-311D1D9E5307', 'Dice Rolling', 2072);
INSERT INTO Mechanics (Id, Name, BoardGameGeekId) VALUES ('BDB2ED44-54AF-4338-9A91-3F835529E435', 'Modular Board', 2011);
INSERT INTO Mechanics (Id, Name, BoardGameGeekId) VALUES ('288F3504-82EE-4710-80A4-464184780A32', 'Action Point Allowance System', 2001);
INSERT INTO Mechanics (Id, Name, BoardGameGeekId) VALUES ('28673CEE-9634-4ABD-BBA7-4A0153F19138', 'Simultaneous Action Selection', 2020);
INSERT INTO Mechanics (Id, Name, BoardGameGeekId) VALUES ('1ABF5B58-3D6E-4608-B8A6-4DEBF36CF514', 'Press Your Luck', 2661);
INSERT INTO Mechanics (Id, Name, BoardGameGeekId) VALUES ('717D83C4-7302-4E10-B489-6A20208B2B1E', 'Area Movement', 2046);
INSERT INTO Mechanics (Id, Name, BoardGameGeekId) VALUES ('78CB0285-F794-48DD-8C23-6CEB9D344FD8', 'Pattern Building', 2048);
INSERT INTO Mechanics (Id, Name, BoardGameGeekId) VALUES ('8B81F5B1-16C8-48B1-8F38-7771F48C9E2F', 'Voting', 2017);
INSERT INTO Mechanics (Id, Name, BoardGameGeekId) VALUES ('AEE7BDB2-1632-4A87-A7A5-7822A5203145', 'Tile Placement', 2002);
INSERT INTO Mechanics (Id, Name, BoardGameGeekId) VALUES ('F6D345DA-594D-4DC3-8083-7ED60974AE67', 'Pick-up and Deliver', 2007);
INSERT INTO Mechanics (Id, Name, BoardGameGeekId) VALUES ('DADA1111-4EEC-4855-81EB-80138CD8C31C', 'Roll / Spin and Move', 2035);
INSERT INTO Mechanics (Id, Name, BoardGameGeekId) VALUES ('414DF22A-5300-4C68-9BE9-821FA91B42F3', 'Take That', 2686);
INSERT INTO Mechanics (Id, Name, BoardGameGeekId) VALUES ('A10C9F58-7AB5-4628-A937-896B7AE8922A', 'Action / Movement Programming', 2689);
INSERT INTO Mechanics (Id, Name, BoardGameGeekId) VALUES ('62D76410-0D05-4026-A745-89739C0595FD', 'Hand Management', 2040);
INSERT INTO Mechanics (Id, Name, BoardGameGeekId) VALUES ('F0EDECD9-1DF1-4E54-9964-8B8132F4EB57', 'Player Elimination', 2685);
INSERT INTO Mechanics (Id, Name, BoardGameGeekId) VALUES ('163D0787-D294-4F3A-AB83-8E07D004FE37', 'Partnerships', 2019);
INSERT INTO Mechanics (Id, Name, BoardGameGeekId) VALUES ('5D9E92D5-8A13-45BE-8A85-910F0441992C', 'Simulation', 2070);
INSERT INTO Mechanics (Id, Name, BoardGameGeekId) VALUES ('071DA9B5-CD5C-4E6B-9572-9392A187E5ED', 'Grid Movement', 2676);
INSERT INTO Mechanics (Id, Name, BoardGameGeekId) VALUES ('397C8CD8-99DD-4BE8-A455-98759B820EDF', 'Role Playing', 2028);
INSERT INTO Mechanics (Id, Name, BoardGameGeekId) VALUES ('DBA7681E-3ECD-4569-91D6-A26B7079115F', 'Worker Placement', 2082);
INSERT INTO Mechanics (Id, Name, BoardGameGeekId) VALUES ('4E7458C7-F7EC-4BC4-BC38-A33403472A55', 'Cooperative Play', 2023);
INSERT INTO Mechanics (Id, Name, BoardGameGeekId) VALUES ('C0E026C5-6FDC-4936-A7AC-A40A5057CC83', 'Auction/Bidding', 2012);
INSERT INTO Mechanics (Id, Name, BoardGameGeekId) VALUES ('EE6C1E82-4820-4D42-92DE-AEE3A9F00D90', 'Trading', 2008);
INSERT INTO Mechanics (Id, Name, BoardGameGeekId) VALUES ('69FDA797-FA42-41E3-B759-B067E818CDC4', 'Area Control / Area Influence', 2080);
INSERT INTO Mechanics (Id, Name, BoardGameGeekId) VALUES ('45AC002A-52AB-4DF2-AE00-B4BB9D92D42B', 'Trick-taking', 2009);
INSERT INTO Mechanics (Id, Name, BoardGameGeekId) VALUES ('5871984D-411B-44C3-9D63-C11DD2C222A9', 'Point to Point Movement', 2078);
INSERT INTO Mechanics (Id, Name, BoardGameGeekId) VALUES ('7FD30336-0431-42DC-BFF8-C445308BF113', 'Variable Player Powers', 2015);
INSERT INTO Mechanics (Id, Name, BoardGameGeekId) VALUES ('6862FD36-107B-4F66-AF6F-D486B3F05858', 'Area Enclosure', 2043);
INSERT INTO Mechanics (Id, Name, BoardGameGeekId) VALUES ('CC76C5FF-865E-4E7F-B358-DA8C637F2469', 'Card Drafting', 2041);
INSERT INTO Mechanics (Id, Name, BoardGameGeekId) VALUES ('7DC45204-C4E1-497D-AD22-DD36CEAF26F6', 'Route/Network Building', 2081);
INSERT INTO Mechanics (Id, Name, BoardGameGeekId) VALUES ('BDFB24FB-E953-4B6B-A648-E8DA2F0F3421', 'Set Collection', 2004);
INSERT INTO Mechanics (Id, Name, BoardGameGeekId) VALUES ('7F6A61B6-01BD-4E56-936C-F96776AB724B', 'Stock Holding', 2005);
INSERT INTO Mechanics (Id, Name, BoardGameGeekId) VALUES ('761A8CF7-17BC-49FE-AAFB-F9E82CDD732D', 'Memory', 2047);
INSERT INTO Persons (Id, Name, BoardGameGeekId) VALUES ('FEAD68C6-6C27-4E1A-BE43-0737702129A1', 'Anthony Devine', 75483);
INSERT INTO Persons (Id, Name, BoardGameGeekId) VALUES ('852786BE-28A7-482E-AC38-07AA77069B3C', 'Oliver Schlemmer', 34863);
INSERT INTO Persons (Id, Name, BoardGameGeekId) VALUES ('596EC8D9-F007-429F-8E3A-091BCD579BCB', 'Matt Leacock', 378);
INSERT INTO Persons (Id, Name, BoardGameGeekId) VALUES ('513B1C83-EA29-42A6-8E35-0D54D5EBBA83', 'Rob Daviau', 442);
INSERT INTO Persons (Id, Name, BoardGameGeekId) VALUES ('9C8215B6-6EEB-47E6-81A7-0DE14657A41F', 'Ryan Valle', 68942);
INSERT INTO Persons (Id, Name, BoardGameGeekId) VALUES ('44773371-4435-428A-B03C-0F7A9BFC12BD', 'John Kovalic', 6815);
INSERT INTO Persons (Id, Name, BoardGameGeekId) VALUES ('99C9F040-360C-46D5-99A0-1064790F3D9C', 'Christopher O''Neal', 114764);
INSERT INTO Persons (Id, Name, BoardGameGeekId) VALUES ('FEEEC549-73A6-4B79-BA45-197131266DA4', 'Sophia Wagner', 102899);
INSERT INTO Persons (Id, Name, BoardGameGeekId) VALUES ('588C3DF8-79DF-45FF-BDDD-19B41CF65804', 'Jonathan Rosenberg', 27381);
INSERT INTO Persons (Id, Name, BoardGameGeekId) VALUES ('4A1FF5D3-B21F-4A9E-8DDE-1E2C676EB90E', 'Nils Nilsson', 103743);
INSERT INTO Persons (Id, Name, BoardGameGeekId) VALUES ('83DDCBA9-0E92-498F-A799-1FC384DB8007', 'Klaus-Jürgen Wrede', 398);
INSERT INTO Persons (Id, Name, BoardGameGeekId) VALUES ('8EF0BECB-89D9-432B-8889-21BD2BFC7C12', 'Jon Bosco', 63040);
INSERT INTO Persons (Id, Name, BoardGameGeekId) VALUES ('5401A7B9-8AD1-4B91-82D0-236372789E41', 'Matt Bradbury', 21426);
INSERT INTO Persons (Id, Name, BoardGameGeekId) VALUES ('15AD7037-B76A-4415-BA06-2470415CE436', 'Phil Foglio', 616);
INSERT INTO Persons (Id, Name, BoardGameGeekId) VALUES ('AB7F0B42-4CE1-4591-A3DF-24FACAE31683', 'Tignous', 45602);
INSERT INTO Persons (Id, Name, BoardGameGeekId) VALUES ('73CD95B8-1E7E-463E-84F5-2585D84B055F', 'Ignacio Bazán Lazcano', 46353);
INSERT INTO Persons (Id, Name, BoardGameGeekId) VALUES ('5C853A1D-28F0-4F35-A266-2597F2E5D059', 'Anne Heidsieck', 88432);
INSERT INTO Persons (Id, Name, BoardGameGeekId) VALUES ('0EA06D1B-31ED-4808-A649-278F939BA733', 'Mark Molnar', 44813);
INSERT INTO Persons (Id, Name, BoardGameGeekId) VALUES ('966698AA-1065-4DA9-AA8A-27A6227FD200', 'Doris Matthäus', 74);
INSERT INTO Persons (Id, Name, BoardGameGeekId) VALUES ('931E4CF4-E289-49E9-A844-2D7AE348B20D', 'Chris Quilliams', 14057);
INSERT INTO Persons (Id, Name, BoardGameGeekId) VALUES ('FB37DD5F-303F-4BCE-91F9-2E01C8ED5527', 'Klaus-Jürgen Wrede', 398);
INSERT INTO Persons (Id, Name, BoardGameGeekId) VALUES ('936CA6F5-4FA0-481F-8EF6-2F006E8A0A87', 'Chris O''Neal', 71054);
INSERT INTO Persons (Id, Name, BoardGameGeekId) VALUES ('569DB6AD-B281-4909-8246-2F34BBD67164', 'Jamey Stegmaier', 62640);
INSERT INTO Persons (Id, Name, BoardGameGeekId) VALUES ('32F7DCF5-7D5B-4CD9-A298-30C33D89E943', 'Hans-Georg Schneider', 12674);
INSERT INTO Persons (Id, Name, BoardGameGeekId) VALUES ('6395BB83-383F-4C48-86AD-331AF9C1B134', 'Adam Burn', 46414);
INSERT INTO Persons (Id, Name, BoardGameGeekId) VALUES ('B1BF9C58-C968-4270-A8EC-33347921900F', 'Zach Graves', 21095);
INSERT INTO Persons (Id, Name, BoardGameGeekId) VALUES ('802E792B-2A33-4938-B1A4-355C2D9D282B', 'Alexandre Dainche', 29808);
INSERT INTO Persons (Id, Name, BoardGameGeekId) VALUES ('992883F7-2ABE-480A-844E-3960BF299113', 'Steffen Benndorf', 11781);
INSERT INTO Persons (Id, Name, BoardGameGeekId) VALUES ('B9E369F0-82C7-4AD1-986F-3A718609FF98', 'Angela Sung', 49393);
INSERT INTO Persons (Id, Name, BoardGameGeekId) VALUES ('F0BB6CF2-8064-4DA1-B55A-3A88ECCA438F', 'Gord!', 3302);
INSERT INTO Persons (Id, Name, BoardGameGeekId) VALUES ('A3689D90-53A3-4247-83BE-3DBF93790608', 'Miguel Coimbra', 12016);
INSERT INTO Persons (Id, Name, BoardGameGeekId) VALUES ('615979B5-99E4-4544-BD14-3E0FD56C38DB', 'Tom Thiel', 16910);
INSERT INTO Persons (Id, Name, BoardGameGeekId) VALUES ('E657A5EC-5249-4343-B1D5-43F7F9D5A6AD', 'Mirko Suzuki', 12836);
INSERT INTO Persons (Id, Name, BoardGameGeekId) VALUES ('53ABEF69-0D0B-429F-A54F-4534F49FBC1E', 'Josh Cappel', 9731);
INSERT INTO Persons (Id, Name, BoardGameGeekId) VALUES ('B190FD40-411C-409D-A4A3-488C68BC2FAE', 'Jeff Lee Johnson', 38000);
INSERT INTO Persons (Id, Name, BoardGameGeekId) VALUES ('1E350817-C23A-4678-9684-4928DDB025B4', 'Ameen Naksewee', 59149);
INSERT INTO Persons (Id, Name, BoardGameGeekId) VALUES ('01235F95-D910-4AB3-B720-4983542FDDB9', 'Corey Konieczka', 6651);
INSERT INTO Persons (Id, Name, BoardGameGeekId) VALUES ('8511D57D-2565-4C85-9714-49DD32725054', 'Mariusz Gandzel', 18700);
INSERT INTO Persons (Id, Name, BoardGameGeekId) VALUES ('5D42F290-0EE8-4C49-BC60-4A2FB56325CE', 'Luke Peterschmidt', 644);
INSERT INTO Persons (Id, Name, BoardGameGeekId) VALUES ('ECABC930-A299-45A4-96BE-4A4E6865A4A2', 'Jean-Louis Roubira', 11547);
INSERT INTO Persons (Id, Name, BoardGameGeekId) VALUES ('EF54EEDD-CD2C-4667-A477-4B0A74905A1D', 'Tony Foti', 43532);
INSERT INTO Persons (Id, Name, BoardGameGeekId) VALUES ('EFE87A30-8CC5-4B0D-87D4-4CE6592AE0A3', 'Uwe Rosenberg', 10);
INSERT INTO Persons (Id, Name, BoardGameGeekId) VALUES ('2402ECFB-DFA0-494B-87CE-4D26754E4693', 'Anne Pätzke', 43873);
INSERT INTO Persons (Id, Name, BoardGameGeekId) VALUES ('291817BF-2667-423D-BECE-4D9424C2810F', 'Julien Delval', 11886);
INSERT INTO Persons (Id, Name, BoardGameGeekId) VALUES ('0FDC9867-8B22-414F-B1D9-4E644198E900', 'Jorge Barrero', 81542);
INSERT INTO Persons (Id, Name, BoardGameGeekId) VALUES ('A1465426-3076-47E5-9437-4EFCECB2DD84', 'Sandra Freudenreich', 66181);
INSERT INTO Persons (Id, Name, BoardGameGeekId) VALUES ('212AE2C9-3B0B-4DA4-BC32-501B85229FC8', 'Johnny O''Neal', 27052);
INSERT INTO Persons (Id, Name, BoardGameGeekId) VALUES ('F64AFD7E-2A13-4339-901E-501DA0516022', 'Katrina Guillermo', 75424);
INSERT INTO Persons (Id, Name, BoardGameGeekId) VALUES ('52DFD4CF-ABDD-4AFC-90D2-504FDD806EF0', 'Jose Mikhail Elloso', 90475);
INSERT INTO Persons (Id, Name, BoardGameGeekId) VALUES ('0BAE5283-E528-4772-863D-5343F7F2370D', 'Bruno Cathala', 1727);
INSERT INTO Persons (Id, Name, BoardGameGeekId) VALUES ('043A7A03-41D0-420C-AB04-55CBEC5407A7', 'Steve Jackson (I)', 22);
INSERT INTO Persons (Id, Name, BoardGameGeekId) VALUES ('61D6189C-D2CA-4105-8B2C-56E8125359BE', 'Stephen Somers', 77085);
INSERT INTO Persons (Id, Name, BoardGameGeekId) VALUES ('0124B657-140C-4F2F-8736-581412689B3C', 'Fabien Riffaud', 79931);
INSERT INTO Persons (Id, Name, BoardGameGeekId) VALUES ('AC94A8EA-E5DF-4938-B8A1-592B977A37E7', 'Andres Sanabria', 75429);
INSERT INTO Persons (Id, Name, BoardGameGeekId) VALUES ('0DB0C6B8-5739-41A3-8397-59791F43E707', 'Oliver Richtberg', 27641);
INSERT INTO Persons (Id, Name, BoardGameGeekId) VALUES ('44ADE932-616C-4141-A580-5DE0CAC24C6A', 'Elizabeth J. Magie (Phillips)', 3109);
INSERT INTO Persons (Id, Name, BoardGameGeekId) VALUES ('94A7581F-E3E4-4854-A30B-60349494D843', 'Franz Vohwinkel', 11883);
INSERT INTO Persons (Id, Name, BoardGameGeekId) VALUES ('2BCBDAAC-2ECA-4EE8-887E-6336A016CD62', 'Kwanchai Moriya', 28024);
INSERT INTO Persons (Id, Name, BoardGameGeekId) VALUES ('29B1AF47-B9E6-4959-81C0-63A7A3FBC6EE', 'Frantz Rey', 46292);
INSERT INTO Persons (Id, Name, BoardGameGeekId) VALUES ('2DB1F124-F679-410F-882D-65FCE8B3ECD5', 'Jason Juta', 14590);
INSERT INTO Persons (Id, Name, BoardGameGeekId) VALUES ('8E694F09-81D4-4916-88C0-68005DD42EBD', 'Marie Cardouat', 12691);
INSERT INTO Persons (Id, Name, BoardGameGeekId) VALUES ('622D0CFC-4EFA-42C2-99A7-6A130FF0BE0D', 'Bernd Kienitz', 4089);
INSERT INTO Persons (Id, Name, BoardGameGeekId) VALUES ('44248E24-F2D0-459D-8D68-6A5192D321AA', 'Antoine Bauza', 9714);
INSERT INTO Persons (Id, Name, BoardGameGeekId) VALUES ('F6309C30-3BE2-4B8B-B1E8-6F167AFEFA44', 'Christina Davis', 69244);
INSERT INTO Persons (Id, Name, BoardGameGeekId) VALUES ('D00184A9-09C7-4BC0-8B17-6FB0DEF93D7B', 'Francisco Coda', 75425);
INSERT INTO Persons (Id, Name, BoardGameGeekId) VALUES ('6DB67EBA-12B8-4B60-A2DD-70766D889D61', '(Uncredited)', 3);
INSERT INTO Persons (Id, Name, BoardGameGeekId) VALUES ('91370359-5373-40BE-8C56-730DEE6856E6', 'Oliver Freudenreich', 11901);
INSERT INTO Persons (Id, Name, BoardGameGeekId) VALUES ('9885EC92-60D2-4BF7-BC74-73C5D706BD36', 'Sébastien Caiveau', 89700);
INSERT INTO Persons (Id, Name, BoardGameGeekId) VALUES ('1C3557D4-7E82-4E49-A053-757037276BB0', 'Stéphane Gantiez', 12402);
INSERT INTO Persons (Id, Name, BoardGameGeekId) VALUES ('5838A744-9252-43CD-AB77-77975735C977', 'Sabrina Miramon', 69113);
INSERT INTO Persons (Id, Name, BoardGameGeekId) VALUES ('83F5CFF9-8726-40B9-9280-7BD3D2933B24', 'Christian Pachis', 4522);
INSERT INTO Persons (Id, Name, BoardGameGeekId) VALUES ('B05D842A-98E2-4FC0-83C4-7E22C82235A1', 'Tiziano Baracchi', 19822);
INSERT INTO Persons (Id, Name, BoardGameGeekId) VALUES ('B29CDB1D-5744-4BFC-9598-7E2AC164109D', 'Ryan Barger', 12438);
INSERT INTO Persons (Id, Name, BoardGameGeekId) VALUES ('016B4449-3B12-4626-95BF-7E3261069717', 'Masao Suganuma', 68814);
INSERT INTO Persons (Id, Name, BoardGameGeekId) VALUES ('9203A802-CD09-44BF-94CC-80461B8FC01E', 'Ian Parovel', 37676);
INSERT INTO Persons (Id, Name, BoardGameGeekId) VALUES ('EDA43045-BEE0-4728-8264-81CBD33436FE', 'Richard Hanuschek', 86222);
INSERT INTO Persons (Id, Name, BoardGameGeekId) VALUES ('53B81326-0AA5-4361-8AB0-81ECF365BD01', 'Beau Buckley', 75426);
INSERT INTO Persons (Id, Name, BoardGameGeekId) VALUES ('B615829E-2D81-49D3-AD92-836B073EB43E', 'Nicholas Stohlman', 41679);
INSERT INTO Persons (Id, Name, BoardGameGeekId) VALUES ('928AA81B-0969-4E3C-A7F7-8416902CB900', 'David Nyari', 75427);
INSERT INTO Persons (Id, Name, BoardGameGeekId) VALUES ('551ADA56-2C15-4650-9076-8690FB96BA49', 'Nate French', 11655);
INSERT INTO Persons (Id, Name, BoardGameGeekId) VALUES ('37D1574C-7244-418D-AB46-8A982D296881', 'Matthew Starbuck', 41678);
INSERT INTO Persons (Id, Name, BoardGameGeekId) VALUES ('4CFD51E1-B427-43E0-90BA-8BE05E6CC589', 'Matt Allsopp', 34719);
INSERT INTO Persons (Id, Name, BoardGameGeekId) VALUES ('41DEA361-BD9E-4C70-9B88-8E03475D23B7', 'Jake Murray', 41638);
INSERT INTO Persons (Id, Name, BoardGameGeekId) VALUES ('B024EF39-6716-417A-8C22-8FD39676204A', 'Christopher Hosch', 50786);
INSERT INTO Persons (Id, Name, BoardGameGeekId) VALUES ('D0C46E9B-3225-437E-95BF-92E03A1A2ABF', 'David A. Nash', 49382);
INSERT INTO Persons (Id, Name, BoardGameGeekId) VALUES ('4909E83A-B96E-4851-AA0A-94601D89F742', 'Sören Meding', 102584);
INSERT INTO Persons (Id, Name, BoardGameGeekId) VALUES ('41FF88DB-6D01-42EF-B9D1-9544A077C63A', 'Rüdiger Dorn', 381);
INSERT INTO Persons (Id, Name, BoardGameGeekId) VALUES ('F5B8921C-A11A-4305-AC36-9993B11CE3A3', 'Tomáš Kucerovský', 28450);
INSERT INTO Persons (Id, Name, BoardGameGeekId) VALUES ('1DA6CD8C-BB50-4863-B958-99EF90FD0341', 'Christian Hanisch', 33088);
INSERT INTO Persons (Id, Name, BoardGameGeekId) VALUES ('D3DFAEF0-54F0-4AF5-AE84-9A5605F055D7', 'Noboru Hotta', 69648);
INSERT INTO Persons (Id, Name, BoardGameGeekId) VALUES ('9331F494-A80D-4A23-A26C-9DD2EC8967D6', 'Mike Nash', 19549);
INSERT INTO Persons (Id, Name, BoardGameGeekId) VALUES ('4049A013-B95D-4EE6-B0E9-9DF6776654A5', 'Christian Martinez', 6997);
INSERT INTO Persons (Id, Name, BoardGameGeekId) VALUES ('26629DBF-5FA4-40E8-B747-A0FFCD93BAB1', 'Mihajlo Dimitrievski', 70533);
INSERT INTO Persons (Id, Name, BoardGameGeekId) VALUES ('33C8F20B-A6EE-487D-94A4-A1A989FFBCA3', 'Michele Frigo', 86045);
INSERT INTO Persons (Id, Name, BoardGameGeekId) VALUES ('45B3F99D-BEDC-45D8-BE4D-A1FF76DFB6AA', 'Edge Studio', 38263);
INSERT INTO Persons (Id, Name, BoardGameGeekId) VALUES ('71E0079A-8327-49E9-A037-A5DAA5D49B0C', 'Klemens Franz', 11507);
INSERT INTO Persons (Id, Name, BoardGameGeekId) VALUES ('DD05E57C-F44A-4011-BE90-A6FE8C3A1074', 'Ralph McQuarrie', 13442);
INSERT INTO Persons (Id, Name, BoardGameGeekId) VALUES ('CF7F461E-EBD7-4535-B917-A9272689E2CE', 'Jacob Walker', 12627);
INSERT INTO Persons (Id, Name, BoardGameGeekId) VALUES ('56F05AC6-6313-447C-A28D-ACF3CDA0886B', 'Darren Tan', 69091);
INSERT INTO Persons (Id, Name, BoardGameGeekId) VALUES ('2722A16F-5BBE-4B90-A974-ACF6FF92E608', 'Wolfgang Panning', 249);
INSERT INTO Persons (Id, Name, BoardGameGeekId) VALUES ('8C784F1B-9B08-420F-B53D-AF57A688A898', 'Alexander Pfister', 11767);
INSERT INTO Persons (Id, Name, BoardGameGeekId) VALUES ('F2D3BFF5-EC0E-4E87-8D40-B2D00F3387B5', 'Steve Noon', 62851);
INSERT INTO Persons (Id, Name, BoardGameGeekId) VALUES ('13F1946C-F3C7-44CD-A841-B2DBEBAB4B30', 'Magali Villeneuve', 29809);
INSERT INTO Persons (Id, Name, BoardGameGeekId) VALUES ('BE579153-208B-456D-9EC7-B361B419D7E8', 'Brett J. Gilbert', 56058);
INSERT INTO Persons (Id, Name, BoardGameGeekId) VALUES ('224764A6-D976-4CBF-A804-B4676E641AAC', 'Andrew Johanson', 17704);
INSERT INTO Persons (Id, Name, BoardGameGeekId) VALUES ('AEF2DF96-5249-4133-9E8D-B6951F0CC951', 'Matthew Newman', 40573);
INSERT INTO Persons (Id, Name, BoardGameGeekId) VALUES ('539F427D-16AD-4DC1-95F5-BEEF055F0708', 'Cyrille Daujean', 12519);
INSERT INTO Persons (Id, Name, BoardGameGeekId) VALUES ('15CEF5D4-9A0E-406C-9276-C1BF8FA7C55E', 'Jérémie Fleury', 80515);
INSERT INTO Persons (Id, Name, BoardGameGeekId) VALUES ('273EBDBA-441A-4B3C-8ED4-C1D70C71E8DF', 'Tony Shasteen', 14318);
INSERT INTO Persons (Id, Name, BoardGameGeekId) VALUES ('BD3B18CF-768F-49A1-8E72-C66E3A582B1B', 'Alan R. Moon', 9);
INSERT INTO Persons (Id, Name, BoardGameGeekId) VALUES ('B49B8757-C175-4502-8A99-C66ED5838D08', 'Alexander Olsen', 75428);
INSERT INTO Persons (Id, Name, BoardGameGeekId) VALUES ('B7551137-219F-4E54-9216-C6A8AA51CDFA', 'Hjalmar Hach', 91211);
INSERT INTO Persons (Id, Name, BoardGameGeekId) VALUES ('E44980B4-CBC6-4443-ADA7-C8A29D3948A9', 'Balaskas', 20067);
INSERT INTO Persons (Id, Name, BoardGameGeekId) VALUES ('55042807-1E75-4AC9-AF88-CA1211AA943D', 'Antje Stephan', 72917);
INSERT INTO Persons (Id, Name, BoardGameGeekId) VALUES ('7ACACB76-2FFC-484B-B8A0-CA882A8634C0', 'Ralf Berszuck', 24459);
INSERT INTO Persons (Id, Name, BoardGameGeekId) VALUES ('2767C7C5-E23E-4DAB-94DD-CA8E50EF7653', 'Adam Lane', 46416);
INSERT INTO Persons (Id, Name, BoardGameGeekId) VALUES ('7F3513D3-66B0-4C76-AA0F-CB6710D7422B', 'Ben Zweifel', 49280);
INSERT INTO Persons (Id, Name, BoardGameGeekId) VALUES ('B91A227F-E1C2-4B21-B42A-CBAEE36AC3D5', 'Ryan Miller', 2276);
INSERT INTO Persons (Id, Name, BoardGameGeekId) VALUES ('BD5CCBC4-1619-4D94-B7E5-CF578B84D217', 'Cassandre Bolan', 86044);
INSERT INTO Persons (Id, Name, BoardGameGeekId) VALUES ('DD11116F-D0AE-4350-B649-CF83BCA6E1EB', 'Bluguy Grafikdesign', 98161);
INSERT INTO Persons (Id, Name, BoardGameGeekId) VALUES ('35489DFC-12E6-42A0-AD60-D256A2137837', 'Michael Menzel', 11825);
INSERT INTO Persons (Id, Name, BoardGameGeekId) VALUES ('9293F240-FC35-4979-B11C-D34DE1E180CC', 'Jose Vega', 77254);
INSERT INTO Persons (Id, Name, BoardGameGeekId) VALUES ('E2708139-5CE0-4CE8-9682-D5A568E2CCDE', 'Maciej Rebisz', 48661);
INSERT INTO Persons (Id, Name, BoardGameGeekId) VALUES ('A4A20785-39CB-4200-B308-D6D98CFAFB76', 'Shem Phillips', 12547);
INSERT INTO Persons (Id, Name, BoardGameGeekId) VALUES ('AE7D3B83-6DDB-4F3F-A227-D7E36324594E', 'Matthew Dunstan', 69007);
INSERT INTO Persons (Id, Name, BoardGameGeekId) VALUES ('AA9FD3DC-8CC0-4B3C-86A3-D9A955E9CE16', 'Régis Moulun', 11935);
INSERT INTO Persons (Id, Name, BoardGameGeekId) VALUES ('789FADA0-E890-4081-976E-DA1D526AF12B', 'David Ardila', 32122);
INSERT INTO Persons (Id, Name, BoardGameGeekId) VALUES ('63FBEDD1-6FE6-48B8-9515-DC9E8248AF2F', 'Martin Wallace', 6);
INSERT INTO Persons (Id, Name, BoardGameGeekId) VALUES ('4F6278CC-5E67-4078-A0E2-DD28AF6BF2C3', 'Fabrice Besson', 11258);
INSERT INTO Persons (Id, Name, BoardGameGeekId) VALUES ('DEBB3057-6B5C-4AFB-8D0E-DDA48E909286', 'Andreas Resch', 11898);
INSERT INTO Persons (Id, Name, BoardGameGeekId) VALUES ('42101E58-774F-4EB0-BCCA-DF539CF9F686', 'Juan Rodriguez', 2098);
INSERT INTO Persons (Id, Name, BoardGameGeekId) VALUES ('C6354A2C-5D5C-4F3F-BEC8-E3370E61E71F', 'Anne-Marie De Witt', 43440);
INSERT INTO Persons (Id, Name, BoardGameGeekId) VALUES ('CFFBF689-A93E-4A6F-91BE-E593EEE3564B', 'Vlaada Chvátil', 789);
INSERT INTO Persons (Id, Name, BoardGameGeekId) VALUES ('76A7CEA7-95D1-44CA-BF49-E743D7B406AA', 'Gus Batts', 70607);
INSERT INTO Persons (Id, Name, BoardGameGeekId) VALUES ('68688E75-34B0-4AFB-B2B8-E9A6680944E3', 'Kyle Merritt', 67746);
INSERT INTO Persons (Id, Name, BoardGameGeekId) VALUES ('0C8AF378-704C-4754-97F3-E9FCEF2BE04B', 'Ghislain Masson', 89487);
INSERT INTO Persons (Id, Name, BoardGameGeekId) VALUES ('9B5794BA-59D8-4EE6-A5E9-EDF7383BAB02', 'Marc André', 50969);
INSERT INTO Persons (Id, Name, BoardGameGeekId) VALUES ('B38C941C-740A-4B8E-AD87-EF569DC15819', 'Allen Douglas', 17275);
INSERT INTO Persons (Id, Name, BoardGameGeekId) VALUES ('3A2D7AB2-833F-4353-B1EC-EFE3CCDDF627', 'Alexandru Sabo', 49381);
INSERT INTO Persons (Id, Name, BoardGameGeekId) VALUES ('09C64C14-28E0-40AB-B8BD-F0A14DE86C90', 'Tom Ricket', 27380);
INSERT INTO Persons (Id, Name, BoardGameGeekId) VALUES ('64B01082-64C7-4CA2-85AE-F182DAF0A24F', 'Pete Abrams', 27379);
INSERT INTO Persons (Id, Name, BoardGameGeekId) VALUES ('7B526B31-C1A6-438C-8FDE-F2AE0FD379E6', 'Jakub Rozalski', 33148);
INSERT INTO Persons (Id, Name, BoardGameGeekId) VALUES ('B6F1D0C0-9DDD-4AAD-91F0-F632C36258F6', 'Lauge Luchau', 43874);
INSERT INTO Persons (Id, Name, BoardGameGeekId) VALUES ('A9479DE2-3BD2-4492-936A-F8922272263F', 'VIKO', 90477);
INSERT INTO Persons (Id, Name, BoardGameGeekId) VALUES ('B7807DC9-1D43-481F-98A3-F8EB893DF634', 'Charles Darrow', 1268);
INSERT INTO Persons (Id, Name, BoardGameGeekId) VALUES ('3E9B9D5C-90B7-4CCE-9F30-FA692F9B0034', 'Donald X. Vaccarino', 10525);
INSERT INTO Persons (Id, Name, BoardGameGeekId) VALUES ('B2938EED-207E-4982-A7B0-FD0EF379F4B0', 'Samuel Shimota', 78938);
INSERT INTO Persons (Id, Name, BoardGameGeekId) VALUES ('BEE9B4D4-E18E-4442-A410-FF866818291A', 'Tomasz Jedruszek', 12019);
INSERT INTO Persons (Id, Name, BoardGameGeekId) VALUES ('2384C952-AF61-473F-8284-FF910CB24299', 'Randy Milholland', 13504);
INSERT INTO Publishers (Id, Name, Priority, BoardGameGeekId) VALUES ('6F7D65CB-0BF7-439E-A2E2-0DC70D37E179', 'Ravensburger', 10, 34);
INSERT INTO Publishers (Id, Name, Priority, BoardGameGeekId) VALUES ('FF19A341-8372-423A-B82F-2C36031681EA', 'Artipia Games', 10, 16755);
INSERT INTO Publishers (Id, Name, Priority, BoardGameGeekId) VALUES ('72E8B7A0-1C25-4DEB-BD7E-2CB339DBC163', 'KOSMOS', 10, 37);
INSERT INTO Publishers (Id, Name, Priority, BoardGameGeekId) VALUES ('5F647A2B-57B4-429D-8D82-449C385F0DFF', 'Schmidt Spiele', 10, 38);
INSERT INTO Publishers (Id, Name, Priority, BoardGameGeekId) VALUES ('F203898B-B16E-4B7C-BB93-4DA741DD5497', 'Queen Games', 10, 47);
INSERT INTO Publishers (Id, Name, Priority, BoardGameGeekId) VALUES ('14AFBB88-2516-4D1F-A9C5-5C9034517115', 'HABA', 10, 384);
INSERT INTO Publishers (Id, Name, Priority, BoardGameGeekId) VALUES ('F849595C-801B-4D95-99B2-5DEBF824DE14', 'Heidelberger Spieleverlag', 10, 264);
INSERT INTO Publishers (Id, Name, Priority, BoardGameGeekId) VALUES ('8AE956CA-2B04-4507-907D-641522EACA68', 'Repos Production', 10, 4384);
INSERT INTO Publishers (Id, Name, Priority, BoardGameGeekId) VALUES ('1C2B088C-B318-40FC-ADA3-65A66CBC6DFD', 'Parker Brothers', 10, 28);
INSERT INTO Publishers (Id, Name, Priority, BoardGameGeekId) VALUES ('2C763E90-945D-4106-B841-6628A82219AC', 'Hans im Glück', 10, 133);
INSERT INTO Publishers (Id, Name, Priority, BoardGameGeekId) VALUES ('68A28E87-170C-4AC6-A77F-75B49DE67452', 'ABACUSSPIELE', 10, 29);
INSERT INTO Publishers (Id, Name, Priority, BoardGameGeekId) VALUES ('EE826793-FA24-4020-8F8F-792E88F205F6', 'IELLO', 10, 8923);
INSERT INTO Publishers (Id, Name, Priority, BoardGameGeekId) VALUES ('FD455EAE-58C2-4123-9BFF-8877FFC9263F', 'eggertspiele', 10, 1015);
INSERT INTO Publishers (Id, Name, Priority, BoardGameGeekId) VALUES ('28D49A02-E9F7-4C6A-8F5C-8A387EF75155', 'Zoch Spiele', 10, 87);
INSERT INTO Publishers (Id, Name, Priority, BoardGameGeekId) VALUES ('31496E22-0A06-40EB-A502-92269F09F2F8', 'Nürnberger-Spielkarten-Verlag', 10, 1020);
INSERT INTO Publishers (Id, Name, Priority, BoardGameGeekId) VALUES ('24DF76E5-A4C5-4435-8104-9A48DCE08A37', 'Stonemaier Games', 10, 23202);
INSERT INTO Publishers (Id, Name, Priority, BoardGameGeekId) VALUES ('BAE5F53E-6873-4637-A07C-A40C6730ACC2', 'Pegasus Spiele', 10, 39);
INSERT INTO Publishers (Id, Name, Priority, BoardGameGeekId) VALUES ('B69B5032-43A8-44FA-BD21-A4A7604BF5E9', 'Ludonaute', 10, 11688);
INSERT INTO Publishers (Id, Name, Priority, BoardGameGeekId) VALUES ('8C8C8781-4926-403B-86AC-A4B61975FE31', 'Asmodee', 10, 157);
INSERT INTO Publishers (Id, Name, Priority, BoardGameGeekId) VALUES ('0AF6D8F6-D09B-4C12-9908-A4BB377117BA', 'Czech Games Edition ', 10, 7345);
INSERT INTO Publishers (Id, Name, Priority, BoardGameGeekId) VALUES ('EC30755A-C867-4ED3-936F-BF55FD1C47F2', 'Fantasy Flight Games', 10, 17);
INSERT INTO Publishers (Id, Name, Priority, BoardGameGeekId) VALUES ('5AFEAAEA-B451-413C-9AF4-C61C71E388D3', 'Lookout Games', 10, 234);
INSERT INTO Publishers (Id, Name, Priority, BoardGameGeekId) VALUES ('1714A25E-66DF-4DEF-A5C4-D1C102E1BA43', 'Feuerland Spiele', 10, 22380);
INSERT INTO Publishers (Id, Name, Priority, BoardGameGeekId) VALUES ('E262AF92-F68A-4AB7-97F7-E4169380E1C5', 'Edition Spielwiese', 10, 33419);
INSERT INTO Publishers (Id, Name, Priority, BoardGameGeekId) VALUES ('38AAC893-67DE-4FCD-9DA3-E94BDC024DBE', 'Days of Wonder', 10, 1027);
INSERT INTO Publishers (Id, Name, Priority, BoardGameGeekId) VALUES ('767C317F-B4BE-4D84-A873-EB0BE1B41962', 'Matagot', 10, 5400);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('D9F32603-ED77-49C2-A327-0026748224F5', 'Xalapa', 1, 6, 8, 30, 30, null, null, 153510, null, null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('DBDF1C0B-F813-45DA-B66C-00632E2B113F', 'Epic PvP: Erweiterung 1 – Dunkelelf, Ork, Barbar & Mönch', 2, 4, 14, 15, 15, null, null, 192348, '8A6BABDE-AF95-4F12-96AE-64F21DBEDAA3', null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('4721FAB4-B8E9-432C-A6A6-02741EE71E45', 'Caverna: Höhle gegen Höhle', 1, 2, 10, 20, 40, null, null, 220520, null, null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('83F314E6-760D-46DF-9559-03F7E92FBD4A', 'Arkham Horror: Das Kartenspiel – Das Vermächtnis von Dunwich: Erweiterung', 1, 2, 14, 60, 120, null, null, 208545, '9413EC6D-1116-483D-B2F7-E993B82013C4', null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('DBC6FBB7-FEF0-422C-8DCA-049AA98AF2A9', 'Berge des Wahnsinns', 3, 5, 12, 60, 90, null, null, 214293, null, null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('DD6FD171-28B8-48B5-A92D-04C7DE4F2D9A', 'Star Wars: Rebellion', 2, 4, 14, 180, 240, null, null, 187645, null, null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('D9551C9B-951D-4EE3-A2AD-05515CDCB676', 'P. I.', 2, 5, 13, 45, 60, null, null, 129050, null, null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('6B007B51-FDB7-4C6C-9634-0653E55A40FB', 'Würfelkönig', 2, 5, 8, 20, 30, null, null, 235922, null, null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('945D2891-B7F4-4A92-BBE0-07223F0C397D', 'Was ''ne Frage', 3, 6, 15, 30, 30, null, null, 226322, null, null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('BFAB2E65-3973-4B9D-A2F0-0761D70E9AAF', 'Queendomino', 2, 4, 0, 40, 40, null, null, 232043, null, null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('BCEBA3EE-5CAC-4000-8B9C-086E80F5DC27', 'Santorini', 2, 3, 0, 20, 20, null, null, 9963, null, null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('C77731A0-55DE-4650-81E2-0A585A94E813', 'SchatzJäger', 2, 6, 0, 45, 45, null, null, 8275, null, null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('A3F5432F-7FD3-4809-9EA7-0D1C3A428281', 'Time Arena', 2, 4, 0, 10, 10, null, null, 229599, null, null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('F16908F8-8811-4DD9-9773-0E69A096FE15', 'Kingdom Builder', 2, 4, 0, 45, 45, null, null, 107529, null, null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('79F5544A-A0A1-46F8-AAF2-0EC2EE3FD146', 'Zug um Zug - Europa', 2, 5, 8, 30, 60, null, null, 14996, null, null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('F8E3A417-6C7C-4A98-9203-0EE5F4596A35', 'Isola', 2, 2, 8, 20, 20, null, null, 1875, null, null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('1817DEB6-D889-4462-8316-0F589B7F728C', 'Auf den Spuren von Marco Polo', 2, 4, 0, 90, 90, null, null, 9139, null, null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('7F3E6FB1-AC6F-49C6-B569-11E897677DA3', 'Concept', 4, 12, 0, 40, 40, null, null, 147151, null, null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('13D45015-C762-4BA5-ADF9-11E9E8EE90BC', 'Kampf um den Olymp', 2, 2, 0, 30, 30, null, null, 192834, null, null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('D970C12F-BA3C-43D9-8843-12B0DEBD7FE9', 'ekö', 2, 4, 0, null, null, null, null, 177197, null, null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('FC16DF4B-DED4-4429-A275-12E25F080B65', 'Escape - Zombie City', 2, 4, 0, 20, 20, null, null, null, null, null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('96D146E3-22D9-49CF-BA10-12EEF2C72FDB', 'Jäger + Späher', 2, 2, 0, 60, 60, null, null, null, null, null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('64EDCCE8-11CC-4EAC-BA1A-138E2F9F99EB', 'Die Grimoire des Wahnsinns', 2, 5, 0, 90, 90, null, null, null, null, null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('F7602D80-74AA-4AFC-A3BD-14E158369E5B', 'Spiel des Wissens', 2, 6, 0, 90, 90, null, null, null, null, null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('4DAA3A03-3395-4F87-BDF8-15416D4B93AE', 'Scrabble', 2, 4, 0, 90, 90, null, null, 320, null, null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('322F3FCC-0D12-4D18-827F-1673BD6A1F5A', '7 Wonders', 3, 7, 0, 40, 40, null, null, 68448, null, null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('19455DA1-7D53-4146-B9F0-16B3A1216A4D', 'Terraforming Mars', 1, 5, 0, 120, 120, null, null, 167791, null, null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('1030D528-9EA5-453E-9F3D-183E7F52E46B', 'Abluxxen', 2, 5, 0, 20, 20, null, null, 153065, null, null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('AA24A784-0644-4880-9A21-1883C7F1A814', 'Colt Express', 2, 6, 0, 30, 30, null, null, 158899, null, null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('13B852A0-8256-4243-A3A9-1B551FF9411B', 'Gùgong', 1, 5, 0, 90, 90, null, null, null, null, null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('229BE380-7129-47F7-B6CC-1C1F21B843C8', 'Moorea', 2, 5, 0, 30, 30, null, null, 247984, null, null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('C555835D-8676-4DDF-9034-1D88197175FE', 'Rumms - Voll auf die Krone', 2, 4, 0, 20, 20, null, null, null, null, null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('7C46E7F3-B827-41ED-8989-1E35063CE47A', 'Camel Up', 2, 8, 0, 30, 30, null, null, 153938, null, null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('B425095C-36BD-4E74-9DE6-204F1BBAB61A', 'Activity', 4, 16, 0, null, null, null, null, 8790, null, null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('8C9FDDCD-A3C4-41B4-BFD4-204F32BF0861', 'Qwixx', 2, 5, 0, 20, 20, null, null, 131260, null, null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('11DC8C7B-928E-4B6B-990E-21B35FA4EC01', 'Ohne Furcht und Adel', 3, 7, 0, 60, 60, null, null, 130060, null, null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('2FFF25B9-A5BC-46D4-B1E8-22CCE37A1E03', 'Ein Fest für Odin', 1, 4, 0, 120, 120, null, null, 177736, null, null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('88894445-48C5-41BE-855E-2349B49EAA32', 'Sequence', 2, 12, 0, 45, 45, null, null, 2375, null, null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('29CF6E49-8B34-4457-BE0A-260BF0BC6E7C', 'Carcassonne - Wirtshäuser und Kathedralen', 2, 5, 0, 30, 30, null, null, null, '883D0F36-BA48-475B-A64D-430C6DCEDDA0', null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('6BFD7371-9363-47F0-97F6-26D0CCD55683', 'Colt Express - Postkutsche & Pferde', 2, 6, 0, 40, 40, null, null, null, 'AA24A784-0644-4880-9A21-1883C7F1A814', null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('670DE3E8-DB58-4CF7-9994-274CCBA767E8', 'Agricola (Kennerspiel) - 5-6 Spieler', 2, 6, 0, 150, 150, null, null, null, '5986B3A2-BF4D-4EC0-A65D-79E5771F9404', null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('11695870-B396-48BD-B1D7-278B720D9821', 'Elysium', 2, 4, 0, null, null, null, null, 163968, null, null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('00B1EA68-70E0-4E4C-9594-27C7B6222F89', 'Zombie 15', 2, 4, 0, null, null, null, null, null, null, null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('F1FA4B3F-E47E-4B9D-9DA2-28E97A3852AB', 'Great Western Trail - Rail to the North', 2, 4, 0, 150, 150, null, null, null, 'CEB0E64B-B269-4E4C-BD59-F7ED8BB90A03', null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('969CEAA9-FFC5-4D74-96BA-29D780DB7A71', 'Raxxon', 1, 4, 0, null, null, null, null, 231197, null, null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('23AF2D0D-E19C-406D-9FAE-2D09E1A75408', 'We will Wok you', 2, 4, 0, 40, 40, null, null, 121993, null, null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('73AEA4C3-67A6-44CD-963F-2E9FB80C8EAB', 'Munchkin Quest', 2, 4, 0, 180, 180, null, null, 29678, null, null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('B29B4106-4DB4-4049-A66B-2ED60E5670E1', 'Ganz schön clever', 1, 4, 0, 20, 20, null, null, 244522, null, null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('13D52346-B5FE-471F-8B22-2EE62C6040A1', 'Munchkin 5: Wirre Waldläufer', 3, 6, 0, 60, 60, null, null, 28327, 'FE177D2B-0B5D-46EB-A113-81CD9FCE4775', null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('E004F1AD-E404-456D-82E9-2EE754CA2B02', 'Die Zwerge - Das Duell', 2, 2, 0, 20, 20, null, null, null, null, null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('A754D44B-EDD2-4586-B195-2F6F85DB92C6', 'Die blutige Herberge', 1, 4, 0, 60, 60, null, null, 180593, null, null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('EF49DDC6-76C7-4DE6-A23B-310555047D16', 'Unfair', 2, 5, 0, 125, 125, null, null, 175220, null, null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('0049B042-E0F7-4BFA-9EEF-3111AF621F0B', 'Tokaido', 2, 5, 0, 45, 45, null, null, 123540, null, null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('C8BD31AD-1C07-44B2-8AB0-347473CB5ECC', 'Takenoko', 2, 4, 0, 60, 60, null, null, 70919, null, null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('6F27F128-6CC5-4F53-850E-3564AC56BE34', 'Wettlauf nach El Dorado', 2, 4, 0, 60, 60, null, null, 217372, null, null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('E04D568C-C583-4888-9F4E-377094434063', 'Die Quacksalber von Quedlinburg', 2, 4, 0, 45, 45, null, null, 244521, null, null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('5FA611EF-3C93-432D-AC03-37A345A6678D', 'Exploding Kittens - NSFW Edition', 2, 5, 0, 15, 15, null, null, null, null, null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('70C4BF5B-774C-4A2D-A8E1-388459044852', 'Lost Cities', 2, 2, 0, 40, 40, null, null, 50, null, null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('43A3FC6E-EC69-414D-9B0E-3906D1A37032', 'Barragoon', 2, 2, 0, 90, 90, null, null, 157779, null, null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('DEB3BDC9-641D-4901-BB24-391BE1C51739', 'Privacy 2', 5, 12, 0, 45, 45, null, null, 38062, null, null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('F9CBCDE5-F1BE-4099-A052-394E8751A68A', 'Stratego', 2, 2, 0, 45, 45, null, null, 1917, null, null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('4AEFAB27-1A5A-42E2-AA7D-3A552803EB7F', '7 Wonders Duel - Pantheon', 2, 2, 0, 30, 30, null, null, null, '9F73EF15-F210-4C4C-84C1-437CB5A93883', null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('7B93D12D-A676-42DF-9217-3B2118659A1D', 'Munchkin 6: Durchgeknallte Dungeons', 3, 6, 0, 60, 60, null, null, 33493, 'FE177D2B-0B5D-46EB-A113-81CD9FCE4775', null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('9EE3159D-8E3C-4BF8-9E3A-3C61F8907A18', 'Ta-Ke', 2, 2, 0, 30, 30, null, null, null, null, null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('5E6773A8-620B-4489-8AF5-3C6997C097C3', 'Chicago Express', 2, 6, 0, 60, 60, null, null, 31730, null, null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('39638CAE-BBEF-45B1-86C8-3CABE626F170', 'Agricola (Familienspiel)', 1, 4, 0, 60, 60, null, null, null, null, null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('6514AB74-013D-4DA8-86A9-3DC949F77AED', 'Die Portale von Molthar', 2, 5, 0, 45, 45, null, null, 181960, null, null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('12230528-C9FB-4511-B23C-3DDBBE793B1A', 'Mombasa', 2, 4, 0, 150, 150, null, null, 172386, null, null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('ADD19568-F7DD-4D39-9074-3DE36255CB36', 'Boss Monster', 2, 4, 0, 60, 60, null, null, null, null, null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('025D5EC1-A371-4832-BE45-40DD21E50D30', 'Scythe', 1, 5, 0, 115, 115, null, null, 169786, null, null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('CEBF983E-0A43-437B-9A05-41A069F0D9DA', '7 Wonders - Leaders', 3, 7, 0, 40, 40, null, null, null, '322F3FCC-0D12-4D18-827F-1673BD6A1F5A', null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('35D65DAD-36E4-42DF-9EA5-4208771D4FE4', 'Räuber der Nordsee', 2, 4, 0, 80, 80, null, null, 170042, null, null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('ADB16C86-8F08-44E7-B25B-42CFF53DAEE3', 'Menara', 1, 4, 0, 45, 45, null, null, 244608, null, null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('883D0F36-BA48-475B-A64D-430C6DCEDDA0', 'Carcassonne', 2, 5, 0, 30, 30, null, null, 822, null, null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('9F73EF15-F210-4C4C-84C1-437CB5A93883', '7 Wonders Duel', 2, 2, 0, 30, 30, null, null, 173346, null, null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('F6672718-39D9-46C5-9211-45544B135723', 'Machi Koro', 2, 4, 0, 30, 30, null, null, 143884, null, null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('285F5601-1535-468B-A443-4589ABC5F800', 'Istanbul', 2, 5, 0, 50, 50, null, null, 148949, null, null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('1409E0D0-5CC2-40F4-B272-46852CBF0423', 'Zug um Zug', 2, 5, 0, 60, 60, null, null, 32221, null, null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('07AF3FFB-1866-45E2-AD91-47DC71F4A1C1', 'The Game', 1, 5, 0, 20, 20, null, null, 173090, null, null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('388131C6-4120-4DF7-85E1-47EE2B64EE89', 'Codenames', 2, 8, 0, 15, 15, null, null, 178900, null, null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('0E043E67-7B3C-4647-9001-487F20D5A669', 'Not Alone', 2, 7, 0, 45, 45, null, null, 194879, null, null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('07C6D4B4-76E3-422E-8F0E-494D78FF67CA', 'Giants', 3, 5, 0, 60, 60, null, null, 38862, null, null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('BF6AFD5E-3EDB-4886-9374-4A94A569266B', 'Pandemie', 2, 4, 0, 45, 45, null, null, 30549, null, null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('65CCAD52-ACD9-4BDD-9208-4B9743E250ED', 'Kingdom Builder - Nomads', 2, 5, 0, 45, 45, null, null, null, 'F16908F8-8811-4DD9-9773-0E69A096FE15', null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('8D46FD79-B15B-437C-AA9B-4C654DD35026', 'Monopoly', 2, 8, 0, 240, 240, null, null, 1406, null, null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('43A2BBD5-1D5D-4ABC-8C92-4CADA3A5729E', 'Hoftheater', 2, 5, 0, 40, 40, null, null, 182120, null, null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('155E4F6C-368C-4FA2-B388-4D4FCE8A359C', 'Port Royal', 2, 4, 0, 40, 40, null, null, 565, null, null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('CDA55509-4E8D-4F9B-AFB1-4D6CFAFA08C1', 'Majesty - Deine Krone, dein Königreich', 2, 4, 0, 30, 30, null, null, null, null, null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('FCD61FF3-F6A7-4A55-B8B4-4EE879A3AEC4', 'Munchkin 6.5: Grausige Grüfte', 3, 6, 0, 60, 60, null, null, 193528, 'FE177D2B-0B5D-46EB-A113-81CD9FCE4775', null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('88555477-A992-4289-BD2C-4F061CC36B47', 'Photosynthese', 2, 4, 0, 60, 60, null, null, 218603, null, null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('A074F3F3-96D0-4031-8008-4FE420BB4713', 'Noria', 2, 4, 0, 120, 120, null, null, 233676, null, null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('E41AAA87-6337-4D68-9B96-5037E75B503C', 'Fußball-Duell', 2, 2, 0, 40, 40, null, null, null, null, null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('F1868BD0-7BBA-4611-A5B8-5097523BAD56', 'Arkham Horror - Das Kartenspiel - Wo das Verderben wartet (Mythos-Pack Dunwich 5)', 1, 2, 0, 120, 120, null, null, null, '9413EC6D-1116-483D-B2F7-E993B82013C4', null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('974D1366-202B-4C2E-8950-526434508858', 'Istanbul - Brief & Siegel', 2, 5, 0, 50, 50, null, null, null, '285F5601-1535-468B-A443-4589ABC5F800', null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('69E4AD75-32A0-4C57-919D-533E7BB3AF21', 'Was kostet die Welt?', 2, 6, 0, 120, 120, null, null, null, null, null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('22E94F0A-11AB-43FE-9E10-53D591766311', 'Heckmeck am Bratwurmeck', 2, 7, 0, 30, 30, null, null, 15818, null, null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('0814C055-AD39-428A-B5E3-545C955D6FE8', '7 Wonders - Babel', 2, 7, 0, 40, 40, null, null, null, '322F3FCC-0D12-4D18-827F-1673BD6A1F5A', null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('470D38EE-0124-4C01-9237-5BA2355839CC', 'Memoarrr!', 2, 4, 0, 20, 20, null, null, 230383, null, null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('78BCEDE3-DF00-42EF-9EBE-5BA821203DFA', 'Das MAD Spiel', 2, 4, 0, 60, 60, null, null, 1604, null, null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('CDC87B86-6189-460F-8F0F-5C1DD369B5FD', 'Rosenkönig', 2, 2, 0, 40, 40, null, null, null, null, null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('A449F162-96FB-48C0-9010-5DBC50772C02', 'Crazy Race', 2, 5, 0, 60, 60, null, null, 1346, null, null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('EFC8A59F-816E-4838-B174-5FA69E6E1341', 'Klong!', 2, 4, 0, null, null, null, null, 201808, null, null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('EBD04584-4B9B-4D2B-B37B-60E921441971', 'Zooloretto Duell', 2, 2, 0, 20, 20, null, null, 232481, null, null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('8A6BABDE-AF95-4F12-96AE-64F21DBEDAA3', 'Epic PvP', 2, 2, 0, 30, 30, null, null, null, null, null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('3BDAAA7E-B793-4E1B-B0B5-64FA1B3B3CC7', 'Scotland Yard', 2, 6, 0, 45, 45, null, null, 438, null, null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('DCCE13C2-6EBE-4F55-9375-66A61A2A567A', 'Munchkin Panic', 1, 6, 0, 60, 60, null, null, 156786, null, null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('8CDB3979-6615-4FD1-90C5-699AFAA6D68D', 'Arkham Horror - Das Kartenspiel - Der Pfad nach Carcosa', 1, 2, 0, 120, 120, null, null, null, '9413EC6D-1116-483D-B2F7-E993B82013C4', null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('073BCF86-63ED-49C5-ABC8-6B018B598D41', 'Gaia', 2, 5, 0, 30, 30, null, null, 27787, null, null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('59451AA4-DA4A-4F8F-A076-6DB8D0DC7B03', 'This War of Mine - Das Brettspiel', 1, 6, 0, 120, 120, null, null, null, null, null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('C0E0264A-6FC4-4280-8181-6FC55ED0D5CE', 'Phase 10 - Das Brettspiel', 2, 6, 0, 60, 60, null, null, null, null, null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('CD0EA0B7-9B8D-4DB1-B203-7005022234B5', 'Dungeon Lords', 2, 4, 0, 90, 90, null, null, 45315, null, null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('A3FD5543-611E-4900-9891-714AE7FA8C96', 'Haus der Sonne', 2, 2, 0, 40, 40, null, null, 177727, null, null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('C2B83001-E8C9-4F2D-90D3-74B0A6BA8F99', 'Die verbotene Insel', 2, 4, 0, 30, 30, null, null, 65244, null, null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('CA97F406-4EC3-4EFD-B1A2-74DB3CDE1BE6', 'Zug um Zug - Deutschland', 2, 5, 0, 60, 60, null, null, null, null, null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('2577C0DE-CC96-426E-96F3-756B36412C86', 'Munchkin 8: Echsenmenschen und Zentauren', 3, 6, 0, 60, 60, null, null, null, 'FE177D2B-0B5D-46EB-A113-81CD9FCE4775', null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('06FAA74C-AC79-4D93-82EF-75E490D5FA1C', 'Catan - Das Duell', 2, 2, 0, 45, 45, null, null, null, null, null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('1E0BD59B-CDBA-45CE-9568-76B47E180231', 'Munchkin 7: Mit beiden Händen schummeln', 3, 6, 0, 60, 60, null, null, 90567, 'FE177D2B-0B5D-46EB-A113-81CD9FCE4775', null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('5986B3A2-BF4D-4EC0-A65D-79E5771F9404', 'Agricola (Kennerspiel)', 1, 4, 0, 90, 90, null, null, null, null, null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('33039BE9-31D3-4FA8-8D9A-7B7B855B0F8D', 'Arkham Horror - Das Kartenspiel - Blut auf dem Altar (Mythos-Pack Dunwich 3)', 1, 2, 0, 120, 120, null, null, null, '9413EC6D-1116-483D-B2F7-E993B82013C4', null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('3EFAFF16-5853-4764-8A9E-7BCF185B1ACE', 'Uno Rummy', 2, 4, 0, 45, 45, null, null, null, null, null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('ABB9F35C-93B0-4E63-BD8F-7C0896C5B8D2', 'Junta', 2, 7, 0, 120, 120, null, null, 242, null, null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('829482D0-A6BA-417C-82EF-7C22E099A2A4', 'Funkenschlag Fabrikmanager', 2, 5, 0, 60, 60, null, null, null, null, null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('20263A53-C360-4512-A282-7CB8F15D30AB', 'Maharani - Mosaic Palace', 2, 4, 0, 40, 40, null, null, null, null, null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('35E6583D-6AC0-4C03-B744-7CD876896746', 'Battlestar Galactica', 3, 6, 0, 180, 180, null, null, 2688, null, null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('6D3176EF-D51A-4629-B74D-7E5EC514BF87', 'Magic Maze', 1, 8, 0, 15, 15, null, null, 209778, null, null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('C4ECF35C-684C-4396-A743-7E8CBD3BEF54', 'Arkham Horror - Das Kartenspiel - Widerhall aus der Vergangenheit (Mythos-Pack Carcosa 1)', 1, 2, 0, 120, 120, null, null, null, '9413EC6D-1116-483D-B2F7-E993B82013C4', null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('9084D1B8-56F2-4238-BF6B-8169710526F4', 'The Pursuit of Happiness', 1, 4, 0, 90, 90, null, null, 181687, null, null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('FE177D2B-0B5D-46EB-A113-81CD9FCE4775', 'Munchkin', 3, 6, 0, 60, 60, null, null, 1927, null, null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('11BE64CC-1C4E-409F-9A7A-81EF6E321359', 'Amerigo', 2, 4, 0, 75, 75, null, null, 38924, null, null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('822F3D4E-92BF-4C9E-ABA8-82532EF4B486', 'Port Royal: Das Abenteuer beginnt...', 1, 4, 0, 20, 50, null, null, 219265, '155E4F6C-368C-4FA2-B388-4D4FCE8A359C', null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('234D4745-2D22-4AFF-8291-8479C3928021', 'Robinson Crusoe', 1, 4, 0, 120, 120, null, null, 51319, null, null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('AAAD0FD6-1ABB-4565-8C00-850C01A5DD4E', 'Colt Express - Marshal & Gefangene', 3, 8, 0, 40, 40, null, null, null, 'AA24A784-0644-4880-9A21-1883C7F1A814', null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('F046051F-4086-4421-9A75-8522C593E9C3', 'The Lost Expedition', 1, 5, 0, 50, 50, null, null, 216459, null, null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('174D35D4-F38E-4AC8-903E-856549742A18', 'Shogun', 3, 5, 0, 240, 240, null, null, 2043, null, null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('35FFB2E6-898C-48F8-8667-872AC39C13F1', 'Galileo - The Game', 2, 6, 0, 30, 30, null, null, null, null, null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('5B4FFE3E-4D49-4224-811F-8882022036F7', 'Arkham Horror - Das Kartenspiel - Der unausprechliche Eid (Mythos-Pack Carcosa 2)', 1, 2, 0, 120, 120, null, null, null, '9413EC6D-1116-483D-B2F7-E993B82013C4', null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('2388E667-2ECA-4967-BEB0-88DAC67B5F56', 'Epic PvP - Halbling, Katzenmensch, Dunkler Ritter, Kleriker', 2, 2, 0, 30, 30, null, null, null, '8A6BABDE-AF95-4F12-96AE-64F21DBEDAA3', null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('BCC0A9D0-87E9-4832-BFD2-8DB38D1C8AE4', 'Captain Sonar - Volles Rohr', 2, 8, 0, 60, 60, null, null, null, '68EDD0A8-CE28-4040-A069-DD3622B971DB', null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('593B1B0B-57E8-4C2D-8AEC-8DF73B91C258', 'Love Letter', 2, 4, 0, 20, 20, null, null, 129622, null, null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('C8E74AB9-41F2-47E7-BB17-8E0ED30A53A9', 'Labyrinth - Das Duell', 2, 2, 0, 20, 20, null, null, null, null, null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('1A213CCF-1948-4F3A-9329-8E7AF6243BC5', 'Wanted 7', 2, 6, 0, 20, 20, null, null, 232242, null, null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('15549408-2B28-496A-B1E0-8EFC9A2FDF9E', 'Mythotopia', 2, 4, 0, 60, 60, null, null, 133632, null, null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('36547083-0D1D-4ECE-83D6-8F77A5E6C69B', 'Das Labyrinth der Meister', 2, 4, 0, 30, 30, null, null, 437, null, null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('2F34A425-D9E2-4B2B-8974-90A680D49F08', 'Metro', 2, 6, 0, 45, 45, null, null, 559, null, null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('80165333-CD21-4FD6-B4FF-92339023DF5A', 'Do De Li Do', 2, 6, 0, 20, 20, null, null, 206939, null, null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('C3183FAE-FD59-45F5-9B1A-9293B2A7235E', 'Camel Up - Supercup', 2, 10, 0, 30, 30, null, null, null, '7C46E7F3-B827-41ED-8989-1E35063CE47A', null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('9082D6FC-C0AF-4762-AE5E-9305D8526636', 'Agricola - Die Bauern und das liebe Vieh', 2, 2, 0, 30, 30, null, null, null, null, null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('551BC8E5-924F-415E-89FC-9481CB0BD3D5', 'Scythe - Invasoren aus der Ferne', 1, 7, 0, 120, 120, null, null, null, '025D5EC1-A371-4832-BE45-40DD21E50D30', null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('8F089DB6-1B79-4E1D-B022-96985CD9F18A', 'Die Legenden von Andor: Der Sternenschild', 2, 4, 10, 60, 90, null, null, 136986, '0DCA2D1A-6845-4375-A488-C0E867EA90F8', null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('9AB949EC-CA83-4BF1-A833-98A8688684AC', 'Otys', 2, 4, 0, null, null, null, null, 222542, null, null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('EECC55EA-26F6-45AA-8F6A-99025044BF91', 'Munchkin 3: Beschwörungsfehler', 3, 6, 0, 90, 90, null, null, 6606, 'FE177D2B-0B5D-46EB-A113-81CD9FCE4775', null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('11E15132-6F75-4D3C-A43D-9A17CB8952FC', 'Mixmo', 2, 6, 0, 15, 15, null, null, 42105, null, null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('22C47D49-FFAB-49F7-9D34-9B0E91E90F4A', 'Rajas of the Ganges', 2, 4, 0, 75, 75, null, null, 220877, null, null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('1D3EE804-3650-48E6-B0FC-9C23EEC9095A', 'Elfer Raus', 2, 6, 0, 20, 20, null, null, null, null, null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('1E852C6B-FB0C-4C49-B162-9C64E2BA2B89', 'Tichu', 3, 10, 0, 30, 30, null, null, 215, null, null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('8314DE0E-D496-49E5-8709-9CACB4908E9B', 'Kanban - Automotive Revolution', 2, 4, 0, 120, 120, null, null, null, null, null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('4BCFA1EE-1665-449B-9C17-9D6D84E96459', 'Zooloretto', 2, 5, 0, 45, 45, null, null, 27588, null, null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('8946FAF9-6C9A-4D13-AC3F-9F672821EB23', 'Tobago', 2, 4, 0, 60, 60, null, null, 42215, null, null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('356E7929-34A6-44EC-A3C7-9F9421080185', 'Zombicide', 2, 6, 0, 180, 180, null, null, 113924, null, null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('478C650A-A103-4870-A079-A0C9FD7B8AA6', 'Fresko', 2, 4, 0, 60, 60, null, null, 66188, null, null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('CD110B0C-00A2-4F62-B2F6-A2FE68311CA3', 'Istanbul - Das Würfelspiel', 2, 4, 0, 40, 40, null, null, null, null, null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('AAE9FAFA-7D1C-4BFB-9878-A311D3D87335', 'Trans America', 2, 6, 0, 30, 30, null, null, 2842, null, null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('FACAB68C-B4F1-4254-978B-A33076E1E222', 'Karuba', 2, 4, 0, 40, 40, null, null, 183251, null, null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('740252B3-6E4B-4E79-973C-A5DE36530693', 'Die Siedler von Catan - 5-6 Spieler', 3, 6, 0, 90, 90, null, null, null, 'B895B5A3-2BD7-49F6-96E8-AC6BF916EDDA', null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('667236D6-207B-4C88-8F8B-A6B1DCD908C4', 'Luxor', 2, 4, 0, 45, 45, null, null, 1205, null, null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('4A5C55C4-DF87-4D5F-9BC0-A72A8AD8979C', 'The Lost Expedition - The Fountain of Youth', 1, 5, 0, 50, 50, null, null, null, 'F046051F-4086-4421-9A75-8522C593E9C3', null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('722E81EB-FA93-48AA-B41E-A7E295FD79E4', 'Scythe - Kolosse der Lüfte', 1, 7, 0, 120, 120, null, null, null, '025D5EC1-A371-4832-BE45-40DD21E50D30', null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('B941438B-1190-45CD-96CE-A7F236CC235D', 'Arkham Horror - Das Kartenspiel - Gestaltlos und unsichtbar (Mythos-Pack Dunwich 4)', 1, 2, 0, 120, 120, null, null, null, '9413EC6D-1116-483D-B2F7-E993B82013C4', null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('019EBA74-3D95-482A-A981-A86776CE28BE', 'Exploding Kittens', 2, 5, 0, 15, 15, null, null, 172225, null, null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('95C8A642-3650-4AAF-A1E0-A93127EDCA01', 'Vor den Toren von Loyang', 1, 4, 0, 120, 120, null, null, 39683, null, null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('010F1367-CDF5-4593-8769-A94F84F85719', 'Food Chain Magnate', 2, 5, 0, 240, 240, null, null, 175914, null, null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('CA927B7D-F5F5-43EE-BDE1-AB8E8DD293FA', 'Jenseits von Theben', 2, 4, 0, 60, 60, null, null, 13883, null, null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('07BC6BF1-F269-4C2F-A2CF-AC4100E61B23', 'Fiese Freunde Fette Feten', 2, 6, 0, null, null, null, null, 16366, null, null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('B895B5A3-2BD7-49F6-96E8-AC6BF916EDDA', 'Die Siedler von Catan', 3, 4, 0, 75, 75, null, null, 13, null, null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('1FF967CE-B4F7-4C13-A1E4-AC6CF7519EB1', 'Imhotep - Eine neue Dynastie', 2, 4, 0, 50, 50, null, null, null, null, null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('1A76B5DD-B9E8-4B54-8A43-ACCE111605C2', 'Die Legenden von Andor - Chada und Thorn', 2, 2, 0, 45, 45, null, null, null, '0DCA2D1A-6845-4375-A488-C0E867EA90F8', null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('81D1B407-7922-4804-B052-AF83AB6B4CE2', 'Leaders', 2, 6, 0, 180, 180, null, null, null, null, null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('F88AFDF1-645B-426D-9A26-AFE9007E6BCF', 'Lost Galaxy', 2, 4, 0, 15, 15, null, null, null, null, null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('B683BD59-494D-40F4-8A65-AFFF8E997035', 'Village', 2, 4, 0, 90, 90, null, null, 104006, null, null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('4EC2FD40-372D-4C58-A599-B28AB39FAE3D', 'Machi Koro - Großstadt', 2, 4, 0, 40, 40, null, null, null, 'F6672718-39D9-46C5-9211-45544B135723', null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('0C09626F-CEE1-4CE4-BABA-B2FE602DE46F', 'Escape - Der Fluch des Tempels', 2, 5, 0, 10, 10, null, null, null, null, null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('2DDD0247-0973-4771-9254-B3F1D75C8A84', 'Dixit', 3, 6, 0, 30, 30, null, null, 39856, null, null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('241F305A-258C-4DF7-BD46-B43D8700892C', 'Istanbul - Mokka & Backschich', 2, 5, 0, 50, 50, null, null, null, '285F5601-1535-468B-A443-4589ABC5F800', null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('C7F2D69C-EDBC-42D2-AB0E-B48D61F826BA', 'Futschikato', 3, 8, 0, 15, 15, null, null, 203430, null, null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('A85F0C32-A277-4DFD-8247-B7AABB369FC0', 'Tumult Royal', 2, 4, 0, 40, 40, null, null, null, null, null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('A7937D54-AD31-4085-B9E9-B8CF00C2992D', 'Tabu', 4, 99, 0, 90, 90, null, null, 7106, null, null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('1A667C5F-4705-464A-A243-B97D570198F5', 'Ohne Furcht und Adel - Die dunklen Lande', 3, 7, 0, 60, 60, null, null, null, '11DC8C7B-928E-4B6B-990E-21B35FA4EC01', null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('A9A3FA85-7AC8-4EF1-88BF-BBEEBA4D797E', 'Mercado', 2, 4, 0, 30, 30, null, null, 244794, null, null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('ECD65D28-4808-4C05-8E48-BDEBE7923ED3', 'Trivial Pursuit Genus Edition', 2, 6, 0, 60, 60, null, null, null, null, null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('073EC751-EB82-48C6-A030-C0AB5B4E659B', 'Sushi Dice', 2, 6, 0, 20, 20, null, null, 142334, null, null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('0DCA2D1A-6845-4375-A488-C0E867EA90F8', 'Die Legenden von Andor', 2, 4, 0, 90, 90, null, null, 127398, null, null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('56AEC2B6-B022-4A91-B58F-C4B05A6F0592', 'Die holde Isolde', 2, 5, 0, 30, 30, null, null, 181327, null, null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('BB943E77-DDCD-46DB-9A0E-C8A8CE82F4A5', 'Kingdomino', 2, 4, 0, 30, 30, null, null, 204583, null, null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('C7DDBEE7-2540-42ED-BEB9-C967012D8552', 'Carta Impera Victoria', 2, 4, 0, 20, 20, null, null, null, null, null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('66025CBC-A5BE-4925-959B-CA74DE5E3088', 'Galaxy Trucker', 2, 4, 0, 60, 60, null, null, 31481, null, null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('AEEDD075-A8BF-4CD8-9379-CAB56064A2B8', 'Targi', 2, 2, 0, 60, 60, null, null, 118048, null, null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('40303998-2DC6-4A8B-BC8D-CC4ACAD4F34C', 'St. Petersburg', 2, 4, 0, 60, 60, null, null, 9217, null, null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('4A78E006-5651-4713-8D7A-CE9F0D9F826A', 'Azul', 2, 4, 0, 45, 45, null, null, 230802, null, null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('DE3171AA-6C46-47C0-BC02-D0C475C9506C', 'Tempel des Schreckens', 3, 10, 0, 15, 15, null, null, 206915, null, null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('E33EC3F8-2851-456D-89EE-D1D25208AB3B', 'Nmbr9', 1, 4, 0, 20, 20, null, null, 217449, null, null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('D5E07C29-0992-4701-9412-D2B53449F4EE', 'Die Legenden von Andor - Die Reise in den Norden', 2, 4, 0, 60, 60, null, null, null, '0DCA2D1A-6845-4375-A488-C0E867EA90F8', null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('05FE0E56-E514-4077-9285-D2D7EACE5303', 'Paku Paku', 2, 8, 0, 10, 10, null, null, 214491, null, null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('E8207978-40D1-4C71-9A48-D3544EC3C720', 'St. Petersburg - In bester Gesellschaft - Das Bankett', 2, 5, 0, 30, 30, null, null, null, '40303998-2DC6-4A8B-BC8D-CC4ACAD4F34C', null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('F012A3D8-07C5-4DE1-AC17-D5107A91BCDC', 'Arkham Horror - Das Kartenspiel - Verloren in Zeit und Raum (Mythos-Pack Dunwich 6)', 1, 2, 0, 120, 120, null, null, null, '9413EC6D-1116-483D-B2F7-E993B82013C4', null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('89A70D5B-533F-48A1-A428-D54E6B026B8D', 'Wizard', 3, 6, 0, 20, 20, null, null, 1465, null, null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('0832A6B3-ED5E-4C83-93B8-D55C104D88EF', 'Isle of Skye - Der Wanderer', 2, 5, 0, 60, 60, null, null, null, '8C0E584E-5B48-42AD-8C04-DBB68E434D6E', null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('96F9374E-8877-4727-9D79-D58212B13B9F', 'Race for the Galaxy', 2, 4, 0, 60, 60, null, null, 28143, null, null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('A209B7C8-1EB8-4E57-BEFA-D65EA9095410', 'Das Orakel von Delphi', 2, 4, 0, 100, 100, null, null, 193558, null, null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('D2728977-3F57-4822-A006-D842A501A86D', 'Die Legenden von Andor - Neue Helden', 5, 6, 0, 75, 75, null, null, null, '0DCA2D1A-6845-4375-A488-C0E867EA90F8', null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('3FD38E60-3635-4487-9F1A-D9314AA0E8E9', 'Orléans', 2, 4, 0, 90, 90, null, null, 164928, null, null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('29051000-9D56-41D1-B4D5-DB4323AA63DD', 'Imaginarium', 2, 5, 0, 90, 90, null, null, 146548, null, null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('11E054D8-33FE-4317-A34B-DBA8D7C82BCB', 'Die Alchemisten', 2, 4, 0, 120, 120, null, null, 161970, null, null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('8C0E584E-5B48-42AD-8C04-DBB68E434D6E', 'Isle of Skye', 2, 5, 0, 60, 60, null, null, null, null, null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('3EC097A1-B0BB-470B-958B-DBBC60EF93F7', 'Arkham Horror - Das Kartenspiel - Das Miskatonic-Museum (Mythos-Pack Dunwich 1)', 1, 2, 0, 120, 120, null, null, null, '9413EC6D-1116-483D-B2F7-E993B82013C4', null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('6E9E9C7F-2222-4B6C-9074-DBC5E337874D', 'Halt mal kurz', 3, 6, 0, 20, 20, null, null, null, null, null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('D110DC93-BEC0-4776-8B6F-DD2B6DABC070', 'The Cave', 2, 5, 0, 60, 60, null, null, 129351, null, null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('68EDD0A8-CE28-4040-A069-DD3622B971DB', 'Captain Sonar', 2, 8, 0, 60, 60, null, null, 171131, null, null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('E250CA4F-C4EE-4116-B628-DD8D05D19AE9', 'Raptor', 2, 2, 0, 30, 30, null, null, 133450, null, null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('C119A2C9-0564-49E4-A5A2-DE580F2D4BE8', 'Drecksau', 2, 4, 0, 20, 20, null, null, 118410, null, null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('960DEF58-0405-40C1-9A19-DE78B950C4EE', 'Thurn und Taxis', 2, 4, 0, 50, 50, null, null, 21790, null, null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('B601B7F4-A99B-4272-ADAC-DEEA3732CF47', 'Bang!', 4, 7, 0, 30, 30, null, null, 3955, null, null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('69187B47-09A2-4EB7-923E-E0CF57290FB6', 'Inis', 2, 4, 0, 60, 60, null, null, 155821, null, null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('049523D7-8C43-42BF-91E5-E1BB93115EF6', 'Game of Trains', 2, 4, 0, 20, 20, null, null, 180602, null, null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('CEDD8157-8CFA-448E-A301-E2DB916212E7', 'Adrenalin', 3, 5, 0, 60, 60, null, null, null, null, null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('68CBAF3B-68D3-4EF3-879B-E5569F2276F5', 'Gentes', 1, 4, 0, 90, 90, null, null, 217780, null, null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('4EBEAA88-D3AB-4932-AB6F-E68B2CA9A4A6', 'The Manhattan Project', 2, 5, 0, 90, 90, null, null, 63628, null, null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('4EC2ADC6-51BE-4B7B-BB2A-E7282192AE61', 'Memo Dice', 2, 4, 0, 10, 10, null, null, 232425, null, null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('36F84A54-4971-4428-8E3E-E899B7D04BAD', 'Munchkin 4: Rasende Rösser', 3, 6, 0, 60, 60, null, null, 20660, 'FE177D2B-0B5D-46EB-A113-81CD9FCE4775', null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('9413EC6D-1116-483D-B2F7-E993B82013C4', 'Arkham Horror - Das Kartenspiel', 1, 2, 0, 120, 120, null, null, null, null, null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('CCE27FE9-C5AC-4A0A-B82A-EC26303968B2', 'Alhambra', 2, 6, 0, 60, 60, null, null, 6249, null, null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('B6AC92BD-8E0E-4C62-9F22-ED3BAA2B1AEA', 'Targi - Die Erweiterung', 2, 2, 0, 60, 60, null, null, null, 'AEEDD075-A8BF-4CD8-9379-CAB56064A2B8', null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('411E50AD-C5BD-47DF-BC02-ED5BF32B591D', '7 Wonders - Wonder Pack', 3, 7, 0, 40, 40, null, null, null, '322F3FCC-0D12-4D18-827F-1673BD6A1F5A', null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('B8BE2745-CB60-44E5-883A-EF3C9F866E10', 'Carcassonne - Händler und Baumeister', 2, 6, 0, 40, 40, null, null, null, '883D0F36-BA48-475B-A64D-430C6DCEDDA0', null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('44210C39-85B6-46C3-899E-F2CA6A560530', 'Numeri', 2, 4, 0, 30, 30, null, null, 11215, null, null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('6CDD9868-4FD6-4485-B38A-F3940B5CAFBC', 'Cottage Garden', 1, 4, 0, 60, 60, null, null, 204027, null, null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('E1D47477-A75C-41C7-9EB0-F3E12D9EC004', 'Hit Z Road', 1, 4, 0, 60, 60, null, null, 176083, null, null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('0B341063-0DE9-4FF3-BD9A-F460B527FE6E', 'Heaven & Ale', 2, 4, 0, 90, 90, null, null, 227789, null, null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('15B40C76-5B37-4A2B-86E0-F4C637D39F3C', 'Nobody is Perfect', 3, 10, 0, 60, 60, null, null, 8829, null, null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('D01CA390-8F5D-4AFE-8F7B-F616FBC6275A', 'Risiko', 2, 5, 0, 90, 90, null, null, 181, null, null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('AB99BD1E-44E8-4572-8A76-F64774D2B252', 'Port Royal - Ein Auftrag geht noch...', 2, 4, 0, 40, 40, null, null, null, '155E4F6C-368C-4FA2-B388-4D4FCE8A359C', null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('E85C892A-AFB1-46AA-A2AB-F67378CE5819', 'Munchkin 2: Abartige Axt', 3, 6, 0, 60, 60, null, null, 3943, 'FE177D2B-0B5D-46EB-A113-81CD9FCE4775', null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('CEB0E64B-B269-4E4C-BD59-F7ED8BB90A03', 'Great Western Trail', 2, 4, 0, 150, 150, null, null, 193738, null, null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('D7DC077B-23D4-49DC-B6EF-F924C6B2C125', 'Arkham Horror - Das Kartenspiel - Der Essex-County-Express (Mythos-Pack Dunwich 2)', 1, 2, 0, 120, 120, null, null, null, '9413EC6D-1116-483D-B2F7-E993B82013C4', null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('578AA2AB-AF21-49D2-BB82-F93E99797F10', 'Imhotep', 2, 4, 0, 40, 40, null, null, 36919, null, null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('939C6FBA-4C00-4168-92FC-F946C4292AD7', 'Decrypto', 3, 8, 0, 30, 30, null, null, 65952, null, null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('7453CB53-2DEB-4A8C-B7EF-F9A4F12E9710', 'Kahuna', 2, 2, 0, 40, 40, null, null, 394, null, null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('25FBFB5E-7D3D-46A1-A7E9-F9CFC5C83FD3', 'When I Dream', 4, 10, 0, 30, 30, null, null, 198454, null, null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('F66FA62C-FAAC-44BD-BF77-FA10EEFA62C3', 'Sea of Clouds', 2, 4, 0, 40, 40, null, null, 189052, null, null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('98634FC4-D569-4459-8503-FA3BDD377A42', '7 Wonders: Cities', 2, 8, 10, 40, 40, null, null, 111661, '322F3FCC-0D12-4D18-827F-1673BD6A1F5A', null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('7C5F18E4-EB0E-402E-A518-FB4B9F485380', 'Robinson Crusoe - Die Fahrt der Beagle', 1, 4, 0, 120, 120, null, null, null, '234D4745-2D22-4AFF-8291-8479C3928021', null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('4674E54B-63AA-4D63-A44C-FC1A63C7C631', 'Les Poilus', 2, 5, 14, 30, 30, null, null, 171668, null, null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('400682B7-0017-4BE2-9090-FCDC6C454363', 'Tiefseeabenteuer', 3, 6, 0, 30, 30, null, null, 169654, null, null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('B5344F07-6BB8-4054-ADF5-FD5DE1B1B9B8', 'Outlive', 2, 4, 0, 90, 90, null, null, 191051, null, null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('585A13F8-EA52-414A-8CBF-FE172823281F', 'Armageddon', 3, 4, 0, 90, 90, null, null, 15602, null, null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('02BCE846-0407-458D-9D11-FE471CAB204A', 'Dominion', 2, 4, 0, 60, 60, null, null, 36218, null, null);
INSERT INTO Games (Id, Name, MinPlayers, MaxPlayers, MinAge, MinDuration, MaxDuration, BuyPrice, BuyDate, BoardGameGeekId, MainGameId, PublisherId) VALUES ('61E7FC15-611A-484B-BF91-FE69C7468FE3', 'Loot Island', 2, 5, 0, 60, 60, null, null, 235512, null, null);
INSERT INTO GameAuthor (GameId, AuthorId) VALUES ('BF6AFD5E-3EDB-4886-9374-4A94A569266B', '596EC8D9-F007-429F-8E3A-091BCD579BCB');
INSERT INTO GameAuthor (GameId, AuthorId) VALUES ('DBC6FBB7-FEF0-422C-8DCA-049AA98AF2A9', '513B1C83-EA29-42A6-8E35-0D54D5EBBA83');
INSERT INTO GameAuthor (GameId, AuthorId) VALUES ('ADD19568-F7DD-4D39-9074-3DE36255CB36', '99C9F040-360C-46D5-99A0-1064790F3D9C');
INSERT INTO GameAuthor (GameId, AuthorId) VALUES ('A074F3F3-96D0-4031-8008-4FE420BB4713', 'FEEEC549-73A6-4B79-BA45-197131266DA4');
INSERT INTO GameAuthor (GameId, AuthorId) VALUES ('6B007B51-FDB7-4C6C-9634-0653E55A40FB', '4A1FF5D3-B21F-4A9E-8DDE-1E2C676EB90E');
INSERT INTO GameAuthor (GameId, AuthorId) VALUES ('883D0F36-BA48-475B-A64D-430C6DCEDDA0', 'FB37DD5F-303F-4BCE-91F9-2E01C8ED5527');
INSERT INTO GameAuthor (GameId, AuthorId) VALUES ('ADD19568-F7DD-4D39-9074-3DE36255CB36', '936CA6F5-4FA0-481F-8EF6-2F006E8A0A87');
INSERT INTO GameAuthor (GameId, AuthorId) VALUES ('025D5EC1-A371-4832-BE45-40DD21E50D30', '569DB6AD-B281-4909-8246-2F34BBD67164');
INSERT INTO GameAuthor (GameId, AuthorId) VALUES ('07AF3FFB-1866-45E2-AD91-47DC71F4A1C1', '992883F7-2ABE-480A-844E-3960BF299113');
INSERT INTO GameAuthor (GameId, AuthorId) VALUES ('BCEBA3EE-5CAC-4000-8B9C-086E80F5DC27', 'F0BB6CF2-8064-4DA1-B55A-3A88ECCA438F');
INSERT INTO GameAuthor (GameId, AuthorId) VALUES ('DD6FD171-28B8-48B5-A92D-04C7DE4F2D9A', '01235F95-D910-4AB3-B720-4983542FDDB9');
INSERT INTO GameAuthor (GameId, AuthorId) VALUES ('DBDF1C0B-F813-45DA-B66C-00632E2B113F', '5D42F290-0EE8-4C49-BC60-4A2FB56325CE');
INSERT INTO GameAuthor (GameId, AuthorId) VALUES ('2DDD0247-0973-4771-9254-B3F1D75C8A84', 'ECABC930-A299-45A4-96BE-4A4E6865A4A2');
INSERT INTO GameAuthor (GameId, AuthorId) VALUES ('4721FAB4-B8E9-432C-A6A6-02741EE71E45', 'EFE87A30-8CC5-4B0D-87D4-4CE6592AE0A3');
INSERT INTO GameAuthor (GameId, AuthorId) VALUES ('ADD19568-F7DD-4D39-9074-3DE36255CB36', '212AE2C9-3B0B-4DA4-BC32-501B85229FC8');
INSERT INTO GameAuthor (GameId, AuthorId) VALUES ('9F73EF15-F210-4C4C-84C1-437CB5A93883', '0BAE5283-E528-4772-863D-5343F7F2370D');
INSERT INTO GameAuthor (GameId, AuthorId) VALUES ('43A2BBD5-1D5D-4ABC-8C92-4CADA3A5729E', '0BAE5283-E528-4772-863D-5343F7F2370D');
INSERT INTO GameAuthor (GameId, AuthorId) VALUES ('FCD61FF3-F6A7-4A55-B8B4-4EE879A3AEC4', '043A7A03-41D0-420C-AB04-55CBEC5407A7');
INSERT INTO GameAuthor (GameId, AuthorId) VALUES ('EECC55EA-26F6-45AA-8F6A-99025044BF91', '043A7A03-41D0-420C-AB04-55CBEC5407A7');
INSERT INTO GameAuthor (GameId, AuthorId) VALUES ('4674E54B-63AA-4D63-A44C-FC1A63C7C631', '0124B657-140C-4F2F-8736-581412689B3C');
INSERT INTO GameAuthor (GameId, AuthorId) VALUES ('ADB16C86-8F08-44E7-B25B-42CFF53DAEE3', '0DB0C6B8-5739-41A3-8397-59791F43E707');
INSERT INTO GameAuthor (GameId, AuthorId) VALUES ('8D46FD79-B15B-437C-AA9B-4C654DD35026', '44ADE932-616C-4141-A580-5DE0CAC24C6A');
INSERT INTO GameAuthor (GameId, AuthorId) VALUES ('F8E3A417-6C7C-4A98-9203-0EE5F4596A35', '622D0CFC-4EFA-42C2-99A7-6A130FF0BE0D');
INSERT INTO GameAuthor (GameId, AuthorId) VALUES ('CEBF983E-0A43-437B-9A05-41A069F0D9DA', '44248E24-F2D0-459D-8D68-6A5192D321AA');
INSERT INTO GameAuthor (GameId, AuthorId) VALUES ('9F73EF15-F210-4C4C-84C1-437CB5A93883', '44248E24-F2D0-459D-8D68-6A5192D321AA');
INSERT INTO GameAuthor (GameId, AuthorId) VALUES ('98634FC4-D569-4459-8503-FA3BDD377A42', '44248E24-F2D0-459D-8D68-6A5192D321AA');
INSERT INTO GameAuthor (GameId, AuthorId) VALUES ('69E4AD75-32A0-4C57-919D-533E7BB3AF21', '83F5CFF9-8726-40B9-9280-7BD3D2933B24');
INSERT INTO GameAuthor (GameId, AuthorId) VALUES ('F6672718-39D9-46C5-9211-45544B135723', '016B4449-3B12-4626-95BF-7E3261069717');
INSERT INTO GameAuthor (GameId, AuthorId) VALUES ('83F314E6-760D-46DF-9559-03F7E92FBD4A', '551ADA56-2C15-4650-9076-8690FB96BA49');
INSERT INTO GameAuthor (GameId, AuthorId) VALUES ('285F5601-1535-468B-A443-4589ABC5F800', '41FF88DB-6D01-42EF-B9D1-9544A077C63A');
INSERT INTO GameAuthor (GameId, AuthorId) VALUES ('974D1366-202B-4C2E-8950-526434508858', '41FF88DB-6D01-42EF-B9D1-9544A077C63A');
INSERT INTO GameAuthor (GameId, AuthorId) VALUES ('43A2BBD5-1D5D-4ABC-8C92-4CADA3A5729E', '4049A013-B95D-4EE6-B0E9-9DF6776654A5');
INSERT INTO GameAuthor (GameId, AuthorId) VALUES ('155E4F6C-368C-4FA2-B388-4D4FCE8A359C', '2722A16F-5BBE-4B90-A974-ACF6FF92E608');
INSERT INTO GameAuthor (GameId, AuthorId) VALUES ('12230528-C9FB-4511-B23C-3DDBBE793B1A', '8C784F1B-9B08-420F-B53D-AF57A688A898');
INSERT INTO GameAuthor (GameId, AuthorId) VALUES ('822F3D4E-92BF-4C9E-ABA8-82532EF4B486', '8C784F1B-9B08-420F-B53D-AF57A688A898');
INSERT INTO GameAuthor (GameId, AuthorId) VALUES ('E41AAA87-6337-4D68-9B96-5037E75B503C', 'BE579153-208B-456D-9EC7-B361B419D7E8');
INSERT INTO GameAuthor (GameId, AuthorId) VALUES ('83F314E6-760D-46DF-9559-03F7E92FBD4A', 'AEF2DF96-5249-4133-9E8D-B6951F0CC951');
INSERT INTO GameAuthor (GameId, AuthorId) VALUES ('79F5544A-A0A1-46F8-AAF2-0EC2EE3FD146', 'BD3B18CF-768F-49A1-8E72-C66E3A582B1B');
INSERT INTO GameAuthor (GameId, AuthorId) VALUES ('1409E0D0-5CC2-40F4-B272-46852CBF0423', 'BD3B18CF-768F-49A1-8E72-C66E3A582B1B');
INSERT INTO GameAuthor (GameId, AuthorId) VALUES ('88555477-A992-4289-BD2C-4F061CC36B47', 'B7551137-219F-4E54-9216-C6A8AA51CDFA');
INSERT INTO GameAuthor (GameId, AuthorId) VALUES ('DBDF1C0B-F813-45DA-B66C-00632E2B113F', 'B91A227F-E1C2-4B21-B42A-CBAEE36AC3D5');
INSERT INTO GameAuthor (GameId, AuthorId) VALUES ('8F089DB6-1B79-4E1D-B022-96985CD9F18A', '35489DFC-12E6-42A0-AD60-D256A2137837');
INSERT INTO GameAuthor (GameId, AuthorId) VALUES ('35D65DAD-36E4-42DF-9EA5-4208771D4FE4', 'A4A20785-39CB-4200-B308-D6D98CFAFB76');
INSERT INTO GameAuthor (GameId, AuthorId) VALUES ('E41AAA87-6337-4D68-9B96-5037E75B503C', 'AE7D3B83-6DDB-4F3F-A227-D7E36324594E');
INSERT INTO GameAuthor (GameId, AuthorId) VALUES ('D9551C9B-951D-4EE3-A2AD-05515CDCB676', '63FBEDD1-6FE6-48B8-9515-DC9E8248AF2F');
INSERT INTO GameAuthor (GameId, AuthorId) VALUES ('07C6D4B4-76E3-422E-8F0E-494D78FF67CA', '4F6278CC-5E67-4078-A0E2-DD28AF6BF2C3');
INSERT INTO GameAuthor (GameId, AuthorId) VALUES ('4674E54B-63AA-4D63-A44C-FC1A63C7C631', '42101E58-774F-4EB0-BCCA-DF539CF9F686');
INSERT INTO GameAuthor (GameId, AuthorId) VALUES ('DCCE13C2-6EBE-4F55-9375-66A61A2A567A', 'C6354A2C-5D5C-4F3F-BEC8-E3370E61E71F');
INSERT INTO GameAuthor (GameId, AuthorId) VALUES ('945D2891-B7F4-4A92-BBE0-07223F0C397D', 'CFFBF689-A93E-4A6F-91BE-E593EEE3564B');
INSERT INTO GameAuthor (GameId, AuthorId) VALUES ('388131C6-4120-4DF7-85E1-47EE2B64EE89', 'CFFBF689-A93E-4A6F-91BE-E593EEE3564B');
INSERT INTO GameAuthor (GameId, AuthorId) VALUES ('0E043E67-7B3C-4647-9001-487F20D5A669', '0C8AF378-704C-4754-97F3-E9FCEF2BE04B');
INSERT INTO GameAuthor (GameId, AuthorId) VALUES ('CDA55509-4E8D-4F9B-AFB1-4D6CFAFA08C1', '9B5794BA-59D8-4EE6-A5E9-EDF7383BAB02');
INSERT INTO GameAuthor (GameId, AuthorId) VALUES ('D9F32603-ED77-49C2-A327-0026748224F5', 'B6F1D0C0-9DDD-4AAD-91F0-F632C36258F6');
INSERT INTO GameAuthor (GameId, AuthorId) VALUES ('8D46FD79-B15B-437C-AA9B-4C654DD35026', 'B7807DC9-1D43-481F-98A3-F8EB893DF634');
INSERT INTO GameAuthor (GameId, AuthorId) VALUES ('65CCAD52-ACD9-4BDD-9208-4B9743E250ED', '3E9B9D5C-90B7-4CCE-9F30-FA692F9B0034');
INSERT INTO GameCategory (GameId, CategoryId) VALUES ('69E4AD75-32A0-4C57-919D-533E7BB3AF21', '68C2FDF3-9ECC-4991-9E41-069EDFA11CA8');
INSERT INTO GameCategory (GameId, CategoryId) VALUES ('4721FAB4-B8E9-432C-A6A6-02741EE71E45', '8318BE2E-CDB5-4561-92BB-06A48C433EE4');
INSERT INTO GameCategory (GameId, CategoryId) VALUES ('12230528-C9FB-4511-B23C-3DDBBE793B1A', '8318BE2E-CDB5-4561-92BB-06A48C433EE4');
INSERT INTO GameCategory (GameId, CategoryId) VALUES ('025D5EC1-A371-4832-BE45-40DD21E50D30', '8318BE2E-CDB5-4561-92BB-06A48C433EE4');
INSERT INTO GameCategory (GameId, CategoryId) VALUES ('285F5601-1535-468B-A443-4589ABC5F800', '8318BE2E-CDB5-4561-92BB-06A48C433EE4');
INSERT INTO GameCategory (GameId, CategoryId) VALUES ('8D46FD79-B15B-437C-AA9B-4C654DD35026', '8318BE2E-CDB5-4561-92BB-06A48C433EE4');
INSERT INTO GameCategory (GameId, CategoryId) VALUES ('A074F3F3-96D0-4031-8008-4FE420BB4713', '8318BE2E-CDB5-4561-92BB-06A48C433EE4');
INSERT INTO GameCategory (GameId, CategoryId) VALUES ('69E4AD75-32A0-4C57-919D-533E7BB3AF21', '8318BE2E-CDB5-4561-92BB-06A48C433EE4');
INSERT INTO GameCategory (GameId, CategoryId) VALUES ('0E043E67-7B3C-4647-9001-487F20D5A669', '852264C9-A989-41FC-A59C-09AB003DFE16');
INSERT INTO GameCategory (GameId, CategoryId) VALUES ('43A2BBD5-1D5D-4ABC-8C92-4CADA3A5729E', '852264C9-A989-41FC-A59C-09AB003DFE16');
INSERT INTO GameCategory (GameId, CategoryId) VALUES ('83F314E6-760D-46DF-9559-03F7E92FBD4A', '3AFEA2B4-BCC0-443D-8D8E-0A3E67CF9E7D');
INSERT INTO GameCategory (GameId, CategoryId) VALUES ('6B007B51-FDB7-4C6C-9634-0653E55A40FB', '3AFEA2B4-BCC0-443D-8D8E-0A3E67CF9E7D');
INSERT INTO GameCategory (GameId, CategoryId) VALUES ('ADD19568-F7DD-4D39-9074-3DE36255CB36', '3AFEA2B4-BCC0-443D-8D8E-0A3E67CF9E7D');
INSERT INTO GameCategory (GameId, CategoryId) VALUES ('CEBF983E-0A43-437B-9A05-41A069F0D9DA', '3AFEA2B4-BCC0-443D-8D8E-0A3E67CF9E7D');
INSERT INTO GameCategory (GameId, CategoryId) VALUES ('9F73EF15-F210-4C4C-84C1-437CB5A93883', '3AFEA2B4-BCC0-443D-8D8E-0A3E67CF9E7D');
INSERT INTO GameCategory (GameId, CategoryId) VALUES ('F6672718-39D9-46C5-9211-45544B135723', '3AFEA2B4-BCC0-443D-8D8E-0A3E67CF9E7D');
INSERT INTO GameCategory (GameId, CategoryId) VALUES ('07AF3FFB-1866-45E2-AD91-47DC71F4A1C1', '3AFEA2B4-BCC0-443D-8D8E-0A3E67CF9E7D');
INSERT INTO GameCategory (GameId, CategoryId) VALUES ('388131C6-4120-4DF7-85E1-47EE2B64EE89', '3AFEA2B4-BCC0-443D-8D8E-0A3E67CF9E7D');
INSERT INTO GameCategory (GameId, CategoryId) VALUES ('0E043E67-7B3C-4647-9001-487F20D5A669', '3AFEA2B4-BCC0-443D-8D8E-0A3E67CF9E7D');
INSERT INTO GameCategory (GameId, CategoryId) VALUES ('155E4F6C-368C-4FA2-B388-4D4FCE8A359C', '3AFEA2B4-BCC0-443D-8D8E-0A3E67CF9E7D');
INSERT INTO GameCategory (GameId, CategoryId) VALUES ('CDA55509-4E8D-4F9B-AFB1-4D6CFAFA08C1', '3AFEA2B4-BCC0-443D-8D8E-0A3E67CF9E7D');
INSERT INTO GameCategory (GameId, CategoryId) VALUES ('FCD61FF3-F6A7-4A55-B8B4-4EE879A3AEC4', '3AFEA2B4-BCC0-443D-8D8E-0A3E67CF9E7D');
INSERT INTO GameCategory (GameId, CategoryId) VALUES ('DCCE13C2-6EBE-4F55-9375-66A61A2A567A', '3AFEA2B4-BCC0-443D-8D8E-0A3E67CF9E7D');
INSERT INTO GameCategory (GameId, CategoryId) VALUES ('EECC55EA-26F6-45AA-8F6A-99025044BF91', '3AFEA2B4-BCC0-443D-8D8E-0A3E67CF9E7D');
INSERT INTO GameCategory (GameId, CategoryId) VALUES ('2DDD0247-0973-4771-9254-B3F1D75C8A84', '3AFEA2B4-BCC0-443D-8D8E-0A3E67CF9E7D');
INSERT INTO GameCategory (GameId, CategoryId) VALUES ('98634FC4-D569-4459-8503-FA3BDD377A42', '3AFEA2B4-BCC0-443D-8D8E-0A3E67CF9E7D');
INSERT INTO GameCategory (GameId, CategoryId) VALUES ('4674E54B-63AA-4D63-A44C-FC1A63C7C631', '3AFEA2B4-BCC0-443D-8D8E-0A3E67CF9E7D');
INSERT INTO GameCategory (GameId, CategoryId) VALUES ('69E4AD75-32A0-4C57-919D-533E7BB3AF21', '9B2D55D8-0B2C-482C-A2D0-13D93EAD0957');
INSERT INTO GameCategory (GameId, CategoryId) VALUES ('D9F32603-ED77-49C2-A327-0026748224F5', '831F8AB6-1EA7-4FEC-9401-2BA77D340807');
INSERT INTO GameCategory (GameId, CategoryId) VALUES ('DBC6FBB7-FEF0-422C-8DCA-049AA98AF2A9', '831F8AB6-1EA7-4FEC-9401-2BA77D340807');
INSERT INTO GameCategory (GameId, CategoryId) VALUES ('DBDF1C0B-F813-45DA-B66C-00632E2B113F', '51A4B116-ADCD-442A-A0F7-2C62166CB48B');
INSERT INTO GameCategory (GameId, CategoryId) VALUES ('4721FAB4-B8E9-432C-A6A6-02741EE71E45', '51A4B116-ADCD-442A-A0F7-2C62166CB48B');
INSERT INTO GameCategory (GameId, CategoryId) VALUES ('83F314E6-760D-46DF-9559-03F7E92FBD4A', '51A4B116-ADCD-442A-A0F7-2C62166CB48B');
INSERT INTO GameCategory (GameId, CategoryId) VALUES ('6B007B51-FDB7-4C6C-9634-0653E55A40FB', '51A4B116-ADCD-442A-A0F7-2C62166CB48B');
INSERT INTO GameCategory (GameId, CategoryId) VALUES ('ADD19568-F7DD-4D39-9074-3DE36255CB36', '51A4B116-ADCD-442A-A0F7-2C62166CB48B');
INSERT INTO GameCategory (GameId, CategoryId) VALUES ('ADB16C86-8F08-44E7-B25B-42CFF53DAEE3', '51A4B116-ADCD-442A-A0F7-2C62166CB48B');
INSERT INTO GameCategory (GameId, CategoryId) VALUES ('43A2BBD5-1D5D-4ABC-8C92-4CADA3A5729E', '51A4B116-ADCD-442A-A0F7-2C62166CB48B');
INSERT INTO GameCategory (GameId, CategoryId) VALUES ('FCD61FF3-F6A7-4A55-B8B4-4EE879A3AEC4', '51A4B116-ADCD-442A-A0F7-2C62166CB48B');
INSERT INTO GameCategory (GameId, CategoryId) VALUES ('A074F3F3-96D0-4031-8008-4FE420BB4713', '51A4B116-ADCD-442A-A0F7-2C62166CB48B');
INSERT INTO GameCategory (GameId, CategoryId) VALUES ('DCCE13C2-6EBE-4F55-9375-66A61A2A567A', '51A4B116-ADCD-442A-A0F7-2C62166CB48B');
INSERT INTO GameCategory (GameId, CategoryId) VALUES ('8F089DB6-1B79-4E1D-B022-96985CD9F18A', '51A4B116-ADCD-442A-A0F7-2C62166CB48B');
INSERT INTO GameCategory (GameId, CategoryId) VALUES ('EECC55EA-26F6-45AA-8F6A-99025044BF91', '51A4B116-ADCD-442A-A0F7-2C62166CB48B');
INSERT INTO GameCategory (GameId, CategoryId) VALUES ('4721FAB4-B8E9-432C-A6A6-02741EE71E45', '21B190D2-41DF-4BF4-A142-332DC0052854');
INSERT INTO GameCategory (GameId, CategoryId) VALUES ('83F314E6-760D-46DF-9559-03F7E92FBD4A', 'FC8BC31F-40F1-4D9A-A86B-34786361656C');
INSERT INTO GameCategory (GameId, CategoryId) VALUES ('DBC6FBB7-FEF0-422C-8DCA-049AA98AF2A9', 'FC8BC31F-40F1-4D9A-A86B-34786361656C');
INSERT INTO GameCategory (GameId, CategoryId) VALUES ('4674E54B-63AA-4D63-A44C-FC1A63C7C631', 'FFB0DB97-A147-42CF-A3F0-37E11E737B9C');
INSERT INTO GameCategory (GameId, CategoryId) VALUES ('A074F3F3-96D0-4031-8008-4FE420BB4713', '3138700C-64F6-4305-B7EF-3D57A0648A05');
INSERT INTO GameCategory (GameId, CategoryId) VALUES ('69E4AD75-32A0-4C57-919D-533E7BB3AF21', '3138700C-64F6-4305-B7EF-3D57A0648A05');
INSERT INTO GameCategory (GameId, CategoryId) VALUES ('D9F32603-ED77-49C2-A327-0026748224F5', '5A91EC0C-AC9B-4723-8B8D-3D93DABBB2D9');
INSERT INTO GameCategory (GameId, CategoryId) VALUES ('D9551C9B-951D-4EE3-A2AD-05515CDCB676', '90AB3A59-853D-49EF-90E5-3D9C0BB119B8');
INSERT INTO GameCategory (GameId, CategoryId) VALUES ('388131C6-4120-4DF7-85E1-47EE2B64EE89', '90AB3A59-853D-49EF-90E5-3D9C0BB119B8');
INSERT INTO GameCategory (GameId, CategoryId) VALUES ('0E043E67-7B3C-4647-9001-487F20D5A669', '90AB3A59-853D-49EF-90E5-3D9C0BB119B8');
INSERT INTO GameCategory (GameId, CategoryId) VALUES ('025D5EC1-A371-4832-BE45-40DD21E50D30', '3FC2F310-FC3B-4600-9C3D-44418D01164A');
INSERT INTO GameCategory (GameId, CategoryId) VALUES ('883D0F36-BA48-475B-A64D-430C6DCEDDA0', '3FC2F310-FC3B-4600-9C3D-44418D01164A');
INSERT INTO GameCategory (GameId, CategoryId) VALUES ('65CCAD52-ACD9-4BDD-9208-4B9743E250ED', '3FC2F310-FC3B-4600-9C3D-44418D01164A');
INSERT INTO GameCategory (GameId, CategoryId) VALUES ('DBDF1C0B-F813-45DA-B66C-00632E2B113F', '6435A986-7D40-4407-8C56-48DFACF2BE57');
INSERT INTO GameCategory (GameId, CategoryId) VALUES ('83F314E6-760D-46DF-9559-03F7E92FBD4A', '6435A986-7D40-4407-8C56-48DFACF2BE57');
INSERT INTO GameCategory (GameId, CategoryId) VALUES ('CEBF983E-0A43-437B-9A05-41A069F0D9DA', '6435A986-7D40-4407-8C56-48DFACF2BE57');
INSERT INTO GameCategory (GameId, CategoryId) VALUES ('65CCAD52-ACD9-4BDD-9208-4B9743E250ED', '6435A986-7D40-4407-8C56-48DFACF2BE57');
INSERT INTO GameCategory (GameId, CategoryId) VALUES ('FCD61FF3-F6A7-4A55-B8B4-4EE879A3AEC4', '6435A986-7D40-4407-8C56-48DFACF2BE57');
INSERT INTO GameCategory (GameId, CategoryId) VALUES ('8F089DB6-1B79-4E1D-B022-96985CD9F18A', '6435A986-7D40-4407-8C56-48DFACF2BE57');
INSERT INTO GameCategory (GameId, CategoryId) VALUES ('EECC55EA-26F6-45AA-8F6A-99025044BF91', '6435A986-7D40-4407-8C56-48DFACF2BE57');
INSERT INTO GameCategory (GameId, CategoryId) VALUES ('98634FC4-D569-4459-8503-FA3BDD377A42', '6435A986-7D40-4407-8C56-48DFACF2BE57');
INSERT INTO GameCategory (GameId, CategoryId) VALUES ('6B007B51-FDB7-4C6C-9634-0653E55A40FB', 'C831D482-FD1C-4E80-B8A2-4A3D347822A3');
INSERT INTO GameCategory (GameId, CategoryId) VALUES ('F6672718-39D9-46C5-9211-45544B135723', 'C831D482-FD1C-4E80-B8A2-4A3D347822A3');
INSERT INTO GameCategory (GameId, CategoryId) VALUES ('35D65DAD-36E4-42DF-9EA5-4208771D4FE4', 'E81A0F07-E2CF-4010-A623-4C816E2A65EE');
INSERT INTO GameCategory (GameId, CategoryId) VALUES ('07C6D4B4-76E3-422E-8F0E-494D78FF67CA', '44C73A39-4DCF-482E-BAF5-4E53C0B1425C');
INSERT INTO GameCategory (GameId, CategoryId) VALUES ('43A2BBD5-1D5D-4ABC-8C92-4CADA3A5729E', '3D180F26-DC72-4CB4-B477-54AAC47C4642');
INSERT INTO GameCategory (GameId, CategoryId) VALUES ('D9551C9B-951D-4EE3-A2AD-05515CDCB676', '0B7A2178-C10F-42E4-A85E-5BE633A1A38A');
INSERT INTO GameCategory (GameId, CategoryId) VALUES ('388131C6-4120-4DF7-85E1-47EE2B64EE89', '0B7A2178-C10F-42E4-A85E-5BE633A1A38A');
INSERT INTO GameCategory (GameId, CategoryId) VALUES ('8F089DB6-1B79-4E1D-B022-96985CD9F18A', '00470BAC-69A8-4474-8974-5D3030B3619A');
INSERT INTO GameCategory (GameId, CategoryId) VALUES ('CEBF983E-0A43-437B-9A05-41A069F0D9DA', '0262E208-C2A6-44CB-85EB-675C4C2C5688');
INSERT INTO GameCategory (GameId, CategoryId) VALUES ('883D0F36-BA48-475B-A64D-430C6DCEDDA0', '0262E208-C2A6-44CB-85EB-675C4C2C5688');
INSERT INTO GameCategory (GameId, CategoryId) VALUES ('9F73EF15-F210-4C4C-84C1-437CB5A93883', '0262E208-C2A6-44CB-85EB-675C4C2C5688');
INSERT INTO GameCategory (GameId, CategoryId) VALUES ('F6672718-39D9-46C5-9211-45544B135723', '0262E208-C2A6-44CB-85EB-675C4C2C5688');
INSERT INTO GameCategory (GameId, CategoryId) VALUES ('98634FC4-D569-4459-8503-FA3BDD377A42', '0262E208-C2A6-44CB-85EB-675C4C2C5688');
INSERT INTO GameCategory (GameId, CategoryId) VALUES ('35D65DAD-36E4-42DF-9EA5-4208771D4FE4', '524187E8-85E4-452C-9FC2-7666F9C1B233');
INSERT INTO GameCategory (GameId, CategoryId) VALUES ('883D0F36-BA48-475B-A64D-430C6DCEDDA0', '524187E8-85E4-452C-9FC2-7666F9C1B233');
INSERT INTO GameCategory (GameId, CategoryId) VALUES ('65CCAD52-ACD9-4BDD-9208-4B9743E250ED', '524187E8-85E4-452C-9FC2-7666F9C1B233');
INSERT INTO GameCategory (GameId, CategoryId) VALUES ('CDA55509-4E8D-4F9B-AFB1-4D6CFAFA08C1', '524187E8-85E4-452C-9FC2-7666F9C1B233');
INSERT INTO GameCategory (GameId, CategoryId) VALUES ('945D2891-B7F4-4A92-BBE0-07223F0C397D', 'DF321D72-46A4-4AB3-93E4-79A624C7759F');
INSERT INTO GameCategory (GameId, CategoryId) VALUES ('388131C6-4120-4DF7-85E1-47EE2B64EE89', 'DF321D72-46A4-4AB3-93E4-79A624C7759F');
INSERT INTO GameCategory (GameId, CategoryId) VALUES ('2DDD0247-0973-4771-9254-B3F1D75C8A84', 'DF321D72-46A4-4AB3-93E4-79A624C7759F');
INSERT INTO GameCategory (GameId, CategoryId) VALUES ('8D46FD79-B15B-437C-AA9B-4C654DD35026', 'B66B7A85-346F-47DD-ADFD-79AF41EAE274');
INSERT INTO GameCategory (GameId, CategoryId) VALUES ('69E4AD75-32A0-4C57-919D-533E7BB3AF21', 'B66B7A85-346F-47DD-ADFD-79AF41EAE274');
INSERT INTO GameCategory (GameId, CategoryId) VALUES ('388131C6-4120-4DF7-85E1-47EE2B64EE89', '521FF3FF-E692-451A-B72E-897456CCFE31');
INSERT INTO GameCategory (GameId, CategoryId) VALUES ('BF6AFD5E-3EDB-4886-9374-4A94A569266B', '4D7DBA0C-1549-402C-BD3F-8AF8842EC659');
INSERT INTO GameCategory (GameId, CategoryId) VALUES ('DD6FD171-28B8-48B5-A92D-04C7DE4F2D9A', '27AE421C-A74A-40EF-BE6A-8B8AE11A6586');
INSERT INTO GameCategory (GameId, CategoryId) VALUES ('025D5EC1-A371-4832-BE45-40DD21E50D30', '27AE421C-A74A-40EF-BE6A-8B8AE11A6586');
INSERT INTO GameCategory (GameId, CategoryId) VALUES ('FCD61FF3-F6A7-4A55-B8B4-4EE879A3AEC4', '27AE421C-A74A-40EF-BE6A-8B8AE11A6586');
INSERT INTO GameCategory (GameId, CategoryId) VALUES ('EECC55EA-26F6-45AA-8F6A-99025044BF91', '27AE421C-A74A-40EF-BE6A-8B8AE11A6586');
INSERT INTO GameCategory (GameId, CategoryId) VALUES ('F8E3A417-6C7C-4A98-9203-0EE5F4596A35', 'DE57D2F0-7785-4744-94A0-8E427FA33458');
INSERT INTO GameCategory (GameId, CategoryId) VALUES ('CEBF983E-0A43-437B-9A05-41A069F0D9DA', 'CB14BA0D-BCDB-4E91-B0A6-90B5DCA211A9');
INSERT INTO GameCategory (GameId, CategoryId) VALUES ('9F73EF15-F210-4C4C-84C1-437CB5A93883', 'CB14BA0D-BCDB-4E91-B0A6-90B5DCA211A9');
INSERT INTO GameCategory (GameId, CategoryId) VALUES ('98634FC4-D569-4459-8503-FA3BDD377A42', 'CB14BA0D-BCDB-4E91-B0A6-90B5DCA211A9');
INSERT INTO GameCategory (GameId, CategoryId) VALUES ('43A2BBD5-1D5D-4ABC-8C92-4CADA3A5729E', '09CF3354-2D15-4EF3-900B-9788B4AC2E99');
INSERT INTO GameCategory (GameId, CategoryId) VALUES ('BCEBA3EE-5CAC-4000-8B9C-086E80F5DC27', 'CE2739D4-CAF8-4C56-B0DF-A33879641F1E');
INSERT INTO GameCategory (GameId, CategoryId) VALUES ('DD6FD171-28B8-48B5-A92D-04C7DE4F2D9A', 'F5C8EC29-DFB3-4125-A6BB-A8378616C60E');
INSERT INTO GameCategory (GameId, CategoryId) VALUES ('BCEBA3EE-5CAC-4000-8B9C-086E80F5DC27', 'B3B69F88-6B0B-4095-A6AC-A926354998AE');
INSERT INTO GameCategory (GameId, CategoryId) VALUES ('CEBF983E-0A43-437B-9A05-41A069F0D9DA', 'B3B69F88-6B0B-4095-A6AC-A926354998AE');
INSERT INTO GameCategory (GameId, CategoryId) VALUES ('9F73EF15-F210-4C4C-84C1-437CB5A93883', 'B3B69F88-6B0B-4095-A6AC-A926354998AE');
INSERT INTO GameCategory (GameId, CategoryId) VALUES ('98634FC4-D569-4459-8503-FA3BDD377A42', 'B3B69F88-6B0B-4095-A6AC-A926354998AE');
INSERT INTO GameCategory (GameId, CategoryId) VALUES ('D9551C9B-951D-4EE3-A2AD-05515CDCB676', '744A8FC4-9A96-44F1-A27B-AC5275B51C22');
INSERT INTO GameCategory (GameId, CategoryId) VALUES ('88555477-A992-4289-BD2C-4F061CC36B47', 'B16790EB-2BB9-456C-BF7A-B66A0B20B060');
INSERT INTO GameCategory (GameId, CategoryId) VALUES ('E41AAA87-6337-4D68-9B96-5037E75B503C', '29763546-335E-4F49-B353-BB49402FF171');
INSERT INTO GameCategory (GameId, CategoryId) VALUES ('83F314E6-760D-46DF-9559-03F7E92FBD4A', '1E96AC37-A1AF-49B9-9723-C06921D882A7');
INSERT INTO GameCategory (GameId, CategoryId) VALUES ('DBC6FBB7-FEF0-422C-8DCA-049AA98AF2A9', '1E96AC37-A1AF-49B9-9723-C06921D882A7');
INSERT INTO GameCategory (GameId, CategoryId) VALUES ('ADB16C86-8F08-44E7-B25B-42CFF53DAEE3', '547802C2-56E5-4753-BCC2-C4E626115C85');
INSERT INTO GameCategory (GameId, CategoryId) VALUES ('BCEBA3EE-5CAC-4000-8B9C-086E80F5DC27', '1E77B631-88B4-42FD-B829-CF6944DA8035');
INSERT INTO GameCategory (GameId, CategoryId) VALUES ('F8E3A417-6C7C-4A98-9203-0EE5F4596A35', '1E77B631-88B4-42FD-B829-CF6944DA8035');
INSERT INTO GameCategory (GameId, CategoryId) VALUES ('88555477-A992-4289-BD2C-4F061CC36B47', '1E77B631-88B4-42FD-B829-CF6944DA8035');
INSERT INTO GameCategory (GameId, CategoryId) VALUES ('ADD19568-F7DD-4D39-9074-3DE36255CB36', '880FF14B-D679-4AB3-97DD-D1E5C2B5E2A6');
INSERT INTO GameCategory (GameId, CategoryId) VALUES ('07AF3FFB-1866-45E2-AD91-47DC71F4A1C1', 'AA4F9245-C30E-4378-BB42-D44B1EAC0F81');
INSERT INTO GameCategory (GameId, CategoryId) VALUES ('DD6FD171-28B8-48B5-A92D-04C7DE4F2D9A', '19D81AE3-B3B1-4452-938E-D504975A23A7');
INSERT INTO GameCategory (GameId, CategoryId) VALUES ('DD6FD171-28B8-48B5-A92D-04C7DE4F2D9A', 'F9E27215-A485-4208-BB99-D7300EE25625');
INSERT INTO GameCategory (GameId, CategoryId) VALUES ('79F5544A-A0A1-46F8-AAF2-0EC2EE3FD146', '36D0D4D0-1EFB-4A6F-9690-D8DDA743BCCE');
INSERT INTO GameCategory (GameId, CategoryId) VALUES ('1409E0D0-5CC2-40F4-B272-46852CBF0423', '36D0D4D0-1EFB-4A6F-9690-D8DDA743BCCE');
INSERT INTO GameCategory (GameId, CategoryId) VALUES ('FCD61FF3-F6A7-4A55-B8B4-4EE879A3AEC4', '69815AF8-21B4-4CBD-81EA-DA860B5951F9');
INSERT INTO GameCategory (GameId, CategoryId) VALUES ('EECC55EA-26F6-45AA-8F6A-99025044BF91', '69815AF8-21B4-4CBD-81EA-DA860B5951F9');
INSERT INTO GameCategory (GameId, CategoryId) VALUES ('2DDD0247-0973-4771-9254-B3F1D75C8A84', '69815AF8-21B4-4CBD-81EA-DA860B5951F9');
INSERT INTO GameCategory (GameId, CategoryId) VALUES ('DD6FD171-28B8-48B5-A92D-04C7DE4F2D9A', '35A5EAC9-DBEB-4305-A55F-EE0490CA3078');
INSERT INTO GameCategory (GameId, CategoryId) VALUES ('025D5EC1-A371-4832-BE45-40DD21E50D30', '35A5EAC9-DBEB-4305-A55F-EE0490CA3078');
INSERT INTO GameCategory (GameId, CategoryId) VALUES ('0E043E67-7B3C-4647-9001-487F20D5A669', '35A5EAC9-DBEB-4305-A55F-EE0490CA3078');
INSERT INTO GameIllustrator (GameId, IllustratorId) VALUES ('DD6FD171-28B8-48B5-A92D-04C7DE4F2D9A', 'FEAD68C6-6C27-4E1A-BE43-0737702129A1');
INSERT INTO GameIllustrator (GameId, IllustratorId) VALUES ('65CCAD52-ACD9-4BDD-9208-4B9743E250ED', '852786BE-28A7-482E-AC38-07AA77069B3C');
INSERT INTO GameIllustrator (GameId, IllustratorId) VALUES ('DBC6FBB7-FEF0-422C-8DCA-049AA98AF2A9', '513B1C83-EA29-42A6-8E35-0D54D5EBBA83');
INSERT INTO GameIllustrator (GameId, IllustratorId) VALUES ('DD6FD171-28B8-48B5-A92D-04C7DE4F2D9A', '9C8215B6-6EEB-47E6-81A7-0DE14657A41F');
INSERT INTO GameIllustrator (GameId, IllustratorId) VALUES ('FCD61FF3-F6A7-4A55-B8B4-4EE879A3AEC4', '44773371-4435-428A-B03C-0F7A9BFC12BD');
INSERT INTO GameIllustrator (GameId, IllustratorId) VALUES ('DCCE13C2-6EBE-4F55-9375-66A61A2A567A', '44773371-4435-428A-B03C-0F7A9BFC12BD');
INSERT INTO GameIllustrator (GameId, IllustratorId) VALUES ('EECC55EA-26F6-45AA-8F6A-99025044BF91', '44773371-4435-428A-B03C-0F7A9BFC12BD');
INSERT INTO GameIllustrator (GameId, IllustratorId) VALUES ('EECC55EA-26F6-45AA-8F6A-99025044BF91', '588C3DF8-79DF-45FF-BDDD-19B41CF65804');
INSERT INTO GameIllustrator (GameId, IllustratorId) VALUES ('883D0F36-BA48-475B-A64D-430C6DCEDDA0', '83DDCBA9-0E92-498F-A799-1FC384DB8007');
INSERT INTO GameIllustrator (GameId, IllustratorId) VALUES ('DD6FD171-28B8-48B5-A92D-04C7DE4F2D9A', '8EF0BECB-89D9-432B-8889-21BD2BFC7C12');
INSERT INTO GameIllustrator (GameId, IllustratorId) VALUES ('DD6FD171-28B8-48B5-A92D-04C7DE4F2D9A', '5401A7B9-8AD1-4B91-82D0-236372789E41');
INSERT INTO GameIllustrator (GameId, IllustratorId) VALUES ('EECC55EA-26F6-45AA-8F6A-99025044BF91', '15AD7037-B76A-4415-BA06-2470415CE436');
INSERT INTO GameIllustrator (GameId, IllustratorId) VALUES ('4674E54B-63AA-4D63-A44C-FC1A63C7C631', 'AB7F0B42-4CE1-4591-A3DF-24FACAE31683');
INSERT INTO GameIllustrator (GameId, IllustratorId) VALUES ('DD6FD171-28B8-48B5-A92D-04C7DE4F2D9A', '73CD95B8-1E7E-463E-84F5-2585D84B055F');
INSERT INTO GameIllustrator (GameId, IllustratorId) VALUES ('CDA55509-4E8D-4F9B-AFB1-4D6CFAFA08C1', '5C853A1D-28F0-4F35-A266-2597F2E5D059');
INSERT INTO GameIllustrator (GameId, IllustratorId) VALUES ('DD6FD171-28B8-48B5-A92D-04C7DE4F2D9A', '0EA06D1B-31ED-4808-A649-278F939BA733');
INSERT INTO GameIllustrator (GameId, IllustratorId) VALUES ('883D0F36-BA48-475B-A64D-430C6DCEDDA0', '966698AA-1065-4DA9-AA8A-27A6227FD200');
INSERT INTO GameIllustrator (GameId, IllustratorId) VALUES ('883D0F36-BA48-475B-A64D-430C6DCEDDA0', '931E4CF4-E289-49E9-A844-2D7AE348B20D');
INSERT INTO GameIllustrator (GameId, IllustratorId) VALUES ('BF6AFD5E-3EDB-4886-9374-4A94A569266B', '931E4CF4-E289-49E9-A844-2D7AE348B20D');
INSERT INTO GameIllustrator (GameId, IllustratorId) VALUES ('285F5601-1535-468B-A443-4589ABC5F800', '32F7DCF5-7D5B-4CD9-A298-30C33D89E943');
INSERT INTO GameIllustrator (GameId, IllustratorId) VALUES ('DD6FD171-28B8-48B5-A92D-04C7DE4F2D9A', '6395BB83-383F-4C48-86AD-331AF9C1B134');
INSERT INTO GameIllustrator (GameId, IllustratorId) VALUES ('DD6FD171-28B8-48B5-A92D-04C7DE4F2D9A', 'B1BF9C58-C968-4270-A8EC-33347921900F');
INSERT INTO GameIllustrator (GameId, IllustratorId) VALUES ('DD6FD171-28B8-48B5-A92D-04C7DE4F2D9A', '802E792B-2A33-4938-B1A4-355C2D9D282B');
INSERT INTO GameIllustrator (GameId, IllustratorId) VALUES ('DD6FD171-28B8-48B5-A92D-04C7DE4F2D9A', 'B9E369F0-82C7-4AD1-986F-3A718609FF98');
INSERT INTO GameIllustrator (GameId, IllustratorId) VALUES ('DBC6FBB7-FEF0-422C-8DCA-049AA98AF2A9', 'A3689D90-53A3-4247-83BE-3DBF93790608');
INSERT INTO GameIllustrator (GameId, IllustratorId) VALUES ('CEBF983E-0A43-437B-9A05-41A069F0D9DA', 'A3689D90-53A3-4247-83BE-3DBF93790608');
INSERT INTO GameIllustrator (GameId, IllustratorId) VALUES ('9F73EF15-F210-4C4C-84C1-437CB5A93883', 'A3689D90-53A3-4247-83BE-3DBF93790608');
INSERT INTO GameIllustrator (GameId, IllustratorId) VALUES ('07C6D4B4-76E3-422E-8F0E-494D78FF67CA', 'A3689D90-53A3-4247-83BE-3DBF93790608');
INSERT INTO GameIllustrator (GameId, IllustratorId) VALUES ('98634FC4-D569-4459-8503-FA3BDD377A42', 'A3689D90-53A3-4247-83BE-3DBF93790608');
INSERT INTO GameIllustrator (GameId, IllustratorId) VALUES ('BF6AFD5E-3EDB-4886-9374-4A94A569266B', '615979B5-99E4-4544-BD14-3E0FD56C38DB');
INSERT INTO GameIllustrator (GameId, IllustratorId) VALUES ('F6672718-39D9-46C5-9211-45544B135723', 'E657A5EC-5249-4343-B1D5-43F7F9D5A6AD');
INSERT INTO GameIllustrator (GameId, IllustratorId) VALUES ('BF6AFD5E-3EDB-4886-9374-4A94A569266B', '53ABEF69-0D0B-429F-A54F-4534F49FBC1E');
INSERT INTO GameIllustrator (GameId, IllustratorId) VALUES ('DD6FD171-28B8-48B5-A92D-04C7DE4F2D9A', 'B190FD40-411C-409D-A4A3-488C68BC2FAE');
INSERT INTO GameIllustrator (GameId, IllustratorId) VALUES ('DD6FD171-28B8-48B5-A92D-04C7DE4F2D9A', '1E350817-C23A-4678-9684-4928DDB025B4');
INSERT INTO GameIllustrator (GameId, IllustratorId) VALUES ('DD6FD171-28B8-48B5-A92D-04C7DE4F2D9A', '8511D57D-2565-4C85-9714-49DD32725054');
INSERT INTO GameIllustrator (GameId, IllustratorId) VALUES ('DD6FD171-28B8-48B5-A92D-04C7DE4F2D9A', 'EF54EEDD-CD2C-4667-A477-4B0A74905A1D');
INSERT INTO GameIllustrator (GameId, IllustratorId) VALUES ('4721FAB4-B8E9-432C-A6A6-02741EE71E45', 'EFE87A30-8CC5-4B0D-87D4-4CE6592AE0A3');
INSERT INTO GameIllustrator (GameId, IllustratorId) VALUES ('883D0F36-BA48-475B-A64D-430C6DCEDDA0', '2402ECFB-DFA0-494B-87CE-4D26754E4693');
INSERT INTO GameIllustrator (GameId, IllustratorId) VALUES ('79F5544A-A0A1-46F8-AAF2-0EC2EE3FD146', '291817BF-2667-423D-BECE-4D9424C2810F');
INSERT INTO GameIllustrator (GameId, IllustratorId) VALUES ('1409E0D0-5CC2-40F4-B272-46852CBF0423', '291817BF-2667-423D-BECE-4D9424C2810F');
INSERT INTO GameIllustrator (GameId, IllustratorId) VALUES ('DD6FD171-28B8-48B5-A92D-04C7DE4F2D9A', '0FDC9867-8B22-414F-B1D9-4E644198E900');
INSERT INTO GameIllustrator (GameId, IllustratorId) VALUES ('07AF3FFB-1866-45E2-AD91-47DC71F4A1C1', 'A1465426-3076-47E5-9437-4EFCECB2DD84');
INSERT INTO GameIllustrator (GameId, IllustratorId) VALUES ('ADD19568-F7DD-4D39-9074-3DE36255CB36', 'F64AFD7E-2A13-4339-901E-501DA0516022');
INSERT INTO GameIllustrator (GameId, IllustratorId) VALUES ('DD6FD171-28B8-48B5-A92D-04C7DE4F2D9A', '52DFD4CF-ABDD-4AFC-90D2-504FDD806EF0');
INSERT INTO GameIllustrator (GameId, IllustratorId) VALUES ('DD6FD171-28B8-48B5-A92D-04C7DE4F2D9A', '61D6189C-D2CA-4105-8B2C-56E8125359BE');
INSERT INTO GameIllustrator (GameId, IllustratorId) VALUES ('ADD19568-F7DD-4D39-9074-3DE36255CB36', 'AC94A8EA-E5DF-4938-B8A1-592B977A37E7');
INSERT INTO GameIllustrator (GameId, IllustratorId) VALUES ('155E4F6C-368C-4FA2-B388-4D4FCE8A359C', '94A7581F-E3E4-4854-A30B-60349494D843');
INSERT INTO GameIllustrator (GameId, IllustratorId) VALUES ('07AF3FFB-1866-45E2-AD91-47DC71F4A1C1', '2BCBDAAC-2ECA-4EE8-887E-6336A016CD62');
INSERT INTO GameIllustrator (GameId, IllustratorId) VALUES ('8D46FD79-B15B-437C-AA9B-4C654DD35026', '29B1AF47-B9E6-4959-81C0-63A7A3FBC6EE');
INSERT INTO GameIllustrator (GameId, IllustratorId) VALUES ('DD6FD171-28B8-48B5-A92D-04C7DE4F2D9A', '2DB1F124-F679-410F-882D-65FCE8B3ECD5');
INSERT INTO GameIllustrator (GameId, IllustratorId) VALUES ('2DDD0247-0973-4771-9254-B3F1D75C8A84', '8E694F09-81D4-4916-88C0-68005DD42EBD');
INSERT INTO GameIllustrator (GameId, IllustratorId) VALUES ('F8E3A417-6C7C-4A98-9203-0EE5F4596A35', '622D0CFC-4EFA-42C2-99A7-6A130FF0BE0D');
INSERT INTO GameIllustrator (GameId, IllustratorId) VALUES ('DD6FD171-28B8-48B5-A92D-04C7DE4F2D9A', 'F6309C30-3BE2-4B8B-B1E8-6F167AFEFA44');
INSERT INTO GameIllustrator (GameId, IllustratorId) VALUES ('ADD19568-F7DD-4D39-9074-3DE36255CB36', 'D00184A9-09C7-4BC0-8B17-6FB0DEF93D7B');
INSERT INTO GameIllustrator (GameId, IllustratorId) VALUES ('07AF3FFB-1866-45E2-AD91-47DC71F4A1C1', '91370359-5373-40BE-8C56-730DEE6856E6');
INSERT INTO GameIllustrator (GameId, IllustratorId) VALUES ('ADB16C86-8F08-44E7-B25B-42CFF53DAEE3', '9885EC92-60D2-4BF7-BC74-73C5D706BD36');
INSERT INTO GameIllustrator (GameId, IllustratorId) VALUES ('0E043E67-7B3C-4647-9001-487F20D5A669', '9885EC92-60D2-4BF7-BC74-73C5D706BD36');
INSERT INTO GameIllustrator (GameId, IllustratorId) VALUES ('388131C6-4120-4DF7-85E1-47EE2B64EE89', '1C3557D4-7E82-4E49-A053-757037276BB0');
INSERT INTO GameIllustrator (GameId, IllustratorId) VALUES ('88555477-A992-4289-BD2C-4F061CC36B47', '5838A744-9252-43CD-AB77-77975735C977');
INSERT INTO GameIllustrator (GameId, IllustratorId) VALUES ('DD6FD171-28B8-48B5-A92D-04C7DE4F2D9A', 'B05D842A-98E2-4FC0-83C4-7E22C82235A1');
INSERT INTO GameIllustrator (GameId, IllustratorId) VALUES ('DD6FD171-28B8-48B5-A92D-04C7DE4F2D9A', 'B29CDB1D-5744-4BFC-9598-7E2AC164109D');
INSERT INTO GameIllustrator (GameId, IllustratorId) VALUES ('F6672718-39D9-46C5-9211-45544B135723', '9203A802-CD09-44BF-94CC-80461B8FC01E');
INSERT INTO GameIllustrator (GameId, IllustratorId) VALUES ('DD6FD171-28B8-48B5-A92D-04C7DE4F2D9A', 'EDA43045-BEE0-4728-8264-81CBD33436FE');
INSERT INTO GameIllustrator (GameId, IllustratorId) VALUES ('ADD19568-F7DD-4D39-9074-3DE36255CB36', '53B81326-0AA5-4361-8AB0-81ECF365BD01');
INSERT INTO GameIllustrator (GameId, IllustratorId) VALUES ('DD6FD171-28B8-48B5-A92D-04C7DE4F2D9A', 'B615829E-2D81-49D3-AD92-836B073EB43E');
INSERT INTO GameIllustrator (GameId, IllustratorId) VALUES ('ADD19568-F7DD-4D39-9074-3DE36255CB36', '928AA81B-0969-4E3C-A7F7-8416902CB900');
INSERT INTO GameIllustrator (GameId, IllustratorId) VALUES ('DD6FD171-28B8-48B5-A92D-04C7DE4F2D9A', '37D1574C-7244-418D-AB46-8A982D296881');
INSERT INTO GameIllustrator (GameId, IllustratorId) VALUES ('DD6FD171-28B8-48B5-A92D-04C7DE4F2D9A', '4CFD51E1-B427-43E0-90BA-8BE05E6CC589');
INSERT INTO GameIllustrator (GameId, IllustratorId) VALUES ('DD6FD171-28B8-48B5-A92D-04C7DE4F2D9A', '41DEA361-BD9E-4C70-9B88-8E03475D23B7');
INSERT INTO GameIllustrator (GameId, IllustratorId) VALUES ('DD6FD171-28B8-48B5-A92D-04C7DE4F2D9A', 'B024EF39-6716-417A-8C22-8FD39676204A');
INSERT INTO GameIllustrator (GameId, IllustratorId) VALUES ('DD6FD171-28B8-48B5-A92D-04C7DE4F2D9A', 'D0C46E9B-3225-437E-95BF-92E03A1A2ABF');
INSERT INTO GameIllustrator (GameId, IllustratorId) VALUES ('945D2891-B7F4-4A92-BBE0-07223F0C397D', '4909E83A-B96E-4851-AA0A-94601D89F742');
INSERT INTO GameIllustrator (GameId, IllustratorId) VALUES ('388131C6-4120-4DF7-85E1-47EE2B64EE89', 'F5B8921C-A11A-4305-AC36-9993B11CE3A3');
INSERT INTO GameIllustrator (GameId, IllustratorId) VALUES ('BF6AFD5E-3EDB-4886-9374-4A94A569266B', '1DA6CD8C-BB50-4863-B958-99EF90FD0341');
INSERT INTO GameIllustrator (GameId, IllustratorId) VALUES ('F6672718-39D9-46C5-9211-45544B135723', 'D3DFAEF0-54F0-4AF5-AE84-9A5605F055D7');
INSERT INTO GameIllustrator (GameId, IllustratorId) VALUES ('DD6FD171-28B8-48B5-A92D-04C7DE4F2D9A', '9331F494-A80D-4A23-A26C-9DD2EC8967D6');
INSERT INTO GameIllustrator (GameId, IllustratorId) VALUES ('35D65DAD-36E4-42DF-9EA5-4208771D4FE4', '26629DBF-5FA4-40E8-B747-A0FFCD93BAB1');
INSERT INTO GameIllustrator (GameId, IllustratorId) VALUES ('DD6FD171-28B8-48B5-A92D-04C7DE4F2D9A', '33C8F20B-A6EE-487D-94A4-A1A989FFBCA3');
INSERT INTO GameIllustrator (GameId, IllustratorId) VALUES ('DD6FD171-28B8-48B5-A92D-04C7DE4F2D9A', '45B3F99D-BEDC-45D8-BE4D-A1FF76DFB6AA');
INSERT INTO GameIllustrator (GameId, IllustratorId) VALUES ('4721FAB4-B8E9-432C-A6A6-02741EE71E45', '71E0079A-8327-49E9-A037-A5DAA5D49B0C');
INSERT INTO GameIllustrator (GameId, IllustratorId) VALUES ('12230528-C9FB-4511-B23C-3DDBBE793B1A', '71E0079A-8327-49E9-A037-A5DAA5D49B0C');
INSERT INTO GameIllustrator (GameId, IllustratorId) VALUES ('A074F3F3-96D0-4031-8008-4FE420BB4713', '71E0079A-8327-49E9-A037-A5DAA5D49B0C');
INSERT INTO GameIllustrator (GameId, IllustratorId) VALUES ('822F3D4E-92BF-4C9E-ABA8-82532EF4B486', '71E0079A-8327-49E9-A037-A5DAA5D49B0C');
INSERT INTO GameIllustrator (GameId, IllustratorId) VALUES ('DD6FD171-28B8-48B5-A92D-04C7DE4F2D9A', 'DD05E57C-F44A-4011-BE90-A6FE8C3A1074');
INSERT INTO GameIllustrator (GameId, IllustratorId) VALUES ('DD6FD171-28B8-48B5-A92D-04C7DE4F2D9A', 'CF7F461E-EBD7-4535-B917-A9272689E2CE');
INSERT INTO GameIllustrator (GameId, IllustratorId) VALUES ('DD6FD171-28B8-48B5-A92D-04C7DE4F2D9A', '56F05AC6-6313-447C-A28D-ACF3CDA0886B');
INSERT INTO GameIllustrator (GameId, IllustratorId) VALUES ('D9551C9B-951D-4EE3-A2AD-05515CDCB676', 'F2D3BFF5-EC0E-4E87-8D40-B2D00F3387B5');
INSERT INTO GameIllustrator (GameId, IllustratorId) VALUES ('83F314E6-760D-46DF-9559-03F7E92FBD4A', '13F1946C-F3C7-44CD-A841-B2DBEBAB4B30');
INSERT INTO GameIllustrator (GameId, IllustratorId) VALUES ('DD6FD171-28B8-48B5-A92D-04C7DE4F2D9A', '13F1946C-F3C7-44CD-A841-B2DBEBAB4B30');
INSERT INTO GameIllustrator (GameId, IllustratorId) VALUES ('DD6FD171-28B8-48B5-A92D-04C7DE4F2D9A', '224764A6-D976-4CBF-A804-B4676E641AAC');
INSERT INTO GameIllustrator (GameId, IllustratorId) VALUES ('79F5544A-A0A1-46F8-AAF2-0EC2EE3FD146', '539F427D-16AD-4DC1-95F5-BEEF055F0708');
INSERT INTO GameIllustrator (GameId, IllustratorId) VALUES ('1409E0D0-5CC2-40F4-B272-46852CBF0423', '539F427D-16AD-4DC1-95F5-BEEF055F0708');
INSERT INTO GameIllustrator (GameId, IllustratorId) VALUES ('43A2BBD5-1D5D-4ABC-8C92-4CADA3A5729E', '15CEF5D4-9A0E-406C-9276-C1BF8FA7C55E');
INSERT INTO GameIllustrator (GameId, IllustratorId) VALUES ('DD6FD171-28B8-48B5-A92D-04C7DE4F2D9A', '273EBDBA-441A-4B3C-8ED4-C1D70C71E8DF');
INSERT INTO GameIllustrator (GameId, IllustratorId) VALUES ('ADD19568-F7DD-4D39-9074-3DE36255CB36', 'B49B8757-C175-4502-8A99-C66ED5838D08');
INSERT INTO GameIllustrator (GameId, IllustratorId) VALUES ('DD6FD171-28B8-48B5-A92D-04C7DE4F2D9A', 'E44980B4-CBC6-4443-ADA7-C8A29D3948A9');
INSERT INTO GameIllustrator (GameId, IllustratorId) VALUES ('D9F32603-ED77-49C2-A327-0026748224F5', '55042807-1E75-4AC9-AF88-CA1211AA943D');
INSERT INTO GameIllustrator (GameId, IllustratorId) VALUES ('DBDF1C0B-F813-45DA-B66C-00632E2B113F', '7ACACB76-2FFC-484B-B8A0-CA882A8634C0');
INSERT INTO GameIllustrator (GameId, IllustratorId) VALUES ('DD6FD171-28B8-48B5-A92D-04C7DE4F2D9A', '2767C7C5-E23E-4DAB-94DD-CA8E50EF7653');
INSERT INTO GameIllustrator (GameId, IllustratorId) VALUES ('DD6FD171-28B8-48B5-A92D-04C7DE4F2D9A', '7F3513D3-66B0-4C76-AA0F-CB6710D7422B');
INSERT INTO GameIllustrator (GameId, IllustratorId) VALUES ('DD6FD171-28B8-48B5-A92D-04C7DE4F2D9A', 'BD5CCBC4-1619-4D94-B7E5-CF578B84D217');
INSERT INTO GameIllustrator (GameId, IllustratorId) VALUES ('E41AAA87-6337-4D68-9B96-5037E75B503C', 'DD11116F-D0AE-4350-B649-CF83BCA6E1EB');
INSERT INTO GameIllustrator (GameId, IllustratorId) VALUES ('A074F3F3-96D0-4031-8008-4FE420BB4713', '35489DFC-12E6-42A0-AD60-D256A2137837');
INSERT INTO GameIllustrator (GameId, IllustratorId) VALUES ('8F089DB6-1B79-4E1D-B022-96985CD9F18A', '35489DFC-12E6-42A0-AD60-D256A2137837');
INSERT INTO GameIllustrator (GameId, IllustratorId) VALUES ('DD6FD171-28B8-48B5-A92D-04C7DE4F2D9A', '9293F240-FC35-4979-B11C-D34DE1E180CC');
INSERT INTO GameIllustrator (GameId, IllustratorId) VALUES ('DD6FD171-28B8-48B5-A92D-04C7DE4F2D9A', 'E2708139-5CE0-4CE8-9682-D5A568E2CCDE');
INSERT INTO GameIllustrator (GameId, IllustratorId) VALUES ('BF6AFD5E-3EDB-4886-9374-4A94A569266B', 'AA9FD3DC-8CC0-4B3C-86A3-D9A955E9CE16');
INSERT INTO GameIllustrator (GameId, IllustratorId) VALUES ('DD6FD171-28B8-48B5-A92D-04C7DE4F2D9A', '789FADA0-E890-4081-976E-DA1D526AF12B');
INSERT INTO GameIllustrator (GameId, IllustratorId) VALUES ('12230528-C9FB-4511-B23C-3DDBBE793B1A', 'DEBB3057-6B5C-4AFB-8D0E-DDA48E909286');
INSERT INTO GameIllustrator (GameId, IllustratorId) VALUES ('285F5601-1535-468B-A443-4589ABC5F800', 'DEBB3057-6B5C-4AFB-8D0E-DDA48E909286');
INSERT INTO GameIllustrator (GameId, IllustratorId) VALUES ('974D1366-202B-4C2E-8950-526434508858', 'DEBB3057-6B5C-4AFB-8D0E-DDA48E909286');
INSERT INTO GameIllustrator (GameId, IllustratorId) VALUES ('6B007B51-FDB7-4C6C-9634-0653E55A40FB', '76A7CEA7-95D1-44CA-BF49-E743D7B406AA');
INSERT INTO GameIllustrator (GameId, IllustratorId) VALUES ('ADD19568-F7DD-4D39-9074-3DE36255CB36', '68688E75-34B0-4AFB-B2B8-E9A6680944E3');
INSERT INTO GameIllustrator (GameId, IllustratorId) VALUES ('DD6FD171-28B8-48B5-A92D-04C7DE4F2D9A', 'B38C941C-740A-4B8E-AD87-EF569DC15819');
INSERT INTO GameIllustrator (GameId, IllustratorId) VALUES ('DD6FD171-28B8-48B5-A92D-04C7DE4F2D9A', '3A2D7AB2-833F-4353-B1EC-EFE3CCDDF627');
INSERT INTO GameIllustrator (GameId, IllustratorId) VALUES ('EECC55EA-26F6-45AA-8F6A-99025044BF91', '09C64C14-28E0-40AB-B8BD-F0A14DE86C90');
INSERT INTO GameIllustrator (GameId, IllustratorId) VALUES ('EECC55EA-26F6-45AA-8F6A-99025044BF91', '64B01082-64C7-4CA2-85AE-F182DAF0A24F');
INSERT INTO GameIllustrator (GameId, IllustratorId) VALUES ('025D5EC1-A371-4832-BE45-40DD21E50D30', '7B526B31-C1A6-438C-8FDE-F2AE0FD379E6');
INSERT INTO GameIllustrator (GameId, IllustratorId) VALUES ('DD6FD171-28B8-48B5-A92D-04C7DE4F2D9A', 'A9479DE2-3BD2-4492-936A-F8922272263F');
INSERT INTO GameIllustrator (GameId, IllustratorId) VALUES ('DD6FD171-28B8-48B5-A92D-04C7DE4F2D9A', 'B2938EED-207E-4982-A7B0-FD0EF379F4B0');
INSERT INTO GameIllustrator (GameId, IllustratorId) VALUES ('83F314E6-760D-46DF-9559-03F7E92FBD4A', 'BEE9B4D4-E18E-4442-A410-FF866818291A');
INSERT INTO GameIllustrator (GameId, IllustratorId) VALUES ('EECC55EA-26F6-45AA-8F6A-99025044BF91', '2384C952-AF61-473F-8284-FF910CB24299');
INSERT INTO GameMechanic (GameId, MechanicId) VALUES ('12230528-C9FB-4511-B23C-3DDBBE793B1A', 'AD43DD09-0AD2-4E59-8071-0601871B0B41');
INSERT INTO GameMechanic (GameId, MechanicId) VALUES ('0E043E67-7B3C-4647-9001-487F20D5A669', 'AD43DD09-0AD2-4E59-8071-0601871B0B41');
INSERT INTO GameMechanic (GameId, MechanicId) VALUES ('A074F3F3-96D0-4031-8008-4FE420BB4713', 'AD43DD09-0AD2-4E59-8071-0601871B0B41');
INSERT INTO GameMechanic (GameId, MechanicId) VALUES ('69E4AD75-32A0-4C57-919D-533E7BB3AF21', '8E8CEB06-8EE1-47A3-9EB4-11458C4735D6');
INSERT INTO GameMechanic (GameId, MechanicId) VALUES ('69E4AD75-32A0-4C57-919D-533E7BB3AF21', 'E649EF7C-1C48-449B-8B59-160F6518B07D');
INSERT INTO GameMechanic (GameId, MechanicId) VALUES ('2DDD0247-0973-4771-9254-B3F1D75C8A84', 'F7D2DCC4-703A-45FA-B17B-26E06087A9B6');
INSERT INTO GameMechanic (GameId, MechanicId) VALUES ('DBC6FBB7-FEF0-422C-8DCA-049AA98AF2A9', '67809247-1524-4F47-9E52-311D1D9E5307');
INSERT INTO GameMechanic (GameId, MechanicId) VALUES ('DD6FD171-28B8-48B5-A92D-04C7DE4F2D9A', '67809247-1524-4F47-9E52-311D1D9E5307');
INSERT INTO GameMechanic (GameId, MechanicId) VALUES ('6B007B51-FDB7-4C6C-9634-0653E55A40FB', '67809247-1524-4F47-9E52-311D1D9E5307');
INSERT INTO GameMechanic (GameId, MechanicId) VALUES ('F6672718-39D9-46C5-9211-45544B135723', '67809247-1524-4F47-9E52-311D1D9E5307');
INSERT INTO GameMechanic (GameId, MechanicId) VALUES ('285F5601-1535-468B-A443-4589ABC5F800', '67809247-1524-4F47-9E52-311D1D9E5307');
INSERT INTO GameMechanic (GameId, MechanicId) VALUES ('FCD61FF3-F6A7-4A55-B8B4-4EE879A3AEC4', '67809247-1524-4F47-9E52-311D1D9E5307');
INSERT INTO GameMechanic (GameId, MechanicId) VALUES ('8F089DB6-1B79-4E1D-B022-96985CD9F18A', '67809247-1524-4F47-9E52-311D1D9E5307');
INSERT INTO GameMechanic (GameId, MechanicId) VALUES ('EECC55EA-26F6-45AA-8F6A-99025044BF91', '67809247-1524-4F47-9E52-311D1D9E5307');
INSERT INTO GameMechanic (GameId, MechanicId) VALUES ('D9551C9B-951D-4EE3-A2AD-05515CDCB676', 'BDB2ED44-54AF-4338-9A91-3F835529E435');
INSERT INTO GameMechanic (GameId, MechanicId) VALUES ('285F5601-1535-468B-A443-4589ABC5F800', 'BDB2ED44-54AF-4338-9A91-3F835529E435');
INSERT INTO GameMechanic (GameId, MechanicId) VALUES ('65CCAD52-ACD9-4BDD-9208-4B9743E250ED', 'BDB2ED44-54AF-4338-9A91-3F835529E435');
INSERT INTO GameMechanic (GameId, MechanicId) VALUES ('BF6AFD5E-3EDB-4886-9374-4A94A569266B', '288F3504-82EE-4710-80A4-464184780A32');
INSERT INTO GameMechanic (GameId, MechanicId) VALUES ('88555477-A992-4289-BD2C-4F061CC36B47', '288F3504-82EE-4710-80A4-464184780A32');
INSERT INTO GameMechanic (GameId, MechanicId) VALUES ('12230528-C9FB-4511-B23C-3DDBBE793B1A', '28673CEE-9634-4ABD-BBA7-4A0153F19138');
INSERT INTO GameMechanic (GameId, MechanicId) VALUES ('CEBF983E-0A43-437B-9A05-41A069F0D9DA', '28673CEE-9634-4ABD-BBA7-4A0153F19138');
INSERT INTO GameMechanic (GameId, MechanicId) VALUES ('0E043E67-7B3C-4647-9001-487F20D5A669', '28673CEE-9634-4ABD-BBA7-4A0153F19138');
INSERT INTO GameMechanic (GameId, MechanicId) VALUES ('43A2BBD5-1D5D-4ABC-8C92-4CADA3A5729E', '28673CEE-9634-4ABD-BBA7-4A0153F19138');
INSERT INTO GameMechanic (GameId, MechanicId) VALUES ('2DDD0247-0973-4771-9254-B3F1D75C8A84', '28673CEE-9634-4ABD-BBA7-4A0153F19138');
INSERT INTO GameMechanic (GameId, MechanicId) VALUES ('98634FC4-D569-4459-8503-FA3BDD377A42', '28673CEE-9634-4ABD-BBA7-4A0153F19138');
INSERT INTO GameMechanic (GameId, MechanicId) VALUES ('6B007B51-FDB7-4C6C-9634-0653E55A40FB', '1ABF5B58-3D6E-4608-B8A6-4DEBF36CF514');
INSERT INTO GameMechanic (GameId, MechanicId) VALUES ('388131C6-4120-4DF7-85E1-47EE2B64EE89', '1ABF5B58-3D6E-4608-B8A6-4DEBF36CF514');
INSERT INTO GameMechanic (GameId, MechanicId) VALUES ('4674E54B-63AA-4D63-A44C-FC1A63C7C631', '1ABF5B58-3D6E-4608-B8A6-4DEBF36CF514');
INSERT INTO GameMechanic (GameId, MechanicId) VALUES ('DD6FD171-28B8-48B5-A92D-04C7DE4F2D9A', '717D83C4-7302-4E10-B489-6A20208B2B1E');
INSERT INTO GameMechanic (GameId, MechanicId) VALUES ('ADB16C86-8F08-44E7-B25B-42CFF53DAEE3', '78CB0285-F794-48DD-8C23-6CEB9D344FD8');
INSERT INTO GameMechanic (GameId, MechanicId) VALUES ('2DDD0247-0973-4771-9254-B3F1D75C8A84', '8B81F5B1-16C8-48B1-8F38-7771F48C9E2F');
INSERT INTO GameMechanic (GameId, MechanicId) VALUES ('4721FAB4-B8E9-432C-A6A6-02741EE71E45', 'AEE7BDB2-1632-4A87-A7A5-7822A5203145');
INSERT INTO GameMechanic (GameId, MechanicId) VALUES ('D9551C9B-951D-4EE3-A2AD-05515CDCB676', 'AEE7BDB2-1632-4A87-A7A5-7822A5203145');
INSERT INTO GameMechanic (GameId, MechanicId) VALUES ('BCEBA3EE-5CAC-4000-8B9C-086E80F5DC27', 'AEE7BDB2-1632-4A87-A7A5-7822A5203145');
INSERT INTO GameMechanic (GameId, MechanicId) VALUES ('883D0F36-BA48-475B-A64D-430C6DCEDDA0', 'AEE7BDB2-1632-4A87-A7A5-7822A5203145');
INSERT INTO GameMechanic (GameId, MechanicId) VALUES ('285F5601-1535-468B-A443-4589ABC5F800', 'F6D345DA-594D-4DC3-8083-7ED60974AE67');
INSERT INTO GameMechanic (GameId, MechanicId) VALUES ('07C6D4B4-76E3-422E-8F0E-494D78FF67CA', 'F6D345DA-594D-4DC3-8083-7ED60974AE67');
INSERT INTO GameMechanic (GameId, MechanicId) VALUES ('8D46FD79-B15B-437C-AA9B-4C654DD35026', 'DADA1111-4EEC-4855-81EB-80138CD8C31C');
INSERT INTO GameMechanic (GameId, MechanicId) VALUES ('69E4AD75-32A0-4C57-919D-533E7BB3AF21', 'DADA1111-4EEC-4855-81EB-80138CD8C31C');
INSERT INTO GameMechanic (GameId, MechanicId) VALUES ('ADD19568-F7DD-4D39-9074-3DE36255CB36', '414DF22A-5300-4C68-9BE9-821FA91B42F3');
INSERT INTO GameMechanic (GameId, MechanicId) VALUES ('FCD61FF3-F6A7-4A55-B8B4-4EE879A3AEC4', '414DF22A-5300-4C68-9BE9-821FA91B42F3');
INSERT INTO GameMechanic (GameId, MechanicId) VALUES ('EECC55EA-26F6-45AA-8F6A-99025044BF91', '414DF22A-5300-4C68-9BE9-821FA91B42F3');
INSERT INTO GameMechanic (GameId, MechanicId) VALUES ('12230528-C9FB-4511-B23C-3DDBBE793B1A', 'A10C9F58-7AB5-4628-A937-896B7AE8922A');
INSERT INTO GameMechanic (GameId, MechanicId) VALUES ('0E043E67-7B3C-4647-9001-487F20D5A669', 'A10C9F58-7AB5-4628-A937-896B7AE8922A');
INSERT INTO GameMechanic (GameId, MechanicId) VALUES ('43A2BBD5-1D5D-4ABC-8C92-4CADA3A5729E', 'A10C9F58-7AB5-4628-A937-896B7AE8922A');
INSERT INTO GameMechanic (GameId, MechanicId) VALUES ('DBDF1C0B-F813-45DA-B66C-00632E2B113F', '62D76410-0D05-4026-A745-89739C0595FD');
INSERT INTO GameMechanic (GameId, MechanicId) VALUES ('83F314E6-760D-46DF-9559-03F7E92FBD4A', '62D76410-0D05-4026-A745-89739C0595FD');
INSERT INTO GameMechanic (GameId, MechanicId) VALUES ('DBC6FBB7-FEF0-422C-8DCA-049AA98AF2A9', '62D76410-0D05-4026-A745-89739C0595FD');
INSERT INTO GameMechanic (GameId, MechanicId) VALUES ('DD6FD171-28B8-48B5-A92D-04C7DE4F2D9A', '62D76410-0D05-4026-A745-89739C0595FD');
INSERT INTO GameMechanic (GameId, MechanicId) VALUES ('79F5544A-A0A1-46F8-AAF2-0EC2EE3FD146', '62D76410-0D05-4026-A745-89739C0595FD');
INSERT INTO GameMechanic (GameId, MechanicId) VALUES ('12230528-C9FB-4511-B23C-3DDBBE793B1A', '62D76410-0D05-4026-A745-89739C0595FD');
INSERT INTO GameMechanic (GameId, MechanicId) VALUES ('ADD19568-F7DD-4D39-9074-3DE36255CB36', '62D76410-0D05-4026-A745-89739C0595FD');
INSERT INTO GameMechanic (GameId, MechanicId) VALUES ('CEBF983E-0A43-437B-9A05-41A069F0D9DA', '62D76410-0D05-4026-A745-89739C0595FD');
INSERT INTO GameMechanic (GameId, MechanicId) VALUES ('35D65DAD-36E4-42DF-9EA5-4208771D4FE4', '62D76410-0D05-4026-A745-89739C0595FD');
INSERT INTO GameMechanic (GameId, MechanicId) VALUES ('1409E0D0-5CC2-40F4-B272-46852CBF0423', '62D76410-0D05-4026-A745-89739C0595FD');
INSERT INTO GameMechanic (GameId, MechanicId) VALUES ('07AF3FFB-1866-45E2-AD91-47DC71F4A1C1', '62D76410-0D05-4026-A745-89739C0595FD');
INSERT INTO GameMechanic (GameId, MechanicId) VALUES ('0E043E67-7B3C-4647-9001-487F20D5A669', '62D76410-0D05-4026-A745-89739C0595FD');
INSERT INTO GameMechanic (GameId, MechanicId) VALUES ('BF6AFD5E-3EDB-4886-9374-4A94A569266B', '62D76410-0D05-4026-A745-89739C0595FD');
INSERT INTO GameMechanic (GameId, MechanicId) VALUES ('43A2BBD5-1D5D-4ABC-8C92-4CADA3A5729E', '62D76410-0D05-4026-A745-89739C0595FD');
INSERT INTO GameMechanic (GameId, MechanicId) VALUES ('FCD61FF3-F6A7-4A55-B8B4-4EE879A3AEC4', '62D76410-0D05-4026-A745-89739C0595FD');
INSERT INTO GameMechanic (GameId, MechanicId) VALUES ('E41AAA87-6337-4D68-9B96-5037E75B503C', '62D76410-0D05-4026-A745-89739C0595FD');
INSERT INTO GameMechanic (GameId, MechanicId) VALUES ('DCCE13C2-6EBE-4F55-9375-66A61A2A567A', '62D76410-0D05-4026-A745-89739C0595FD');
INSERT INTO GameMechanic (GameId, MechanicId) VALUES ('EECC55EA-26F6-45AA-8F6A-99025044BF91', '62D76410-0D05-4026-A745-89739C0595FD');
INSERT INTO GameMechanic (GameId, MechanicId) VALUES ('98634FC4-D569-4459-8503-FA3BDD377A42', '62D76410-0D05-4026-A745-89739C0595FD');
INSERT INTO GameMechanic (GameId, MechanicId) VALUES ('4674E54B-63AA-4D63-A44C-FC1A63C7C631', '62D76410-0D05-4026-A745-89739C0595FD');
INSERT INTO GameMechanic (GameId, MechanicId) VALUES ('ADD19568-F7DD-4D39-9074-3DE36255CB36', 'F0EDECD9-1DF1-4E54-9964-8B8132F4EB57');
INSERT INTO GameMechanic (GameId, MechanicId) VALUES ('8D46FD79-B15B-437C-AA9B-4C654DD35026', 'F0EDECD9-1DF1-4E54-9964-8B8132F4EB57');
INSERT INTO GameMechanic (GameId, MechanicId) VALUES ('DD6FD171-28B8-48B5-A92D-04C7DE4F2D9A', '163D0787-D294-4F3A-AB83-8E07D004FE37');
INSERT INTO GameMechanic (GameId, MechanicId) VALUES ('388131C6-4120-4DF7-85E1-47EE2B64EE89', '163D0787-D294-4F3A-AB83-8E07D004FE37');
INSERT INTO GameMechanic (GameId, MechanicId) VALUES ('0E043E67-7B3C-4647-9001-487F20D5A669', '163D0787-D294-4F3A-AB83-8E07D004FE37');
INSERT INTO GameMechanic (GameId, MechanicId) VALUES ('98634FC4-D569-4459-8503-FA3BDD377A42', '163D0787-D294-4F3A-AB83-8E07D004FE37');
INSERT INTO GameMechanic (GameId, MechanicId) VALUES ('69E4AD75-32A0-4C57-919D-533E7BB3AF21', '5D9E92D5-8A13-45BE-8A85-910F0441992C');
INSERT INTO GameMechanic (GameId, MechanicId) VALUES ('DBC6FBB7-FEF0-422C-8DCA-049AA98AF2A9', '071DA9B5-CD5C-4E6B-9572-9392A187E5ED');
INSERT INTO GameMechanic (GameId, MechanicId) VALUES ('BCEBA3EE-5CAC-4000-8B9C-086E80F5DC27', '071DA9B5-CD5C-4E6B-9572-9392A187E5ED');
INSERT INTO GameMechanic (GameId, MechanicId) VALUES ('F8E3A417-6C7C-4A98-9203-0EE5F4596A35', '071DA9B5-CD5C-4E6B-9572-9392A187E5ED');
INSERT INTO GameMechanic (GameId, MechanicId) VALUES ('025D5EC1-A371-4832-BE45-40DD21E50D30', '071DA9B5-CD5C-4E6B-9572-9392A187E5ED');
INSERT INTO GameMechanic (GameId, MechanicId) VALUES ('285F5601-1535-468B-A443-4589ABC5F800', '071DA9B5-CD5C-4E6B-9572-9392A187E5ED');
INSERT INTO GameMechanic (GameId, MechanicId) VALUES ('65CCAD52-ACD9-4BDD-9208-4B9743E250ED', '071DA9B5-CD5C-4E6B-9572-9392A187E5ED');
INSERT INTO GameMechanic (GameId, MechanicId) VALUES ('DBC6FBB7-FEF0-422C-8DCA-049AA98AF2A9', '397C8CD8-99DD-4BE8-A455-98759B820EDF');
INSERT INTO GameMechanic (GameId, MechanicId) VALUES ('4721FAB4-B8E9-432C-A6A6-02741EE71E45', 'DBA7681E-3ECD-4569-91D6-A26B7079115F');
INSERT INTO GameMechanic (GameId, MechanicId) VALUES ('12230528-C9FB-4511-B23C-3DDBBE793B1A', 'DBA7681E-3ECD-4569-91D6-A26B7079115F');
INSERT INTO GameMechanic (GameId, MechanicId) VALUES ('35D65DAD-36E4-42DF-9EA5-4208771D4FE4', 'DBA7681E-3ECD-4569-91D6-A26B7079115F');
INSERT INTO GameMechanic (GameId, MechanicId) VALUES ('07C6D4B4-76E3-422E-8F0E-494D78FF67CA', 'DBA7681E-3ECD-4569-91D6-A26B7079115F');
INSERT INTO GameMechanic (GameId, MechanicId) VALUES ('83F314E6-760D-46DF-9559-03F7E92FBD4A', '4E7458C7-F7EC-4BC4-BC38-A33403472A55');
INSERT INTO GameMechanic (GameId, MechanicId) VALUES ('DBC6FBB7-FEF0-422C-8DCA-049AA98AF2A9', '4E7458C7-F7EC-4BC4-BC38-A33403472A55');
INSERT INTO GameMechanic (GameId, MechanicId) VALUES ('ADB16C86-8F08-44E7-B25B-42CFF53DAEE3', '4E7458C7-F7EC-4BC4-BC38-A33403472A55');
INSERT INTO GameMechanic (GameId, MechanicId) VALUES ('07AF3FFB-1866-45E2-AD91-47DC71F4A1C1', '4E7458C7-F7EC-4BC4-BC38-A33403472A55');
INSERT INTO GameMechanic (GameId, MechanicId) VALUES ('BF6AFD5E-3EDB-4886-9374-4A94A569266B', '4E7458C7-F7EC-4BC4-BC38-A33403472A55');
INSERT INTO GameMechanic (GameId, MechanicId) VALUES ('DCCE13C2-6EBE-4F55-9375-66A61A2A567A', '4E7458C7-F7EC-4BC4-BC38-A33403472A55');
INSERT INTO GameMechanic (GameId, MechanicId) VALUES ('8F089DB6-1B79-4E1D-B022-96985CD9F18A', '4E7458C7-F7EC-4BC4-BC38-A33403472A55');
INSERT INTO GameMechanic (GameId, MechanicId) VALUES ('4674E54B-63AA-4D63-A44C-FC1A63C7C631', '4E7458C7-F7EC-4BC4-BC38-A33403472A55');
INSERT INTO GameMechanic (GameId, MechanicId) VALUES ('ADD19568-F7DD-4D39-9074-3DE36255CB36', 'C0E026C5-6FDC-4936-A7AC-A40A5057CC83');
INSERT INTO GameMechanic (GameId, MechanicId) VALUES ('07C6D4B4-76E3-422E-8F0E-494D78FF67CA', 'C0E026C5-6FDC-4936-A7AC-A40A5057CC83');
INSERT INTO GameMechanic (GameId, MechanicId) VALUES ('8D46FD79-B15B-437C-AA9B-4C654DD35026', 'C0E026C5-6FDC-4936-A7AC-A40A5057CC83');
INSERT INTO GameMechanic (GameId, MechanicId) VALUES ('69E4AD75-32A0-4C57-919D-533E7BB3AF21', 'C0E026C5-6FDC-4936-A7AC-A40A5057CC83');
INSERT INTO GameMechanic (GameId, MechanicId) VALUES ('BF6AFD5E-3EDB-4886-9374-4A94A569266B', 'EE6C1E82-4820-4D42-92DE-AEE3A9F00D90');
INSERT INTO GameMechanic (GameId, MechanicId) VALUES ('8D46FD79-B15B-437C-AA9B-4C654DD35026', 'EE6C1E82-4820-4D42-92DE-AEE3A9F00D90');
INSERT INTO GameMechanic (GameId, MechanicId) VALUES ('69E4AD75-32A0-4C57-919D-533E7BB3AF21', 'EE6C1E82-4820-4D42-92DE-AEE3A9F00D90');
INSERT INTO GameMechanic (GameId, MechanicId) VALUES ('DD6FD171-28B8-48B5-A92D-04C7DE4F2D9A', '69FDA797-FA42-41E3-B759-B067E818CDC4');
INSERT INTO GameMechanic (GameId, MechanicId) VALUES ('12230528-C9FB-4511-B23C-3DDBBE793B1A', '69FDA797-FA42-41E3-B759-B067E818CDC4');
INSERT INTO GameMechanic (GameId, MechanicId) VALUES ('025D5EC1-A371-4832-BE45-40DD21E50D30', '69FDA797-FA42-41E3-B759-B067E818CDC4');
INSERT INTO GameMechanic (GameId, MechanicId) VALUES ('883D0F36-BA48-475B-A64D-430C6DCEDDA0', '69FDA797-FA42-41E3-B759-B067E818CDC4');
INSERT INTO GameMechanic (GameId, MechanicId) VALUES ('65CCAD52-ACD9-4BDD-9208-4B9743E250ED', '69FDA797-FA42-41E3-B759-B067E818CDC4');
INSERT INTO GameMechanic (GameId, MechanicId) VALUES ('155E4F6C-368C-4FA2-B388-4D4FCE8A359C', '45AC002A-52AB-4DF2-AE00-B4BB9D92D42B');
INSERT INTO GameMechanic (GameId, MechanicId) VALUES ('BF6AFD5E-3EDB-4886-9374-4A94A569266B', '5871984D-411B-44C3-9D63-C11DD2C222A9');
INSERT INTO GameMechanic (GameId, MechanicId) VALUES ('83F314E6-760D-46DF-9559-03F7E92FBD4A', '7FD30336-0431-42DC-BFF8-C445308BF113');
INSERT INTO GameMechanic (GameId, MechanicId) VALUES ('DD6FD171-28B8-48B5-A92D-04C7DE4F2D9A', '7FD30336-0431-42DC-BFF8-C445308BF113');
INSERT INTO GameMechanic (GameId, MechanicId) VALUES ('BCEBA3EE-5CAC-4000-8B9C-086E80F5DC27', '7FD30336-0431-42DC-BFF8-C445308BF113');
INSERT INTO GameMechanic (GameId, MechanicId) VALUES ('12230528-C9FB-4511-B23C-3DDBBE793B1A', '7FD30336-0431-42DC-BFF8-C445308BF113');
INSERT INTO GameMechanic (GameId, MechanicId) VALUES ('ADD19568-F7DD-4D39-9074-3DE36255CB36', '7FD30336-0431-42DC-BFF8-C445308BF113');
INSERT INTO GameMechanic (GameId, MechanicId) VALUES ('025D5EC1-A371-4832-BE45-40DD21E50D30', '7FD30336-0431-42DC-BFF8-C445308BF113');
INSERT INTO GameMechanic (GameId, MechanicId) VALUES ('CEBF983E-0A43-437B-9A05-41A069F0D9DA', '7FD30336-0431-42DC-BFF8-C445308BF113');
INSERT INTO GameMechanic (GameId, MechanicId) VALUES ('BF6AFD5E-3EDB-4886-9374-4A94A569266B', '7FD30336-0431-42DC-BFF8-C445308BF113');
INSERT INTO GameMechanic (GameId, MechanicId) VALUES ('FCD61FF3-F6A7-4A55-B8B4-4EE879A3AEC4', '7FD30336-0431-42DC-BFF8-C445308BF113');
INSERT INTO GameMechanic (GameId, MechanicId) VALUES ('EECC55EA-26F6-45AA-8F6A-99025044BF91', '7FD30336-0431-42DC-BFF8-C445308BF113');
INSERT INTO GameMechanic (GameId, MechanicId) VALUES ('98634FC4-D569-4459-8503-FA3BDD377A42', '7FD30336-0431-42DC-BFF8-C445308BF113');
INSERT INTO GameMechanic (GameId, MechanicId) VALUES ('4674E54B-63AA-4D63-A44C-FC1A63C7C631', '7FD30336-0431-42DC-BFF8-C445308BF113');
INSERT INTO GameMechanic (GameId, MechanicId) VALUES ('65CCAD52-ACD9-4BDD-9208-4B9743E250ED', '6862FD36-107B-4F66-AF6F-D486B3F05858');
INSERT INTO GameMechanic (GameId, MechanicId) VALUES ('D9551C9B-951D-4EE3-A2AD-05515CDCB676', 'CC76C5FF-865E-4E7F-B358-DA8C637F2469');
INSERT INTO GameMechanic (GameId, MechanicId) VALUES ('79F5544A-A0A1-46F8-AAF2-0EC2EE3FD146', 'CC76C5FF-865E-4E7F-B358-DA8C637F2469');
INSERT INTO GameMechanic (GameId, MechanicId) VALUES ('12230528-C9FB-4511-B23C-3DDBBE793B1A', 'CC76C5FF-865E-4E7F-B358-DA8C637F2469');
INSERT INTO GameMechanic (GameId, MechanicId) VALUES ('CEBF983E-0A43-437B-9A05-41A069F0D9DA', 'CC76C5FF-865E-4E7F-B358-DA8C637F2469');
INSERT INTO GameMechanic (GameId, MechanicId) VALUES ('9F73EF15-F210-4C4C-84C1-437CB5A93883', 'CC76C5FF-865E-4E7F-B358-DA8C637F2469');
INSERT INTO GameMechanic (GameId, MechanicId) VALUES ('F6672718-39D9-46C5-9211-45544B135723', 'CC76C5FF-865E-4E7F-B358-DA8C637F2469');
INSERT INTO GameMechanic (GameId, MechanicId) VALUES ('1409E0D0-5CC2-40F4-B272-46852CBF0423', 'CC76C5FF-865E-4E7F-B358-DA8C637F2469');
INSERT INTO GameMechanic (GameId, MechanicId) VALUES ('CDA55509-4E8D-4F9B-AFB1-4D6CFAFA08C1', 'CC76C5FF-865E-4E7F-B358-DA8C637F2469');
INSERT INTO GameMechanic (GameId, MechanicId) VALUES ('FCD61FF3-F6A7-4A55-B8B4-4EE879A3AEC4', 'CC76C5FF-865E-4E7F-B358-DA8C637F2469');
INSERT INTO GameMechanic (GameId, MechanicId) VALUES ('EECC55EA-26F6-45AA-8F6A-99025044BF91', 'CC76C5FF-865E-4E7F-B358-DA8C637F2469');
INSERT INTO GameMechanic (GameId, MechanicId) VALUES ('98634FC4-D569-4459-8503-FA3BDD377A42', 'CC76C5FF-865E-4E7F-B358-DA8C637F2469');
INSERT INTO GameMechanic (GameId, MechanicId) VALUES ('79F5544A-A0A1-46F8-AAF2-0EC2EE3FD146', '7DC45204-C4E1-497D-AD22-DD36CEAF26F6');
INSERT INTO GameMechanic (GameId, MechanicId) VALUES ('1409E0D0-5CC2-40F4-B272-46852CBF0423', '7DC45204-C4E1-497D-AD22-DD36CEAF26F6');
INSERT INTO GameMechanic (GameId, MechanicId) VALUES ('07C6D4B4-76E3-422E-8F0E-494D78FF67CA', '7DC45204-C4E1-497D-AD22-DD36CEAF26F6');
INSERT INTO GameMechanic (GameId, MechanicId) VALUES ('65CCAD52-ACD9-4BDD-9208-4B9743E250ED', '7DC45204-C4E1-497D-AD22-DD36CEAF26F6');
INSERT INTO GameMechanic (GameId, MechanicId) VALUES ('6B007B51-FDB7-4C6C-9634-0653E55A40FB', 'BDFB24FB-E953-4B6B-A648-E8DA2F0F3421');
INSERT INTO GameMechanic (GameId, MechanicId) VALUES ('79F5544A-A0A1-46F8-AAF2-0EC2EE3FD146', 'BDFB24FB-E953-4B6B-A648-E8DA2F0F3421');
INSERT INTO GameMechanic (GameId, MechanicId) VALUES ('CEBF983E-0A43-437B-9A05-41A069F0D9DA', 'BDFB24FB-E953-4B6B-A648-E8DA2F0F3421');
INSERT INTO GameMechanic (GameId, MechanicId) VALUES ('35D65DAD-36E4-42DF-9EA5-4208771D4FE4', 'BDFB24FB-E953-4B6B-A648-E8DA2F0F3421');
INSERT INTO GameMechanic (GameId, MechanicId) VALUES ('9F73EF15-F210-4C4C-84C1-437CB5A93883', 'BDFB24FB-E953-4B6B-A648-E8DA2F0F3421');
INSERT INTO GameMechanic (GameId, MechanicId) VALUES ('1409E0D0-5CC2-40F4-B272-46852CBF0423', 'BDFB24FB-E953-4B6B-A648-E8DA2F0F3421');
INSERT INTO GameMechanic (GameId, MechanicId) VALUES ('BF6AFD5E-3EDB-4886-9374-4A94A569266B', 'BDFB24FB-E953-4B6B-A648-E8DA2F0F3421');
INSERT INTO GameMechanic (GameId, MechanicId) VALUES ('8D46FD79-B15B-437C-AA9B-4C654DD35026', 'BDFB24FB-E953-4B6B-A648-E8DA2F0F3421');
INSERT INTO GameMechanic (GameId, MechanicId) VALUES ('CDA55509-4E8D-4F9B-AFB1-4D6CFAFA08C1', 'BDFB24FB-E953-4B6B-A648-E8DA2F0F3421');
INSERT INTO GameMechanic (GameId, MechanicId) VALUES ('98634FC4-D569-4459-8503-FA3BDD377A42', 'BDFB24FB-E953-4B6B-A648-E8DA2F0F3421');
INSERT INTO GameMechanic (GameId, MechanicId) VALUES ('12230528-C9FB-4511-B23C-3DDBBE793B1A', '7F6A61B6-01BD-4E56-936C-F96776AB724B');
INSERT INTO GameMechanic (GameId, MechanicId) VALUES ('69E4AD75-32A0-4C57-919D-533E7BB3AF21', '7F6A61B6-01BD-4E56-936C-F96776AB724B');
INSERT INTO GameMechanic (GameId, MechanicId) VALUES ('388131C6-4120-4DF7-85E1-47EE2B64EE89', '761A8CF7-17BC-49FE-AAFB-F9E82CDD732D');

exec sp_MSforeachtable ""ALTER TABLE ? WITH CHECK CHECK CONSTRAINT all""
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
