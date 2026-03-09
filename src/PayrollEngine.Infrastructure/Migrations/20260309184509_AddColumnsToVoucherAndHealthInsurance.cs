using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PayrollEngine.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddColumnsToVoucherAndHealthInsurance : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "StampTax",
                table: "ShoppingVouchers",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "IncomeTax",
                table: "PrivateHealthInsurances",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "StampTax",
                table: "PrivateHealthInsurances",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StampTax",
                table: "ShoppingVouchers");

            migrationBuilder.DropColumn(
                name: "IncomeTax",
                table: "PrivateHealthInsurances");

            migrationBuilder.DropColumn(
                name: "StampTax",
                table: "PrivateHealthInsurances");
        }
    }
}
