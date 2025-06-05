using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Clubify.Migrations
{
    /// <inheritdoc />
    public partial class CreateLicenseTiersTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LicenseTiers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    MemberCap = table.Column<int>(type: "int", nullable: false),
                    MonthlyPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AnnualPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LicenseTiers", x => x.Id);
                });

            migrationBuilder.Sql(
        "INSERT INTO LicenseTiers (Name, MemberCap, MonthlyPrice, AnnualPrice, Notes) " +
        "VALUES ('Default Tier', 5, 0, 0, 'Seeded default tier')");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LicenseTiers");
        }
    }
}
