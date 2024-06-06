using ScreenSound.Menus;
using ScreenSound.Modelos;
using ScreenSound5.Banco;

internal class MenuMostrarMusicasPorAno : Menu
{
    public override void Executar(GenericDAL<Artista> artistaDAL, GenericDAL<Musica> musicaDAL)
    {
        base.Executar(artistaDAL, musicaDAL);
        ExibirTituloDaOpcao("Exibir musicas por ano de lançamento");
        Console.Write("Digite o ano de lançamento: ");
        int anoDeLancamentoRequerido = Convert.ToInt32(Console.ReadLine()!);
        var musicasRecuperadas = musicaDAL.RecuperarListaDeObjPor(musica =>
        musica.AnoLancamento == anoDeLancamentoRequerido)
            .ToList(); // -> A execução da consulta é forçada imediatamente, carregando todos os dados em uma lista.
        if (musicasRecuperadas.Any())
        {
            Console.WriteLine("Músicas recuperadas");
            foreach (var musica in musicasRecuperadas)
            {
                musica.ExibirFichaTecnica();
            }
            Console.WriteLine("\nDigite uma tecla para voltar ao menu principal");
            Console.ReadKey();
            Console.Clear();
        }
        else
        {
            Console.WriteLine($"\nNenhuma música foi encontrada no ano {anoDeLancamentoRequerido}");
            Console.WriteLine("\nDigite uma tecla para voltar ao menu principal");
            Console.ReadKey();
            Console.Clear();
        }
    }
}