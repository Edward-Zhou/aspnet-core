using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace EdwardAbp.Migrations
{
    public partial class four : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "AbpProducts");

            migrationBuilder.AddColumn<int>(
                name: "PTypeId",
                table: "AbpProducts",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PType",
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
                    Name = table.Column<string>(nullable: true),
                    TenantId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PType", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AbpProducts_PTypeId",
                table: "AbpProducts",
                column: "PTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_AbpProducts_PType_PTypeId",
                table: "AbpProducts",
                column: "PTypeId",
                principalTable: "PType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AbpProducts_PType_PTypeId",
                table: "AbpProducts");

            migrationBuilder.DropTable(
                name: "PType");

            migrationBuilder.DropIndex(
                name: "IX_AbpProducts_PTypeId",
                table: "AbpProducts");

            migrationBuilder.DropColumn(
                name: "PTypeId",
                table: "AbpProducts");

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "AbpProducts",
                nullable: true);
        }
    }
}
