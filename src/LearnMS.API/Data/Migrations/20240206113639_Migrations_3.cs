using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LearnMS.API.Data.Migrations
{
    /// <inheritdoc />
    public partial class Migrations_3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "CourseItem");

            migrationBuilder.AddColumn<bool>(
                name: "IsPublished",
                table: "Courses",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsPublished",
                table: "CourseItem",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPublished",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "IsPublished",
                table: "CourseItem");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Courses",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "CourseItem",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
