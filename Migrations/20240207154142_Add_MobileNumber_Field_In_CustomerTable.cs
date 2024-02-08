using Microsoft.EntityFrameworkCore.Migrations;

namespace ATM.Migrations
{
    public partial class Add_MobileNumber_Field_In_CustomerTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MobileNumber",
                table: "Customers",
                maxLength: 12,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MobileNumber",
                table: "Customers");
        }
    }
}
