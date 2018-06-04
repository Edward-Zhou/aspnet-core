using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace EdwardAbp.Migrations
{
    public partial class order : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AddressId = table.Column<long>(nullable: true),
                    ConsignTime = table.Column<DateTime>(nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    DeleterUserId = table.Column<long>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    DiscountFee = table.Column<double>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    Number = table.Column<string>(nullable: true),
                    OrderDateTime = table.Column<DateTime>(nullable: false),
                    OrderId = table.Column<long>(nullable: false),
                    OrganizationUnitId = table.Column<long>(nullable: true),
                    OuterId = table.Column<string>(nullable: true),
                    PayTime = table.Column<DateTime>(nullable: false),
                    PayType = table.Column<string>(nullable: true),
                    Payment = table.Column<double>(nullable: false),
                    PostFee = table.Column<double>(nullable: false),
                    RefundTime = table.Column<DateTime>(nullable: true),
                    ReturnsTime = table.Column<DateTime>(nullable: true),
                    ShippingType = table.Column<string>(nullable: true),
                    ShopPick = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    TaxFee = table.Column<double>(nullable: false),
                    TenantId = table.Column<int>(nullable: false),
                    TotalFee = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrderItems",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    DeleterUserId = table.Column<long>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    Number = table.Column<int>(nullable: false),
                    OrderId = table.Column<long>(nullable: false),
                    OrganizationUnitId = table.Column<long>(nullable: true),
                    Payment = table.Column<double>(nullable: false),
                    PicUrl = table.Column<string>(nullable: true),
                    SkuId = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    TaxFee = table.Column<double>(nullable: false),
                    TenantId = table.Column<int>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    TotalFee = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderItems_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_OrderId",
                table: "OrderItems",
                column: "OrderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderItems");

            migrationBuilder.DropTable(
                name: "Orders");
        }
    }
}
