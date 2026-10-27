using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LuckyRest.Migrations
{
    /// <inheritdoc />
    public partial class AddedSpawnCounts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CounterTerroristSpawns",
                table: "Maps",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TerroristSpawns",
                table: "Maps",
                type: "integer",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CounterTerroristSpawns",
                table: "Maps");

            migrationBuilder.DropColumn(
                name: "TerroristSpawns",
                table: "Maps");
        }
    }
}
