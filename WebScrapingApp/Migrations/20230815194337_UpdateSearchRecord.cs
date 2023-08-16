using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebScrapingApp.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSearchRecord : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SearchResultItem");

            migrationBuilder.DropTable(
                name: "SearchResults");

            migrationBuilder.CreateTable(
                name: "SearchRecords",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SearchTerm = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SearchUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SelectedSearchEngine = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PositionOfInitialOccurrence = table.Column<int>(type: "int", nullable: false),
                    DateSearched = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SearchRecords", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SearchRecords");

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
                    SearchResultSearchId = table.Column<int>(type: "int", nullable: false),
                    Position = table.Column<int>(type: "int", nullable: false),
                    SearchId = table.Column<int>(type: "int", nullable: false)
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
    }
}
