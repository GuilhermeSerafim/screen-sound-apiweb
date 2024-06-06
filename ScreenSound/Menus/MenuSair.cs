using ScreenSound.Modelos;
using ScreenSound.Banco;

namespace ScreenSound.Menus;

internal class MenuSair : Menu
{
    public override void Executar(GenericDAL<Artista> artistaDAL, GenericDAL<Musica> musicaDAL)
    {
        Console.WriteLine("Tchau tchau :)");
    }
}
