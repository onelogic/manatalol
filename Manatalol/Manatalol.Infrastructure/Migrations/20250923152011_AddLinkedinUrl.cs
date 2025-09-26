using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Manatalol.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddLinkedinUrl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LinkedinUrl",
                table: "Candidates",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Candidates_LinkedinUrl",
                table: "Candidates",
                column: "LinkedinUrl",
                unique: true,
                filter: "[LinkedinUrl] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Candidates_LinkedinUrl",
                table: "Candidates");

            migrationBuilder.DropColumn(
                name: "LinkedinUrl",
                table: "Candidates");
        }
    }
}
