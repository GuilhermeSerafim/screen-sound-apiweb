using ScreenSound.Modelos;
using ScreenSound.Banco;
using System.Text.Json.Serialization;
using System.Runtime.Intrinsics.X86;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
//  builder.Services.Configure<TOptions>: Esse método adiciona uma configuração específica para um tipo de opções. No caso, estamos configurando JsonOptions.

//  <Microsoft.AspNetCore.Http.Json.JsonOptions>: Especifica que estamos configurando as opções relacionadas à serialização JSON
//  usadas pelo middleware HTTP da ASP.NET Core.

//  options => options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles:
//  Aqui estamos definindo uma configuração específica dentro das JsonOptions. Veja bem:
//  options.SerializerOptions: A propriedade SerializerOptions permite configurar opções para o serializador JSON usado pela ASP.NET Core.
//  ReferenceHandler.IgnoreCycles: Define o ReferenceHandler como IgnoreCycles. Isso significa que, durante a serialização, se houver ciclos de referência
//  (por exemplo, um objeto A referenciando um objeto B, que por sua vez referencia o objeto A), esses ciclos serão ignorados em vez de causar uma exceção.

builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>
    (options => options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);


var app = builder.Build();

app.MapGet("/Artistas", () => { 
    var dal = new GenericDAL<Artista>(new ScreenSoundContext());
    return Results.Ok(dal.Listar());
});

app.MapGet("/Artistas/{nome}", (string nome) =>
{
    var dal = new GenericDAL<Artista>(new ScreenSoundContext());
    var artistaRecuperado = dal.RecuperarObjPor(a => a.Nome.ToUpper().Equals(nome.ToUpper()));
    if(artistaRecuperado == null)
    {
        return Results.NotFound();
    }
    return Results.Ok(artistaRecuperado);
});

// Quando uma solicitação POST é enviada para /Artistas, o código dentro desta lambda será executado.
app.MapPost("/Artistas", ([FromBody]Artista artista) => // indica que os dados do corpo da solicitação (request body) serão desserializados em uma instância da classe Artista. 
{
    var dal = new GenericDAL<Artista>(new ScreenSoundContext());
    dal.Adicionar(artista);
    return Results.Ok();
});

app.Run();
