using ScreenSound.Modelos;
using ScreenSound.Banco;
using System.Text.Json.Serialization;
using ScreenSound.API.Endpoints;

var builder = WebApplication.CreateBuilder(args);
// Quando uma classe precisa de uma depend�ncia, o cont�iner resolve essa depend�ncia e a injeta na classe.
builder.Services.AddDbContext<ScreenSoundContext>();  // Registra o DbContext no cont�iner de DI
builder.Services.AddTransient<GenericDAL<Artista>>(); // Cria uma nova inst�ncia de GenericDAL<Artista> sempre que solicitado (CONTAINER DI).
builder.Services.AddTransient<GenericDAL<Musica>>();  // Cria uma nova inst�ncia de GenericDAL<Musica> sempre que solicitado.
builder.Services.AddTransient<GenericDAL<Genero>>();  // Cria uma nova inst�ncia de GenericDAL<Musica> sempre que solicitado.
//  builder.Services.Configure<TOptions>: Esse m�todo adiciona uma configura��o espec�fica para um tipo de op��es. No caso, estamos configurando JsonOptions.

//  <Microsoft.AspNetCore.Http.Json.JsonOptions>: Especifica que estamos configurando as op��es relacionadas � serializa��o JSON
//  usadas pelo middleware HTTP da ASP.NET Core.

//  options => options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles:
//  Aqui estamos definindo uma configura��o espec�fica dentro das JsonOptions. Veja bem:
//  options.SerializerOptions: A propriedade SerializerOptions permite configurar op��es para o serializador JSON usado pela ASP.NET Core.
//  ReferenceHandler.IgnoreCycles: Define o ReferenceHandler como IgnoreCycles. Isso significa que, durante a serializa��o, se houver ciclos de refer�ncia
//  (por exemplo, um objeto A referenciando um objeto B, que por sua vez referencia o objeto A), esses ciclos ser�o ignorados em vez de causar uma exce��o.

// Documenta��o dinamica
// Termos importantes:
// Middleware: Componentes que processam requisi��es HTTP em um pipeline.
// Pipeline de Middleware: Sequ�ncia de middleware que uma requisi��o percorre do in�cio ao fim.
builder.Services.AddEndpointsApiExplorer(); // adiciona servi�os necess�rios para explorar e documentar os endpoints da API.
builder.Services.AddSwaggerGen(); // adiciona os servi�os necess�rios para gerar a documenta��o Swagger.

builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>
    (options => options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

var app = builder.Build();

app.AddEndpointsArtistas();
app.AddEndpointsMusicas();
app.AddEndpointsGeneros();

app.UseSwagger(); // Configura o middleware para gerar e servir a especifica��o OpenAPI da API como um endpoint JSON.
app.UseSwaggerUI();  // Configura o middleware para servir a interface de usu�rio do Swagger, permitindo a explora��o e teste interativos da API no navegador.

app.Run();
