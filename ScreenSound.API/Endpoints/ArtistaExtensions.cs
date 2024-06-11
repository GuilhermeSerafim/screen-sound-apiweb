using Microsoft.AspNetCore.Mvc;
using ScreenSound.API.Request;
using ScreenSound.Banco;
using ScreenSound.Modelos;

namespace ScreenSound.API.Endpoints;

public static class ArtistaExtensions
{
    public static void AddEndpointsArtistas(this WebApplication app)
    {
        app.MapGet("/Artistas", ([FromServices] GenericDAL<Artista> dal) =>
        {
            return Results.Ok(dal.Listar());
        });

        app.MapGet("/Artistas/{nome}", ([FromServices] GenericDAL<Artista> dal, string nome) =>
        {
            var artistasRecuperado = dal.RecuperarListaDeObjPor(a => a.Nome.ToUpper().Equals(nome.ToUpper())); ;
            return Results.Ok(artistasRecuperado);
        });

        // Quando uma solicitação POST é enviada para /Artistas, o código dentro desta lambda será executado.
        app.MapPost("/Artistas", ([FromServices] GenericDAL<Artista> dal, [FromBody] ArtistaRequest artistaRequest) =>
        {
            var artista = new Artista(artistaRequest.nome, artistaRequest.bio, artistaRequest.fotoPerfil);
            dal.Adicionar(artista);
            return Results.Ok();
        });

        app.MapDelete("/Artistas/{id}", ([FromServices] GenericDAL<Artista> dal, int id) =>
        {
            var artistaRecuperado = dal.RecuperarObjPor(a => a.Id == id);
            if (artistaRecuperado == null)
            {
                return Results.NotFound();
            }
            dal.Deletar(artistaRecuperado);
            return Results.NoContent();
        });

        app.MapPut("/Artistas", ([FromServices] GenericDAL<Artista> dal, [FromBody] Artista artista) =>
        {
            var artistaRecuperado = dal.RecuperarObjPor(a => a.Id == artista.Id);
            if (artistaRecuperado == null)
            {
                return Results.NotFound();
            }
            artistaRecuperado.Nome = artista.Nome;
            artistaRecuperado.Bio = artista.Bio;
            artistaRecuperado.FotoPerfil = artista.FotoPerfil;
            dal.Atualizar(artistaRecuperado);
            return Results.Ok();
        });
    }
}
