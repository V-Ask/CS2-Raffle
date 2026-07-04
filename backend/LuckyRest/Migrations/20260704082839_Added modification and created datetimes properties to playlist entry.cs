using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LuckyRest.Migrations
{
    /// <inheritdoc />
    public partial class Addedmodificationandcreateddatetimespropertiestoplaylistentry : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlaylistMaps_Maps_WorkshopMapId",
                table: "PlaylistMaps");

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "Playlists",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Modified",
                table: "Playlists",
                type: "timestamp with time zone",
                nullable: true);

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

            migrationBuilder.DropColumn(
                name: "Created",
                table: "Playlists");

            migrationBuilder.DropColumn(
                name: "Modified",
                table: "Playlists");

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
