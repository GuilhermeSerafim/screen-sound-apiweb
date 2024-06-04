using ScreenSound.Modelos;
using ScreenSound5.Banco;

namespace ScreenSound.Menus;

internal class MenuSair : Menu
{
    public override void Executar(ArtistaDAO artistaDAO, MusicaDAO musicaDAO)
    {
        Console.WriteLine("Tchau tchau :)");
    }
}
