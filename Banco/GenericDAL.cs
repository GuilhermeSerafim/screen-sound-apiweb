using Microsoft.EntityFrameworkCore;

namespace ScreenSound5.Banco;

internal class GenericDAL<T> where T : class // T - Tipo generico, deve ser uma classe
{
    protected readonly ScreenSoundContext _context;

    public GenericDAL(ScreenSoundContext context)
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

    // Func<in T, out TResult>
    public T? RecuperarObjPor(Func<T, bool> condicao) // Ex: musicaDao.RecuperarObjPor(m => m.Nome == "Song A") || var musicaPorId = musicaDao.RecuperarObjPor(m => m.Id == 3) ...
    { 
        // A execução começa aqui, a condição será executa por último
        return _context.Set<T>().FirstOrDefault(condicao);
        // PROCESSO -> FirstOrDefault(condicao) é um método de extensão LINQ que aplica a condição condicao a cada elemento da coleção DbSet<T>.
        // Ele itera sobre a coleção de entidades Artista.
    }

    public IEnumerable<T> RecuperarListaDeObjPor(Func<T, bool> condicao)
    {
        // Aplica a condição (condicao) a cada elemento da coleção e retorna apenas os elementos que satisfazem a condição.
        return _context.Set<T>().Where(condicao);
    }
}
