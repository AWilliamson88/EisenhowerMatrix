using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataModels.Migrations
{
    public partial class ToDoPropertyUpdates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Priority",
                table: "ToDoItems");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "ToDoLists",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "ToDoItems",
                newName: "Title");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "ToDoItems",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "ToDoItems");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "ToDoLists",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "ToDoItems",
                newName: "Name");

            migrationBuilder.AddColumn<int>(
                name: "Priority",
                table: "ToDoItems",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
