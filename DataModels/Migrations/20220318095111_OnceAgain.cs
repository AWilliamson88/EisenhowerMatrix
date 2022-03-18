using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataModels.Migrations
{
    public partial class OnceAgain : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ToDoItems_ToDoLists_ToDoListId",
                table: "ToDoItems");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "ToDoLists",
                newName: "ToDoListId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "ToDoItems",
                newName: "ToDoItemId");

            migrationBuilder.AlterColumn<int>(
                name: "ToDoListId",
                table: "ToDoItems",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_ToDoItems_ToDoLists_ToDoListId",
                table: "ToDoItems",
                column: "ToDoListId",
                principalTable: "ToDoLists",
                principalColumn: "ToDoListId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ToDoItems_ToDoLists_ToDoListId",
                table: "ToDoItems");

            migrationBuilder.RenameColumn(
                name: "ToDoListId",
                table: "ToDoLists",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "ToDoItemId",
                table: "ToDoItems",
                newName: "Id");

            migrationBuilder.AlterColumn<int>(
                name: "ToDoListId",
                table: "ToDoItems",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ToDoItems_ToDoLists_ToDoListId",
                table: "ToDoItems",
                column: "ToDoListId",
                principalTable: "ToDoLists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
