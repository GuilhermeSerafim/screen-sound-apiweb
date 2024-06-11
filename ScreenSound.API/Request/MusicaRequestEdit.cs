namespace ScreenSound.API.Request;

// Percebi um erro - mesmo eu mudando o id do artista, a referência NÃO MUDA do artista!
public record MusicaRequestEdit(int Id, int ArtistaId, string nome, int? anoLancamento) : MusicaRequest(nome, anoLancamento);
