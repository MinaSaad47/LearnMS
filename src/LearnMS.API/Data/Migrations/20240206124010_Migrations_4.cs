using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LearnMS.API.Data.Migrations
{
    /// <inheritdoc />
    public partial class Migrations_4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "VideoEmbed",
                table: "Lessons",
                newName: "VideoSrc");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "VideoSrc",
                table: "Lessons",
                newName: "VideoEmbed");
        }
    }
}
