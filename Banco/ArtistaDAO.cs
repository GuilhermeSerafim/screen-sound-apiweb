namespace ScreenSound5.Banco;
using Microsoft.Data.SqlClient;
using ScreenSound.Modelos;

// DAL(Data Access Layer) é uma CAMADA INTEIRA que pode conter múltiplos DAOs,
// serviços e utilitários para acesso aos dados.Foca na abstração do acesso ao banco de dados e pode lidar com múltiplas entidades e tipos de fontes de dados.

// DAO(Data Access Object) é um componente específico dentro da DAL
// que trata de uma entidade particular.Cada DAO é uma classe dedicada a operações de CRUD(Create, Read, Update, Delete) para uma entidade específica.
internal class ArtistaDAO : DAL<Artista>
{
    public ArtistaDAO(ScreenSoundContext _context) : base(_context) { }

    // Artista? -> Pode retornar nulo
    public override Artista? RecuperarObjPeloNome(string nome)
    {
        var listaArtistas = Listar();
        var artistaRecuperadoPeloNome = listaArtistas.ToList().Find(artista => artista.Nome.Equals(nome));
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

}
