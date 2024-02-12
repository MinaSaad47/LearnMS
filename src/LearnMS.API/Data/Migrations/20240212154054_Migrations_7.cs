using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LearnMS.API.Data.Migrations
{
    /// <inheritdoc />
    public partial class Migrations_7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Credit",
                table: "Students");

            migrationBuilder.AddColumn<DateTime>(
                name: "ClaimedAt",
                table: "CreditCodes",
                type: "timestamp with time zone",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClaimedAt",
                table: "CreditCodes");

            migrationBuilder.AddColumn<decimal>(
                name: "Credit",
                table: "Students",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
