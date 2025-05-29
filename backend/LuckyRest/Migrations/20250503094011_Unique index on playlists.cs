using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace LuckyRest.Migrations
{
    /// <inheritdoc />
    public partial class Uniqueindexonplaylists : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Playlists_AspNetUsers_AuthorId",
                table: "Playlists");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkshopMapWorkshopPlaylist_Playlists_PlaylistsCollectionNa~",
                table: "WorkshopMapWorkshopPlaylist");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WorkshopMapWorkshopPlaylist",
                table: "WorkshopMapWorkshopPlaylist");

            migrationBuilder.DropIndex(
                name: "IX_WorkshopMapWorkshopPlaylist_PlaylistsCollectionName",
                table: "WorkshopMapWorkshopPlaylist");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Playlists",
                table: "Playlists");

            migrationBuilder.DropColumn(
                name: "PlaylistsCollectionName",
                table: "WorkshopMapWorkshopPlaylist");

            migrationBuilder.AddColumn<int>(
                name: "PlaylistsWorkshopPlaylistId",
                table: "WorkshopMapWorkshopPlaylist",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "AuthorId",
                table: "Playlists",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "WorkshopPlaylistId",
                table: "Playlists",
                type: "integer",
                nullable: false,
                defaultValue: 0)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_WorkshopMapWorkshopPlaylist",
                table: "WorkshopMapWorkshopPlaylist",
                columns: new[] { "MapsWorkshopMapId", "PlaylistsWorkshopPlaylistId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Playlists",
                table: "Playlists",
                column: "WorkshopPlaylistId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkshopMapWorkshopPlaylist_PlaylistsWorkshopPlaylistId",
                table: "WorkshopMapWorkshopPlaylist",
                column: "PlaylistsWorkshopPlaylistId");

            migrationBuilder.CreateIndex(
                name: "IX_Playlists_CollectionName_AuthorId",
                table: "Playlists",
                columns: new[] { "CollectionName", "AuthorId" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Playlists_AspNetUsers_AuthorId",
                table: "Playlists",
                column: "AuthorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkshopMapWorkshopPlaylist_Playlists_PlaylistsWorkshopPlay~",
                table: "WorkshopMapWorkshopPlaylist",
                column: "PlaylistsWorkshopPlaylistId",
                principalTable: "Playlists",
                principalColumn: "WorkshopPlaylistId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Playlists_AspNetUsers_AuthorId",
                table: "Playlists");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkshopMapWorkshopPlaylist_Playlists_PlaylistsWorkshopPlay~",
                table: "WorkshopMapWorkshopPlaylist");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WorkshopMapWorkshopPlaylist",
                table: "WorkshopMapWorkshopPlaylist");

            migrationBuilder.DropIndex(
                name: "IX_WorkshopMapWorkshopPlaylist_PlaylistsWorkshopPlaylistId",
                table: "WorkshopMapWorkshopPlaylist");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Playlists",
                table: "Playlists");

            migrationBuilder.DropIndex(
                name: "IX_Playlists_CollectionName_AuthorId",
                table: "Playlists");

            migrationBuilder.DropColumn(
                name: "PlaylistsWorkshopPlaylistId",
                table: "WorkshopMapWorkshopPlaylist");

            migrationBuilder.DropColumn(
                name: "WorkshopPlaylistId",
                table: "Playlists");

            migrationBuilder.AddColumn<string>(
                name: "PlaylistsCollectionName",
                table: "WorkshopMapWorkshopPlaylist",
                type: "character varying(50)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "AuthorId",
                table: "Playlists",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WorkshopMapWorkshopPlaylist",
                table: "WorkshopMapWorkshopPlaylist",
                columns: new[] { "MapsWorkshopMapId", "PlaylistsCollectionName" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Playlists",
                table: "Playlists",
                column: "CollectionName");

            migrationBuilder.CreateIndex(
                name: "IX_WorkshopMapWorkshopPlaylist_PlaylistsCollectionName",
                table: "WorkshopMapWorkshopPlaylist",
                column: "PlaylistsCollectionName");

            migrationBuilder.AddForeignKey(
                name: "FK_Playlists_AspNetUsers_AuthorId",
                table: "Playlists",
                column: "AuthorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkshopMapWorkshopPlaylist_Playlists_PlaylistsCollectionNa~",
                table: "WorkshopMapWorkshopPlaylist",
                column: "PlaylistsCollectionName",
                principalTable: "Playlists",
                principalColumn: "CollectionName",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
