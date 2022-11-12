using Microsoft.EntityFrameworkCore.Migrations;

namespace Unjumble.EntityFrameworkCore.Migrations
{
    public partial class UpdateDocumentationWithComanyId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CompanyId",
                table: "Documentation",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Documentation_CompanyId",
                table: "Documentation",
                column: "CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Documentation_Companies_CompanyId",
                table: "Documentation",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Documentation_Companies_CompanyId",
                table: "Documentation");

            migrationBuilder.DropIndex(
                name: "IX_Documentation_CompanyId",
                table: "Documentation");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "Documentation");
        }
    }
}
