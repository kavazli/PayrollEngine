using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PayrollEngine.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class EmployeeScenarioDisabilityDegreeToEnum : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeScenarios_DisabilityDegrees_DisabilityDegreeId",
                table: "EmployeeScenarios");

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
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DisabilityDegree",
                table: "EmployeeScenarios");

            migrationBuilder.AddColumn<Guid>(
                name: "DisabilityDegreeId",
                table: "EmployeeScenarios",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeScenarios_DisabilityDegreeId",
                table: "EmployeeScenarios",
                column: "DisabilityDegreeId");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeScenarios_DisabilityDegrees_DisabilityDegreeId",
                table: "EmployeeScenarios",
                column: "DisabilityDegreeId",
                principalTable: "DisabilityDegrees",
                principalColumn: "Id");
        }
    }
}
