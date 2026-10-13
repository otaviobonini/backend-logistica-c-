using Logistica_API.Dtos;
using Logistica_API.Models;

namespace Logistica_API.Mappers;

// CAMADA: Mapper
// RESPONSABILIDADE: converter DTO em Entrega e Entrega em DTO.
// SITUAÇÃO: implementado.
public class EntregaMapper
{
    private readonly VeiculoMapper _veiculoMapper;
    private readonly MotoristaMapper _motoristaMapper;

    public EntregaMapper(VeiculoMapper veiculoMapper, MotoristaMapper motoristaMapper)
    {
        _veiculoMapper = veiculoMapper;
        _motoristaMapper = motoristaMapper;
    }

    public Entrega ParaEntidade(EntregaRequestDTO dto, Veiculo veiculo, Motorista motorista)
    {
        return new Entrega
        {
            Descricao = dto.Descricao,
            Destino = dto.Destino,
            PesoKg = dto.PesoKg,
            Veiculo = veiculo,
            Motorista = motorista
        };
    }

    public EntregaResponseDTO ParaResposta(Entrega entrega)
    {
        return new EntregaResponseDTO
        {
            Id = entrega.Id,
            Descricao = entrega.Descricao,
            Destino = entrega.Destino,
            PesoKg = entrega.PesoKg,
            DataSaida = entrega.DataSaida,
            DataConclusao = entrega.DataConclusao,
            EmAndamento = entrega.DataConclusao == null,
            Veiculo = _veiculoMapper.ParaResposta(entrega.Veiculo),
            Motorista = _motoristaMapper.ParaResposta(entrega.Motorista)
        };
    }
}
