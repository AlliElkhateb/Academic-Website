using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAppRepositoryWithUOW.EF.Migrations
{
    public partial class AddStudentDegreeToTableStudentCourse : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StudentDegree",
                table: "Courses");

            migrationBuilder.AddColumn<int>(
                name: "StudentDegree",
                table: "StudentCourses",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StudentDegree",
                table: "StudentCourses");

            migrationBuilder.AddColumn<int>(
                name: "StudentDegree",
                table: "Courses",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
