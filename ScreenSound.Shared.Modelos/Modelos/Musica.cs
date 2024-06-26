﻿using ScreenSound.Shared.Modelos.Modelos;

namespace ScreenSound.Modelos;

public class Musica
{
    public Musica(string nome, int artistaId, int? anoLancamento = 0)
    {
        Nome = nome;
        AnoLancamento = anoLancamento ?? 0;
        ArtistaId = artistaId;
    }

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