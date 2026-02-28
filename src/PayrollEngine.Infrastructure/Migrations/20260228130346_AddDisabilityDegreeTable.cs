using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PayrollEngine.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddDisabilityDegreeTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DisabilityDegree",
                table: "EmployeeScenarios");

            migrationBuilder.AddColumn<Guid>(
                name: "DisabilityDegreeId",
                table: "EmployeeScenarios",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "DisabilityDegrees",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Year = table.Column<int>(type: "INTEGER", nullable: false),
                    DegreeId = table.Column<Guid>(type: "TEXT", nullable: true),
                    Amount = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DisabilityDegrees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DisabilityDegrees_DisabilityDegrees_DegreeId",
                        column: x => x.DegreeId,
                        principalTable: "DisabilityDegrees",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeScenarios_DisabilityDegreeId",
                table: "EmployeeScenarios",
                column: "DisabilityDegreeId");

            migrationBuilder.CreateIndex(
                name: "IX_DisabilityDegrees_DegreeId",
                table: "DisabilityDegrees",
                column: "DegreeId");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeScenarios_DisabilityDegrees_DisabilityDegreeId",
                table: "EmployeeScenarios",
                column: "DisabilityDegreeId",
                principalTable: "DisabilityDegrees",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeScenarios_DisabilityDegrees_DisabilityDegreeId",
                table: "EmployeeScenarios");

            migrationBuilder.DropTable(
                name: "DisabilityDegrees");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeScenarios_DisabilityDegreeId",
                table: "EmployeeScenarios");

            migrationBuilder.DropColumn(
                name: "DisabilityDegreeId",
                table: "EmployeeScenarios");

            migrationBuilder.AddColumn<int>(
                name: "DisabilityDegree",
                table: "EmployeeScenarios",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }
    }
}
