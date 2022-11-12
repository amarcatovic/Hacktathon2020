using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Unjumble.EntityFrameworkCore.Migrations
{
    public partial class AddDocumentation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Documentation",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    PdfUrl = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    StarCount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Documentation", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "documentationPieces",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    DocumentationId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Purpose = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_documentationPieces", x => x.Id);
                    table.ForeignKey(
                        name: "FK_documentationPieces_Documentation_DocumentationId",
                        column: x => x.DocumentationId,
                        principalTable: "Documentation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_documentationPieces_DocumentationId",
                table: "documentationPieces",
                column: "DocumentationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "documentationPieces");

            migrationBuilder.DropTable(
                name: "Documentation");
        }
    }
}
