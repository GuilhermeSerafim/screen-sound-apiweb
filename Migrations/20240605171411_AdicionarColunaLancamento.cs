using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ScreenSound5.Migrations
{
    /// <inheritdoc />
    public partial class AdicionarColunaLancamento : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AnoLancamento",
                table: "Musicas",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AnoLancamento",
                table: "Musicas");
        }
    }
}
