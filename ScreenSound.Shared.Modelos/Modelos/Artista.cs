namespace ScreenSound.Modelos;

public class Artista
{
    // Por que usar ICollection:
    //    Melhor encapsulamento e abstração.
    //    Facilita a criação de contratos e testes unitários mais reutilizáveis.
    //    Maior flexibilidade para alterar a implementação subjacente (Podemos mudar futuramente para HashSet<Musica>, LinkedList<Musica>, etc).


    public Artista(string nome, string bio)
    {
        Nome = nome;
        Bio = bio;
        FotoPerfil = "https://cdn.pixabay.com/photo/2016/08/08/09/17/avatar-1577909_1280.png";
    }

    public string Nome { get; set; }
    public string FotoPerfil { get; set; }
    public string Bio { get; set; }
    public int Id { get; set; }
    // Para usar lazy loading, as propriedades de navegação devem ser marcadas como virtual.
    public virtual ICollection<Musica> Musicas { get; set; } = new List<Musica>(); // Propriedade de navegação virtual

    public void AdicionarMusica(Musica musica)
    {
        Musicas.Add(musica);
    }

    public void ExibirDiscografia()
    {
        Console.WriteLine($"Discografia do artista {Nome}");
        foreach (var musica in Musicas)
        {
            Console.WriteLine($"Música: {musica.Nome}");
            Console.WriteLine($"Ano de lançamento: {musica.AnoLancamento}");
            Console.WriteLine();
        }
    }

    public override string ToString()
    {
        return $@"Id: {Id}
            Nome: {Nome}
            Foto de Perfil: {FotoPerfil}
            Bio: {Bio}";
    }
}