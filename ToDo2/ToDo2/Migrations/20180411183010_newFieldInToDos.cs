using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace ToDo2.Migrations
{
    public partial class newFieldInToDos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ListItemsId",
                table: "ListItems",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ToDosId",
                table: "ListItems",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ListItems_ListItemsId",
                table: "ListItems",
                column: "ListItemsId");

            migrationBuilder.CreateIndex(
                name: "IX_ListItems_ToDosId",
                table: "ListItems",
                column: "ToDosId");

            migrationBuilder.AddForeignKey(
                name: "FK_ListItems_ListItems_ListItemsId",
                table: "ListItems",
                column: "ListItemsId",
                principalTable: "ListItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ListItems_ToDos_ToDosId",
                table: "ListItems",
                column: "ToDosId",
                principalTable: "ToDos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ListItems_ListItems_ListItemsId",
                table: "ListItems");

            migrationBuilder.DropForeignKey(
                name: "FK_ListItems_ToDos_ToDosId",
                table: "ListItems");

            migrationBuilder.DropIndex(
                name: "IX_ListItems_ListItemsId",
                table: "ListItems");

            migrationBuilder.DropIndex(
                name: "IX_ListItems_ToDosId",
                table: "ListItems");

            migrationBuilder.DropColumn(
                name: "ListItemsId",
                table: "ListItems");

            migrationBuilder.DropColumn(
                name: "ToDosId",
                table: "ListItems");
        }
    }
}
