﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using to_do_backend;

namespace to_do_backend.Migrations
{
    [DbContext(typeof(BackendContext))]
    [Migration("20211226142319_chapChanges")]
    partial class chapChanges
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.11");

            modelBuilder.Entity("to_do_backend.Models.Challenge", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<byte[]>("ChallengeValue")
                        .HasColumnType("BLOB");

                    b.Property<bool>("IsFailed")
                        .HasColumnType("INTEGER");

                    b.Property<int>("IterationCount")
                        .HasColumnType("INTEGER");

                    b.Property<int>("UserId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("secret")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Challenges");
                });

            modelBuilder.Entity("to_do_backend.Models.ToDoItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsDone")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Title")
                        .HasColumnType("TEXT");

                    b.Property<int>("UserId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Items");
                });

            modelBuilder.Entity("to_do_backend.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Password")
                        .HasColumnType("TEXT");

                    b.Property<string>("Role")
                        .HasColumnType("TEXT");

                    b.Property<string>("Username")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Password = "password",
                            Role = "admin",
                            Username = "Aurimas"
                        },
                        new
                        {
                            Id = 2,
                            Password = "password",
                            Role = "user",
                            Username = "Ana"
                        });
                });

            modelBuilder.Entity("to_do_backend.Models.Challenge", b =>
                {
                    b.HasOne("to_do_backend.User", "User")
                        .WithMany("Challenges")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("to_do_backend.Models.ToDoItem", b =>
                {
                    b.HasOne("to_do_backend.User", "User")
                        .WithMany("Items")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("to_do_backend.User", b =>
                {
                    b.Navigation("Challenges");

                    b.Navigation("Items");
                });
#pragma warning restore 612, 618
        }
    }
}
