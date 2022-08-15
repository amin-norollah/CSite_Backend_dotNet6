using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CSite.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cars",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Account = table.Column<long>(type: "bigint", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cars", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Account = table.Column<long>(type: "bigint", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Expenses",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Expenses", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BuyingPrice = table.Column<long>(type: "bigint", nullable: false),
                    SellingPrice = table.Column<long>(type: "bigint", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Suppliers",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Account = table.Column<long>(type: "bigint", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suppliers", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountID = table.Column<int>(type: "int", nullable: true),
                    AccountType = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<long>(type: "bigint", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    OperationID = table.Column<int>(type: "int", nullable: true),
                    Operation = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<int>(type: "int", nullable: false),
                    CarID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserName);
                    table.ForeignKey(
                        name: "FK_Users_Cars_CarID",
                        column: x => x.CarID,
                        principalTable: "Cars",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "CarProducts",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    CarID = table.Column<int>(type: "int", nullable: true),
                    ProductID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarProducts", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CarProducts_Cars_CarID",
                        column: x => x.CarID,
                        principalTable: "Cars",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_CarProducts_Products_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Products",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExportReciepts",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Total = table.Column<long>(type: "bigint", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Paid = table.Column<long>(type: "bigint", nullable: false),
                    Remaining = table.Column<long>(type: "bigint", nullable: false),
                    CustomerID = table.Column<int>(type: "int", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CarID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExportReciepts", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ExportReciepts_Cars_CarID",
                        column: x => x.CarID,
                        principalTable: "Cars",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_ExportReciepts_Customers_CustomerID",
                        column: x => x.CustomerID,
                        principalTable: "Customers",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_ExportReciepts_Users_UserName",
                        column: x => x.UserName,
                        principalTable: "Users",
                        principalColumn: "UserName");
                });

            migrationBuilder.CreateTable(
                name: "ImportReciepts",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Total = table.Column<long>(type: "bigint", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Paid = table.Column<long>(type: "bigint", nullable: false),
                    Remaining = table.Column<long>(type: "bigint", nullable: false),
                    SupplierID = table.Column<int>(type: "int", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImportReciepts", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ImportReciepts_Suppliers_SupplierID",
                        column: x => x.SupplierID,
                        principalTable: "Suppliers",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_ImportReciepts_Users_UserName",
                        column: x => x.UserName,
                        principalTable: "Users",
                        principalColumn: "UserName");
                });

            migrationBuilder.CreateTable(
                name: "ExportProducts",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    TotalPrice = table.Column<long>(type: "bigint", nullable: false),
                    Price = table.Column<long>(type: "bigint", nullable: false),
                    ExportRecieptID = table.Column<int>(type: "int", nullable: true),
                    ReceiptID = table.Column<int>(type: "int", nullable: false),
                    ProductID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExportProducts", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ExportProducts_ExportReciepts_ExportRecieptID",
                        column: x => x.ExportRecieptID,
                        principalTable: "ExportReciepts",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_ExportProducts_Products_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Products",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ImportProducts",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    TotalPrice = table.Column<long>(type: "bigint", nullable: false),
                    Price = table.Column<long>(type: "bigint", nullable: false),
                    ReceiptID = table.Column<int>(type: "int", nullable: false),
                    ProductID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImportProducts", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ImportProducts_ImportReciepts_ReceiptID",
                        column: x => x.ReceiptID,
                        principalTable: "ImportReciepts",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ImportProducts_Products_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Products",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Cars",
                columns: new[] { "ID", "Account", "Name", "Notes" },
                values: new object[,]
                {
                    { 20, 200L, "new car 1", "Empty" },
                    { 21, 201L, "new car 2", "Empty" },
                    { 22, 202L, "new car 3", "Empty" }
                });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "ID", "Account", "Name", "Notes", "Phone" },
                values: new object[,]
                {
                    { 500, 210L, "Customer 1", "Empty", "+902020" },
                    { 501, 211L, "Customer 2", "Empty", "+902020" },
                    { 502, 212L, "Customer 3", "Empty", "+902020" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ID", "BuyingPrice", "Name", "Quantity", "SellingPrice" },
                values: new object[,]
                {
                    { 300, 10300L, "New Car 1", 10, 11660L },
                    { 301, 8400L, "New Car 2", 5, 9458L },
                    { 302, 15790L, "New Car 3", 8, 17820L }
                });

            migrationBuilder.InsertData(
                table: "Suppliers",
                columns: new[] { "ID", "Account", "Name", "Notes", "Phone" },
                values: new object[,]
                {
                    { 400, 205L, "Supplier 1", "Empty", "+902020" },
                    { 401, 206L, "Supplier 2", "Empty", "+902020" },
                    { 402, 207L, "Supplier 3", "Empty", "+902020" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserName", "CarID", "Password", "Type" },
                values: new object[] { "Amin", 20, "csite@123", 0 });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserName", "CarID", "Password", "Type" },
                values: new object[] { "Javad", 22, "csite@123", 1 });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserName", "CarID", "Password", "Type" },
                values: new object[] { "Sara", 21, "csite@123", 0 });

            migrationBuilder.CreateIndex(
                name: "IX_CarProducts_CarID",
                table: "CarProducts",
                column: "CarID");

            migrationBuilder.CreateIndex(
                name: "IX_CarProducts_ProductID",
                table: "CarProducts",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_ExportProducts_ExportRecieptID",
                table: "ExportProducts",
                column: "ExportRecieptID");

            migrationBuilder.CreateIndex(
                name: "IX_ExportProducts_ProductID",
                table: "ExportProducts",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_ExportReciepts_CarID",
                table: "ExportReciepts",
                column: "CarID");

            migrationBuilder.CreateIndex(
                name: "IX_ExportReciepts_CustomerID",
                table: "ExportReciepts",
                column: "CustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_ExportReciepts_UserName",
                table: "ExportReciepts",
                column: "UserName");

            migrationBuilder.CreateIndex(
                name: "IX_ImportProducts_ProductID",
                table: "ImportProducts",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_ImportProducts_ReceiptID",
                table: "ImportProducts",
                column: "ReceiptID");

            migrationBuilder.CreateIndex(
                name: "IX_ImportReciepts_SupplierID",
                table: "ImportReciepts",
                column: "SupplierID");

            migrationBuilder.CreateIndex(
                name: "IX_ImportReciepts_UserName",
                table: "ImportReciepts",
                column: "UserName");

            migrationBuilder.CreateIndex(
                name: "IX_Users_CarID",
                table: "Users",
                column: "CarID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CarProducts");

            migrationBuilder.DropTable(
                name: "Expenses");

            migrationBuilder.DropTable(
                name: "ExportProducts");

            migrationBuilder.DropTable(
                name: "ImportProducts");

            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "ExportReciepts");

            migrationBuilder.DropTable(
                name: "ImportReciepts");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Suppliers");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Cars");
        }
    }
}
