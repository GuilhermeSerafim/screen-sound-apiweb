namespace ScreenSound5.Banco;

using Microsoft.Data.SqlClient;
using ScreenSound.Modelos;

// O DAL é a camada de acesso a dados que promove a abstração desses dados e vai emitir todos os comandos de SELECT, INSERT, UPDATE E DELETE
// de maneira separada da lógica das classes do projeto e independente da fonte de dados
// , enquanto o DAO é um objeto do banco de dados que representa um banco aberto.
internal class ArtistaDAL
{
    // Indica que o método retorna uma coleção de Artista que pode ser iterada.
    public IEnumerable<Artista> Listar()
    {
        var lista = new List<Artista>();
        using var connection = new Connection().ObterConexao(); // O using faz a conexão, assim que obtida, descartada, para melhor gerenciamento de recursos
        connection.Open();

        SqlCommand command = new("SELECT * FROM ARTISTAS", connection);  // Representa a instrução SQL que será executada no banco de dados;
        using SqlDataReader dataReader = command.ExecuteReader(); // Fornece um modo de ler as linhas do banco de dados.

        while (dataReader.Read()) // Leitura das informações
        {
            // Extraindo valores das colunas
            // O método ToString() retorna uma string formatada com os valores das propriedades do objeto Artista. Por exemplo:
            string nomeArtista = Convert.ToString(dataReader["Nome"])!;
            string BioArtista = Convert.ToString(dataReader["Bio"])!;
            int IdArtista = Convert.ToInt32(dataReader["Id"])!;
            // Cria uma nova instância de Artista e adiciona à lista lista.
            Artista artista = new(nomeArtista, BioArtista) { Id = IdArtista }; // { x = x } - Inicializadores de Propriedades
            lista.Add(artista);
        }
        return lista;
    }
    public void Adicionar(Artista artista)
    {
        using var connection = new Connection().ObterConexao(); // O using faz a conexão, assim que obtida, descartada, para melhor gerenciamento de recursos
        connection.Open();
        SqlCommand command = new("INSERT INTO Artistas (Nome, FotoPerfil, Bio) VALUES (@nome, @perfilPadrao, @bio)", connection);

        // Parameters é uma propriedade de SqlCommand que retorna uma coleção de parâmetros 
        // AddWithValue adiciona um novo parâmetro à coleção de parâmetros do comando SQL e define seu valor.
        command.Parameters.AddWithValue("@nome", artista.Nome);
        command.Parameters.AddWithValue("@perfilPadrao", artista.FotoPerfil);
        command.Parameters.AddWithValue("@bio", artista.Bio);

        int retorno = command.ExecuteNonQuery();
        Console.WriteLine($"Linhas afetadas: {retorno}");
    }
}
