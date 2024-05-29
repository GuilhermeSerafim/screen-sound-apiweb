using Microsoft.Data.SqlClient;
using ScreenSound.Modelos;

namespace ScreenSound5.Banco;

internal class Connection
{
    private string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=ScreenSound5;Integrated Security=True;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
    public SqlConnection ObterConexao()
    {
        return new SqlConnection(connectionString);
    }

    // Indica que o método retorna uma coleção de Artista que pode ser iterada.
    public IEnumerable<Artista> Listar()
    {
        var lista = new List<Artista>();
        using var connection = ObterConexao();
        connection.Open();

        string selectAllArtist = "SELECT * FROM ARTISTAS"; // Define uma string SQL que seleciona todos os registros da tabela ARTISTAS
        SqlCommand command = new(selectAllArtist, connection); 
        using SqlDataReader dataReader = command.ExecuteReader(); // SqlDataReader - Para ler os dados retornados. 

        while (dataReader.Read()) // Leitura das informações
        {
            // Extraindo valores das colunas
            string nomeArtista = Convert.ToString(dataReader["Nome"])!;
            string BioArtista = Convert.ToString(dataReader["Bio"])!;
            int IdArtista = Convert.ToInt32(dataReader["Id"])!;
            // Cria uma nova instância de Artista e adiciona à lista lista.
            Artista artista = new(nomeArtista, BioArtista) { Id = IdArtista }; // { x = x } - Inicializadores de Propriedades
            lista.Add(artista);
        }
        return lista;
    }
}
