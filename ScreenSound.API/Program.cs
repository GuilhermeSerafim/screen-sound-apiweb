using ScreenSound.Modelos;
using ScreenSound.Banco;
using System.Text.Json.Serialization;
using System.Runtime.Intrinsics.X86;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32;

var builder = WebApplication.CreateBuilder(args);
// Quando uma classe precisa de uma depend�ncia, o cont�iner resolve essa depend�ncia e a injeta na classe.
builder.Services.AddDbContext<ScreenSoundContext>();  // Registra o DbContext no cont�iner de DI
builder.Services.AddTransient<GenericDAL<Artista>>(); // Cria uma nova inst�ncia de GenericDAL<Artista> sempre que solicitado.
builder.Services.AddTransient<GenericDAL<Musica>>();  // Cria uma nova inst�ncia de GenericDAL<Musica> sempre que solicitado.
//  builder.Services.Configure<TOptions>: Esse m�todo adiciona uma configura��o espec�fica para um tipo de op��es. No caso, estamos configurando JsonOptions.

//  <Microsoft.AspNetCore.Http.Json.JsonOptions>: Especifica que estamos configurando as op��es relacionadas � serializa��o JSON
//  usadas pelo middleware HTTP da ASP.NET Core.

//  options => options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles:
//  Aqui estamos definindo uma configura��o espec�fica dentro das JsonOptions. Veja bem:
//  options.SerializerOptions: A propriedade SerializerOptions permite configurar op��es para o serializador JSON usado pela ASP.NET Core.
//  ReferenceHandler.IgnoreCycles: Define o ReferenceHandler como IgnoreCycles. Isso significa que, durante a serializa��o, se houver ciclos de refer�ncia
//  (por exemplo, um objeto A referenciando um objeto B, que por sua vez referencia o objeto A), esses ciclos ser�o ignorados em vez de causar uma exce��o.

builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>
    (options => options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);


var app = builder.Build();

app.MapGet("/Artistas", ([FromServices] GenericDAL<Artista> dal) =>
{
    return Results.Ok(dal.Listar());
});

app.MapGet("/Artistas/{nome}", ([FromServices] GenericDAL<Artista> dal, string nome) =>
{
    var artistasRecuperado = dal.RecuperarListaDeObjPor(a => a.Nome.ToUpper().Equals(nome.ToUpper())); ;
    return Results.Ok(  artistasRecuperado);
});

// Quando uma solicita��o POST � enviada para /Artistas, o c�digo dentro desta lambda ser� executado.
app.MapPost("/Artistas", ([FromServices] GenericDAL<Artista> dal, [FromBody] Artista artista) => // indica que os dados do corpo da solicita��o (request body) ser�o desserializados em uma inst�ncia da classe Artista. 
{
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
    if(artistaRecuperado == null)
    {
        return Results.NotFound();
    }
    artistaRecuperado.Nome = artista.Nome;
    artistaRecuperado.Bio = artista.Bio;
    artistaRecuperado.FotoPerfil = artista.FotoPerfil;
    dal.Atualizar(artistaRecuperado);
    return Results.Ok();
});

app.MapGet("/Musicas", ([FromServices] GenericDAL<Musica> dal) =>
{
    return Results.Ok(dal.Listar());
});

app.MapGet("/Musicas/{ano}", ([FromServices] GenericDAL<Musica> dal, int ano) =>
{
    var musicasRecuperadas = dal.RecuperarListaDeObjPor(a => a.AnoLancamento == ano).ToList();
    if(musicasRecuperadas.Count <= 0)
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
    var musicaRecuperada = dal.RecuperarObjPor(a => a.Id == id);
    if (musicaRecuperada == null)
    {
        return Results.NotFound();
    }
    dal.Deletar(musicaRecuperada);
    return Results.NoContent();
});

app.MapPut("/Musicas", ([FromServices] GenericDAL<Musica> dal, [FromBody] Musica artista) =>
{
    var musicaRecuperada = dal.RecuperarObjPor(a => a.Id == artista.Id);
    if (musicaRecuperada == null)
    {
        return Results.NotFound();
    }
    musicaRecuperada.Nome = artista.Nome;
    musicaRecuperada.Artista = artista.Artista;
    musicaRecuperada.AnoLancamento = artista.AnoLancamento;
    dal.Atualizar(musicaRecuperada);
    return Results.Ok();
});

app.Run();
