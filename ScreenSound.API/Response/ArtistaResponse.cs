namespace ScreenSound.API.Response;

// Após a criação, os valores das propriedades não podem ser alterados.
// ArtistaResponse encapsula os dados de um artista em um formato imutável e com igualdade de valor.
public record ArtistaResponse(int Id, string Nome, string Bio, string? FotoPerfil);
