using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RepositoryLayer.Migrations
{
    public partial class firstmigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "adminUserRegistrations",
                columns: table => new
                {
                    AdminId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FullName = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: false),
                    Password = table.Column<string>(nullable: false),
                    Phone = table.Column<string>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: true),
                    UpdatedAt = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_adminUserRegistrations", x => x.AdminId);
                });

            migrationBuilder.CreateTable(
                name: "customerDetails",
                columns: table => new
                {
                    CustomerId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Email = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Phone = table.Column<string>(nullable: false),
                    Pincode = table.Column<string>(nullable: false),
                    Locality = table.Column<string>(nullable: false),
                    Address = table.Column<string>(nullable: false),
                    City = table.Column<string>(nullable: false),
                    Landmark = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_customerDetails", x => x.CustomerId);
                });

            migrationBuilder.CreateTable(
                name: "products",
                columns: table => new
                {
                    Product_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BookName = table.Column<string>(nullable: false),
                    BookImage = table.Column<string>(nullable: true),
                    Author = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: false),
                    Quantity = table.Column<long>(nullable: false),
                    Price = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_products", x => x.Product_id);
                });

            migrationBuilder.CreateTable(
                name: "userRegistrations",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FullName = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: false),
                    Password = table.Column<string>(nullable: false),
                    Phone = table.Column<string>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: true),
                    UpdatedAt = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_userRegistrations", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "newOrders",
                columns: table => new
                {
                    OrderId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CustomerId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_newOrders", x => x.OrderId);
                    table.ForeignKey(
                        name: "FK_newOrders_customerDetails_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "customerDetails",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "cartItems",
                columns: table => new
                {
                    CartItem_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Product_id = table.Column<int>(nullable: false),
                    LoginUser = table.Column<string>(nullable: true),
                    QuantityToBuy = table.Column<int>(nullable: false),
                    NewOrderOrderId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cartItems", x => x.CartItem_id);
                    table.ForeignKey(
                        name: "FK_cartItems_newOrders_NewOrderOrderId",
                        column: x => x.NewOrderOrderId,
                        principalTable: "newOrders",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_cartItems_products_Product_id",
                        column: x => x.Product_id,
                        principalTable: "products",
                        principalColumn: "Product_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_cartItems_NewOrderOrderId",
                table: "cartItems",
                column: "NewOrderOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_cartItems_Product_id",
                table: "cartItems",
                column: "Product_id");

            migrationBuilder.CreateIndex(
                name: "IX_newOrders_CustomerId",
                table: "newOrders",
                column: "CustomerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "adminUserRegistrations");

            migrationBuilder.DropTable(
                name: "cartItems");

            migrationBuilder.DropTable(
                name: "userRegistrations");

            migrationBuilder.DropTable(
                name: "newOrders");

            migrationBuilder.DropTable(
                name: "products");

            migrationBuilder.DropTable(
                name: "customerDetails");
        }
    }
}
