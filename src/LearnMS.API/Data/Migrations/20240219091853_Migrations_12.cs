using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LearnMS.API.Data.Migrations
{
    /// <inheritdoc />
    public partial class Migrations_12 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsReady",
                table: "Lessons");

            migrationBuilder.AddColumn<string>(
                name: "VideoStatus",
                table: "Lessons",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VideoStatus",
                table: "Lessons");

            migrationBuilder.AddColumn<bool>(
                name: "IsReady",
                table: "Lessons",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
