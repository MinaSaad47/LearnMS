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
            migrationBuilder.CreateIndex(
                name: "IX_StudentCredit_AssistantId",
                table: "StudentCredit",
                column: "AssistantId");

            migrationBuilder.CreateIndex(
                name: "IX_CreditCodes_SellerId",
                table: "CreditCodes",
                column: "SellerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_StudentCredit_AssistantId",
                table: "StudentCredit");

            migrationBuilder.DropIndex(
                name: "IX_CreditCodes_SellerId",
                table: "CreditCodes");
        }
    }
}
