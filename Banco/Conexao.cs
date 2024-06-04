using Microsoft.Data.SqlClient;

namespace ScreenSound5.Banco;

internal class Conexao
{
    private string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=ScreenSound5;Integrated Security=True;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
    public SqlConnection ObterConexao() // Representa a conexão com o banco de dados;
    {
        return new SqlConnection(connectionString);
    }
}