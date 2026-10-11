using Logistica_API.Dtos;
using Logistica_API.Models;

namespace Logistica_API.Mappers;

// CAMADA: Mapper
// RESPONSABILIDADE: converter DTO em Veiculo e Veiculo em DTO.
// SITUAÇÃO: implementado.
public class VeiculoMapper
{
    public Veiculo ParaEntidade(VeiculoRequestDTO dto)
    {
        return new Veiculo
        {
            Placa = dto.Placa,
            Modelo = dto.Modelo,
            CapacidadeKg = dto.CapacidadeKg
        };
    }

    public VeiculoResponseDTO ParaResposta(Veiculo veiculo)
    {
        return new VeiculoResponseDTO
        {
            Id = veiculo.Id,
            Placa = veiculo.Placa,
            Modelo = veiculo.Modelo,
            CapacidadeKg = veiculo.CapacidadeKg
        };
    }
}
