using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataModels.Migrations
{
    public partial class ThirdCreateToUpdateEntityNames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ToDoItems_ToDoLists_ToDoListModelId",
                table: "ToDoItems");

            migrationBuilder.RenameColumn(
                name: "ToDoListModelId",
                table: "ToDoItems",
                newName: "ToDoListId");

            migrationBuilder.RenameIndex(
                name: "IX_ToDoItems_ToDoListModelId",
                table: "ToDoItems",
                newName: "IX_ToDoItems_ToDoListId");

            migrationBuilder.AddForeignKey(
                name: "FK_ToDoItems_ToDoLists_ToDoListId",
                table: "ToDoItems",
                column: "ToDoListId",
                principalTable: "ToDoLists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ToDoItems_ToDoLists_ToDoListId",
                table: "ToDoItems");

            migrationBuilder.RenameColumn(
                name: "ToDoListId",
                table: "ToDoItems",
                newName: "ToDoListModelId");

            migrationBuilder.RenameIndex(
                name: "IX_ToDoItems_ToDoListId",
                table: "ToDoItems",
                newName: "IX_ToDoItems_ToDoListModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_ToDoItems_ToDoLists_ToDoListModelId",
                table: "ToDoItems",
                column: "ToDoListModelId",
                principalTable: "ToDoLists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
