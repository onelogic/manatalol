using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Manatalol.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddSkillAndEductionTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AddedBy",
                table: "Candidates");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Candidates",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "Education",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    School = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Degree = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CandidateId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Education", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Education_Candidates_CandidateId",
                        column: x => x.CandidateId,
                        principalTable: "Candidates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Skill",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CandidateId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Skill", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Skill_Candidates_CandidateId",
                        column: x => x.CandidateId,
                        principalTable: "Candidates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Education_CandidateId",
                table: "Education",
                column: "CandidateId");

            migrationBuilder.CreateIndex(
                name: "IX_Skill_CandidateId",
                table: "Skill",
                column: "CandidateId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Education");

            migrationBuilder.DropTable(
                name: "Skill");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Candidates");

            migrationBuilder.AddColumn<string>(
                name: "AddedBy",
                table: "Candidates",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
