using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Unjumble.EntityFrameworkCore.Migrations
{
    public partial class AddIssueBaseEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "IssueBases",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(nullable: false),
                    Content = table.Column<string>(nullable: false),
                    CompanyId = table.Column<string>(nullable: false),
                    UserId = table.Column<string>(nullable: false),
                    Code = table.Column<byte>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IssueBases", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IssueBases_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IssueBases_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IssueBaseComments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(nullable: false),
                    Content = table.Column<string>(nullable: false),
                    IssueBaseId = table.Column<int>(nullable: false),
                    UserId = table.Column<string>(nullable: false),
                    Code = table.Column<byte>(nullable: false),
                    CompanyId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IssueBaseComments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IssueBaseComments_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_IssueBaseComments_IssueBases_IssueBaseId",
                        column: x => x.IssueBaseId,
                        principalTable: "IssueBases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IssueBaseComments_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_IssueBaseComments_CompanyId",
                table: "IssueBaseComments",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_IssueBaseComments_IssueBaseId",
                table: "IssueBaseComments",
                column: "IssueBaseId");

            migrationBuilder.CreateIndex(
                name: "IX_IssueBaseComments_UserId",
                table: "IssueBaseComments",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_IssueBases_CompanyId",
                table: "IssueBases",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_IssueBases_UserId",
                table: "IssueBases",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IssueBaseComments");

            migrationBuilder.DropTable(
                name: "IssueBases");
        }
    }
}
