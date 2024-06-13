using Microsoft.AspNetCore.Mvc;
using ScreenSound.API.Request;
using ScreenSound.API.Response;
using ScreenSound.Banco;
using ScreenSound.Modelos;

namespace ScreenSound.API.Endpoints;

public static class GeneroExtensions
{
    public static void AddEndpointsGeneros(this WebApplication app)
    {
        app.MapGet("/Generos", ([FromServices] GenericDAL<Genero> dalGenero) =>
        {
            var listaDeEntityGeneros = dalGenero.Listar();
            if (listaDeEntityGeneros is null)
            {
                return Results.NotFound();
            }
            return Results.Ok(EntityListToResponseList(listaDeEntityGeneros!));
        });

        app.MapGet("/Generos/{nome}", ([FromServices] GenericDAL<Genero> dalGenero, string nome) =>
        {
            var listaEntityGeneros = dalGenero.RecuperarListaDeObjPor(g => g.Nome.ToUpper().Equals(nome.ToUpper())).ToList();
            if (listaEntityGeneros.Count == 0)
            {
                return Results.NotFound();
            }
            return Results.Ok(EntityListToResponseList(listaEntityGeneros));
        });

        app.MapPost("/Generos", ([FromServices] GenericDAL<Genero> dal, [FromBody] GeneroRequest generoReq) =>
        {
            dal.Adicionar(RequestToEntity(generoReq));
            return Results.Created();
        });


        app.MapDelete("/Generos/{id}", ([FromServices] GenericDAL<Genero> dalGenero, int id) =>
        {
            var generoRecuperado = dalGenero.RecuperarObjPor(g => g.Id == id);
            if (generoRecuperado == null)
            {
                return Results.NotFound();
            }
            dalGenero.Deletar(generoRecuperado);
            return Results.NoContent();
        });
    }
    private static List<GeneroResponse> EntityListToResponseList(IEnumerable<Genero> entitiesGeneros) => entitiesGeneros.Select(a => EntityToResponse(a)).ToList();
    private static GeneroResponse EntityToResponse(Genero entitieGeneros) => new(entitieGeneros.Id, entitieGeneros.Nome, entitieGeneros.Descricao);
    private static Genero RequestToEntity(GeneroRequest generoRequest)
    {
        return new Genero(generoRequest.Nome) { Descricao = generoRequest.Descricao };
    }
}