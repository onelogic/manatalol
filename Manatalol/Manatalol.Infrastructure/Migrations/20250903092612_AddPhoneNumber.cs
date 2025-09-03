using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Manatalol.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddPhoneNumber : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsCurrent",
                table: "Experiences",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Candidates",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCurrent",
                table: "Experiences");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Candidates");
        }
    }
}
