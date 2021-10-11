using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApi.Ecommerce.Migrations
{
    public partial class CreateInitial : Migration
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
                name: "PaymentStatus",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    description = table.Column<string>(type: "VARCHAR(200)", nullable: false),
                    paymentstatusid = table.Column<int>(type: "INT", nullable: false),
                    createdat = table.Column<DateTime>(type: "TIMESTAMP", nullable: false),
                    updatedat = table.Column<DateTime>(type: "TIMESTAMP", nullable: true),
                    active = table.Column<bool>(type: "boolean", nullable: false, defaultValueSql: "TRUE")
                },
                constraints: table =>
                {
                    table.PrimaryKey("Pk_PaymentStatus_Id", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "PaymentTypes",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    description = table.Column<string>(type: "VARCHAR(100)", nullable: false),
                    iscreditcard = table.Column<bool>(type: "boolean", nullable: false, defaultValueSql: "FALSE"),
                    createdat = table.Column<DateTime>(type: "TIMESTAMP", nullable: false),
                    updatedat = table.Column<DateTime>(type: "TIMESTAMP", nullable: true),
                    active = table.Column<bool>(type: "boolean", nullable: false, defaultValueSql: "TRUE")
                },
                constraints: table =>
                {
                    table.PrimaryKey("Pk_PaymentTypes_Id", x => x.id);
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
                name: "Sales",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    creditcard = table.Column<string>(type: "VARCHAR(20)", nullable: true),
                    verificationcode = table.Column<string>(type: "VARCHAR(5)", nullable: true),
                    validitymonth = table.Column<string>(type: "VARCHAR(2)", nullable: true),
                    validityyear = table.Column<string>(type: "VARCHAR(4)", nullable: true),
                    creditcardname = table.Column<string>(type: "VARCHAR(200)", nullable: true),
                    transaction = table.Column<string>(type: "VARCHAR(50)", nullable: true),
                    customerid = table.Column<Guid>(type: "uuid", nullable: true),
                    paymenttypeid = table.Column<Guid>(type: "uuid", nullable: true),
                    paymentstatusid = table.Column<Guid>(type: "uuid", nullable: true),
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
                        name: "fk_sales_paymentstatus_paymentstatusid",
                        column: x => x.paymentstatusid,
                        principalTable: "PaymentStatus",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_sales_paymenttype_paymenttypeid",
                        column: x => x.paymenttypeid,
                        principalTable: "PaymentTypes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SaleProducts",
                columns: table => new
                {
                    saleid = table.Column<Guid>(type: "uuid", nullable: false),
                    productid = table.Column<Guid>(type: "uuid", nullable: false),
                    amount = table.Column<decimal>(type: "numeric", nullable: false),
                    quantity = table.Column<int>(type: "INT", nullable: false),
                    sale = table.Column<decimal>(type: "numeric(19,4)", nullable: true),
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    createdat = table.Column<DateTime>(type: "TIMESTAMP", nullable: false),
                    updatedat = table.Column<DateTime>(type: "TIMESTAMP", nullable: true),
                    active = table.Column<bool>(type: "boolean", nullable: false, defaultValueSql: "TRUE")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_saleproducts", x => new { x.saleid, x.productid });
                    table.ForeignKey(
                        name: "fk_saleproducts_products_productid",
                        column: x => x.productid,
                        principalTable: "Products",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_saleproducts_sales_saleid",
                        column: x => x.saleid,
                        principalTable: "Sales",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "Uq_PaymentStatus_PaymentStatusId",
                table: "PaymentStatus",
                column: "paymentstatusid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_saleproducts_productid",
                table: "SaleProducts",
                column: "productid");

            migrationBuilder.CreateIndex(
                name: "ix_sales_customerid",
                table: "Sales",
                column: "customerid");

            migrationBuilder.CreateIndex(
                name: "ix_sales_paymentstatusid",
                table: "Sales",
                column: "paymentstatusid");

            migrationBuilder.CreateIndex(
                name: "ix_sales_paymenttypeid",
                table: "Sales",
                column: "paymenttypeid");

            migrationBuilder.Sql(@"
                                    INSERT INTO public.""PaymentTypes""
                                        (id, description, iscreditcard, createdat, updatedat, active)
                                    VALUES
                                        (md5(random()::text || clock_timestamp()::text)::uuid, 'Débito', true, now(), null, true),
                                        (md5(random()::text || clock_timestamp()::text)::uuid, 'Crédito', true, now(), null, true),
                                        (md5(random()::text || clock_timestamp()::text)::uuid, 'Boleto', false, now(), null, true),
                                        (md5(random()::text || clock_timestamp()::text)::uuid, 'Pix', false, now(), null, true);
            ");

            migrationBuilder.Sql(@"
									INSERT INTO public.""PaymentStatus""
                                        (id, paymentstatusid, description, createdat, updatedat, active)
                                    VALUES
                                        (md5(random()::text || clock_timestamp()::text)::uuid, 1, 'Pedido Gerado', now(), null, true),
                                        (md5(random()::text || clock_timestamp()::text)::uuid, 2, 'Aguardando Pagamento', now(), null, true),
                                        (md5(random()::text || clock_timestamp()::text)::uuid, 3, 'Pagamento Confirmado', now(), null, true),
                                        (md5(random()::text || clock_timestamp()::text)::uuid, 4, 'Aguardando Envio', now(), null, true),
                                        (md5(random()::text || clock_timestamp()::text)::uuid, 5, 'Enviado', now(), null, true),
                                        (md5(random()::text || clock_timestamp()::text)::uuid, 6, 'Em Transito', now(), null, true),
                                        (md5(random()::text || clock_timestamp()::text)::uuid, 7, 'Em Rota de Entrega', now(), null, true),
                                        (md5(random()::text || clock_timestamp()::text)::uuid, 8, 'Entregue', now(), null, true),
                                        (md5(random()::text || clock_timestamp()::text)::uuid, 9, 'Pagamento Recusado', now(), null, true);
            ");
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
                name: "PaymentStatus");

            migrationBuilder.DropTable(
                name: "PaymentTypes");
        }
    }
}
