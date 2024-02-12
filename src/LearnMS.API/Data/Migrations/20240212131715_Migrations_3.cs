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
            migrationBuilder.DropForeignKey(
                name: "FK_CreditCodes_Assistants_AssistantId",
                table: "CreditCodes");

            migrationBuilder.RenameColumn(
                name: "AssistantId",
                table: "CreditCodes",
                newName: "GeneratorId");

            migrationBuilder.CreateIndex(
                name: "IX_CreditCodes_Code",
                table: "CreditCodes",
                column: "Code",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CreditCodes_Assistants_GeneratorId",
                table: "CreditCodes",
                column: "GeneratorId",
                principalTable: "Assistants",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CreditCodes_Assistants_GeneratorId",
                table: "CreditCodes");

            migrationBuilder.DropIndex(
                name: "IX_CreditCodes_Code",
                table: "CreditCodes");

            migrationBuilder.RenameColumn(
                name: "GeneratorId",
                table: "CreditCodes",
                newName: "AssistantId");

            migrationBuilder.AddForeignKey(
                name: "FK_CreditCodes_Assistants_AssistantId",
                table: "CreditCodes",
                column: "AssistantId",
                principalTable: "Assistants",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
