using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Manatalol.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateCreateByColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CreatedById",
                table: "Notes",
                newName: "CreatedBy");

            migrationBuilder.RenameColumn(
                name: "CreatedById",
                table: "Candidates",
                newName: "CreatedBy");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                table: "Notes",
                newName: "CreatedById");

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                table: "Candidates",
                newName: "CreatedById");
        }
    }
}
