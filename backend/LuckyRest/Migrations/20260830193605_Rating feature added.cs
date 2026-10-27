using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LuckyRest.Migrations
{
    /// <inheritdoc />
    public partial class Ratingfeatureadded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Rating",
                table: "PlaylistMaps",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Rating",
                table: "PlaylistMaps");
        }
    }
}
