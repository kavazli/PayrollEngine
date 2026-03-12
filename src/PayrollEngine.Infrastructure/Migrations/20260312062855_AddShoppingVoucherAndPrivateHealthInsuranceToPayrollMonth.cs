using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PayrollEngine.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddShoppingVoucherAndPrivateHealthInsuranceToPayrollMonth : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PrivateHealthInsurance",
                table: "PayrollMonths");

            migrationBuilder.DropColumn(
                name: "ShoppingVoucher",
                table: "PayrollMonths");
        }
    }
}
