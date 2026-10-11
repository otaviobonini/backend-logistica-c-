using Logistica_API.Dtos;
using Logistica_API.Mappers;
using Logistica_API.Models;
using Logistica_API.Repositories;

namespace Logistica_API.Services;

// CAMADA: Service
// RESPONSABILIDADE: cadastro, listagem e busca de motoristas.
// SITUAÇÃO: implementado.
public class MotoristaService
{
    private readonly MotoristaRepository _repository;
    private readonly MotoristaMapper _mapper;

    public MotoristaService(MotoristaRepository repository, MotoristaMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public MotoristaResponseDTO Criar(MotoristaRequestDTO dto)
    {
        Motorista salvo = _repository.Salvar(_mapper.ParaEntidade(dto));
        return _mapper.ParaResposta(salvo);
    }

    public List<MotoristaResponseDTO> Listar()
    {
        return _repository.ListarTodos().Select(_mapper.ParaResposta).ToList();
    }

    public MotoristaResponseDTO BuscarPorId(long id)
    {
        Motorista motorista = _repository.BuscarPorId(id);
        if (motorista == null)
        {
            throw new MotoristaNaoEncontradoException(id);
        }
        return _mapper.ParaResposta(motorista);
    }
}
