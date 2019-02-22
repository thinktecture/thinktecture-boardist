﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Thinktecture.Boardist.WebApi.Database;

namespace Thinktecture.Boardist.WebApi.Migrations
{
    [DbContext(typeof(BoardistContext))]
    partial class BoardistContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.2-servicing-10034")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Thinktecture.Boardist.WebApi.Database.Models.Category", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("BoardGameGeekId");

                    b.Property<string>("Name")
                        .HasMaxLength(250);

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("Thinktecture.Boardist.WebApi.Database.Models.Game", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("BoardGameGeekId");

                    b.Property<DateTime?>("BuyDate");

                    b.Property<decimal?>("BuyPrice")
                        .HasColumnType("decimal(3,2)");

                    b.Property<Guid?>("MainGameId");

                    b.Property<int?>("MaxDuration");

                    b.Property<int>("MaxPlayers");

                    b.Property<int>("MinAge");

                    b.Property<int?>("MinDuration");

                    b.Property<int>("MinPlayers");

                    b.Property<string>("Name")
                        .HasMaxLength(250);

                    b.Property<Guid?>("PublisherId");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.HasKey("Id");

                    b.HasIndex("MainGameId");

                    b.HasIndex("PublisherId");

                    b.ToTable("Games");
                });

            modelBuilder.Entity("Thinktecture.Boardist.WebApi.Database.Models.GameAuthor", b =>
                {
                    b.Property<Guid>("GameId");

                    b.Property<Guid>("AuthorId");

                    b.HasKey("GameId", "AuthorId");

                    b.HasIndex("AuthorId");

                    b.ToTable("GameAuthor");
                });

            modelBuilder.Entity("Thinktecture.Boardist.WebApi.Database.Models.GameCategory", b =>
                {
                    b.Property<Guid>("GameId");

                    b.Property<Guid>("CategoryId");

                    b.HasKey("GameId", "CategoryId");

                    b.HasIndex("CategoryId");

                    b.ToTable("GameCategory");
                });

            modelBuilder.Entity("Thinktecture.Boardist.WebApi.Database.Models.GameIllustrator", b =>
                {
                    b.Property<Guid>("GameId");

                    b.Property<Guid>("IllustratorId");

                    b.HasKey("GameId", "IllustratorId");

                    b.HasIndex("IllustratorId");

                    b.ToTable("GameIllustrator");
                });

            modelBuilder.Entity("Thinktecture.Boardist.WebApi.Database.Models.GameMechanic", b =>
                {
                    b.Property<Guid>("GameId");

                    b.Property<Guid>("MechanicId");

                    b.HasKey("GameId", "MechanicId");

                    b.HasIndex("MechanicId");

                    b.ToTable("GameMechanic");
                });

            modelBuilder.Entity("Thinktecture.Boardist.WebApi.Database.Models.Mechanic", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("BoardGameGeekId");

                    b.Property<string>("Name")
                        .HasMaxLength(250);

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.HasKey("Id");

                    b.ToTable("Mechanics");
                });

            modelBuilder.Entity("Thinktecture.Boardist.WebApi.Database.Models.Person", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("BoardGameGeekId");

                    b.Property<string>("Name")
                        .HasMaxLength(250);

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.HasKey("Id");

                    b.ToTable("Persons");
                });

            modelBuilder.Entity("Thinktecture.Boardist.WebApi.Database.Models.Publisher", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("BoardGameGeekId");

                    b.Property<string>("Name")
                        .HasMaxLength(250);

                    b.Property<int>("Priority");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.HasKey("Id");

                    b.ToTable("Publishers");
                });

            modelBuilder.Entity("Thinktecture.Boardist.WebApi.Database.Models.Game", b =>
                {
                    b.HasOne("Thinktecture.Boardist.WebApi.Database.Models.Game", "MainGame")
                        .WithMany()
                        .HasForeignKey("MainGameId");

                    b.HasOne("Thinktecture.Boardist.WebApi.Database.Models.Publisher", "Publisher")
                        .WithMany()
                        .HasForeignKey("PublisherId")
                        .OnDelete(DeleteBehavior.SetNull);
                });

            modelBuilder.Entity("Thinktecture.Boardist.WebApi.Database.Models.GameAuthor", b =>
                {
                    b.HasOne("Thinktecture.Boardist.WebApi.Database.Models.Person", "Author")
                        .WithMany()
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Thinktecture.Boardist.WebApi.Database.Models.Game", "Game")
                        .WithMany("Authors")
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Thinktecture.Boardist.WebApi.Database.Models.GameCategory", b =>
                {
                    b.HasOne("Thinktecture.Boardist.WebApi.Database.Models.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Thinktecture.Boardist.WebApi.Database.Models.Game", "Game")
                        .WithMany("Categories")
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Thinktecture.Boardist.WebApi.Database.Models.GameIllustrator", b =>
                {
                    b.HasOne("Thinktecture.Boardist.WebApi.Database.Models.Game", "Game")
                        .WithMany("Illustrators")
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Thinktecture.Boardist.WebApi.Database.Models.Person", "Illustrator")
                        .WithMany()
                        .HasForeignKey("IllustratorId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Thinktecture.Boardist.WebApi.Database.Models.GameMechanic", b =>
                {
                    b.HasOne("Thinktecture.Boardist.WebApi.Database.Models.Game", "Game")
                        .WithMany("Mechanics")
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Thinktecture.Boardist.WebApi.Database.Models.Mechanic", "Mechanic")
                        .WithMany()
                        .HasForeignKey("MechanicId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
