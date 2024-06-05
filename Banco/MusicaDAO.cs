using ScreenSound.Modelos;

namespace ScreenSound5.Banco;

internal class MusicaDAO : DAL<Musica>
{
    private readonly ScreenSoundContext _context;

    public MusicaDAO(ScreenSoundContext context)
    {
        _context = context;
    }

    public override IEnumerable<Musica> Listar()
    {
        return _context.Musicas.ToList();
    }

    public override void Adicionar(Musica musica)
    {
        _context.Musicas.Add(musica);
        _context.SaveChanges();
    }
    public override void Atualizar(Musica musica)
    {
        _context.Musicas.Update(musica);
        _context.SaveChanges();

    }

    public override void Deletar(int id)
    {
        var listaMusicas = _context.Musicas.ToList();
        var musicaASerDeletada = listaMusicas.Find(musica => musica.Id == id);
        if (musicaASerDeletada is not null)
        {
            Console.WriteLine($"Musica {musicaASerDeletada.Nome} do id {musicaASerDeletada.Id} removido");
            _context.Musicas.Remove(musicaASerDeletada);
            _context.SaveChanges();
        }
        else
        {
            Console.WriteLine("Musica não encontrado, informe um ID válido");
        }

    }

    public override Musica? RecuperarObjPeloNome(string nome)
    {
        return _context.Musicas.FirstOrDefault(a => a.Nome.Equals(nome));

    }
}
