using Logistica_API.Dtos;
using Logistica_API.Mappers;
using Logistica_API.Models;
using Logistica_API.Repositories;

namespace Logistica_API.Services;

// CAMADA: Service
// RESPONSABILIDADE: cadastro, listagem e busca de veículos (API simples).
// SITUAÇÃO: implementado.
public class VeiculoService
{
    private readonly VeiculoRepository _repository;
    private readonly VeiculoMapper _mapper;

    public VeiculoService(VeiculoRepository repository, VeiculoMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public VeiculoResponseDTO Criar(VeiculoRequestDTO dto)
    {
        Veiculo salvo = _repository.Salvar(_mapper.ParaEntidade(dto));
        return _mapper.ParaResposta(salvo);
    }

    public List<VeiculoResponseDTO> Listar()
    {
        return _repository.ListarTodos().Select(_mapper.ParaResposta).ToList();
    }

    public VeiculoResponseDTO BuscarPorId(long id)
    {
        Veiculo veiculo = _repository.BuscarPorId(id);
        if (veiculo == null)
        {
            throw new VeiculoNaoEncontradoException(id);
        }
        return _mapper.ParaResposta(veiculo);
    }
}
