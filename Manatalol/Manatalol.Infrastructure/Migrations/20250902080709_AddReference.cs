using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Manatalol.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddReference : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Reference",
                table: "Candidates",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Candidates_Reference",
                table: "Candidates",
                column: "Reference",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Candidates_Reference",
                table: "Candidates");

            migrationBuilder.DropColumn(
                name: "Reference",
                table: "Candidates");
        }
    }
}
