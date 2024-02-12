using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LearnMS.API.Data.Migrations
{
    /// <inheritdoc />
    public partial class Migrations_2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "GeneratedAt",
                table: "CreditCodes",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "RedeemedAt",
                table: "CreditCodes",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "SellerId",
                table: "CreditCodes",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "SoldAt",
                table: "CreditCodes",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CreditCodes_Assistants_SellerId",
                table: "CreditCodes",
                column: "SellerId",
                principalTable: "Assistants",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CreditCodes_Assistants_SellerId",
                table: "CreditCodes");

            migrationBuilder.DropColumn(
                name: "GeneratedAt",
                table: "CreditCodes");

            migrationBuilder.DropColumn(
                name: "RedeemedAt",
                table: "CreditCodes");

            migrationBuilder.DropColumn(
                name: "SellerId",
                table: "CreditCodes");

            migrationBuilder.DropColumn(
                name: "SoldAt",
                table: "CreditCodes");
        }
    }
}
