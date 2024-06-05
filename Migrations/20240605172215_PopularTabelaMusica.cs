using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ScreenSound5.Migrations
{
    /// <inheritdoc />
    public partial class PopularTabelaMusica : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData("Musicas", new string[] { "Nome", "AnoLancamento" }, new object[] { "Best Friend", 2019 });
            migrationBuilder.InsertData("Musicas", new string[] { "Nome", "AnoLancamento" }, new object[] { "The Message", 2019 });
            migrationBuilder.InsertData("Musicas", new string[] { "Nome", "AnoLancamento" }, new object[] { "N.Y. State of Mind", 2017 });
            migrationBuilder.InsertData("Musicas", new string[] { "Nome", "AnoLancamento" }, new object[] { "Hypnotize", 2022 });
            migrationBuilder.InsertData("Musicas", new string[] { "Nome", "AnoLancamento" }, new object[] { "We Don't Trust You", 2024 });

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM MUSICAS"); // Para deletar todos os registros de uma tabela
        }
    }
}
