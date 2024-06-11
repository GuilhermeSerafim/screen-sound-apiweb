using Microsoft.AspNetCore.Mvc;
using ScreenSound.API.Request;
using ScreenSound.API.Response;
using ScreenSound.Banco;
using ScreenSound.Modelos;

namespace ScreenSound.API.Endpoints;

public static class ArtistaExtensions
{
    public static void AddEndpointsArtistas(this WebApplication app)
    {
        app.MapGet("/Artistas", ([FromServices] GenericDAL<Artista> dal) =>
        {
            var listaObjArtistas = dal.Listar();
            if(listaObjArtistas is null)
            {
                return Results.NotFound();
            }
            return Results.Ok(EntityListToResponseList(listaObjArtistas));

        });

        app.MapGet("/Artistas/{nome}", ([FromServices] GenericDAL<Artista> dal, string nome) =>
        {
            var artistasRecuperado = dal.RecuperarListaDeObjPor(a => a.Nome.ToUpper().Equals(nome.ToUpper()));
            if(artistasRecuperado is null)
            {
                Results.NotFound();
            }
            return Results.Ok(EntityListToResponseList(artistasRecuperado!));
        });

        // Quando uma solicitação POST é enviada para /Artistas, o código dentro desta lambda será executado.
        app.MapPost("/Artistas", ([FromServices] GenericDAL<Artista> dal, [FromBody] ArtistaRequest artistaRequest) =>
        {
            var artista = new Artista(artistaRequest.nome, artistaRequest.bio, artistaRequest.fotoPerfil);
            dal.Adicionar(artista);
            return Results.Created();
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

        app.MapPut("/Artistas", ([FromServices] GenericDAL<Artista> dal, [FromBody] ArtistaRequestEdit artistaEdit) =>
        {
            var artistaRecuperado = dal.RecuperarObjPor(a => a.Id == artistaEdit.id);
            if (artistaRecuperado == null)
            {
                return Results.NotFound();
            }
            artistaRecuperado.Nome = artistaEdit.nome;
            artistaRecuperado.Bio = artistaEdit.bio;
            artistaRecuperado.FotoPerfil = artistaEdit.fotoPerfil ?? "https://cdn.pixabay.com/photo/2016/08/08/09/17/avatar-1577909_1280.png";
            dal.Atualizar(artistaRecuperado);   
            return Results.Ok();
        });
    }

    private static ICollection<ArtistaResponse> EntityListToResponseList(IEnumerable<Artista> entitiesArtistas)
    {
        return entitiesArtistas.Select(a => EntityToResponse(a)).ToList();
    }

    private static ArtistaResponse EntityToResponse(Artista entitieArtista)
    {
        return new ArtistaResponse(entitieArtista.Id, entitieArtista.Nome, entitieArtista.Bio, entitieArtista.FotoPerfil);
    }
}
