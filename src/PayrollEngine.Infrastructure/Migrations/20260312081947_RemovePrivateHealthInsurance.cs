using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PayrollEngine.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemovePrivateHealthInsurance : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PrivateHealthInsurances");

            migrationBuilder.DropColumn(
                name: "PrivateHealthInsurance",
                table: "PayrollTemplateMonths");

            migrationBuilder.DropColumn(
                name: "PrivateHealthInsurance",
                table: "PayrollMonths");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "PrivateHealthInsurance",
                table: "PayrollTemplateMonths",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "PrivateHealthInsurance",
                table: "PayrollMonths",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateTable(
                name: "PrivateHealthInsurances",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    GrossAmount = table.Column<decimal>(type: "TEXT", nullable: false),
                    IncomeTax = table.Column<decimal>(type: "TEXT", nullable: false),
                    Month = table.Column<int>(type: "INTEGER", nullable: false),
                    NetAmount = table.Column<decimal>(type: "TEXT", nullable: false),
                    StampTax = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrivateHealthInsurances", x => x.Id);
                });
        }
    }
}
