using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataModels.Migrations
{
    public partial class addIsCompleteVariable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsComplete",
                table: "ToDoItems",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsComplete",
                table: "ToDoItems");
        }
    }
}
