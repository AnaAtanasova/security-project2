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
    [Migration("20211106005931_PasswordHashing")]
    partial class PasswordHashing
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.11");

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

                    b.Property<byte[]>("Salt")
                        .HasColumnType("BLOB");

                    b.Property<string>("Username")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Password = "Dya06vXfuz2DayaSWeglgAat/0aJDXgkJFJARW6jZjU=",
                            Role = "admin",
                            Salt = new byte[] { 227, 238, 51, 97, 0, 69, 54, 50, 37, 26, 238, 64, 247, 35, 2, 166 },
                            Username = "Aurimas"
                        },
                        new
                        {
                            Id = 2,
                            Password = "4l7KgywR6+ByZR9m6hRQr2MqQsm/dS0O+hF4AyMOU10=",
                            Role = "user",
                            Salt = new byte[] { 99, 130, 157, 132, 250, 198, 116, 101, 51, 59, 179, 45, 38, 24, 90, 17 },
                            Username = "Ana"
                        });
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
                    b.Navigation("Items");
                });
#pragma warning restore 612, 618
        }
    }
}
