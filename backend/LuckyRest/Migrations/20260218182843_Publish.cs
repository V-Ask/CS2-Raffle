using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LuckyRest.Migrations
{
    /// <inheritdoc />
    public partial class Publish : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlaylistMaps_Maps_WorkshopMapId",
                table: "PlaylistMaps");

            migrationBuilder.AddForeignKey(
                name: "FK_PlaylistMaps_Maps_WorkshopMapId",
                table: "PlaylistMaps",
                column: "WorkshopMapId",
                principalTable: "Maps",
                principalColumn: "WorkshopMapId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlaylistMaps_Maps_WorkshopMapId",
                table: "PlaylistMaps");

            migrationBuilder.AddForeignKey(
                name: "FK_PlaylistMaps_Maps_WorkshopMapId",
                table: "PlaylistMaps",
                column: "WorkshopMapId",
                principalTable: "Maps",
                principalColumn: "WorkshopMapId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
