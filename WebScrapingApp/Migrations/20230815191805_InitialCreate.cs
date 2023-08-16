using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebScrapingApp.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SearchResults",
                columns: table => new
                {
                    SearchId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SearchTerm = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SearchUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SelectedSearchEngine = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SearchResults", x => x.SearchId);
                });

            migrationBuilder.CreateTable(
                name: "SearchResultItem",
                columns: table => new
                {
                    PositionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Position = table.Column<int>(type: "int", nullable: false),
                    SearchId = table.Column<int>(type: "int", nullable: false),
                    SearchResultSearchId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SearchResultItem", x => x.PositionId);
                    table.ForeignKey(
                        name: "FK_SearchResultItem_SearchResults_SearchResultSearchId",
                        column: x => x.SearchResultSearchId,
                        principalTable: "SearchResults",
                        principalColumn: "SearchId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SearchResultItem_SearchResultSearchId",
                table: "SearchResultItem",
                column: "SearchResultSearchId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SearchResultItem");

            migrationBuilder.DropTable(
                name: "SearchResults");
        }
    }
}
