﻿using ScreenSound.Modelos;

namespace ScreenSound.Modelos;

public class Musica
{
    public Musica(string nome) => Nome = nome;
    public Musica() { } // Construtor padrão

    public string Nome { get; set; }
    public int Id { get; set; }
    public int? AnoLancamento { get; set; }
    public virtual Artista? Artista { get; set; }
    public int? ArtistaId { get; set; } // FK
    public virtual ICollection<Genero> Generos { get; set; }

    public void ExibirFichaTecnica()
    {
        Console.WriteLine($"\n{Nome} - {Artista!.Nome}");
      
    }

    public override string ToString()
    {
        return @$"Id: {Id}
        Ano: {AnoLancamento}
        Nome: {Nome}
        Artista: {Artista!.Nome}";
    }
}