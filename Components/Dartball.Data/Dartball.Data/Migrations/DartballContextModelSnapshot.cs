﻿// <auto-generated />
using System;
using Dartball.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Dartball.Data.Migrations
{
    [DbContext(typeof(DartballContext))]
    partial class DartballContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.0-rtm-30799");

            modelBuilder.Entity("Dartball.Domain.Game", b =>
                {
                    b.Property<string>("GameAlternateKey")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("ChangeDate");

                    b.Property<DateTime?>("DeleteDate");

                    b.Property<DateTime>("GameDate");

                    b.Property<int>("GameId");

                    b.Property<string>("LeagueAlternateKey");

                    b.HasKey("GameAlternateKey");

                    b.HasIndex("LeagueAlternateKey");

                    b.ToTable("Games");
                });

            modelBuilder.Entity("Dartball.Domain.GameInning", b =>
                {
                    b.Property<string>("GameAlternateKey");

                    b.Property<int>("InningNumber");

                    b.Property<DateTime>("ChangeDate");

                    b.Property<DateTime?>("DeleteDate");

                    b.Property<string>("GameInningAlternateKey");

                    b.Property<int>("GameInningId");

                    b.HasKey("GameAlternateKey", "InningNumber");

                    b.ToTable("GameInnings");
                });

            modelBuilder.Entity("Dartball.Domain.GameInningTeam", b =>
                {
                    b.Property<string>("GameInningAlternateKey");

                    b.Property<string>("GameTeamAlternateKey");

                    b.Property<DateTime>("ChangeDate");

                    b.Property<DateTime?>("DeleteDate");

                    b.Property<string>("GameInningGameAlternateKey");

                    b.Property<int?>("GameInningInningNumber");

                    b.Property<string>("GameInningTeamAlternateKey");

                    b.Property<int>("GameInningTeamId");

                    b.Property<string>("GameTeamGameAlternateKey");

                    b.Property<string>("GameTeamTeamAlternateKey");

                    b.Property<int>("IsRunnerOnFirst");

                    b.Property<int>("IsRunnerOnSecond");

                    b.Property<int>("IsRunnerOnThird");

                    b.Property<int>("Outs");

                    b.Property<int>("Score");

                    b.HasKey("GameInningAlternateKey", "GameTeamAlternateKey");

                    b.HasIndex("GameInningGameAlternateKey", "GameInningInningNumber");

                    b.HasIndex("GameTeamGameAlternateKey", "GameTeamTeamAlternateKey");

                    b.ToTable("GameInningTeams");
                });

            modelBuilder.Entity("Dartball.Domain.GameInningTeamBatter", b =>
                {
                    b.Property<string>("GameInningTeamAlternateKey");

                    b.Property<int>("Sequence");

                    b.Property<DateTime>("ChangeDate");

                    b.Property<DateTime?>("DeleteDate");

                    b.Property<int>("EventType");

                    b.Property<string>("GameInningTeamBatterAlternateKey");

                    b.Property<int>("GameInningTeamBatterId");

                    b.Property<string>("GameInningTeamGameInningAlternateKey");

                    b.Property<string>("GameInningTeamGameTeamAlternateKey");

                    b.Property<string>("PlayerAlternateKey");

                    b.Property<int>("RBIs");

                    b.Property<int?>("TargetEventType");

                    b.HasKey("GameInningTeamAlternateKey", "Sequence");

                    b.HasIndex("PlayerAlternateKey");

                    b.HasIndex("GameInningTeamGameInningAlternateKey", "GameInningTeamGameTeamAlternateKey");

                    b.ToTable("GameInningTeamBatters");
                });

            modelBuilder.Entity("Dartball.Domain.GameTeam", b =>
                {
                    b.Property<string>("GameAlternateKey");

                    b.Property<string>("TeamAlternateKey");

                    b.Property<DateTime>("ChangeDate");

                    b.Property<DateTime?>("DeleteDate");

                    b.Property<string>("GameTeamAlternateKey");

                    b.Property<int>("GameTeamId");

                    b.HasKey("GameAlternateKey", "TeamAlternateKey");

                    b.HasIndex("TeamAlternateKey");

                    b.ToTable("GameTeams");
                });

            modelBuilder.Entity("Dartball.Domain.League", b =>
                {
                    b.Property<string>("LeagueAlternateKey")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("ChangeDate");

                    b.Property<DateTime?>("DeleteDate");

                    b.Property<int>("LeagueId");

                    b.Property<string>("Name");

                    b.Property<string>("Password");

                    b.HasKey("LeagueAlternateKey");

                    b.ToTable("Leagues");
                });

            modelBuilder.Entity("Dartball.Domain.Player", b =>
                {
                    b.Property<string>("PlayerAlternateKey")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("ChangeDate");

                    b.Property<DateTime?>("DeleteDate");

                    b.Property<string>("EmailAddress");

                    b.Property<string>("Name");

                    b.Property<string>("Password");

                    b.Property<byte[]>("Photo");

                    b.Property<int>("PlayerId");

                    b.Property<int>("ShouldSync");

                    b.Property<string>("UserName");

                    b.HasKey("PlayerAlternateKey");

                    b.ToTable("Players");
                });

            modelBuilder.Entity("Dartball.Domain.PlayerTeam", b =>
                {
                    b.Property<string>("PlayerAlternateKey");

                    b.Property<string>("TeamAlternateKey");

                    b.Property<DateTime>("ChangeDate");

                    b.Property<DateTime?>("DeleteDate");

                    b.Property<string>("PlayerTeamAlternateKey");

                    b.Property<int>("PlayerTeamId");

                    b.HasKey("PlayerAlternateKey", "TeamAlternateKey");

                    b.HasIndex("TeamAlternateKey");

                    b.ToTable("PlayerTeams");
                });

            modelBuilder.Entity("Dartball.Domain.Team", b =>
                {
                    b.Property<string>("TeamAlternateKey")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("ChangeDate");

                    b.Property<DateTime?>("DeleteDate");

                    b.Property<int?>("Handicap");

                    b.Property<string>("LeagueAlternateKey");

                    b.Property<string>("Name");

                    b.Property<string>("Password");

                    b.Property<int>("ShouldSync");

                    b.Property<int>("TeamId");

                    b.HasKey("TeamAlternateKey");

                    b.HasIndex("LeagueAlternateKey");

                    b.ToTable("Teams");
                });

            modelBuilder.Entity("Dartball.Domain.TeamPlayerLineup", b =>
                {
                    b.Property<string>("TeamAlternateKey");

                    b.Property<string>("PlayerAlternateKey");

                    b.Property<int>("BattingOrder");

                    b.Property<DateTime>("ChangeDate");

                    b.Property<DateTime?>("DeleteDate");

                    b.Property<string>("TeamPlayerLineupAlternateKey");

                    b.Property<int>("TeamPlayerLineupId");

                    b.HasKey("TeamAlternateKey", "PlayerAlternateKey");

                    b.HasIndex("PlayerAlternateKey");

                    b.ToTable("TeamPlayerLineups");
                });

            modelBuilder.Entity("Dartball.Domain.Game", b =>
                {
                    b.HasOne("Dartball.Domain.League", "League")
                        .WithMany("Games")
                        .HasForeignKey("LeagueAlternateKey");
                });

            modelBuilder.Entity("Dartball.Domain.GameInning", b =>
                {
                    b.HasOne("Dartball.Domain.Game", "Game")
                        .WithMany("GameInnings")
                        .HasForeignKey("GameAlternateKey")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Dartball.Domain.GameInningTeam", b =>
                {
                    b.HasOne("Dartball.Domain.GameInning", "GameInning")
                        .WithMany("GameInningTeams")
                        .HasForeignKey("GameInningGameAlternateKey", "GameInningInningNumber");

                    b.HasOne("Dartball.Domain.GameTeam", "GameTeam")
                        .WithMany()
                        .HasForeignKey("GameTeamGameAlternateKey", "GameTeamTeamAlternateKey");
                });

            modelBuilder.Entity("Dartball.Domain.GameInningTeamBatter", b =>
                {
                    b.HasOne("Dartball.Domain.Player", "Player")
                        .WithMany()
                        .HasForeignKey("PlayerAlternateKey");

                    b.HasOne("Dartball.Domain.GameInningTeam", "GameInningTeam")
                        .WithMany("GameInningTeamBatters")
                        .HasForeignKey("GameInningTeamGameInningAlternateKey", "GameInningTeamGameTeamAlternateKey");
                });

            modelBuilder.Entity("Dartball.Domain.GameTeam", b =>
                {
                    b.HasOne("Dartball.Domain.Game", "Game")
                        .WithMany()
                        .HasForeignKey("GameAlternateKey")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Dartball.Domain.Team", "Team")
                        .WithMany()
                        .HasForeignKey("TeamAlternateKey")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Dartball.Domain.PlayerTeam", b =>
                {
                    b.HasOne("Dartball.Domain.Player", "Player")
                        .WithMany("PlayerTeams")
                        .HasForeignKey("PlayerAlternateKey")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Dartball.Domain.Team", "Team")
                        .WithMany("PlayerTeams")
                        .HasForeignKey("TeamAlternateKey")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Dartball.Domain.Team", b =>
                {
                    b.HasOne("Dartball.Domain.League", "League")
                        .WithMany("Teams")
                        .HasForeignKey("LeagueAlternateKey");
                });

            modelBuilder.Entity("Dartball.Domain.TeamPlayerLineup", b =>
                {
                    b.HasOne("Dartball.Domain.Player", "Player")
                        .WithMany("TeamPlayerLineups")
                        .HasForeignKey("PlayerAlternateKey")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Dartball.Domain.Team", "Team")
                        .WithMany("TeamPlayerLineups")
                        .HasForeignKey("TeamAlternateKey")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
