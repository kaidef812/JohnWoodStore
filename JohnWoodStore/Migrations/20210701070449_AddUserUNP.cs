using Microsoft.EntityFrameworkCore.Migrations;

namespace JohnWoodStore.Migrations
{
    public partial class AddUserUNP : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UNP",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UNP",
                table: "Users");
        }
    }
}
