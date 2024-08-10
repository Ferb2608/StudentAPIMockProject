using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RepositoryLayer.Migrations
{
    /// <inheritdoc />
    public partial class config_studentcourses_v2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentInCourses_Courses_CourseId1",
                table: "StudentInCourses");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentInCourses_Students_StudentId1",
                table: "StudentInCourses");

            migrationBuilder.DropIndex(
                name: "IX_StudentInCourses_CourseId1",
                table: "StudentInCourses");

            migrationBuilder.DropIndex(
                name: "IX_StudentInCourses_StudentId1",
                table: "StudentInCourses");

            migrationBuilder.DropColumn(
                name: "CourseId1",
                table: "StudentInCourses");

            migrationBuilder.DropColumn(
                name: "StudentId1",
                table: "StudentInCourses");

            migrationBuilder.AlterColumn<int>(
                name: "StudentId",
                table: "StudentInCourses",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "CourseId",
                table: "StudentInCourses",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_StudentInCourses_CourseId",
                table: "StudentInCourses",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentInCourses_StudentId",
                table: "StudentInCourses",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentInCourses_Courses_CourseId",
                table: "StudentInCourses",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentInCourses_Students_StudentId",
                table: "StudentInCourses",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentInCourses_Courses_CourseId",
                table: "StudentInCourses");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentInCourses_Students_StudentId",
                table: "StudentInCourses");

            migrationBuilder.DropIndex(
                name: "IX_StudentInCourses_CourseId",
                table: "StudentInCourses");

            migrationBuilder.DropIndex(
                name: "IX_StudentInCourses_StudentId",
                table: "StudentInCourses");

            migrationBuilder.AlterColumn<string>(
                name: "StudentId",
                table: "StudentInCourses",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "CourseId",
                table: "StudentInCourses",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "CourseId1",
                table: "StudentInCourses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "StudentId1",
                table: "StudentInCourses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_StudentInCourses_CourseId1",
                table: "StudentInCourses",
                column: "CourseId1");

            migrationBuilder.CreateIndex(
                name: "IX_StudentInCourses_StudentId1",
                table: "StudentInCourses",
                column: "StudentId1");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentInCourses_Courses_CourseId1",
                table: "StudentInCourses",
                column: "CourseId1",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentInCourses_Students_StudentId1",
                table: "StudentInCourses",
                column: "StudentId1",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
