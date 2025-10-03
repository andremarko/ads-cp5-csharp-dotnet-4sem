# CP5 - Eventos API

API RESTful em C# - Cadastro e autenticação JWT Bearer de usuários

``` bash
 ApiEventosCulturais.sln
│
└───ApiEventosCulturais
    │   appsettings.json
    │   Program.cs
    ├───Controllers
    │       AuthController.cs
    │       EventoController.cs
    │       UsuarioController.cs
    ├───Models
    │       Evento.cs
    │       MongoConfig.cs
    │       Usuario.cs
    └───Services
            AuthService.cs
            EventoService.cs
```

## Config

```
git clone https://github.com/andremarko/ads-cp4-csharp-dotnet-4sem
cd ads-cp5-csharp-dotnet-4sem
```

### Estrutura mínima do appsettings.json

``` json
  "MongoConfig": {
    "ConnectionString": "",
    "DatabaseName": "",
    "CollectionName": ""
  },

  "Jwt": {
    "Key": "",
    "Issuer": "",
    "Audience": "" - opcional
  } 
```

### Inicializando o projeto
#### Restaurando os pacotes
``` 
dotnet restore
```
#### Build da aplicação
``` 
dotnet build
```
#### Rodando a aplicação
``` 
dotnet run --project ApiEventosCulturais/ApiEventosCulturais.csproj
```
Também pode ser executado via Visual Studio com F5 ou Ctrl+F5.

## Testando a API
Acesse:
`https://localhost:<porta designada>/swagger`
