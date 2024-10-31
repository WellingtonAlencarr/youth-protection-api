﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using YouthProtectionApi.DataBase;

#nullable disable

namespace YouthProtectionApi.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("YouthProtection.Models.CommentsModel", b =>
                {
                    b.Property<long>("CommentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("CommentId"));

                    b.Property<int>("CommentStatus")
                        .HasColumnType("integer");

                    b.Property<string>("ContentComment")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("Varchar");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<long>("PublicationId")
                        .HasColumnType("bigint");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("CommentId");

                    b.HasIndex("PublicationId");

                    b.HasIndex("UserId");

                    b.ToTable("TB_COMMENT", (string)null);
                });

            modelBuilder.Entity("YouthProtection.Models.PublicationsModel", b =>
                {
                    b.Property<long>("PublicationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("PublicationId"));

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<DateTime>("ModificationDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<string>("PublicationContent")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("Varchar");

                    b.Property<string>("PublicationStatus")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PublicationsRole")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.Property<long>("idComment")
                        .HasColumnType("bigint");

                    b.HasKey("PublicationId");

                    b.HasIndex("UserId");

                    b.ToTable("TB_PUBLICATION", (string)null);
                });

            modelBuilder.Entity("YouthProtection.Models.UserModel", b =>
                {
                    b.Property<long>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("UserId"));

                    b.Property<string>("BirthDate")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("Varchar");

                    b.Property<string>("CellPhone")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("Varchar");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("Varchar");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("Varchar");

                    b.Property<string>("FictionalName")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("Varchar");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("Varchar");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Uf")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("Varchar");

                    b.Property<string>("UserStatus")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("UserId");

                    b.ToTable("TB_USER", (string)null);
                });

            modelBuilder.Entity("YouthProtection.Models.CommentsModel", b =>
                {
                    b.HasOne("YouthProtection.Models.PublicationsModel", "PublicationsModel")
                        .WithMany("Comments")
                        .HasForeignKey("PublicationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("YouthProtection.Models.UserModel", "UserModel")
                        .WithMany("Comments")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PublicationsModel");

                    b.Navigation("UserModel");
                });

            modelBuilder.Entity("YouthProtection.Models.PublicationsModel", b =>
                {
                    b.HasOne("YouthProtection.Models.UserModel", "UserModel")
                        .WithMany("Publications")
                        .HasForeignKey("UserId");

                    b.Navigation("UserModel");
                });

            modelBuilder.Entity("YouthProtection.Models.PublicationsModel", b =>
                {
                    b.Navigation("Comments");
                });

            modelBuilder.Entity("YouthProtection.Models.UserModel", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("Publications");
                });
#pragma warning restore 612, 618
        }
    }
}
