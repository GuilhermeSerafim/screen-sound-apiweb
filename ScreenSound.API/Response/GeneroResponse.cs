using System.ComponentModel.DataAnnotations;

namespace ScreenSound.API.Response;

public record GeneroResponse([Required] int Id, [Required] string Nome, string? Descricao);

