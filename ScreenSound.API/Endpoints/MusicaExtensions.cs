﻿using Microsoft.AspNetCore.Mvc;
using ScreenSound.Banco;
using ScreenSound.Modelos;

namespace ScreenSound.API.Endpoints;

public static class MusicaExtensions
{
    public static void AddEndpointsMusicas(this WebApplication app)
    {
        app.MapGet("/Musicas", ([FromServices] GenericDAL<Musica> dal) =>
        {
            return Results.Ok(dal.Listar());
        });

        app.MapGet("/Musicas/{ano}", ([FromServices] GenericDAL<Musica> dal, int ano) =>
        {
            var musicasRecuperadas = dal.RecuperarListaDeObjPor(m => m.AnoLancamento == ano).ToList();
            if (musicasRecuperadas.Count <= 0)
            {
                return Results.NotFound();
            }
            return Results.Ok(musicasRecuperadas);
        });

        app.MapPost("/Musicas", ([FromServices] GenericDAL<Musica> dal, [FromBody] Musica musica) =>
        {
            dal.Adicionar(musica);
            return Results.Ok();
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

        app.MapPut("/Musicas", ([FromServices] GenericDAL<Musica> dal, [FromBody] Musica musica) =>
        {
            var musicaRecuperada = dal.RecuperarObjPor(m => m.Id == m.Id);
            if (musicaRecuperada == null)
            {
                return Results.NotFound();
            }
            musicaRecuperada.Nome = musica.Nome;
            musicaRecuperada.Artista = musica.Artista;
            musicaRecuperada.AnoLancamento = musica.AnoLancamento;
            dal.Atualizar(musicaRecuperada);
            return Results.Ok();
        });
    }
}