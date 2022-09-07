using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CSite.Data.Migrations
{
    public partial class newMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cars",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<float>(type: "real", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cars", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Exported = table.Column<long>(type: "bigint", nullable: true),
                    Imported = table.Column<long>(type: "bigint", nullable: true),
                    CarsId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Cars_CarsId",
                        column: x => x.CarsId,
                        principalTable: "Cars",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Receipts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Price = table.Column<float>(type: "real", nullable: false),
                    ReceiptDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClientUserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SupplierUserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CarId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Receipts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Receipts_Cars_CarId",
                        column: x => x.CarId,
                        principalTable: "Cars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Cars",
                columns: new[] { "Id", "CreatedDate", "Description", "Image", "ModifiedDate", "Name", "Price", "UserId" },
                values: new object[] { 20, new DateTime(2021, 10, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Empty", "", new DateTime(2022, 9, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "new car 1", 100f, "Empty" });

            migrationBuilder.InsertData(
                table: "Cars",
                columns: new[] { "Id", "CreatedDate", "Description", "Image", "ModifiedDate", "Name", "Price", "UserId" },
                values: new object[] { 21, new DateTime(2020, 5, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Empty", "", new DateTime(2022, 1, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "new car 1", 100f, "Empty" });

            migrationBuilder.InsertData(
                table: "Cars",
                columns: new[] { "Id", "CreatedDate", "Description", "Image", "ModifiedDate", "Name", "Price", "UserId" },
                values: new object[] { 22, new DateTime(2022, 3, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Empty", "", new DateTime(2022, 6, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "new car 1", 100f, "Empty" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CarsId", "Exported", "Imported", "Quantity" },
                values: new object[] { 300, 20, 100L, 120L, 20 });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CarsId", "Exported", "Imported", "Quantity" },
                values: new object[] { 301, 21, 230L, 280L, 50 });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CarsId", "Exported", "Imported", "Quantity" },
                values: new object[] { 302, 22, 5L, 85L, 80 });

            migrationBuilder.CreateIndex(
                name: "IX_Products_CarsId",
                table: "Products",
                column: "CarsId");

            migrationBuilder.CreateIndex(
                name: "IX_Receipts_CarId",
                table: "Receipts",
                column: "CarId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Receipts");

            migrationBuilder.DropTable(
                name: "Cars");
        }
    }
}
