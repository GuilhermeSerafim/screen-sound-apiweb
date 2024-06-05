using ScreenSound.Modelos;

namespace ScreenSound5.Banco;

internal abstract class DAL<T> where T : class // T - Tipo generico, deve ser uma classe
{
    protected readonly ScreenSoundContext _context;

    protected DAL(ScreenSoundContext context)
    {
        _context = context;
    }

    // IEnumerable - Indica que o método retorna uma coleção de T que pode ser iterada
    public IEnumerable<T> Listar()
    {
        return _context.Set<T>().ToList(); // // Set<T>() - Obtém o DbSet<T> e converte para uma lista
    }

    // Um método abstrato é, por definição, um método que declara a assinatura mas não fornece a implementação.
    public void Adicionar(T obj)
    {
        _context.Set<T>().Add(obj);
        _context.SaveChanges(); // Salva no banco de dados
    }
    // O ID tem que ser correspondente - Demais informações serão atualizadas
    public void Atualizar(T obj)
    {
        _context.Set<T>().Update(obj);
        _context.SaveChanges();
    }
    public void Deletar(T obj)
    {
        _context.Set<T>().Remove(obj);
        _context.SaveChanges();
    }
    public abstract T? RecuperarObjPeloNome(string nome);
}
