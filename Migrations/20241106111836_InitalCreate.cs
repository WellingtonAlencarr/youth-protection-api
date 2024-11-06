﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YouthProtectionApi.Migrations
{
    /// <inheritdoc />
    public partial class InitalCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TB_USER",
                columns: table => new
                {
                    UserId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FictionalName = table.Column<string>(type: "Varchar(200)", maxLength: 200, nullable: true),
                    Email = table.Column<string>(type: "Varchar(200)", maxLength: 200, nullable: true),
                    PasswordHash = table.Column<string>(type: "Varchar(200)", maxLength: 200, nullable: true),
                    CellPhone = table.Column<string>(type: "Varchar(200)", maxLength: 200, nullable: true),
                    BirthDate = table.Column<string>(type: "Varchar(200)", maxLength: 200, nullable: true),
                    Uf = table.Column<string>(type: "Varchar(200)", maxLength: 200, nullable: true),
                    City = table.Column<string>(type: "Varchar(200)", maxLength: 200, nullable: true),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserStatus = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_USER", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "TB_PUBLICATION",
                columns: table => new
                {
                    PublicationId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    PublicationContent = table.Column<string>(type: "Varchar(200)", maxLength: 200, nullable: true),
                    PublicationsRole = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PublicationStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModificationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_PUBLICATION", x => x.PublicationId);
                    table.ForeignKey(
                        name: "FK_TB_PUBLICATION_TB_USER_UserId",
                        column: x => x.UserId,
                        principalTable: "TB_USER",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "TB_ATTENDANCES",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VolunteerId = table.Column<long>(type: "bigint", nullable: false),
                    PublicationId = table.Column<long>(type: "bigint", nullable: false),
                    StartedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsCompleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_ATTENDANCES", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TB_ATTENDANCES_TB_PUBLICATION_PublicationId",
                        column: x => x.PublicationId,
                        principalTable: "TB_PUBLICATION",
                        principalColumn: "PublicationId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TB_ATTENDANCES_TB_USER_VolunteerId",
                        column: x => x.VolunteerId,
                        principalTable: "TB_USER",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TB_CHAT",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AttendanceId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_CHAT", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TB_CHAT_TB_ATTENDANCES_AttendanceId",
                        column: x => x.AttendanceId,
                        principalTable: "TB_ATTENDANCES",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TB_MESSAGES",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ChatId = table.Column<long>(type: "bigint", nullable: false),
                    SenderId = table.Column<long>(type: "bigint", nullable: false),
                    Content = table.Column<string>(type: "Varchar(200)", maxLength: 200, nullable: true),
                    SentAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_MESSAGES", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TB_MESSAGES_TB_CHAT_ChatId",
                        column: x => x.ChatId,
                        principalTable: "TB_CHAT",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TB_MESSAGES_TB_USER_SenderId",
                        column: x => x.SenderId,
                        principalTable: "TB_USER",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TB_ATTENDANCES_PublicationId",
                table: "TB_ATTENDANCES",
                column: "PublicationId");

            migrationBuilder.CreateIndex(
                name: "IX_TB_ATTENDANCES_VolunteerId",
                table: "TB_ATTENDANCES",
                column: "VolunteerId");

            migrationBuilder.CreateIndex(
                name: "IX_TB_CHAT_AttendanceId",
                table: "TB_CHAT",
                column: "AttendanceId");

            migrationBuilder.CreateIndex(
                name: "IX_TB_MESSAGES_ChatId",
                table: "TB_MESSAGES",
                column: "ChatId");

            migrationBuilder.CreateIndex(
                name: "IX_TB_MESSAGES_SenderId",
                table: "TB_MESSAGES",
                column: "SenderId");

            migrationBuilder.CreateIndex(
                name: "IX_TB_PUBLICATION_UserId",
                table: "TB_PUBLICATION",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TB_MESSAGES");

            migrationBuilder.DropTable(
                name: "TB_CHAT");

            migrationBuilder.DropTable(
                name: "TB_ATTENDANCES");

            migrationBuilder.DropTable(
                name: "TB_PUBLICATION");

            migrationBuilder.DropTable(
                name: "TB_USER");
        }
    }
}
