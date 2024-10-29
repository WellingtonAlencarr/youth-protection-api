using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace YouthProtectionApi.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TB_USER",
                columns: table => new
                {
                    UserId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FictionalName = table.Column<string>(type: "Varchar", maxLength: 200, nullable: false),
                    Email = table.Column<string>(type: "Varchar", maxLength: 200, nullable: false),
                    PasswordHash = table.Column<string>(type: "Varchar", maxLength: 200, nullable: false),
                    CellPhone = table.Column<string>(type: "Varchar", maxLength: 200, nullable: false),
                    BirthDate = table.Column<string>(type: "Varchar", maxLength: 200, nullable: false),
                    Uf = table.Column<string>(type: "Varchar", maxLength: 200, nullable: false),
                    City = table.Column<string>(type: "Varchar", maxLength: 200, nullable: false),
                    Role = table.Column<string>(type: "text", nullable: false),
                    UserStatus = table.Column<string>(type: "text", nullable: false)
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
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    idComment = table.Column<long>(type: "bigint", nullable: false),
                    PublicationContent = table.Column<string>(type: "Varchar", maxLength: 200, nullable: false),
                    PublicationsRole = table.Column<string>(type: "text", nullable: false),
                    PublicationStatus = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    ModificationDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
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
                name: "TB_COMMENT",
                columns: table => new
                {
                    CommentId = table.Column<long>(type: "bigint", nullable: false),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    PublicationId = table.Column<long>(type: "bigint", nullable: false),
                    ContentComment = table.Column<string>(type: "Varchar", maxLength: 200, nullable: false),
                    CommentStatus = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_COMMENT", x => x.CommentId);
                    table.ForeignKey(
                        name: "FK_TB_COMMENT_TB_PUBLICATION_CommentId",
                        column: x => x.CommentId,
                        principalTable: "TB_PUBLICATION",
                        principalColumn: "PublicationId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TB_COMMENT_TB_USER_CommentId",
                        column: x => x.CommentId,
                        principalTable: "TB_USER",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_TB_PUBLICATION_UserId",
                table: "TB_PUBLICATION",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TB_COMMENT");

            migrationBuilder.DropTable(
                name: "TB_PUBLICATION");

            migrationBuilder.DropTable(
                name: "TB_USER");
        }
    }
}
