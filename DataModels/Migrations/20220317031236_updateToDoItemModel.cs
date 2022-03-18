using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataModels.Migrations
{
    public partial class updateToDoItemModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ToDoItems_ToDoLists_ListId",
                table: "ToDoItems");

            migrationBuilder.DropIndex(
                name: "IX_ToDoItems_ListId",
                table: "ToDoItems");

            migrationBuilder.DropColumn(
                name: "ListId",
                table: "ToDoItems");

            migrationBuilder.RenameColumn(
                name: "ToDoListId",
                table: "ToDoItems",
                newName: "ToDoListModelId");

            migrationBuilder.CreateIndex(
                name: "IX_ToDoItems_ToDoListModelId",
                table: "ToDoItems",
                column: "ToDoListModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_ToDoItems_ToDoLists_ToDoListModelId",
                table: "ToDoItems",
                column: "ToDoListModelId",
                principalTable: "ToDoLists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ToDoItems_ToDoLists_ToDoListModelId",
                table: "ToDoItems");

            migrationBuilder.DropIndex(
                name: "IX_ToDoItems_ToDoListModelId",
                table: "ToDoItems");

            migrationBuilder.RenameColumn(
                name: "ToDoListModelId",
                table: "ToDoItems",
                newName: "ToDoListId");

            migrationBuilder.AddColumn<int>(
                name: "ListId",
                table: "ToDoItems",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ToDoItems_ListId",
                table: "ToDoItems",
                column: "ListId");

            migrationBuilder.AddForeignKey(
                name: "FK_ToDoItems_ToDoLists_ListId",
                table: "ToDoItems",
                column: "ListId",
                principalTable: "ToDoLists",
                principalColumn: "Id");
        }
    }
}
