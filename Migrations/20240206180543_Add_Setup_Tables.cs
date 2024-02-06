using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ATM.Migrations
{
    public partial class Add_Setup_Tables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "UserType",
                table: "AspNetUsers",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "UpdatedUserId",
                table: "AspNetUsers",
                maxLength: 450,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                table: "AspNetUsers",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<string>(
                name: "CreatedUserId",
                table: "AspNetUsers",
                maxLength: 450,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    CustomerId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedUserInfoId = table.Column<string>(nullable: true),
                    CreatedUserId = table.Column<string>(nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: true, defaultValueSql: "getdate()"),
                    UpdatedUserInfoId = table.Column<string>(nullable: true),
                    UpdatedUserId = table.Column<string>(nullable: true),
                    UpdatedTime = table.Column<DateTime>(nullable: true),
                    CustomerGuid = table.Column<Guid>(nullable: false, defaultValueSql: "NEWID()"),
                    FirstName = table.Column<string>(maxLength: 100, nullable: true),
                    LastName = table.Column<string>(maxLength: 100, nullable: true),
                    DateOfBirth = table.Column<DateTime>(nullable: false),
                    NRIC = table.Column<string>(maxLength: 50, nullable: true),
                    FatherName = table.Column<string>(maxLength: 100, nullable: true),
                    Email = table.Column<string>(maxLength: 100, nullable: true),
                    JobTitle = table.Column<string>(maxLength: 100, nullable: true),
                    Address = table.Column<string>(maxLength: 1000, nullable: true),
                    LoginUserId = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    Remark = table.Column<string>(maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.CustomerId);
                    table.ForeignKey(
                        name: "FK_Customers_AspNetUsers_CreatedUserId",
                        column: x => x.CreatedUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Customers_AspNetUsers_LoginUserId",
                        column: x => x.LoginUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Customers_AspNetUsers_UpdatedUserId",
                        column: x => x.UpdatedUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BankAccounts",
                columns: table => new
                {
                    BankAccountId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedUserInfoId = table.Column<string>(nullable: true),
                    CreatedUserId = table.Column<string>(nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: true, defaultValueSql: "getdate()"),
                    UpdatedUserInfoId = table.Column<string>(nullable: true),
                    UpdatedUserId = table.Column<string>(nullable: true),
                    UpdatedTime = table.Column<DateTime>(nullable: true),
                    BankAccountGuid = table.Column<Guid>(nullable: false, defaultValueSql: "NEWID()"),
                    CustomerId = table.Column<int>(nullable: false),
                    AccountNumber = table.Column<string>(maxLength: 20, nullable: true),
                    AccountType = table.Column<int>(nullable: false),
                    Balance = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    Remark = table.Column<string>(maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankAccounts", x => x.BankAccountId);
                    table.ForeignKey(
                        name: "FK_BankAccounts_AspNetUsers_CreatedUserId",
                        column: x => x.CreatedUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BankAccounts_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BankAccounts_AspNetUsers_UpdatedUserId",
                        column: x => x.UpdatedUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BalanceHistories",
                columns: table => new
                {
                    BalanceHistoryId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedUserInfoId = table.Column<string>(nullable: true),
                    CreatedUserId = table.Column<string>(nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: true, defaultValueSql: "getdate()"),
                    UpdatedUserInfoId = table.Column<string>(nullable: true),
                    UpdatedUserId = table.Column<string>(nullable: true),
                    UpdatedTime = table.Column<DateTime>(nullable: true),
                    BalanceHistoryGuid = table.Column<Guid>(nullable: false, defaultValueSql: "NEWID()"),
                    CustomerId = table.Column<int>(nullable: false),
                    BankAccountId = table.Column<int>(nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    HistoryType = table.Column<int>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    Remark = table.Column<string>(maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BalanceHistories", x => x.BalanceHistoryId);
                    table.ForeignKey(
                        name: "FK_BalanceHistories_BankAccounts_BankAccountId",
                        column: x => x.BankAccountId,
                        principalTable: "BankAccounts",
                        principalColumn: "BankAccountId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BalanceHistories_AspNetUsers_CreatedUserId",
                        column: x => x.CreatedUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BalanceHistories_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_BalanceHistories_AspNetUsers_UpdatedUserId",
                        column: x => x.UpdatedUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BankCards",
                columns: table => new
                {
                    BankCardId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedUserInfoId = table.Column<string>(nullable: true),
                    CreatedUserId = table.Column<string>(nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: true, defaultValueSql: "getdate()"),
                    UpdatedUserInfoId = table.Column<string>(nullable: true),
                    UpdatedUserId = table.Column<string>(nullable: true),
                    UpdatedTime = table.Column<DateTime>(nullable: true),
                    BankCardGuid = table.Column<Guid>(nullable: false, defaultValueSql: "NEWID()"),
                    CustomerId = table.Column<int>(nullable: false),
                    BankAccountId = table.Column<int>(nullable: false),
                    PIN = table.Column<string>(nullable: true),
                    ValidDate = table.Column<DateTime>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    Remark = table.Column<string>(maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankCards", x => x.BankCardId);
                    table.ForeignKey(
                        name: "FK_BankCards_BankAccounts_BankAccountId",
                        column: x => x.BankAccountId,
                        principalTable: "BankAccounts",
                        principalColumn: "BankAccountId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BankCards_AspNetUsers_CreatedUserId",
                        column: x => x.CreatedUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BankCards_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_BankCards_AspNetUsers_UpdatedUserId",
                        column: x => x.UpdatedUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BalanceHistories_BalanceHistoryGuid",
                table: "BalanceHistories",
                column: "BalanceHistoryGuid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BalanceHistories_BankAccountId",
                table: "BalanceHistories",
                column: "BankAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_BalanceHistories_CreatedUserId",
                table: "BalanceHistories",
                column: "CreatedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_BalanceHistories_CustomerId",
                table: "BalanceHistories",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_BalanceHistories_UpdatedUserId",
                table: "BalanceHistories",
                column: "UpdatedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_BankAccounts_BankAccountGuid",
                table: "BankAccounts",
                column: "BankAccountGuid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BankAccounts_CreatedUserId",
                table: "BankAccounts",
                column: "CreatedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_BankAccounts_CustomerId",
                table: "BankAccounts",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_BankAccounts_UpdatedUserId",
                table: "BankAccounts",
                column: "UpdatedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_BankCards_BankAccountId",
                table: "BankCards",
                column: "BankAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_BankCards_BankCardGuid",
                table: "BankCards",
                column: "BankCardGuid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BankCards_CreatedUserId",
                table: "BankCards",
                column: "CreatedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_BankCards_CustomerId",
                table: "BankCards",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_BankCards_UpdatedUserId",
                table: "BankCards",
                column: "UpdatedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_CreatedUserId",
                table: "Customers",
                column: "CreatedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_CustomerGuid",
                table: "Customers",
                column: "CustomerGuid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Customers_LoginUserId",
                table: "Customers",
                column: "LoginUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_UpdatedUserId",
                table: "Customers",
                column: "UpdatedUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BalanceHistories");

            migrationBuilder.DropTable(
                name: "BankCards");

            migrationBuilder.DropTable(
                name: "BankAccounts");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<int>(
                name: "UserType",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UpdatedUserId",
                table: "AspNetUsers",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 450,
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedUserId",
                table: "AspNetUsers",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 450,
                oldNullable: true);
        }
    }
}
