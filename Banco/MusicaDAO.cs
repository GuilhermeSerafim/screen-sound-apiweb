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
        return _context.Musicas.FirstOrDefault(a => a.Nome.Equals(nome));

    }
}
