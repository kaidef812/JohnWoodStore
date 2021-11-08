using Microsoft.EntityFrameworkCore.Migrations;

namespace JohnWoodStore.Migrations
{
    public partial class AddAddressInuser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OrganizationAddress",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrganizationAddress",
                table: "Users");
        }
    }
}
