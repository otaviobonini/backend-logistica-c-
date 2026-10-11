using Logistica_API.Dtos;
using Logistica_API.Services;
using Microsoft.AspNetCore.Mvc;

namespace Logistica_API.Controllers;

// CAMADA: Controller
// RESPONSABILIDADE: receber as requisições de /veiculos e repassar ao VeiculoService.
// SITUAÇÃO: implementado.
[ApiController]
[Route("veiculos")]
[Produces("application/json")]
public class VeiculoController : ControllerBase
{
    private readonly VeiculoService _service;

    public VeiculoController(VeiculoService service)
    {
        _service = service;
    }

    [HttpPost]
    [ProducesResponseType(typeof(VeiculoResponseDTO), 201)]
    [ProducesResponseType(typeof(ErroDTO), 400)]
    public ActionResult<VeiculoResponseDTO> Criar(VeiculoRequestDTO dto)
    {
        return StatusCode(201, _service.Criar(dto));
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<VeiculoResponseDTO>), 200)]
    public List<VeiculoResponseDTO> Listar()
    {
        return _service.Listar();
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(VeiculoResponseDTO), 200)]
    [ProducesResponseType(typeof(ErroDTO), 404)]
    public VeiculoResponseDTO BuscarPorId(long id)
    {
        return _service.BuscarPorId(id);
    }
}
