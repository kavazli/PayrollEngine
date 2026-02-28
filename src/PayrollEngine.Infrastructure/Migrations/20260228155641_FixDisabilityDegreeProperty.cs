using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PayrollEngine.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixDisabilityDegreeProperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DisabilityDegrees_DisabilityDegrees_DegreeId",
                table: "DisabilityDegrees");

            migrationBuilder.DropIndex(
                name: "IX_DisabilityDegrees_DegreeId",
                table: "DisabilityDegrees");

            migrationBuilder.DropColumn(
                name: "DegreeId",
                table: "DisabilityDegrees");

            migrationBuilder.AddColumn<int>(
                name: "Degree",
                table: "DisabilityDegrees",
                type: "INTEGER",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Degree",
                table: "DisabilityDegrees");

            migrationBuilder.AddColumn<Guid>(
                name: "DegreeId",
                table: "DisabilityDegrees",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_DisabilityDegrees_DegreeId",
                table: "DisabilityDegrees",
                column: "DegreeId");

            migrationBuilder.AddForeignKey(
                name: "FK_DisabilityDegrees_DisabilityDegrees_DegreeId",
                table: "DisabilityDegrees",
                column: "DegreeId",
                principalTable: "DisabilityDegrees",
                principalColumn: "Id");
        }
    }
}
