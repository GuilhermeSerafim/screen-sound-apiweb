namespace ScreenSound5.Banco;

using Microsoft.Data.SqlClient;
using ScreenSound.Modelos;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Diagnostics;
using System.Runtime.Intrinsics.X86;

// DAL(Data Access Layer) é uma camada inteira que pode conter múltiplos DAOs,
// serviços e utilitários para acesso aos dados.Foca na abstração do acesso ao banco de dados e pode lidar com múltiplas entidades e tipos de fontes de dados.
// DAO(Data Access Object) é um componente específico dentro da DAL
// que trata de uma entidade particular.Cada DAO é uma classe dedicada a operações de CRUD(Create, Read, Update, Delete) para uma entidade específica.
internal class ArtistaDAO
{
    // Indica que o método retorna uma coleção de Artista que pode ser iterada.
    public static IEnumerable<Artista> ListarArtista()
    {
        var lista = new List<Artista>();
        using var conexao = new Conexao().ObterConexao(); // O using faz a conexão, assim que obtida, descartada, para melhor gerenciamento de recursos
        conexao.Open();

        SqlCommand command = new("SELECT * FROM ARTISTAS", conexao);  // Representa a instrução SQL que será executada no banco de dados;
        using SqlDataReader dataReader = command.ExecuteReader(); // Fornece um modo de ler as linhas do banco de dados.

        while (dataReader.Read()) // Leitura das informações
        {
            // Extraindo valores das colunas
            // O método ToString() retorna uma string formatada com os valores das propriedades do objeto Artista. Por exemplo:
            string nomeArtista = Convert.ToString(dataReader["Nome"])!;
            int IdArtista = Convert.ToInt32(dataReader["Id"])!;
            string BioArtista = Convert.ToString(dataReader["Bio"])!;
            // Cria uma nova instância de Artista e adiciona à lista lista.
            Artista artista = new(nomeArtista, BioArtista) { Id = IdArtista }; // { x = x } - Inicializadores de Propriedades
            lista.Add(artista);
        }
        return lista;
    }
    public static void AdicionarArtista(Artista artista)
    {
        using var conexao = new Conexao().ObterConexao(); // O using faz a conexão, assim que obtida, descartada, para melhor gerenciamento de recursos
        conexao.Open();
        SqlCommand command = new("INSERT INTO Artistas (Nome, FotoPerfil, Bio) VALUES (@nome, @perfilPadrao, @bio)", conexao);

        // Parameters é uma propriedade de SqlCommand que retorna uma coleção de parâmetros 
        // AddWithValue adiciona um novo parâmetro à coleção de parâmetros do comando SQL e define seu valor.
        command.Parameters.AddWithValue("@nome", artista.Nome);
        command.Parameters.AddWithValue("@perfilPadrao", artista.FotoPerfil);
        command.Parameters.AddWithValue("@bio", artista.Bio);

        int retorno = command.ExecuteNonQuery();
        Console.WriteLine($"Linhas afetadas: {retorno}");
    }

    public static void AtualizarArtista(Artista artista)
    {
        using var conexao = new Conexao().ObterConexao();
        conexao.Open();
        SqlCommand comando = new($"UPDATE Artistas SET Nome = @nome, Bio = @bio WHERE Id = @id", conexao);
        // Parameters é uma propriedade de SqlCommand que retorna uma coleção de parâmetros 
        // AddWithValue adiciona um novo parâmetro à coleção de parâmetros do comando SQL e define seu valor.
        comando.Parameters.AddWithValue("@perfilPadrao", artista.FotoPerfil);
        comando.Parameters.AddWithValue("@bio", artista.Bio);
        comando.Parameters.AddWithValue("@nome", artista.Nome);

        int retorno = comando.ExecuteNonQuery();
        Console.WriteLine($"Linhas afetadas: {retorno}");

    }

    public static void DeletarArtista(Artista artista)
    {
        using var conexao = new Conexao().ObterConexao();
        conexao.Open();
        SqlCommand comando = new($"DELETE FROM Artistas WHERE Id = @id", conexao);
        comando.Parameters.AddWithValue("@id", artista.Id);
        // ExecuteNonQuery - Usado para executar comandos SQL que não retornam resultados 
        int linhasAfetadas = comando.ExecuteNonQuery(); // INSERT, UPDATE, DELETE e comandos que alteram a estrutura do banco de dados (CREATE TABLE, ALTER TABLE, etc.).
        Console.WriteLine($"Linhas afetadas: {linhasAfetadas}");
    }

}
