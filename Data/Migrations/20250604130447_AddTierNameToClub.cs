using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Clubify.Migrations
{
    /// <inheritdoc />
    public partial class AddTierNameToClub : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TierName",
                table: "Clubs",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TierName",
                table: "Clubs");
        }
    }
}
