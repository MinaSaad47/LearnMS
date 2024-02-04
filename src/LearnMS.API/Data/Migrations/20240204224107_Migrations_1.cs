using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LearnMS.API.Data.Migrations
{
    /// <inheritdoc />
    public partial class Migrations_1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_StudentLecture_LectureId",
                table: "StudentLecture");

            migrationBuilder.DropIndex(
                name: "IX_StudentLecture_StudentId",
                table: "StudentLecture");

            migrationBuilder.DropIndex(
                name: "IX_StudentExam_ExamId",
                table: "StudentExam");

            migrationBuilder.DropIndex(
                name: "IX_StudentExam_StudentId",
                table: "StudentExam");

            migrationBuilder.DropIndex(
                name: "IX_StudentCourse_CourseId",
                table: "StudentCourse");

            migrationBuilder.DropIndex(
                name: "IX_StudentCourse_StudentId",
                table: "StudentCourse");

            migrationBuilder.DropIndex(
                name: "IX_LectureItem_LectureId",
                table: "LectureItem");

            migrationBuilder.DropIndex(
                name: "IX_CreditCodes_AssistantId",
                table: "CreditCodes");

            migrationBuilder.DropIndex(
                name: "IX_CreditCodes_StudentId",
                table: "CreditCodes");

            migrationBuilder.DropIndex(
                name: "IX_CourseItem_CourseId",
                table: "CourseItem");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_StudentLecture_LectureId",
                table: "StudentLecture",
                column: "LectureId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StudentLecture_StudentId",
                table: "StudentLecture",
                column: "StudentId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StudentExam_ExamId",
                table: "StudentExam",
                column: "ExamId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StudentExam_StudentId",
                table: "StudentExam",
                column: "StudentId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StudentCourse_CourseId",
                table: "StudentCourse",
                column: "CourseId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StudentCourse_StudentId",
                table: "StudentCourse",
                column: "StudentId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LectureItem_LectureId",
                table: "LectureItem",
                column: "LectureId");

            migrationBuilder.CreateIndex(
                name: "IX_CreditCodes_AssistantId",
                table: "CreditCodes",
                column: "AssistantId");

            migrationBuilder.CreateIndex(
                name: "IX_CreditCodes_StudentId",
                table: "CreditCodes",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseItem_CourseId",
                table: "CourseItem",
                column: "CourseId");
        }
    }
}
