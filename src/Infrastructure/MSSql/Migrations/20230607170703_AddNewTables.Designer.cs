﻿// <auto-generated />
using System;
using BowlingGame.Infrastructure.MSSql;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BowlingGame.Infrastructure.MSSql.Migrations
{
    [DbContext(typeof(BowlingGameContext))]
    [Migration("20230607170703_AddNewTables")]
    partial class AddNewTables
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BowlingGame.Infrastructure.MSSql.Models.DBFrame", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("FirstRoll")
                        .HasColumnType("int");

                    b.Property<Guid?>("GameId")
                        .IsRequired()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Index")
                        .HasColumnType("int");

                    b.Property<bool>("IsSpare")
                        .HasColumnType("bit");

                    b.Property<bool>("IsStrike")
                        .HasColumnType("bit");

                    b.Property<int>("SecondRoll")
                        .HasColumnType("int");

                    b.Property<int?>("ThirdRoll")
                        .HasColumnType("int");

                    b.Property<int?>("TotalScore")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("GameId");

                    b.ToTable("Frame", (string)null);
                });

            modelBuilder.Entity("BowlingGame.Infrastructure.MSSql.Models.DBGame", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("PlayerName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<int>("TotalScore")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Game", (string)null);
                });

            modelBuilder.Entity("BowlingGame.Infrastructure.MSSql.Models.DBFrame", b =>
                {
                    b.HasOne("BowlingGame.Infrastructure.MSSql.Models.DBGame", "Game")
                        .WithMany("Frames")
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Game");
                });

            modelBuilder.Entity("BowlingGame.Infrastructure.MSSql.Models.DBGame", b =>
                {
                    b.Navigation("Frames");
                });
#pragma warning restore 612, 618
        }
    }
}
