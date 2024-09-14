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
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FictionalName = table.Column<string>(type: "Varchar", maxLength: 200, nullable: false),
                    Email = table.Column<string>(type: "Varchar", maxLength: 200, nullable: false),
                    PasswordHash = table.Column<string>(type: "Varchar", maxLength: 200, nullable: false),
                    CellPhone = table.Column<string>(type: "Varchar", maxLength: 200, nullable: false),
                    BirthDate = table.Column<string>(type: "Varchar", maxLength: 200, nullable: false),
                    uf = table.Column<string>(type: "Varchar", maxLength: 200, nullable: false),
                    city = table.Column<string>(type: "Varchar", maxLength: 200, nullable: false),
                    Role = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_USER", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TB_USER");
        }
    }
}
