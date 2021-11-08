using Microsoft.EntityFrameworkCore.Migrations;

namespace JohnWoodStore.Migrations
{
    public partial class TimeOfToDateOf : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TimeOfOrdering",
                table: "Orders",
                newName: "DateOfOrdering");

            migrationBuilder.RenameColumn(
                name: "TimeOfCheck",
                table: "Checks",
                newName: "DateOfCheck");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DateOfOrdering",
                table: "Orders",
                newName: "TimeOfOrdering");

            migrationBuilder.RenameColumn(
                name: "DateOfCheck",
                table: "Checks",
                newName: "TimeOfCheck");
        }
    }
}
