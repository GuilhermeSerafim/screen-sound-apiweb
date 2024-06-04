using ScreenSound.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScreenSound5.Banco;

internal class MusicaDAO
{
    private readonly ScreenSoundContext _context;

    public MusicaDAO(ScreenSoundContext context)
    {
        _context = context;
    }

    public IEnumerable<Musica> ListarMusicas()
    {
        return _context.Musicas.ToList();
    } 

    public void AdicionarMusica(Musica musica)
    {
        _context.Musicas.Add(musica);
        _context.SaveChanges();
    }
    public void AtualizarMusica(Musica musica)
    {
        _context.Musicas.Update(musica);
        _context.SaveChanges();

    }

    public void DeletarMusica(Musica musica)
    {
        _context.Musicas.Remove(musica);
        _context.SaveChanges();

    }

    public Musica? RecuperarMusicaPeloNome(string nome)
    {
        return _context.Musicas.FirstOrDefault(a => a.Nome.Equals(nome));

    }
}
