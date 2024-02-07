using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LearnMS.API.Data.Migrations
{
    /// <inheritdoc />
    public partial class Migrations_6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CreditCodes_Assistants_AssistantId",
                table: "CreditCodes");

            migrationBuilder.DropForeignKey(
                name: "FK_CreditCodes_Students_StudentId",
                table: "CreditCodes");

            migrationBuilder.AddForeignKey(
                name: "FK_CreditCodes_Assistants_AssistantId",
                table: "CreditCodes",
                column: "AssistantId",
                principalTable: "Assistants",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_CreditCodes_Students_StudentId",
                table: "CreditCodes",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CreditCodes_Assistants_AssistantId",
                table: "CreditCodes");

            migrationBuilder.DropForeignKey(
                name: "FK_CreditCodes_Students_StudentId",
                table: "CreditCodes");

            migrationBuilder.AddForeignKey(
                name: "FK_CreditCodes_Assistants_AssistantId",
                table: "CreditCodes",
                column: "AssistantId",
                principalTable: "Assistants",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CreditCodes_Students_StudentId",
                table: "CreditCodes",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id");
        }
    }
}
