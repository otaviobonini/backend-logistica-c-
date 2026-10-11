using Logistica_API.Dtos;
using Logistica_API.Services;
using Microsoft.AspNetCore.Mvc;

namespace Logistica_API.Controllers;

// CAMADA: Controller
// RESPONSABILIDADE: receber as requisições de /motoristas e repassar ao MotoristaService.
// SITUAÇÃO: implementado.
[ApiController]
[Route("motoristas")]
[Produces("application/json")]
public class MotoristaController : ControllerBase
{
    private readonly MotoristaService _service;

    public MotoristaController(MotoristaService service)
    {
        _service = service;
    }

    [HttpPost]
    [ProducesResponseType(typeof(MotoristaResponseDTO), 201)]
    [ProducesResponseType(typeof(ErroDTO), 400)]
    public ActionResult<MotoristaResponseDTO> Criar(MotoristaRequestDTO dto)
    {
        return StatusCode(201, _service.Criar(dto));
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<MotoristaResponseDTO>), 200)]
    public List<MotoristaResponseDTO> Listar()
    {
        return _service.Listar();
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(MotoristaResponseDTO), 200)]
    [ProducesResponseType(typeof(ErroDTO), 404)]
    public MotoristaResponseDTO BuscarPorId(long id)
    {
        return _service.BuscarPorId(id);
    }
}
