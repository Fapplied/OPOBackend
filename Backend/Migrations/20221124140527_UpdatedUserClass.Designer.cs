﻿// <auto-generated />
using System;
using Backend.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Backend.Migrations
{
    [DbContext(typeof(OPODB))]
    [Migration("20221124140527_UpdatedUserClass")]
    partial class UpdatedUserClass
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Backend.Models.Con", b =>
                {
                    b.Property<int>("ConId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ConId"), 1L, 1);

                    b.Property<int>("ProblemId")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ConId");

                    b.HasIndex("ProblemId");

                    b.ToTable("Con");
                });

            modelBuilder.Entity("Backend.Models.Like", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("ConId")
                        .HasColumnType("int");

                    b.Property<int?>("ProId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ConId");

                    b.HasIndex("ProId");

                    b.ToTable("Likes");
                });

            modelBuilder.Entity("Backend.Models.Pro", b =>
                {
                    b.Property<int>("ProId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProId"), 1L, 1);

                    b.Property<int>("ProblemId")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ProId");

                    b.HasIndex("ProblemId");

                    b.ToTable("Pro");
                });

            modelBuilder.Entity("Backend.Models.Problem", b =>
                {
                    b.Property<int>("ProblemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProblemId"), 1L, 1);

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("ProblemId");

                    b.HasIndex("UserId");

                    b.ToTable("Problem");
                });

            modelBuilder.Entity("Backend.Models.ProfilePicture", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ProfilePicture");
                });

            modelBuilder.Entity("Backend.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"), 1L, 1);

                    b.Property<int>("GoogleId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ProfilePictureId")
                        .HasColumnType("int");

                    b.HasKey("UserId");

                    b.HasIndex("ProfilePictureId");

                    b.ToTable("User");
                });

            modelBuilder.Entity("Backend.Models.Con", b =>
                {
                    b.HasOne("Backend.Models.Problem", "Problem")
                        .WithMany("ConList")
                        .HasForeignKey("ProblemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Problem");
                });

            modelBuilder.Entity("Backend.Models.Like", b =>
                {
                    b.HasOne("Backend.Models.Con", null)
                        .WithMany("LikesList")
                        .HasForeignKey("ConId");

                    b.HasOne("Backend.Models.Pro", null)
                        .WithMany("LikesList")
                        .HasForeignKey("ProId");
                });

            modelBuilder.Entity("Backend.Models.Pro", b =>
                {
                    b.HasOne("Backend.Models.Problem", "Problem")
                        .WithMany("ProList")
                        .HasForeignKey("ProblemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Problem");
                });

            modelBuilder.Entity("Backend.Models.Problem", b =>
                {
                    b.HasOne("Backend.Models.User", "User")
                        .WithMany("ProblemList")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Backend.Models.User", b =>
                {
                    b.HasOne("Backend.Models.ProfilePicture", "ProfilePicture")
                        .WithMany()
                        .HasForeignKey("ProfilePictureId");

                    b.Navigation("ProfilePicture");
                });

            modelBuilder.Entity("Backend.Models.Con", b =>
                {
                    b.Navigation("LikesList");
                });

            modelBuilder.Entity("Backend.Models.Pro", b =>
                {
                    b.Navigation("LikesList");
                });

            modelBuilder.Entity("Backend.Models.Problem", b =>
                {
                    b.Navigation("ConList");

                    b.Navigation("ProList");
                });

            modelBuilder.Entity("Backend.Models.User", b =>
                {
                    b.Navigation("ProblemList");
                });
#pragma warning restore 612, 618
        }
    }
}
