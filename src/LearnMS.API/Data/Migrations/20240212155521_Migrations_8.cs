using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LearnMS.API.Data.Migrations
{
    /// <inheritdoc />
    public partial class Migrations_8 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Credit",
                table: "Students",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Credit",
                table: "Students");
        }
    }
}
