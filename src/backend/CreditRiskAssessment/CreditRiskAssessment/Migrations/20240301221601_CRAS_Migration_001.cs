using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CreditRiskAssessment.Migrations
{
    /// <inheritdoc />
    public partial class CRAS_Migration_001 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AssessedCustomers",
                columns: table => new
                {
                    SN = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AssessedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BVN = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    Occupation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LoanAmount = table.Column<double>(type: "float", nullable: false),
                    AnnualIncome = table.Column<double>(type: "float", nullable: false),
                    MonthlyNetSalary = table.Column<double>(type: "float", nullable: false),
                    InterestRate = table.Column<double>(type: "float", nullable: false),
                    NumberOfLoan = table.Column<int>(type: "int", nullable: false),
                    NumberOfDelayedPayment = table.Column<int>(type: "int", nullable: false),
                    OutstandingDebt = table.Column<double>(type: "float", nullable: false),
                    DebtToIncomeRatio = table.Column<float>(type: "real", nullable: false),
                    MonthsOfCreditHistory = table.Column<int>(type: "int", nullable: false),
                    PaymentOfMinimumAmount = table.Column<bool>(type: "bit", nullable: false),
                    MonthlyInstallmentAmount = table.Column<double>(type: "float", nullable: false),
                    AmountInvestedMonthly = table.Column<double>(type: "float", nullable: false),
                    MonthlyBalance = table.Column<double>(type: "float", nullable: false),
                    PredictedCreditScore = table.Column<int>(type: "int", nullable: false),
                    CreditRating = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssessedCustomers", x => x.SN);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    BVN = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    Occupation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LoanAmount = table.Column<double>(type: "float", nullable: false),
                    AnnualIncome = table.Column<double>(type: "float", nullable: false),
                    MonthlyNetSalary = table.Column<double>(type: "float", nullable: false),
                    InterestRate = table.Column<int>(type: "int", nullable: false),
                    NumberOfLoan = table.Column<int>(type: "int", nullable: false),
                    NumberOfDelayedPayment = table.Column<int>(type: "int", nullable: false),
                    OutstandingDebt = table.Column<double>(type: "float", nullable: false),
                    MonthsOfCreditHistory = table.Column<int>(type: "int", nullable: false),
                    PaymentOfMinimumAmount = table.Column<bool>(type: "bit", nullable: false),
                    MonthlyInstallmentAmount = table.Column<double>(type: "float", nullable: false),
                    AmountInvestedMonthly = table.Column<double>(type: "float", nullable: false),
                    MonthlyBalance = table.Column<double>(type: "float", nullable: false)
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
