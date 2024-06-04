using ScreenSound.Modelos;
using ScreenSound5.Banco;

namespace ScreenSound.Menus;

internal class MenuSair : Menu
{
    public override void Executar(ArtistaDAO artistaDAO)
    {
        Console.WriteLine("Tchau tchau :)");
    }
}
