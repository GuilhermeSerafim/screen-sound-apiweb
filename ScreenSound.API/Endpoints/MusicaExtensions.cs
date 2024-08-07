﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.SqlServer.Server;
using ScreenSound.API.Request;
using ScreenSound.API.Response;
using ScreenSound.Banco;
using ScreenSound.Modelos;
using System;

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

        app.MapPost("/Musicas", ([FromServices] GenericDAL<Musica> dalMusica,
            [FromServices] GenericDAL<Genero> dalGenero,
            [FromBody] MusicaRequest musicaRequest) => // MusicaRequest - record que recebe os dados
        {
            var musicaObj = new Musica(musicaRequest.nome)
            {
                ArtistaId = musicaRequest.ArtistaId,
                AnoLancamento = musicaRequest.anoLancamento,
                Generos = musicaRequest.Generos is not null ? // Caso precise atualizar, o entity atualiza?
                // Adiciona dinamicamente a tabela de generos
                GeneroRequestValidateExist(musicaRequest.Generos, dalGenero) : new List<Genero>()
            };
            dalMusica.Adicionar(musicaObj);
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
    // Objetivo: Evitar duplicidade de generos com o mesmo nome
    private static ICollection<Genero> GeneroRequestValidateExist(ICollection<GeneroRequest> generosRequest, GenericDAL<Genero> dalGenero)
    {
        var listaDeGenerosUnicos = new List<Genero>(); // Inicializa uma lista vazia para armazenar os gêneros convertidos.

        foreach (var generoRequest in generosRequest) // Itera sobre cada item da coleção de GeneroRequest.
        {
            var entity = GeneroRequestToEntity(generoRequest); // Converte GeneroRequest para a entidade Genero.

            // Verifica se já existe um gênero no banco de dados com o mesmo nome (ignorando diferenças de maiúsculas/minúsculas).
            var generoRecuperado = dalGenero.RecuperarObjPor(generoEntity => generoEntity.Nome.ToUpper().Equals(generoRequest.Nome.ToUpper()));

            if (generoRecuperado is not null) // Se o gênero já existe no banco de dados...
            {
                listaDeGenerosUnicos.Add(generoRecuperado); // Adiciona o gênero recuperado à lista.
                                                            // Se um gênero já existe, ele é recuperado e adicionado à lista de gêneros da música.
                                                            // EF Core irá detectar que não houve mudanças nesses objetos e não tentará adicioná-los novamente.
            }
            else // Se o gênero não existe no banco de dados...
            {
                listaDeGenerosUnicos.Add(entity); // Adiciona a nova entidade Genero à lista.
            }
        }

        return listaDeGenerosUnicos; // Retorna a lista de gêneros convertidos.
    }

    private static Genero GeneroRequestToEntity(GeneroRequest generoRequest)
    {
        return new Genero(generoRequest.Nome) { Descricao = generoRequest.Descricao };
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
