using ScreenSound.Modelos;
using ScreenSound.Banco;
using System.Text.Json.Serialization;
using ScreenSound.API.Endpoints;

var builder = WebApplication.CreateBuilder(args);
// Quando uma classe precisa de uma dependência, o contêiner resolve essa dependência e a injeta na classe.
builder.Services.AddDbContext<ScreenSoundContext>();  // Registra o DbContext no contêiner de DI
builder.Services.AddTransient<GenericDAL<Artista>>(); // Cria uma nova instância de GenericDAL<Artista> sempre que solicitado (CONTAINER DI).
builder.Services.AddTransient<GenericDAL<Musica>>();  // Cria uma nova instância de GenericDAL<Musica> sempre que solicitado.
builder.Services.AddTransient<GenericDAL<Genero>>();  // Cria uma nova instância de GenericDAL<Musica> sempre que solicitado.
//  builder.Services.Configure<TOptions>: Esse método adiciona uma configuração específica para um tipo de opções. No caso, estamos configurando JsonOptions.

//  <Microsoft.AspNetCore.Http.Json.JsonOptions>: Especifica que estamos configurando as opções relacionadas à serialização JSON
//  usadas pelo middleware HTTP da ASP.NET Core.

//  options => options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles:
//  Aqui estamos definindo uma configuração específica dentro das JsonOptions. Veja bem:
//  options.SerializerOptions: A propriedade SerializerOptions permite configurar opções para o serializador JSON usado pela ASP.NET Core.
//  ReferenceHandler.IgnoreCycles: Define o ReferenceHandler como IgnoreCycles. Isso significa que, durante a serialização, se houver ciclos de referência
//  (por exemplo, um objeto A referenciando um objeto B, que por sua vez referencia o objeto A), esses ciclos serão ignorados em vez de causar uma exceção.

// Documentação dinamica
// Termos importantes:
// Middleware: Componentes que processam requisições HTTP em um pipeline.
// Pipeline de Middleware: Sequência de middleware que uma requisição percorre do início ao fim.
builder.Services.AddEndpointsApiExplorer(); // adiciona serviços necessários para explorar e documentar os endpoints da API.
builder.Services.AddSwaggerGen(); // adiciona os serviços necessários para gerar a documentação Swagger.

builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>
    (options => options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

var app = builder.Build();

app.AddEndpointsArtistas();
app.AddEndpointsMusicas();
app.AddEndpointsGeneros();

app.UseSwagger(); // Configura o middleware para gerar e servir a especificação OpenAPI da API como um endpoint JSON.
app.UseSwaggerUI();  // Configura o middleware para servir a interface de usuário do Swagger, permitindo a exploração e teste interativos da API no navegador.

app.Run();
