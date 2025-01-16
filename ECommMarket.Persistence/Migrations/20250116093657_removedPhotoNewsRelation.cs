using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ECommMarket.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class removedPhotoNewsRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NewsPhoto");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NewsPhoto",
                columns: table => new
                {
                    NewsId = table.Column<int>(type: "int", nullable: false),
                    PhotoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NewsPhoto", x => new { x.NewsId, x.PhotoId });
                    table.ForeignKey(
                        name: "FK_NewsPhoto_News_NewsId",
                        column: x => x.NewsId,
                        principalTable: "News",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NewsPhoto_Photos_PhotoId",
                        column: x => x.PhotoId,
                        principalTable: "Photos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NewsPhoto_PhotoId",
                table: "NewsPhoto",
                column: "PhotoId");
        }
    }
}
