using ScreenSound.Modelos;
using ScreenSound5.Banco;

namespace ScreenSound.Menus;

internal class MenuMostrarArtistas : Menu
{
    public override void Executar(GenericDAL<Artista> artistaDAL, GenericDAL<Musica> musicaDAL)
    {
        base.Executar(artistaDAL, musicaDAL);
        ExibirTituloDaOpcao("Exibindo todos os artistas registradas na nossa aplicação");

        foreach (var artista in artistaDAL.Listar())
        {
            Console.WriteLine($"Artista: {artista}");
        }

        Console.WriteLine("\nDigite uma tecla para voltar ao menu principal");
        Console.ReadKey();
        Console.Clear();
    }
}
