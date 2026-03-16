using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PayrollEngine.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RenameEmployerContributionColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EmployeeUIContributionAmount",
                table: "EmployerContributions",
                newName: "EmployerUIContributionAmount");

            migrationBuilder.RenameColumn(
                name: "EmployeeSSContributionAmount",
                table: "EmployerContributions",
                newName: "EmployerSSContributionAmount");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EmployerUIContributionAmount",
                table: "EmployerContributions",
                newName: "EmployeeUIContributionAmount");

            migrationBuilder.RenameColumn(
                name: "EmployerSSContributionAmount",
                table: "EmployerContributions",
                newName: "EmployeeSSContributionAmount");
        }
    }
}
