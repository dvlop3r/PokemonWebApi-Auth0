﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using PokemonWebApi_Auth0.Models;

namespace PokemonWebApi_Auth0.Migrations
{
    [DbContext(typeof(PokemonContext))]
    [Migration("20211111080540_InitialMigration2")]
    partial class InitialMigration2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.12")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("PokemonWebApi_Auth0.Models.Ability", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<int?>("PokedexId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("PokedexId");

                    b.ToTable("Abilities");
                });

            modelBuilder.Entity("PokemonWebApi_Auth0.Models.Pokedex", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Desc")
                        .HasColumnType("text");

                    b.Property<int?>("EvolvesFromId")
                        .HasColumnType("integer");

                    b.Property<int?>("EvolvesToId")
                        .HasColumnType("integer");

                    b.Property<double>("Height")
                        .HasColumnType("double precision");

                    b.Property<string>("Imagelink")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("EvolvesFromId");

                    b.HasIndex("EvolvesToId");

                    b.ToTable("Pokedexes");
                });

            modelBuilder.Entity("PokemonWebApi_Auth0.Models.PokedexItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Birthdate")
                        .HasColumnType("text");

                    b.Property<string>("Gender")
                        .HasColumnType("text");

                    b.Property<string>("Image")
                        .HasColumnType("text");

                    b.Property<string>("Nickname")
                        .HasColumnType("text");

                    b.Property<int>("PokedexId")
                        .HasColumnType("integer");

                    b.Property<string>("State")
                        .HasColumnType("text");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("PokedexId");

                    b.ToTable("PokemonItems");
                });

            modelBuilder.Entity("PokemonWebApi_Auth0.Models.Type", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<int?>("PokedexId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("PokedexId");

                    b.ToTable("Types");
                });

            modelBuilder.Entity("PokemonWebApi_Auth0.Models.Ability", b =>
                {
                    b.HasOne("PokemonWebApi_Auth0.Models.Pokedex", null)
                        .WithMany("Abilities")
                        .HasForeignKey("PokedexId");
                });

            modelBuilder.Entity("PokemonWebApi_Auth0.Models.Pokedex", b =>
                {
                    b.HasOne("PokemonWebApi_Auth0.Models.Pokedex", "EvolvesFrom")
                        .WithMany()
                        .HasForeignKey("EvolvesFromId");

                    b.HasOne("PokemonWebApi_Auth0.Models.Pokedex", "EvolvesTo")
                        .WithMany()
                        .HasForeignKey("EvolvesToId");

                    b.Navigation("EvolvesFrom");

                    b.Navigation("EvolvesTo");
                });

            modelBuilder.Entity("PokemonWebApi_Auth0.Models.PokedexItem", b =>
                {
                    b.HasOne("PokemonWebApi_Auth0.Models.Pokedex", "Pokedex")
                        .WithMany()
                        .HasForeignKey("PokedexId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Pokedex");
                });

            modelBuilder.Entity("PokemonWebApi_Auth0.Models.Type", b =>
                {
                    b.HasOne("PokemonWebApi_Auth0.Models.Pokedex", null)
                        .WithMany("Types")
                        .HasForeignKey("PokedexId");
                });

            modelBuilder.Entity("PokemonWebApi_Auth0.Models.Pokedex", b =>
                {
                    b.Navigation("Abilities");

                    b.Navigation("Types");
                });
#pragma warning restore 612, 618
        }
    }
}