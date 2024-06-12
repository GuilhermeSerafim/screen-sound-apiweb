namespace ScreenSound.API.Response;

// Após a criação, os valores das propriedades não podem ser alterados.
// ArtistaResponse encapsula os dados de um artista em um formato imutável e com igualdade de valor.
// A nossa api espera receber esses dados do banco para enviar para o cliente
public record ArtistaResponse(int Id, string Nome, string Bio, string? FotoPerfil);
