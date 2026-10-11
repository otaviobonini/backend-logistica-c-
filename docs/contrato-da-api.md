# Contrato da API

Este documento descreve o que o cliente enxerga: rotas, JSON e status.

## Convenções

| Item | Valor |
| Endereço base | `http://localhost:5080` |
| Formato | JSON na requisição e na resposta |
| Datas | `yyyy-MM-dd` |
| Erros | Sempre no formato `ErroDTO` (veja [Erros](#erros)) |
| Swagger | `http://localhost:5080/swagger` |

## Rotas

A coluna **Funciona** diz se a rota já responde nesta etapa.

### Veículos

| Verbo | Caminho | O que faz | Entrada | Saída | Sucesso | Erros | Funciona |
| `GET` | `/veiculos` | Lista os veículos | Nenhuma | lista de `VeiculoResponseDTO` | `200` | Nenhum | sim |
| `GET` | `/veiculos/{id}` | Busca um veículo | Nenhuma | `VeiculoResponseDTO` | `200` | `404` | sim |
| `POST` | `/veiculos` | Cadastra um veículo | `VeiculoRequestDTO` | `VeiculoResponseDTO` | `201` | `400` | sim |
| `PUT` | `/veiculos/{id}` | Atualiza um veículo | `VeiculoRequestDTO` | `VeiculoResponseDTO` | `200` | `400`, `404` | não |
| `DELETE` | `/veiculos/{id}` | Remove um veículo | Nenhuma | Sem corpo | `204` | `404` | não |

### Motoristas

| Verbo | Caminho | O que faz | Entrada | Saída | Sucesso | Erros | Funciona |
| `GET` | `/motoristas` | Lista os motoristas | Nenhuma | lista de `MotoristaResponseDTO` | `200` | Nenhum | sim |
| `GET` | `/motoristas/{id}` | Busca um motorista | Nenhuma | `MotoristaResponseDTO` | `200` | `404` | sim |
| `POST` | `/motoristas` | Cadastra um motorista | `MotoristaRequestDTO` | `MotoristaResponseDTO` | `201` | `400` | sim |
| `PUT` | `/motoristas/{id}` | Atualiza um motorista | `MotoristaRequestDTO` | `MotoristaResponseDTO` | `200` | `400`, `404` | não |
| `DELETE` | `/motoristas/{id}` | Remove um motorista | Nenhuma | Sem corpo | `204` | `404` | não |

### Entregas

| Verbo | Caminho | O que faz | Entrada | Saída | Sucesso | Erros | Funciona |
| `POST` | `/entregas` | Registra a saída de uma entrega (R1, R2 e R3) | `EntregaRequestDTO` | `EntregaResponseDTO` | `201` | `400`, `404` | sim |
| `GET` | `/entregas/{id}` | Busca uma entrega | Nenhuma | `EntregaResponseDTO` | `200` | `404` | sim |
| `PUT` | `/entregas/{id}/concluir` | Conclui a entrega e libera o veículo (R3) | Nenhuma | `EntregaResponseDTO` | `200` | `400`, `404` | sim |
| `GET` | `/entregas` | Lista as entregas | Nenhuma | lista de `EntregaResponseDTO` | `200` | Nenhum | não |

## DTOs

O cliente nunca envia `id` nem os campos que a API decide (como `dataSaida`).

| DTO | Direção | Campos | Validações |
| `VeiculoRequestDTO` | entrada | `placa`, `modelo`, `capacidadeKg` | `placa` e `modelo` obrigatórios; `capacidadeKg` maior que zero |
| `VeiculoResponseDTO` | saída | `id`, `placa`, `modelo`, `capacidadeKg` | Nenhuma |
| `MotoristaRequestDTO` | entrada | `nome`, `cnh`, `validadeCnh` | os três obrigatórios |
| `MotoristaResponseDTO` | saída | `id`, `nome`, `cnh`, `validadeCnh` | Nenhuma |
| `EntregaRequestDTO` | entrada | `descricao`, `destino`, `pesoKg`, `veiculoId`, `motoristaId` | `descricao`, `destino`, `veiculoId` e `motoristaId` obrigatórios; `pesoKg` maior que zero |
| `EntregaResponseDTO` | saída | `id`, `descricao`, `destino`, `pesoKg`, `dataSaida`, `dataConclusao`, `emAndamento`, `veiculo`, `motorista` | Nenhuma |

## Exemplos

### `POST /veiculos`

Requisição:

```json
{
  "placa": "XYZ9K88",
  "modelo": "Fiat Fiorino",
  "capacidadeKg": 650
}
```

Resposta, `201 Created`:

```json
{
  "id": 2,
  "placa": "XYZ9K88",
  "modelo": "Fiat Fiorino",
  "capacidadeKg": 650
}
```

### `POST /motoristas`

Requisição:

```json
{
  "nome": "Carlos Pereira",
  "cnh": "12345678900",
  "validadeCnh": "2028-05-10"
}
```

Resposta, `201 Created`:

```json
{
  "id": 1,
  "nome": "Carlos Pereira",
  "cnh": "12345678900",
  "validadeCnh": "2028-05-10"
}
```

### `POST /entregas`

Requisição:

```json
{
  "descricao": "Cimento CP II, 6 sacos",
  "destino": "Rua Henrique Lage, 500, Criciúma",
  "pesoKg": 300,
  "veiculoId": 2,
  "motoristaId": 1
}
```

Resposta, `201 Created`:

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

### `PUT /entregas/1/concluir`

Não tem corpo. Responde `200` com a mesma entrega, agora com `dataConclusao` preenchida e `emAndamento` falso.

## Erros

Todo erro sai no mesmo formato. Exemplo de `GET /veiculos/999`:

```json
{
  "status": 404,
  "erro": "Not Found",
  "mensagem": "Veículo não encontrado: 999",
  "caminho": "/veiculos/999",
  "timestamp": "2026-10-10T21:43:46",
  "campos": []
}
```

No `400` de validação, a lista `campos` traz um item por campo inválido, por exemplo `{"campo": "placa", "mensagem": "Placa é obrigatória"}`.

| Status | Quando acontece |
| `400` | JSON ilegível, data fora do formato ou campo inválido |
| `400` | Regra de negócio violada: a mensagem começa com R1, R2 ou R3 e explica a regra |
| `404` | Id de veículo, motorista ou entrega que não existe |
