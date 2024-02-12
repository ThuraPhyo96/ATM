using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ATM.Migrations
{
    public partial class Add_TransactionDate_Field_BalanceHistory_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "TransactionDate",
                table: "BalanceHistories",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TransactionDate",
                table: "BalanceHistories");
        }
    }
}
