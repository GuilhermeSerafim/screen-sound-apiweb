using ScreenSound.Modelos;

namespace ScreenSound5.Banco;

internal class MusicaDAO : DAL<Musica>
{
    //  : base(context) - Inicializa a classe base com o contexto do banco de dados fornecido, garantindo que a classe DAL<T> tenha acesso ao mesmo contexto.
    public MusicaDAO(ScreenSoundContext context) : base(context)
    {
        // Indica que não há nenhuma lógica adicional específica para o construtor de MusicaDAO além da chamada ao construtor da classe base.
    }

    public override Musica? RecuperarObjPeloNome(string nome)
    {
        var listaMusicas = Listar().ToList();
        var musicaRecuperadaPeloNome = listaMusicas.Find(musica => musica.Nome == nome);
        if(musicaRecuperadaPeloNome == null)
        {
            Console.WriteLine("Artista não encontrado");
            return null;
        }
        else
        {
            return musicaRecuperadaPeloNome;
        }

    }
}
