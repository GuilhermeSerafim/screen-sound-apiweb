# ScreenSound — API Web

Estudo de criação de API Web com ASP.NET Core. O projeto `ScreenSound.API` declara .NET 8 e referencia os projetos compartilhados de dados e modelos.

## Organização

- `ScreenSound.API/Program.cs`: configura Entity Framework, SQL Server, injeção de dependências, serialização que ignora ciclos e Swagger; registra endpoints de artistas, músicas e gêneros.
- `ScreenSound.API/Endpoints/ArtistaExtensions.cs`: contém listagem, busca por nome, criação, atualização e remoção de artistas.
- `ScreenSound.Shared.Dados`: contexto, acesso a dados e migrações.
- `ScreenSound.Shared.Modelos`: modelos de artista, música e gênero.
- `ScreenSound`: projeto de console com menus, separado da API.

## Dependências e limites

A API usa SQL Server e a chave de configuração `ConnectionStrings:ScreenSoundDB`. Para desenvolvimento, prepare uma instância própria e confira a configuração e as migrações antes de iniciar a API.

Na busca `GET /Artistas/{nome}`, o ramo sem resultados chama `Results.NotFound()` sem retorná-lo e prossegue para `Results.Ok(...)`. Assim, a ausência de artistas não produz a resposta 404 esperada nesse endpoint.
