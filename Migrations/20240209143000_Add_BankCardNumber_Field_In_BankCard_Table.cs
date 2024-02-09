using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ATM.Migrations
{
    public partial class Add_BankCardNumber_Field_In_BankCard_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BankCardNumber",
                table: "BankCards",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationTime",
                table: "AspNetUsers",
                nullable: true,
                defaultValueSql: "getdate()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BankCardNumber",
                table: "BankCards");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationTime",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldNullable: true,
                oldDefaultValueSql: "getdate()");
        }
    }
}
