﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
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
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("YouthProtection.Models.MessageModel", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<long>("ChatId")
                        .HasColumnType("bigint");

                    b.Property<string>("Content")
                        .HasMaxLength(200)
                        .HasColumnType("Varchar");

                    b.Property<long>("SenderId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("SentAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.HasKey("Id");

                    b.HasIndex("ChatId");

                    b.HasIndex("SenderId");

                    b.ToTable("TB_MESSAGES", (string)null);
                });

            modelBuilder.Entity("YouthProtection.Models.PublicationsModel", b =>
                {
                    b.Property<long>("PublicationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("PublicationId"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ModificationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("PublicationContent")
                        .HasMaxLength(200)
                        .HasColumnType("Varchar");

                    b.Property<string>("PublicationStatus")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PublicationsRole")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("UserId")
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

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("UserId"));

                    b.Property<string>("BirthDate")
                        .HasMaxLength(200)
                        .HasColumnType("Varchar");

                    b.Property<string>("CellPhone")
                        .HasMaxLength(200)
                        .HasColumnType("Varchar");

                    b.Property<string>("City")
                        .HasMaxLength(200)
                        .HasColumnType("Varchar");

                    b.Property<string>("Email")
                        .HasMaxLength(200)
                        .HasColumnType("Varchar");

                    b.Property<string>("FictionalName")
                        .HasMaxLength(200)
                        .HasColumnType("Varchar");

                    b.Property<string>("PasswordHash")
                        .HasMaxLength(200)
                        .HasColumnType("Varchar");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Uf")
                        .HasMaxLength(200)
                        .HasColumnType("Varchar");

                    b.Property<string>("UserStatus")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.ToTable("TB_USER", (string)null);
                });

            modelBuilder.Entity("YouthProtectionApi.Models.AttendanceModel", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<bool>("IsCompleted")
                        .HasColumnType("bit");

                    b.Property<long>("PublicationId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("StartedAt")
                        .HasColumnType("datetime2");

                    b.Property<long>("VolunteerId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("PublicationId");

                    b.HasIndex("VolunteerId");

                    b.ToTable("TB_ATTENDANCES", (string)null);
                });

            modelBuilder.Entity("YouthProtectionApi.Models.ChatModel", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<long>("AttendanceId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("AttendanceId");

                    b.ToTable("TB_CHAT", (string)null);
                });

            modelBuilder.Entity("YouthProtection.Models.MessageModel", b =>
                {
                    b.HasOne("YouthProtectionApi.Models.ChatModel", "Chat")
                        .WithMany("Messages")
                        .HasForeignKey("ChatId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("YouthProtection.Models.UserModel", "Sender")
                        .WithMany("Messages")
                        .HasForeignKey("SenderId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Chat");

                    b.Navigation("Sender");
                });

            modelBuilder.Entity("YouthProtection.Models.PublicationsModel", b =>
                {
                    b.HasOne("YouthProtection.Models.UserModel", "UserModel")
                        .WithMany("Publications")
                        .HasForeignKey("UserId");

                    b.Navigation("UserModel");
                });

            modelBuilder.Entity("YouthProtectionApi.Models.AttendanceModel", b =>
                {
                    b.HasOne("YouthProtection.Models.PublicationsModel", "Publication")
                        .WithMany()
                        .HasForeignKey("PublicationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("YouthProtection.Models.UserModel", "Volunteer")
                        .WithMany()
                        .HasForeignKey("VolunteerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Publication");

                    b.Navigation("Volunteer");
                });

            modelBuilder.Entity("YouthProtectionApi.Models.ChatModel", b =>
                {
                    b.HasOne("YouthProtectionApi.Models.AttendanceModel", "Attendance")
                        .WithMany()
                        .HasForeignKey("AttendanceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Attendance");
                });

            modelBuilder.Entity("YouthProtection.Models.UserModel", b =>
                {
                    b.Navigation("Messages");

                    b.Navigation("Publications");
                });

            modelBuilder.Entity("YouthProtectionApi.Models.ChatModel", b =>
                {
                    b.Navigation("Messages");
                });
#pragma warning restore 612, 618
        }
    }
}
