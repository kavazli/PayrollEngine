using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PayrollEngine.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ActiveSSParams",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Year = table.Column<decimal>(type: "TEXT", nullable: false),
                    EmployeeSSRate = table.Column<decimal>(type: "TEXT", nullable: false),
                    EmployeeUIRate = table.Column<decimal>(type: "TEXT", nullable: false),
                    EmployerSSRate = table.Column<decimal>(type: "TEXT", nullable: false),
                    EmployerUIRate = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActiveSSParams", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CumulativeIncomeTaxBases",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Month = table.Column<decimal>(type: "TEXT", nullable: false),
                    CumulativeBase = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CumulativeIncomeTaxBases", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeScenarios",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    SalaryInputType = table.Column<int>(type: "INTEGER", nullable: false),
                    Status = table.Column<int>(type: "INTEGER", nullable: false),
                    DisabilityDegree = table.Column<int>(type: "INTEGER", nullable: false),
                    PayType = table.Column<int>(type: "INTEGER", nullable: false),
                    IncentiveType = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeScenarios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IncomeTaxBrackets",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Year = table.Column<decimal>(type: "TEXT", nullable: false),
                    Bracket = table.Column<decimal>(type: "TEXT", nullable: false),
                    MinAmount = table.Column<decimal>(type: "TEXT", nullable: false),
                    MaxAmount = table.Column<decimal>(type: "TEXT", nullable: false),
                    Rate = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IncomeTaxBrackets", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MinimumWages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Year = table.Column<decimal>(type: "TEXT", nullable: false),
                    GrossAmount = table.Column<decimal>(type: "TEXT", nullable: false),
                    NetAmount = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MinimumWages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PayrollMonths",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Month = table.Column<int>(type: "INTEGER", nullable: false),
                    WorkDay = table.Column<decimal>(type: "TEXT", nullable: false),
                    BaseSalary = table.Column<decimal>(type: "TEXT", nullable: false),
                    Overtime50 = table.Column<decimal>(type: "TEXT", nullable: false),
                    Overtime100 = table.Column<decimal>(type: "TEXT", nullable: false),
                    BonusAmount = table.Column<decimal>(type: "TEXT", nullable: false),
                    GrossSalary = table.Column<decimal>(type: "TEXT", nullable: false),
                    PrivateHealthInsurance = table.Column<decimal>(type: "TEXT", nullable: false),
                    ShoppingVoucher = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PayrollMonths", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PayrollTemplateMonths",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Month = table.Column<int>(type: "INTEGER", nullable: false),
                    WorkDay = table.Column<decimal>(type: "TEXT", nullable: false),
                    BaseSalary = table.Column<decimal>(type: "TEXT", nullable: false),
                    SalaryIncreaseRate = table.Column<decimal>(type: "TEXT", nullable: false),
                    Overtime50 = table.Column<decimal>(type: "TEXT", nullable: false),
                    Overtime100 = table.Column<decimal>(type: "TEXT", nullable: false),
                    BonusAmount = table.Column<decimal>(type: "TEXT", nullable: false),
                    PrivateHealthInsurance = table.Column<decimal>(type: "TEXT", nullable: false),
                    ShoppingVoucher = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PayrollTemplateMonths", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ResultPayrolls",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Month = table.Column<int>(type: "INTEGER", nullable: false),
                    WorkDay = table.Column<decimal>(type: "TEXT", nullable: false),
                    SSDay = table.Column<decimal>(type: "TEXT", nullable: false),
                    CurrentSalary = table.Column<decimal>(type: "TEXT", nullable: false),
                    Overtime50Amount = table.Column<decimal>(type: "TEXT", nullable: false),
                    Overtime100Amount = table.Column<decimal>(type: "TEXT", nullable: false),
                    BonusAmount = table.Column<decimal>(type: "TEXT", nullable: false),
                    TotalSalary = table.Column<decimal>(type: "TEXT", nullable: false),
                    PrivateHealthInsurance = table.Column<decimal>(type: "TEXT", nullable: false),
                    ShoppingVoucher = table.Column<decimal>(type: "TEXT", nullable: false),
                    GrossSalary = table.Column<decimal>(type: "TEXT", nullable: false),
                    SSContributionBase = table.Column<decimal>(type: "TEXT", nullable: false),
                    EmployeeSSContributionAmount = table.Column<decimal>(type: "TEXT", nullable: false),
                    EmployeeUIContributionAmount = table.Column<decimal>(type: "TEXT", nullable: false),
                    CumulativeIncomeTaxBase = table.Column<decimal>(type: "TEXT", nullable: false),
                    IncomeTaxBase = table.Column<decimal>(type: "TEXT", nullable: false),
                    IncomeTax = table.Column<decimal>(type: "TEXT", nullable: false),
                    IncomeTaxExemption = table.Column<decimal>(type: "TEXT", nullable: false),
                    StampTax = table.Column<decimal>(type: "TEXT", nullable: false),
                    StampTaxExemption = table.Column<decimal>(type: "TEXT", nullable: false),
                    NetSalary = table.Column<decimal>(type: "TEXT", nullable: false),
                    EmployerSSContributionAmount = table.Column<decimal>(type: "TEXT", nullable: false),
                    EmployerUIContributionAmount = table.Column<decimal>(type: "TEXT", nullable: false),
                    IncentiveDiscount = table.Column<decimal>(type: "TEXT", nullable: false),
                    TotalEmployerCost = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResultPayrolls", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RetiredSSParams",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Year = table.Column<decimal>(type: "TEXT", nullable: false),
                    EmployeeSSRate = table.Column<decimal>(type: "TEXT", nullable: false),
                    EmployerSSRate = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RetiredSSParams", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SSCeilings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Year = table.Column<decimal>(type: "TEXT", nullable: false),
                    Ceiling = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SSCeilings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StampTaxes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Year = table.Column<decimal>(type: "TEXT", nullable: false),
                    Rate = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StampTaxes", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActiveSSParams");

            migrationBuilder.DropTable(
                name: "CumulativeIncomeTaxBases");

            migrationBuilder.DropTable(
                name: "EmployeeScenarios");

            migrationBuilder.DropTable(
                name: "IncomeTaxBrackets");

            migrationBuilder.DropTable(
                name: "MinimumWages");

            migrationBuilder.DropTable(
                name: "PayrollMonths");

            migrationBuilder.DropTable(
                name: "PayrollTemplateMonths");

            migrationBuilder.DropTable(
                name: "ResultPayrolls");

            migrationBuilder.DropTable(
                name: "RetiredSSParams");

            migrationBuilder.DropTable(
                name: "SSCeilings");

            migrationBuilder.DropTable(
                name: "StampTaxes");
        }
    }
}
