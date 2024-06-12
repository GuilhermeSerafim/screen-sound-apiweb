using Microsoft.AspNetCore.Mvc;
using ScreenSound.API.Request;
using ScreenSound.API.Response;
using ScreenSound.Banco;
using ScreenSound.Modelos;

namespace ScreenSound.API.Endpoints;

public static class MusicaExtensions
{
    public static void AddEndpointsMusicas(this WebApplication app)
    {
        app.MapGet("/Musicas", ([FromServices] GenericDAL<Musica> dal) =>
        {
            var listaObjMusicas = dal.Listar();
            if (listaObjMusicas is null)
            {
                return Results.NotFound();
            }
            var listaResponseMusicas = EntityListToResponseList(listaObjMusicas);
            return Results.Ok(listaResponseMusicas);
        });

        app.MapGet("/Musicas/{ano}", ([FromServices] GenericDAL<Musica> dal, int ano) =>
        {
            var listaObjMusicasFiltradoPorAno = dal.RecuperarListaDeObjPor(m => m.AnoLancamento == ano).ToList();
            if (listaObjMusicasFiltradoPorAno.Count <= 0)
            {
                return Results.NotFound();
            }
            var listaResponseMusicas = EntityListToResponseList(listaObjMusicasFiltradoPorAno);
            return Results.Ok(listaResponseMusicas);
        });

        app.MapPost("/Musicas", ([FromServices] GenericDAL<Musica> dal, [FromBody] MusicaRequest musicaRequest) => // MusicaRequest - record que recebe os dados
        {
            // Verifica se o request é nulo ou se os campos obrigatórios estão ausentes
            if (musicaRequest == null || string.IsNullOrWhiteSpace(musicaRequest.nome) || musicaRequest.ArtistaId == 0)
            {
                return Results.BadRequest("Dados inválidos.");
            }

            dal.Adicionar(new Musica(musicaRequest.nome, musicaRequest.anoLancamento, musicaRequest.ArtistaId));
            return Results.Created();
        });

        app.MapDelete("/Musicas/{id}", ([FromServices] GenericDAL<Musica> dal, int id) =>
        {
            var musicaRecuperada = dal.RecuperarObjPor(m => m.Id == id);
            if (musicaRecuperada == null)
            {
                return Results.NotFound();
            }
            dal.Deletar(musicaRecuperada);
            return Results.NoContent();
        });

        app.MapPut("/Musicas", (
            [FromServices] GenericDAL<Musica> dalMusica,
            [FromServices] GenericDAL<Artista> dalArtista,
            [FromBody] MusicaRequestEdit musicaRequestEdit) =>
        {
            var musicaRecuperada = dalMusica.RecuperarObjPor(m => m.Id == musicaRequestEdit.Id);
            if (musicaRecuperada == null)
            {
                return Results.NotFound();
            }

            var artistaRecuperado = dalArtista.RecuperarObjPor(a => a.Id == musicaRequestEdit.ArtistaId);
            if (artistaRecuperado == null)
            {
                return Results.NotFound();
            }

            musicaRecuperada.Nome = musicaRequestEdit.nome;
            musicaRecuperada.AnoLancamento = musicaRequestEdit.anoLancamento;
            musicaRecuperada.Artista = artistaRecuperado;

            dalMusica.Atualizar(musicaRecuperada);
            return Results.Ok();
        });
    }

    // EntityListToResponseList converte uma lista de entidades Artista em uma lista de ArtistaResponse.
    private static List<MusicaResponse> EntityListToResponseList(IEnumerable<Musica> musicaList)
    {
        // O método Select aplica uma função a cada elemento da coleção de entrada e retorna uma nova coleção contendo os resultados.
        // Para cada Musica na coleção musicaList, aplica a função EntityToResponse.
        return musicaList.Select(m => EntityToResponse(m)).ToList();
    }

    // Converte uma entidade Musica em um objeto MusicaResponse -> Para o cliente visualizar - response
    private static MusicaResponse EntityToResponse(Musica musica)
    {
        return new MusicaResponse(musica.Id, musica.Nome!, musica.Artista!.Id, musica.Artista.Nome, musica.AnoLancamento);
    }
}
