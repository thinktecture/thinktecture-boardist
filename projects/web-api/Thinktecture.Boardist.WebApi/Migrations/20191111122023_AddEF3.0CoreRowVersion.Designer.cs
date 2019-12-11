﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Thinktecture.Boardist.WebApi.Database;

namespace Thinktecture.Boardist.WebApi.Migrations
{
    [DbContext(typeof(BoardistContext))]
    [Migration("20191111122023_AddEF3.0CoreRowVersion")]
    partial class AddEF30CoreRowVersion
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Thinktecture.Boardist.WebApi.Database.Models.Category", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int?>("BoardGameGeekId")
                        .HasColumnType("int");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(250)")
                        .HasMaxLength(250);

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .IsRequired()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion");

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("Thinktecture.Boardist.WebApi.Database.Models.Game", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int?>("BoardGameGeekId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("BuyDate")
                        .HasColumnType("datetime2");

                    b.Property<decimal?>("BuyPrice")
                        .HasColumnType("decimal(3,2)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<Guid?>("MainGameId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int?>("MaxDuration")
                        .HasColumnType("int");

                    b.Property<int>("MaxPlayers")
                        .HasColumnType("int");

                    b.Property<int>("MinAge")
                        .HasColumnType("int");

                    b.Property<int?>("MinDuration")
                        .HasColumnType("int");

                    b.Property<int>("MinPlayers")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(250)")
                        .HasMaxLength(250);

                    b.Property<Guid?>("PublisherId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .IsRequired()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion");

                    b.HasKey("Id");

                    b.HasIndex("MainGameId");

                    b.HasIndex("PublisherId");

                    b.ToTable("Games");
                });

            modelBuilder.Entity("Thinktecture.Boardist.WebApi.Database.Models.GameAuthor", b =>
                {
                    b.Property<Guid>("GameId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AuthorId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("GameId", "AuthorId");

                    b.HasIndex("AuthorId");

                    b.ToTable("GameAuthor");
                });

            modelBuilder.Entity("Thinktecture.Boardist.WebApi.Database.Models.GameCategory", b =>
                {
                    b.Property<Guid>("GameId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CategoryId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("GameId", "CategoryId");

                    b.HasIndex("CategoryId");

                    b.ToTable("GameCategory");
                });

            modelBuilder.Entity("Thinktecture.Boardist.WebApi.Database.Models.GameIllustrator", b =>
                {
                    b.Property<Guid>("GameId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("IllustratorId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("GameId", "IllustratorId");

                    b.HasIndex("IllustratorId");

                    b.ToTable("GameIllustrator");
                });

            modelBuilder.Entity("Thinktecture.Boardist.WebApi.Database.Models.GameMechanic", b =>
                {
                    b.Property<Guid>("GameId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("MechanicId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("GameId", "MechanicId");

                    b.HasIndex("MechanicId");

                    b.ToTable("GameMechanic");
                });

            modelBuilder.Entity("Thinktecture.Boardist.WebApi.Database.Models.Mechanic", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int?>("BoardGameGeekId")
                        .HasColumnType("int");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(250)")
                        .HasMaxLength(250);

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .IsRequired()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion");

                    b.HasKey("Id");

                    b.ToTable("Mechanics");
                });

            modelBuilder.Entity("Thinktecture.Boardist.WebApi.Database.Models.Person", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int?>("BoardGameGeekId")
                        .HasColumnType("int");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(250)")
                        .HasMaxLength(250);

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .IsRequired()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion");

                    b.HasKey("Id");

                    b.ToTable("Persons");
                });

            modelBuilder.Entity("Thinktecture.Boardist.WebApi.Database.Models.Publisher", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int?>("BoardGameGeekId")
                        .HasColumnType("int");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(250)")
                        .HasMaxLength(250);

                    b.Property<int>("Priority")
                        .HasColumnType("int");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .IsRequired()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion");

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
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Thinktecture.Boardist.WebApi.Database.Models.Game", "Game")
                        .WithMany("Authors")
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Thinktecture.Boardist.WebApi.Database.Models.GameCategory", b =>
                {
                    b.HasOne("Thinktecture.Boardist.WebApi.Database.Models.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Thinktecture.Boardist.WebApi.Database.Models.Game", "Game")
                        .WithMany("Categories")
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Thinktecture.Boardist.WebApi.Database.Models.GameIllustrator", b =>
                {
                    b.HasOne("Thinktecture.Boardist.WebApi.Database.Models.Game", "Game")
                        .WithMany("Illustrators")
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Thinktecture.Boardist.WebApi.Database.Models.Person", "Illustrator")
                        .WithMany()
                        .HasForeignKey("IllustratorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Thinktecture.Boardist.WebApi.Database.Models.GameMechanic", b =>
                {
                    b.HasOne("Thinktecture.Boardist.WebApi.Database.Models.Game", "Game")
                        .WithMany("Mechanics")
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Thinktecture.Boardist.WebApi.Database.Models.Mechanic", "Mechanic")
                        .WithMany()
                        .HasForeignKey("MechanicId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}