using Logistica_API.Dtos;
using Logistica_API.Models;

namespace Logistica_API.Mappers;

// CAMADA: Mapper
// RESPONSABILIDADE: converter DTO em Motorista e Motorista em DTO.
// SITUAÇÃO: implementado.
public class MotoristaMapper
{
    public Motorista ParaEntidade(MotoristaRequestDTO dto)
    {
        return new Motorista
        {
            Nome = dto.Nome,
            Cnh = dto.Cnh,
            ValidadeCnh = dto.ValidadeCnh.Value
        };
    }

    public MotoristaResponseDTO ParaResposta(Motorista motorista)
    {
        return new MotoristaResponseDTO
        {
            Id = motorista.Id,
            Nome = motorista.Nome,
            Cnh = motorista.Cnh,
            ValidadeCnh = motorista.ValidadeCnh
        };
    }
}
