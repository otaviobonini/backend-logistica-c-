# Logística API

Sistema para uma transportadora pequena controlar a frota, os motoristas e as entregas, barrando na hora a saída de uma carga irregular: acima da capacidade do veículo, com motorista de CNH vencida ou num veículo que já está em outra entrega.

Projeto Final da disciplina de Backend (Engenharia de Software, SATC). **Etapa 1: arquitetura do backend.**

## Índice

- [Integrantes](#integrantes)
- [O problema](#o-problema)
- [Tecnologias](#tecnologias)
- [Arquitetura](#arquitetura)
- [Entidades](#entidades)
- [Rotas](#rotas)
- [Como executar](#como-executar)
- [Situação do projeto](#situação-do-projeto)
- [Documentação](#documentação)

## Integrantes

| Nome | GitHub |
|---|---|
| Miguel Torres | [@miguel1411mt](https://github.com/miguel1411mt) |
| Otavio Bonini | [@otaviobonini](https://github.com/otaviobonini) |
| João Vinícius | [@Jviiniciuss](https://github.com/Jviiniciuss) |

## O problema

**Tema sorteado:** Logística e Cadeia de Suprimentos

Uma transportadora pequena controla as entregas numa planilha. Quem monta a carga não sabe a capacidade de cada veículo, ninguém confere se a CNH do motorista está em dia e o mesmo veículo às vezes é escalado para duas entregas ao mesmo tempo. O resultado é multa por excesso de peso, motorista dirigindo irregular e entrega atrasada. O sistema guarda a frota, os motoristas e cada entrega, e não deixa sair uma carga que quebre alguma dessas regras.

**Quem usa o sistema:**

| Quem | O que faz no sistema |
|---|---|
| Operador de logística | Cadastra veículos e motoristas, registra a saída e a conclusão das entregas |
| Gerente da transportadora | Consulta a frota, os motoristas e a situação de cada entrega |

**Regras de negócio.** O grupo criou estas três regras olhando para os problemas da planilha: excesso de peso, CNH vencida e veículo escalado duas vezes.

| # | Regra | Onde está no código | Rota que usa |
|---|---|---|---|
| R1 | O peso da entrega não pode passar da capacidade de carga do veículo | `EntregaService.Criar` | `POST /entregas` |
| R2 | Motorista com a CNH vencida não pode receber entrega | `EntregaService.Criar` | `POST /entregas` |
| R3 | Um veículo só pode estar em uma entrega em andamento por vez. Ele é liberado quando a entrega é concluída, e uma entrega concluída não pode ser concluída de novo | `EntregaService.Criar` e `EntregaService.Concluir` | `POST /entregas` e `PUT /entregas/{id}/concluir` |

Quando uma regra barra o pedido, a API devolve `400` com a regra explicada na mensagem.

**Fora do escopo:** cálculo de rotas e de frete, rastreamento em tempo real, login de usuários e tela de frontend.

## Tecnologias

| O quê | Qual |
|---|---|
| Linguagem | C# 12, .NET 8 |
| Framework | ASP.NET Core 8 (Web API, Data Annotations) |
| Acesso ao banco | Entity Framework Core 8, com o provedor Npgsql |
| Banco de dados | PostgreSQL 16, em Docker |
| Migrations | EF Core Migrations |
| Documentação da API | Swashbuckle (Swagger UI) |

## Arquitetura

A API é organizada em camadas. Uma requisição entra pelo Controller, passa pelo Service, que aplica as regras de negócio, e chega ao banco pelo Repository; a resposta faz o caminho de volta. O Mapper converte DTO em entidade e entidade em DTO. Todas as classes recebem as dependências pelo construtor, registradas em `Program.cs`.

**Mapa da estrutura:**

| Entidade | Migration | Model | Repository | DTOs | Mapper | Service | Controller |
|---|---|---|---|---|---|---|---|
| Veiculo | `CriarVeiculos`, pronta | `Veiculo`, pronta | `VeiculoRepository`, pronto | `VeiculoRequestDTO`, `VeiculoResponseDTO`, prontos | `VeiculoMapper`, implementado | `VeiculoService`, implementado | `VeiculoController`, implementado |
| Motorista | `CriarMotoristas`, pronta | `Motorista`, pronta | `MotoristaRepository`, pronto | `MotoristaRequestDTO`, `MotoristaResponseDTO`, prontos | `MotoristaMapper`, implementado | `MotoristaService`, implementado | `MotoristaController`, implementado |
| Entrega | `CriarEntregas`, pronta | `Entrega`, pronta | `EntregaRepository`, pronto | `EntregaRequestDTO`, `EntregaResponseDTO`, prontos | `EntregaMapper`, implementado | `EntregaService`, implementado (R1, R2 e R3) | `EntregaController`, em parte: sem `GET /entregas` |

Também fazem parte do projeto: `AppDbContext` (a ligação com o banco, em `Repositories/`), `ApiExceptionFilter`, `ErroDTO`, `CampoErroDTO`, `RecursoNaoEncontradoException` e `RegraDeNegocioException`. Cada entidade tem a sua exceção de "não encontrado": `VeiculoNaoEncontradoException`, `MotoristaNaoEncontradoException` e `EntregaNaoEncontradaException`.

**Correspondência com o Java.** O enunciado foi escrito para Java e Spring Boot. Este projeto usa C#, e cada peça tem o seu equivalente:

| No enunciado (Java) | Neste projeto (C#) |
|---|---|
| Pacotes `controller`, `service`, `mapper`, `repository`, `model` e `dto` | Pastas `Controllers`, `Services`, `Mappers`, `Repositories`, `Models` e `Dtos` |
| `@Entity` | Classes de `Models/` com `[Table]` e `[Column]`, registradas como `DbSet` no `AppDbContext` |
| `JpaRepository` | Classes de `Repositories/` que usam o `AppDbContext` do Entity Framework Core |
| Flyway, em `db/migration` | EF Core Migrations, em `Migrations/`, aplicadas quando a API sobe |
| `@Valid` e Bean Validation | `[ApiController]` e Data Annotations (`[Required]`, `[StringLength]`, `[Range]`) nos DTOs |
| `ApiExceptionHandler` com `@RestControllerAdvice` | `ApiExceptionFilter`, registrado em `Program.cs` |
| springdoc-openapi | Swashbuckle |

Onde cada critério da revisão aparece no projeto está em [docs/autoavaliacao.md](docs/autoavaliacao.md).

## Entidades

| Entidade | O que representa | Como se relaciona |
|---|---|---|
| Veiculo | Um veículo da frota, com a placa e a capacidade de carga | Aparece em várias entregas |
| Motorista | Um motorista da transportadora, com a CNH e a validade dela | Faz várias entregas |
| Entrega | Uma carga levada até um destino, com a data de saída e a de conclusão | Pertence a um veículo e a um motorista |

Atributos, tipos e o diagrama das tabelas estão em [docs/modelo-de-dominio.md](docs/modelo-de-dominio.md).

## Rotas

### As que já funcionam

| Verbo | Caminho | O que faz | Regra | Sucesso | Erros |
|---|---|---|---|---|---|
| `POST` | `/veiculos` | Cadastra um veículo | Nenhuma | `201` | `400` |
| `GET` | `/veiculos` | Lista os veículos | Nenhuma | `200` | Nenhum |
| `GET` | `/veiculos/{id}` | Busca um veículo pelo id | Nenhuma | `200` | `404` |
| `POST` | `/motoristas` | Cadastra um motorista | Nenhuma | `201` | `400` |
| `GET` | `/motoristas` | Lista os motoristas | Nenhuma | `200` | Nenhum |
| `GET` | `/motoristas/{id}` | Busca um motorista pelo id | Nenhuma | `200` | `404` |
| `POST` | `/entregas` | Registra a saída de uma entrega | R1, R2 e R3 | `201` | `400`, `404` |
| `GET` | `/entregas/{id}` | Busca uma entrega pelo id | Nenhuma | `200` | `404` |
| `PUT` | `/entregas/{id}/concluir` | Conclui a entrega e libera o veículo | R3 | `200` | `400`, `404` |

Exemplo de requisição:

```http
POST /entregas
Content-Type: application/json

{
  "descricao": "Cimento CP II, 6 sacos",
  "destino": "Rua Henrique Lage, 500, Criciúma",
  "pesoKg": 300,
  "veiculoId": 2,
  "motoristaId": 1
}
```

Resposta, `201 Created`. Entram os ids; veículo e motorista voltam completos, e a data de saída quem define é a API:

```json
{
  "id": 1,
  "descricao": "Cimento CP II, 6 sacos",
  "destino": "Rua Henrique Lage, 500, Criciúma",
  "pesoKg": 300,
  "dataSaida": "2026-10-10",
  "dataConclusao": null,
  "emAndamento": true,
  "veiculo": { "id": 2, "placa": "XYZ9K88", "modelo": "Fiat Fiorino", "capacidadeKg": 650.00 },
  "motorista": { "id": 1, "nome": "Carlos Pereira", "cnh": "12345678900", "validadeCnh": "2028-05-10" }
}
```

### Erros

Todo erro sai no mesmo formato. Este é o `400` da regra R1, quando a carga passa da capacidade do veículo:

```json
{
  "status": 400,
  "erro": "Bad Request",
  "mensagem": "R1: o peso da entrega (800 kg) passa da capacidade do veículo XYZ9K88 (650.00 kg)",
  "caminho": "/entregas",
  "timestamp": "2026-10-10T21:43:46",
  "campos": []
}
```

Um campo inválido também devolve `400`, com a lista `campos` dizendo qual campo corrigir. Um veículo, motorista ou entrega que não existe devolve `404`.

### As planejadas

Estão no contrato, mas ainda não respondem.

| Recurso | O que vai fazer |
|---|---|
| `PUT /veiculos/{id}` e `DELETE /veiculos/{id}` | Atualizar e remover veículos |
| `PUT /motoristas/{id}` e `DELETE /motoristas/{id}` | Atualizar e remover motoristas |
| `GET /entregas` | Listar as entregas |

O contrato completo, com todas as rotas e os DTOs, está em [docs/contrato-da-api.md](docs/contrato-da-api.md).

## Como executar

Passo a passo para rodar o projeto do zero em um computador com Windows. Em Linux e Mac os comandos são os mesmos.

### 1. O que instalar

| Programa | Para que serve | Como conferir |
|---|---|---|
| SDK do .NET 8 (<https://dotnet.microsoft.com/download/dotnet/8.0>) | Compila e roda a API | `dotnet --version` deve mostrar `8.x.x` |
| Docker Desktop (<https://www.docker.com/products/docker-desktop>) | Roda o PostgreSQL | `docker --version`; o Docker Desktop precisa estar aberto e com o ícone indicando que está rodando |
| Git | Baixa o projeto | `git --version` |
| Visual Studio 2022 (17.8 ou mais novo), com a carga de trabalho "ASP.NET e desenvolvimento Web", ou VS Code com a extensão C# Dev Kit | Editor, opcional para só rodar | Abre o projeto sem erros |

Depois de instalar o .NET ou o Docker, feche e abra o terminal de novo.

### 2. Baixar o projeto

```bash
git clone https://github.com/otaviobonini/backend-logistica-c-.git
cd backend-logistica-c-
```

### 3. Subir o banco de dados

Com o Docker Desktop aberto, na pasta do projeto:

```bash
docker compose up -d
```

Na primeira vez, o Docker baixa a imagem do PostgreSQL, o que pode demorar um pouco. Para conferir que o banco subiu:

```bash
docker ps
```

Deve aparecer o container `logistica-postgres` com o status `Up` e, depois de alguns segundos, `(healthy)`. O banco fica na porta 5437, com banco, usuário e senha `logistica`. Essas mesmas informações estão em `appsettings.json`.

### 4. Rodar a API

Escolha uma das três formas.

**Pelo terminal**, na pasta do projeto:

```bash
dotnet run
```

Na primeira vez, o .NET baixa os pacotes. Quando aparecer a linha abaixo, a API está no ar. Deixe essa janela aberta.

```
Now listening on: http://localhost:5080
```

**Pelo Visual Studio:** abra o arquivo `Logistica_API.sln`, escolha o perfil `http` ao lado do botão verde de executar e aperte F5. O navegador abre sozinho no Swagger.

**Pelo VS Code:** abra a pasta do projeto, abra o terminal integrado (`Ctrl+'`) e rode `dotnet run`.

Na primeira execução, a API cria as três tabelas sozinha, aplicando as migrations da pasta `Migrations/`. Não é preciso rodar nenhum script SQL.

### 5. Abrir o Swagger

| Endereço | O que é |
|---|---|
| <http://localhost:5080/swagger> | Swagger UI: as rotas, com **Try it out** para enviar requisições |
| <http://localhost:5080/swagger/v1/swagger.json> | O contrato OpenAPI em JSON |

Para usar uma rota no Swagger: clique nela, clique em **Try it out**, preencha o JSON e clique em **Execute**. O resultado aparece logo abaixo, com o status e o JSON da resposta.

No Postman, importe todas as rotas de uma vez: **Import**, depois **Link**, e cole `http://localhost:5080/swagger/v1/swagger.json`.

### 6. Ver as regras funcionando

Cadastre um veículo com capacidade de 650 kg e dois motoristas, um com a CNH válida e outro com a CNH vencida. Tente uma entrega de 800 kg (R1), depois uma entrega com o motorista de CNH vencida (R2). Registre uma entrega válida e tente outra com o mesmo veículo (R3). Conclua a primeira em `PUT /entregas/{id}/concluir` e o veículo volta a aceitar entregas.

### 7. Parar

1. Na janela da API, aperte `Ctrl+C`.
2. Para parar o banco e manter os dados: `docker compose stop`.
3. Para apagar o banco e começar do zero: `docker compose down -v`. Na próxima vez, as migrations recriam as tabelas.

## Situação do projeto

| Parte | Situação |
|---|---|
| Desenho | Completo: problema e regras neste README, modelo e contrato em `docs/` |
| Estrutura | As 3 entidades têm migration, entidade, Repository, DTOs, Mapper, Service e Controller |
| API e regras | Veículos com as 3 rotas da API simples; cadastro, listagem e busca de motoristas; saída, busca e conclusão de entregas com R1, R2 e R3 implementadas |

**Ainda não funciona:** `PUT` e `DELETE` de veículos e motoristas, e `GET /entregas`.

## Documentação

| Documento | O que tem |
|---|---|
| [Modelo de domínio](docs/modelo-de-dominio.md) | Entidades, relacionamentos, diagrama das tabelas, migrations |
| [Contrato da API](docs/contrato-da-api.md) | Todas as rotas, os DTOs, exemplos de JSON e erros |
| [Autoavaliação](docs/autoavaliacao.md) | Os critérios da revisão do código, marcados pelo grupo |
