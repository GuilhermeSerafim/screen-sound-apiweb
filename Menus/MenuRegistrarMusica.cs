using ScreenSound.Modelos;
using ScreenSound5.Banco;

namespace ScreenSound.Menus;

internal class MenuRegistrarMusica : Menu
{
    public override void Executar(GenericDAL<Artista> artistaDAL, GenericDAL<Musica> musicaDAL)
    {
        base.Executar(artistaDAL, musicaDAL);
        ExibirTituloDaOpcao("Registro de músicas");
        Console.Write("Digite o artista cuja música deseja registrar: ");
        string nomeDoArtista = Console.ReadLine()!;
        var artistaRecuperado = artistaDAL.RecuperarObjPor(artista => artista.Nome.Equals(nomeDoArtista));
        if (artistaRecuperado is not null)
        {
            Console.Write("Agora digite o título da música: ");
            string tituloDaMusica = Console.ReadLine()!;
            musicaDAL.Adicionar(new Musica(tituloDaMusica));
            Console.WriteLine($"A música {tituloDaMusica} de {artistaRecuperado.Nome} foi registrada com sucesso!");
            Thread.Sleep(4000);
            Console.Clear();
        }
        else
        {
            Console.WriteLine($"\nO artista {nomeDoArtista} não foi encontrado!");
            Console.WriteLine("Digite uma tecla para voltar ao menu principal");
            Console.ReadKey();
            Console.Clear();
        }
    }
}
