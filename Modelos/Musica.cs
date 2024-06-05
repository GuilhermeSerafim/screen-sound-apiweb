namespace ScreenSound.Modelos;

internal class Musica
{
    public Musica(string nome)
    {
        Nome = nome;
    }

    public string Nome { get; set; }
    public int Id { get; set; }
    public int? AnoLancamento { get; set; }
    public Artista? Artista { get; set; }

    public void ExibirFichaTecnica()
    {
        Console.WriteLine($"Nome: {Nome}, Ano de lançamento: {AnoLancamento} do Artista {Artista}");
      
    }

    public override string ToString()
    {
        return @$"Id: {Id}
        Ano: {AnoLancamento}
        Nome: {Nome}
        Artista: {Artista}";
    }
}