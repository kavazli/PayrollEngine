using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PayrollEngine.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddNewTablesAndRemoveColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // SQLite için transaction'ı devre dışı bırak
            migrationBuilder.Sql("PRAGMA foreign_keys = 0;", suppressTransaction: true);
            migrationBuilder.DropColumn(
                name: "EmployerSSContributionAmount",
                table: "ResultPayrolls");

            migrationBuilder.DropColumn(
                name: "EmployerUIContributionAmount",
                table: "ResultPayrolls");

            migrationBuilder.DropColumn(
                name: "IncentiveDiscount",
                table: "ResultPayrolls");

            migrationBuilder.DropColumn(
                name: "PrivateHealthInsurance",
                table: "ResultPayrolls");

            migrationBuilder.DropColumn(
                name: "ShoppingVoucher",
                table: "ResultPayrolls");

            migrationBuilder.DropColumn(
                name: "TotalEmployerCost",
                table: "ResultPayrolls");

            migrationBuilder.DropColumn(
                name: "PrivateHealthInsurance",
                table: "PayrollMonths");

            migrationBuilder.DropColumn(
                name: "ShoppingVoucher",
                table: "PayrollMonths");

            migrationBuilder.CreateTable(
                name: "EmployerContributions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Month = table.Column<int>(type: "INTEGER", nullable: false),
                    EmployeeSSContributionAmount = table.Column<decimal>(type: "TEXT", nullable: false),
                    EmployeeUIContributionAmount = table.Column<decimal>(type: "TEXT", nullable: false),
                    TotalEmployerCost = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployerContributions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PrivateHealthInsurances",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Month = table.Column<int>(type: "INTEGER", nullable: false),
                    NetAmount = table.Column<decimal>(type: "TEXT", nullable: false),
                    GrossAmount = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrivateHealthInsurances", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ShoppingVouchers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Month = table.Column<int>(type: "INTEGER", nullable: false),
                    NetAmount = table.Column<decimal>(type: "TEXT", nullable: false),
                    GrossAmount = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShoppingVouchers", x => x.Id);
                });
            
            // SQLite için foreign key'leri tekrar aç
            migrationBuilder.Sql("PRAGMA foreign_keys = 1;", suppressTransaction: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployerContributions");

            migrationBuilder.DropTable(
                name: "PrivateHealthInsurances");

            migrationBuilder.DropTable(
                name: "ShoppingVouchers");

            migrationBuilder.AddColumn<decimal>(
                name: "EmployerSSContributionAmount",
                table: "ResultPayrolls",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "EmployerUIContributionAmount",
                table: "ResultPayrolls",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "IncentiveDiscount",
                table: "ResultPayrolls",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "PrivateHealthInsurance",
                table: "ResultPayrolls",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "ShoppingVoucher",
                table: "ResultPayrolls",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalEmployerCost",
                table: "ResultPayrolls",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "PrivateHealthInsurance",
                table: "PayrollMonths",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "ShoppingVoucher",
                table: "PayrollMonths",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
