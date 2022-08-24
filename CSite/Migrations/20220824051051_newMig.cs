using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CSite.Migrations
{
    public partial class newMig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cars",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Account = table.Column<long>(type: "bigint", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cars", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Account = table.Column<long>(type: "bigint", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Expenses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Expenses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BuyingPrice = table.Column<long>(type: "bigint", nullable: false),
                    SellingPrice = table.Column<long>(type: "bigint", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Suppliers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Account = table.Column<long>(type: "bigint", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suppliers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountId = table.Column<int>(type: "int", nullable: true),
                    AccountType = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<long>(type: "bigint", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    OperationId = table.Column<int>(type: "int", nullable: true),
                    Operation = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<int>(type: "int", nullable: false),
                    CarId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Cars_CarId",
                        column: x => x.CarId,
                        principalTable: "Cars",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CarProducts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    CarId = table.Column<int>(type: "int", nullable: true),
                    ProductId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarProducts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CarProducts_Cars_CarId",
                        column: x => x.CarId,
                        principalTable: "Cars",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CarProducts_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExportReciepts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Total = table.Column<long>(type: "bigint", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Paid = table.Column<long>(type: "bigint", nullable: false),
                    Remaining = table.Column<long>(type: "bigint", nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    CarId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExportReciepts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExportReciepts_Cars_CarId",
                        column: x => x.CarId,
                        principalTable: "Cars",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ExportReciepts_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ExportReciepts_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ImportReciepts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Total = table.Column<long>(type: "bigint", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Paid = table.Column<long>(type: "bigint", nullable: false),
                    Remaining = table.Column<long>(type: "bigint", nullable: false),
                    SupplierId = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImportReciepts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ImportReciepts_Suppliers_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Suppliers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ImportReciepts_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ExportProducts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    TotalPrice = table.Column<long>(type: "bigint", nullable: false),
                    Price = table.Column<long>(type: "bigint", nullable: false),
                    ExportRecieptId = table.Column<int>(type: "int", nullable: true),
                    ReceiptId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExportProducts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExportProducts_ExportReciepts_ExportRecieptId",
                        column: x => x.ExportRecieptId,
                        principalTable: "ExportReciepts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ExportProducts_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ImportProducts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    TotalPrice = table.Column<long>(type: "bigint", nullable: false),
                    Price = table.Column<long>(type: "bigint", nullable: false),
                    ReceiptId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImportProducts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ImportProducts_ImportReciepts_ReceiptId",
                        column: x => x.ReceiptId,
                        principalTable: "ImportReciepts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ImportProducts_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Cars",
                columns: new[] { "Id", "Account", "Name", "Notes" },
                values: new object[,]
                {
                    { 20, 200L, "new car 1", "Empty" },
                    { 21, 201L, "new car 2", "Empty" },
                    { 22, 202L, "new car 3", "Empty" }
                });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "Account", "Name", "Notes", "Phone" },
                values: new object[,]
                {
                    { 500, 210L, "Customer 1", "Empty", "+902020" },
                    { 501, 211L, "Customer 2", "Empty", "+902020" },
                    { 502, 212L, "Customer 3", "Empty", "+902020" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "BuyingPrice", "Name", "Quantity", "SellingPrice" },
                values: new object[,]
                {
                    { 300, 10300L, "New Car 1", 10, 11660L },
                    { 301, 8400L, "New Car 2", 5, 9458L },
                    { 302, 15790L, "New Car 3", 8, 17820L }
                });

            migrationBuilder.InsertData(
                table: "Suppliers",
                columns: new[] { "Id", "Account", "Name", "Notes", "Phone" },
                values: new object[,]
                {
                    { 400, 205L, "Supplier 1", "Empty", "+902020" },
                    { 401, 206L, "Supplier 2", "Empty", "+902020" },
                    { 402, 207L, "Supplier 3", "Empty", "+902020" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CarId", "Name", "Password", "Type" },
                values: new object[] { 1, 20, "Amin", "csite@123", 0 });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CarId", "Name", "Password", "Type" },
                values: new object[] { 2, 21, "Sara", "csite@123", 0 });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CarId", "Name", "Password", "Type" },
                values: new object[] { 3, 22, "Javad", "csite@123", 1 });

            migrationBuilder.CreateIndex(
                name: "IX_CarProducts_CarId",
                table: "CarProducts",
                column: "CarId");

            migrationBuilder.CreateIndex(
                name: "IX_CarProducts_ProductId",
                table: "CarProducts",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ExportProducts_ExportRecieptId",
                table: "ExportProducts",
                column: "ExportRecieptId");

            migrationBuilder.CreateIndex(
                name: "IX_ExportProducts_ProductId",
                table: "ExportProducts",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ExportReciepts_CarId",
                table: "ExportReciepts",
                column: "CarId");

            migrationBuilder.CreateIndex(
                name: "IX_ExportReciepts_CustomerId",
                table: "ExportReciepts",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_ExportReciepts_UserId",
                table: "ExportReciepts",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ImportProducts_ProductId",
                table: "ImportProducts",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ImportProducts_ReceiptId",
                table: "ImportProducts",
                column: "ReceiptId");

            migrationBuilder.CreateIndex(
                name: "IX_ImportReciepts_SupplierId",
                table: "ImportReciepts",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_ImportReciepts_UserId",
                table: "ImportReciepts",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_CarId",
                table: "Users",
                column: "CarId");
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
