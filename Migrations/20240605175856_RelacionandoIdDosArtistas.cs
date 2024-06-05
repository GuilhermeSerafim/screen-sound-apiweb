using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ScreenSound5.Migrations
{
    /// <inheritdoc />
    public partial class RelacionandoIdDosArtistas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("UPDATE Musicas SET ArtistaId = 1 WHERE Id = 1");
            migrationBuilder.Sql("UPDATE Musicas SET ArtistaId = 2 WHERE Id = 2");
            migrationBuilder.Sql("UPDATE Musicas SET ArtistaId = 3 WHERE Id = 3");
            migrationBuilder.Sql("UPDATE Musicas SET ArtistaId = 4 WHERE Id = 4");
            migrationBuilder.Sql("UPDATE Musicas SET ArtistaId = 5 WHERE Id = 5");
        }
        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM MUSICAS");

        }
    }
}
