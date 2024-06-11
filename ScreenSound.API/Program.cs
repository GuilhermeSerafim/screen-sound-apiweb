using ScreenSound.Modelos;
using ScreenSound.Banco;
using System.Text.Json.Serialization;
using System.Runtime.Intrinsics.X86;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32;
using ScreenSound.API.Endpoints;

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

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>
    (options => options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

var app = builder.Build();

app.AddEndpointsArtistas();
app.AddEndpointsMusicas();

app.Run();
