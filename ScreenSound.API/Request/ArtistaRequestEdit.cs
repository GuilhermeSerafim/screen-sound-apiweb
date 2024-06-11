namespace ScreenSound.API.Request;

// Herança entre records
// Reutilização de Propriedades:
// Em vez de redefinir Nome, Bio, e FotoPerfil em ArtistaRequestEdit, você herda essas propriedades de ArtistaRequest.Isso reduz a duplicação de código e facilita a manutenção.
// Chama o construtor base de ArtistaRequest com os parâmetros nome, bio, e fotoPerfil.
public record ArtistaRequestEdit(int id, string nome, string bio, string? fotoPerfil) : ArtistaRequest(nome, bio, fotoPerfil);
// ArtistaRequestEdit estende ArtistaRequest adicionando uma nova propriedade Id. Isso é útil quando você precisa de uma versão
// do objeto com informações adicionais (neste caso, um identificador único).
