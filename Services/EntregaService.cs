using Logistica_API.Dtos;
using Logistica_API.Mappers;
using Logistica_API.Models;
using Logistica_API.Repositories;

namespace Logistica_API.Services;

// CAMADA: Service
// RESPONSABILIDADE: regras de negócio das entregas: R1 (capacidade), R2 (CNH válida) e R3 (veículo livre).
// SITUAÇÃO: implementado.
public class EntregaService
{
    private readonly EntregaRepository _repository;
    private readonly VeiculoRepository _veiculoRepository;
    private readonly MotoristaRepository _motoristaRepository;
    private readonly EntregaMapper _mapper;

    public EntregaService(EntregaRepository repository, VeiculoRepository veiculoRepository,
        MotoristaRepository motoristaRepository, EntregaMapper mapper)
    {
        _repository = repository;
        _veiculoRepository = veiculoRepository;
        _motoristaRepository = motoristaRepository;
        _mapper = mapper;
    }

    public EntregaResponseDTO Criar(EntregaRequestDTO dto)
    {
        Veiculo veiculo = _veiculoRepository.BuscarPorId(dto.VeiculoId.Value);
        if (veiculo == null)
        {
            throw new VeiculoNaoEncontradoException(dto.VeiculoId.Value);
        }

        Motorista motorista = _motoristaRepository.BuscarPorId(dto.MotoristaId.Value);
        if (motorista == null)
        {
            throw new MotoristaNaoEncontradoException(dto.MotoristaId.Value);
        }

        DateOnly hoje = DateOnly.FromDateTime(DateTime.Today);

        if (dto.PesoKg > veiculo.CapacidadeKg)
        {
            throw new RegraDeNegocioException("R1: o peso da entrega (" + dto.PesoKg + " kg) passa da capacidade do veículo "
                + veiculo.Placa + " (" + veiculo.CapacidadeKg + " kg)");
        }

        if (motorista.ValidadeCnh < hoje)
        {
            throw new RegraDeNegocioException("R2: a CNH do motorista " + motorista.Nome + " venceu em "
                + motorista.ValidadeCnh.ToString("dd/MM/yyyy"));
        }

        if (_repository.ExisteEmAndamentoParaVeiculo(veiculo.Id))
        {
            throw new RegraDeNegocioException("R3: o veículo " + veiculo.Placa + " já está em uma entrega em andamento");
        }

        Entrega entrega = _mapper.ParaEntidade(dto, veiculo, motorista);
        entrega.DataSaida = hoje;
        Entrega salva = _repository.Salvar(entrega);
        return _mapper.ParaResposta(salva);
    }

    public EntregaResponseDTO BuscarPorId(long id)
    {
        return _mapper.ParaResposta(BuscarEntrega(id));
    }

    public EntregaResponseDTO Concluir(long id)
    {
        Entrega entrega = BuscarEntrega(id);

        if (entrega.DataConclusao != null)
        {
            throw new RegraDeNegocioException("R3: a entrega " + id + " já foi concluída");
        }

        entrega.DataConclusao = DateOnly.FromDateTime(DateTime.Today);
        _repository.SalvarAlteracoes();
        return _mapper.ParaResposta(entrega);
    }

    private Entrega BuscarEntrega(long id)
    {
        Entrega entrega = _repository.BuscarPorId(id);
        if (entrega == null)
        {
            throw new EntregaNaoEncontradaException(id);
        }
        return entrega;
    }
}
