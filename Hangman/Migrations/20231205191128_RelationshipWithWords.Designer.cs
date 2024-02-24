﻿// <auto-generated />
using System;
using Hangman.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Hangman.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20231205191128_RelationshipWithWords")]
    partial class RelationshipWithWords
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Hangman.Models.Game", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ChosenWordId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CorrectLetters")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("GameStatus")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("IncorrectGuessesLeft")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ChosenWordId");

                    b.ToTable("Game");
                });

            modelBuilder.Entity("Hangman.Models.Guess", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CharacterGuessed")
                        .IsRequired()
                        .HasColumnType("nvarchar(1)");

                    b.Property<Guid>("GameId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsCorrect")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("GameId");

                    b.ToTable("Guess");
                });

            modelBuilder.Entity("Hangman.Models.Words", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Word")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Words");
                });

            modelBuilder.Entity("Hangman.Models.Game", b =>
                {
                    b.HasOne("Hangman.Models.Words", "ChosenWord")
                        .WithMany("GamesUsed")
                        .HasForeignKey("ChosenWordId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ChosenWord");
                });

            modelBuilder.Entity("Hangman.Models.Guess", b =>
                {
                    b.HasOne("Hangman.Models.Game", "Game")
                        .WithMany("Guesses")
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Game");
                });

            modelBuilder.Entity("Hangman.Models.Game", b =>
                {
                    b.Navigation("Guesses");
                });

            modelBuilder.Entity("Hangman.Models.Words", b =>
                {
                    b.Navigation("GamesUsed");
                });
#pragma warning restore 612, 618
        }
    }
}