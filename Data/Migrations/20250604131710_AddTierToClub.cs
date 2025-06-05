using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Clubify.Migrations
{
    /// <inheritdoc />
    public partial class AddTierToClub : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TierName",
                table: "Clubs");

            migrationBuilder.AddColumn<int>(
                name: "LicenseTierId",
                table: "Clubs",
                type: "int",
                nullable: false,
                defaultValue: 1); 

            migrationBuilder.CreateIndex(
                name: "IX_Clubs_LicenseTierId",
                table: "Clubs",
                column: "LicenseTierId");

            migrationBuilder.AddForeignKey(
                name: "FK_Clubs_LicenseTiers_LicenseTierId",
                table: "Clubs",
                column: "LicenseTierId",
                principalTable: "LicenseTiers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clubs_LicenseTiers_LicenseTierId",
                table: "Clubs");

            migrationBuilder.DropIndex(
                name: "IX_Clubs_LicenseTierId",
                table: "Clubs");

            migrationBuilder.DropColumn(
                name: "LicenseTierId",
                table: "Clubs");

            migrationBuilder.AddColumn<string>(
                name: "TierName",
                table: "Clubs",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }
    }
}
