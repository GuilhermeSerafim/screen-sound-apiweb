namespace ScreenSound5.Banco;
using Microsoft.Data.SqlClient;
using ScreenSound.Modelos;

// DAL(Data Access Layer) é uma CAMADA INTEIRA que pode conter múltiplos DAOs,
// serviços e utilitários para acesso aos dados.Foca na abstração do acesso ao banco de dados e pode lidar com múltiplas entidades e tipos de fontes de dados.

// DAO(Data Access Object) é um componente específico dentro da DAL
// que trata de uma entidade particular.Cada DAO é uma classe dedicada a operações de CRUD(Create, Read, Update, Delete) para uma entidade específica.
internal class ArtistaDAO
{
    private readonly ScreenSoundContext _context;

    public ArtistaDAO(ScreenSoundContext context)
    {
        _context = context;
    }

    // IEnumerable - Indica que o método retorna uma coleção de Artista que pode ser iterada.
    public IEnumerable<Artista> ListarArtistas()
    {
        return _context.Artistas.ToList();
    }

    // Artista? -> Pode retornar nulo
    public Artista? RecuperarArtistaPeloNome(string nome)
    {
        var listaArtistas = _context.Artistas.ToList();
        var artistaRecuperadoPeloNome = listaArtistas.Find(artista => artista.Nome.Equals(nome));
        if (artistaRecuperadoPeloNome == null)
        {
            Console.WriteLine("Artista não encontrado");
            return null;
        }
        else
        {
            return artistaRecuperadoPeloNome;
        }
    }
    public void AdicionarArtista(Artista artista)
    {
        _context.Artistas.Add(artista);
        _context.SaveChanges(); // Salva no banco de dados
    }

    // O ID tem que ser correspondente - Demais informações serão atualizadas
    public void AtualizarArtista(Artista artista)
    {
        _context.Artistas.Update(artista);
        _context.SaveChanges();
    }

    public void DeletarArtista(int id)
    {
        var listaArtistas = _context.Artistas.ToList();
        var ArtistaASerDeletadoPorId = listaArtistas.Find(artista => artista.Id == id);
        if (ArtistaASerDeletadoPorId == null)
        {
            Console.WriteLine("Artista não encontrado");
            return;
        }
        Console.WriteLine($"Artista {ArtistaASerDeletadoPorId.Nome} removido");
        _context.Artistas.Remove(ArtistaASerDeletadoPorId!);
        _context.SaveChanges();
    }

}
