using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using ScreenSound.Modelos;

namespace ScreenSound5.Banco;

internal class ScreenSoundContext : DbContext
{
    // protected: Acessível dentro da própria classe e por subclasses. 
    private string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=ScreenSound5V0;Integrated Security=True;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // O carregamento lento é uma técnica que carrega dados apenas quando eles são necessários, o que é ideal para otimizar o desempenho e o uso de recursos.
        optionsBuilder
            .UseSqlServer(connectionString)
            .UseLazyLoadingProxies(); // // Habilita proxies de carregamento lento
    }

    // Artistas - Precisa ter o mesmo nome da tabela
    // DbSet - Abstração do Entity framework que permite interagir com a tabelade forma concisa
    public DbSet<Artista> Artistas { get; set; }
    public DbSet<Musica> Musicas { get; set; }
}