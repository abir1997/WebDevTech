using Microsoft.EntityFrameworkCore.Migrations;

namespace CourseManagement.Migrations
{
    public partial class renamedIDs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Students",
                newName: "StudentID");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Instructors",
                newName: "InstructorID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StudentID",
                table: "Students",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "InstructorID",
                table: "Instructors",
                newName: "ID");
        }
    }
}
