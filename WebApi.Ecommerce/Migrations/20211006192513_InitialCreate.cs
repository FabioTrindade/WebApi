using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApi.Ecommerce.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "VARCHAR(200)", nullable: false),
                    document = table.Column<string>(type: "VARCHAR(20)", nullable: false),
                    zipcode = table.Column<string>(type: "VARCHAR(10)", nullable: false),
                    address = table.Column<string>(type: "VARCHAR(200)", nullable: false),
                    number = table.Column<string>(type: "VARCHAR(10)", nullable: false),
                    neighborhood = table.Column<string>(type: "VARCHAR(100)", nullable: false),
                    complement = table.Column<string>(type: "VARCHAR(100)", nullable: true),
                    city = table.Column<string>(type: "VARCHAR(100)", nullable: false),
                    state = table.Column<string>(type: "VARCHAR(10)", nullable: false),
                    country = table.Column<string>(type: "VARCHAR(10)", nullable: false),
                    cellphone = table.Column<string>(type: "VARCHAR(20)", nullable: false),
                    phone = table.Column<string>(type: "VARCHAR(20)", nullable: true),
                    email = table.Column<string>(type: "VARCHAR(200)", nullable: false),
                    createdat = table.Column<DateTime>(type: "TIMESTAMP", nullable: false),
                    updatedat = table.Column<DateTime>(type: "TIMESTAMP", nullable: true),
                    active = table.Column<bool>(type: "boolean", nullable: false, defaultValueSql: "TRUE")
                },
                constraints: table =>
                {
                    table.PrimaryKey("Pk_Customers_Id", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "LogErros",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    method = table.Column<string>(type: "VARCHAR(100)", nullable: true),
                    path = table.Column<string>(type: "VARCHAR(200)", nullable: true),
                    erro = table.Column<string>(type: "VARCHAR(8000)", nullable: true),
                    errocompleto = table.Column<string>(type: "VARCHAR(8000)", nullable: true),
                    query = table.Column<string>(type: "VARCHAR(8000)", nullable: true),
                    createdat = table.Column<DateTime>(type: "TIMESTAMP", nullable: false),
                    updatedat = table.Column<DateTime>(type: "TIMESTAMP", nullable: true),
                    active = table.Column<bool>(type: "boolean", nullable: false, defaultValueSql: "TRUE")
                },
                constraints: table =>
                {
                    table.PrimaryKey("Pk_LogErros_Id", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "LogRequests",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    device = table.Column<string>(type: "VARCHAR(600)", nullable: true),
                    host = table.Column<string>(type: "VARCHAR(600)", nullable: true),
                    method = table.Column<string>(type: "VARCHAR(600)", nullable: true),
                    path = table.Column<string>(type: "VARCHAR(600)", nullable: true),
                    url = table.Column<string>(type: "VARCHAR(600)", nullable: true),
                    header = table.Column<string>(type: "TEXT", nullable: true),
                    body = table.Column<string>(type: "TEXT", nullable: true),
                    query = table.Column<string>(type: "TEXT", nullable: true),
                    ip = table.Column<string>(type: "VARCHAR(200)", nullable: true),
                    statuscode = table.Column<int>(type: "INT", nullable: false),
                    response = table.Column<string>(type: "TEXT", nullable: true),
                    executiontime = table.Column<TimeSpan>(type: "TIME", nullable: false),
                    createdat = table.Column<DateTime>(type: "TIMESTAMP", nullable: false),
                    updatedat = table.Column<DateTime>(type: "TIMESTAMP", nullable: true),
                    active = table.Column<bool>(type: "boolean", nullable: false, defaultValueSql: "TRUE")
                },
                constraints: table =>
                {
                    table.PrimaryKey("Pk_LogRequests_Id", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    description = table.Column<string>(type: "VARCHAR(200)", nullable: false),
                    sku = table.Column<string>(type: "VARCHAR(20)", nullable: false),
                    amount = table.Column<decimal>(type: "numeric", nullable: false),
                    quantity = table.Column<int>(type: "INT", nullable: false, defaultValueSql: "0"),
                    sale = table.Column<decimal>(type: "numeric(19,4)", nullable: true),
                    createdat = table.Column<DateTime>(type: "TIMESTAMP", nullable: false),
                    updatedat = table.Column<DateTime>(type: "TIMESTAMP", nullable: true),
                    active = table.Column<bool>(type: "boolean", nullable: false, defaultValueSql: "TRUE")
                },
                constraints: table =>
                {
                    table.PrimaryKey("Pk_Products_Id", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "SaleTypes",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    description = table.Column<string>(type: "VARCHAR(100)", nullable: false),
                    createdat = table.Column<DateTime>(type: "TIMESTAMP", nullable: false),
                    updatedat = table.Column<DateTime>(type: "TIMESTAMP", nullable: true),
                    active = table.Column<bool>(type: "boolean", nullable: false, defaultValueSql: "TRUE")
                },
                constraints: table =>
                {
                    table.PrimaryKey("Pk_SaleTypes_Id", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Sales",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    customerid = table.Column<Guid>(type: "uuid", nullable: true),
                    saletypeid = table.Column<Guid>(type: "uuid", nullable: true),
                    createdat = table.Column<DateTime>(type: "TIMESTAMP", nullable: false),
                    updatedat = table.Column<DateTime>(type: "TIMESTAMP", nullable: true),
                    active = table.Column<bool>(type: "boolean", nullable: false, defaultValueSql: "TRUE")
                },
                constraints: table =>
                {
                    table.PrimaryKey("Pk_Sales_Id", x => x.id);
                    table.ForeignKey(
                        name: "fk_sales_customers_customerid",
                        column: x => x.customerid,
                        principalTable: "Customers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_sales_saletypes_saletypeid",
                        column: x => x.saletypeid,
                        principalTable: "SaleTypes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SaleProducts",
                columns: table => new
                {
                    salesid = table.Column<Guid>(type: "uuid", nullable: false),
                    productid = table.Column<Guid>(type: "uuid", nullable: false),
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    createdat = table.Column<DateTime>(type: "TIMESTAMP", nullable: false),
                    updatedat = table.Column<DateTime>(type: "TIMESTAMP", nullable: true),
                    active = table.Column<bool>(type: "boolean", nullable: false, defaultValueSql: "TRUE")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_saleproducts", x => new { x.salesid, x.productid });
                    table.ForeignKey(
                        name: "fk_saleproducts_products_productid",
                        column: x => x.productid,
                        principalTable: "Products",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_saleproducts_sales_salesid",
                        column: x => x.salesid,
                        principalTable: "Sales",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "ix_saleproducts_productid",
                table: "SaleProducts",
                column: "productid");

            migrationBuilder.CreateIndex(
                name: "ix_sales_customerid",
                table: "Sales",
                column: "customerid");

            migrationBuilder.CreateIndex(
                name: "ix_sales_saletypeid",
                table: "Sales",
                column: "saletypeid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LogErros");

            migrationBuilder.DropTable(
                name: "LogRequests");

            migrationBuilder.DropTable(
                name: "SaleProducts");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Sales");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "SaleTypes");
        }
    }
}
