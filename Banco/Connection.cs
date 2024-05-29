using Microsoft.Data.SqlClient;

namespace ScreenSound5.Banco;

internal class Connection
{
    private string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=ScreenSound5;Integrated Security=True;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
    public SqlConnection ObterConexao()
    {
        return new SqlConnection(connectionString);
    }
}   
    