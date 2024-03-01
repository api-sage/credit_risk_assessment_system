using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CreditRiskAssessment.Migrations
{
    /// <inheritdoc />
    public partial class CRAS_Initial_Migration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AssessedCustomers",
                columns: table => new
                {
                    BVN = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    AssessedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    Occupation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LoanAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AnnualIncome = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MonthlyNetSalary = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    InterestRate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NumberOfLoan = table.Column<int>(type: "int", nullable: false),
                    NumberOfDelayedPayment = table.Column<int>(type: "int", nullable: false),
                    OutstandingDebt = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DebtToIncomeRatio = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MonthsOfCreditHistory = table.Column<int>(type: "int", nullable: false),
                    PaymentOfMinimumAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MonthlyInstallmentAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AmountInvestedMonthly = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MonthlyBalance = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PredictedCreditScore = table.Column<int>(type: "int", nullable: false),
                    CreditRating = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssessedCustomers", x => x.BVN);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    BVN = table.Column<int>(type: "int", maxLength: 11, nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    Occupation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LoanAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AnnualIncome = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MonthlyNetSalary = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    InterestRate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NumberOfLoan = table.Column<int>(type: "int", nullable: false),
                    NumberOfDelayedPayment = table.Column<int>(type: "int", nullable: false),
                    OutstandingDebt = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MonthsOfCreditHistory = table.Column<int>(type: "int", nullable: false),
                    PaymentOfMinimumAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MonthlyInstallmentAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AmountInvestedMonthly = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MonthlyBalance = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.BVN);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AssessedCustomers");

            migrationBuilder.DropTable(
                name: "Customers");
        }
    }
}
