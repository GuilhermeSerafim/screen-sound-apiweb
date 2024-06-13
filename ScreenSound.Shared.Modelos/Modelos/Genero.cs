using ScreenSound.Modelos;

namespace ScreenSound.Modelos;

public class Genero
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string? Descricao { get; set; } = string.Empty; // string.Empty; inicializa a propriedade Descricao com uma string vazia.
    public virtual ICollection<Musica> Musicas { get; set; }

    public override string ToString()
    {
        return $"Nome: {Nome} - Descrição: {Descricao}";
    }
}

