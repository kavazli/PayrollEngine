using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PayrollEngine.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddRetiredNetAmountToMinimumWage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "RetiredNetAmount",
                table: "MinimumWages",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RetiredNetAmount",
                table: "MinimumWages");
        }
    }
}
