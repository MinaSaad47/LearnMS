using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LearnMS.API.Data.Migrations
{
    /// <inheritdoc />
    public partial class Migrations_10 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ExpirationDays",
                table: "Lectures",
                type: "INTEGER",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExpirationDays",
                table: "Lectures");
        }
    }
}
