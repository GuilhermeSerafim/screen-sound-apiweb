namespace ScreenSound.API.Request;

// Encapsulando a informação
// DTO (Data Transfer Object)   
// public record ArtistaRequest(string Nome, string Bio, string? FotoPerfil = null); -> Não precisa do valor padrão null,
// pois o desserializador do JSON no ASP.NET está lidando com isso corretamente.
public record ArtistaRequest(string nome, string bio, string? fotoPerfil);
