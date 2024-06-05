using ScreenSound.Modelos;

namespace ScreenSound5.Banco;

internal abstract class DAL<T> // T - Tipo generico
{
    public abstract IEnumerable<T> Listar();
    public abstract void Adicionar(T obj);
    public abstract void Deletar(int id);
    public abstract void Atualizar(T obj);
    public abstract T? RecuperarObjPeloNome(string nome);
}
