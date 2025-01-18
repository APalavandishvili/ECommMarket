using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ECommMarket.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class OneToManyPhotoNews : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NewsPhoto");

            migrationBuilder.AddColumn<int>(
                name: "PhotosId",
                table: "News",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_News_PhotosId",
                table: "News",
                column: "PhotosId");

            migrationBuilder.AddForeignKey(
                name: "FK_News_Photos_PhotosId",
                table: "News",
                column: "PhotosId",
                principalTable: "Photos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_News_Photos_PhotosId",
                table: "News");

            migrationBuilder.DropIndex(
                name: "IX_News_PhotosId",
                table: "News");

            migrationBuilder.DropColumn(
                name: "PhotosId",
                table: "News");

            migrationBuilder.CreateTable(
                name: "NewsPhoto",
                columns: table => new
                {
                    NewsId = table.Column<int>(type: "int", nullable: false),
                    PhotosId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NewsPhoto", x => new { x.NewsId, x.PhotosId });
                    table.ForeignKey(
                        name: "FK_NewsPhoto_News_NewsId",
                        column: x => x.NewsId,
                        principalTable: "News",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NewsPhoto_Photos_PhotosId",
                        column: x => x.PhotosId,
                        principalTable: "Photos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NewsPhoto_PhotosId",
                table: "NewsPhoto",
                column: "PhotosId");
        }
    }
}
