using Logistica_API.Dtos;
using Logistica_API.Services;
using Microsoft.AspNetCore.Mvc;

namespace Logistica_API.Controllers;

// CAMADA: Controller
// RESPONSABILIDADE: receber as requisições de /entregas; as regras ficam no EntregaService.
// SITUAÇÃO: implementado.
[ApiController]
[Route("entregas")]
[Produces("application/json")]
public class EntregaController : ControllerBase
{
    private readonly EntregaService _service;

    public EntregaController(EntregaService service)
    {
        _service = service;
    }

    [HttpPost]
    [ProducesResponseType(typeof(EntregaResponseDTO), 201)]
    [ProducesResponseType(typeof(ErroDTO), 400)]
    [ProducesResponseType(typeof(ErroDTO), 404)]
    public ActionResult<EntregaResponseDTO> Criar(EntregaRequestDTO dto)
    {
        return StatusCode(201, _service.Criar(dto));
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(EntregaResponseDTO), 200)]
    [ProducesResponseType(typeof(ErroDTO), 404)]
    public EntregaResponseDTO BuscarPorId(long id)
    {
        return _service.BuscarPorId(id);
    }

    [HttpPut("{id}/concluir")]
    [ProducesResponseType(typeof(EntregaResponseDTO), 200)]
    [ProducesResponseType(typeof(ErroDTO), 400)]
    [ProducesResponseType(typeof(ErroDTO), 404)]
    public EntregaResponseDTO Concluir(long id)
    {
        return _service.Concluir(id);
    }
}
