using ScreenSound.Modelos;

public class Artista
{
    public Artista(string nome, string bio, string? fotoPerfil = null) // Isso significa que se nenhum valor for fornecido para fotoPerfil ao chamar o construtor, ele será null
    {
        Nome = nome;
        Bio = bio;
        // Se o valor não for fornecido, terá uma foto por padrão
        FotoPerfil = fotoPerfil ?? "https://cdn.pixabay.com/photo/2016/08/08/09/17/avatar-1577909_1280.png";
    }

    public Artista() { }

    public string Nome { get; set; }
    public string FotoPerfil { get; set; }
    public string Bio { get; set; }
    public int Id { get; set; }
    public virtual ICollection<Musica> Musicas { get; set; } = new List<Musica>();

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
