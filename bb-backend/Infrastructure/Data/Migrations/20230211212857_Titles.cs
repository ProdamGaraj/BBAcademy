using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Data.Migrations
{
    public partial class Titles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Content",
                table: "Questions",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Exams",
                newName: "Title");

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Courses",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Title",
                table: "Courses");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Questions",
                newName: "Content");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Exams",
                newName: "Description");
        }
    }
}
