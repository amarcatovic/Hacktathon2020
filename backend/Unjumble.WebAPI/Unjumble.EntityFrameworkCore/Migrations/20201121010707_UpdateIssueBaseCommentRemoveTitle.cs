using Microsoft.EntityFrameworkCore.Migrations;

namespace Unjumble.EntityFrameworkCore.Migrations
{
    public partial class UpdateIssueBaseCommentRemoveTitle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Title",
                table: "IssueBaseComments");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "IssueBaseComments",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: false,
                defaultValue: "");
        }
    }
}
