namespace ScreenSound.API.Request;

public record MusicaRequestEdit(int Id, int ArtistaId, string nome, int? anoLancamento) : MusicaRequest(nome, anoLancamento);
