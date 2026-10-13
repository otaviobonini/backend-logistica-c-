# Autoavaliação

Os 10 critérios da revisão do código (`Aula 10-11/AVALIACAO.md`), com a nossa marca e onde está a evidência. Como o projeto é em C#, a evidência aponta para o equivalente de cada nome do Java (veja a tabela de correspondência no README).

Marcas: **Atende**, **Em parte** ou **Não atende**.

| Critério | Marca | Evidência |
| P1 A API sobe seguindo o README | Atende | `README.md`, seção "Como executar" |
| P2 Cada classe no pacote da sua camada | Atende | Pastas `Controllers/`, `Services/`, `Mappers/`, `Repositories/`, `Models/` e `Dtos/` |
| P3 Migrations e entidades de todas as tabelas | Atende | `Migrations/` e `Models/` |
| P4 DTOs de entrada e de saída de todas as entidades | Atende | `Dtos/` |
| P5 Estrutura completa, com o comentário no topo e o mapa no README | Atende | Classes das três entidades e `README.md`, seção "Arquitetura" |
| P6 API simples: `POST`, `GET` e `GET /{id}`, com `400` e `404` | Atende | `VeiculoController` e `VeiculoService` |
| P7 No mínimo 3 regras de negócio implementadas, com `400` | Atende | R1, R2 e R3 em `EntregaService` |
| P8 As regras atendem ao problema e ao tema | Atende | `README.md`, seção "O problema" |
| P9 Código igual ao desenho | Atende | `docs/modelo-de-dominio.md` e `docs/contrato-da-api.md` |
| P10 README completo e bem escrito | Atende | `README.md` |

**Pontos que o grupo espera no projeto:** 5,0

A apresentação vale os outros 5,0 pontos: são 3 perguntas, feitas na Aula 11.
