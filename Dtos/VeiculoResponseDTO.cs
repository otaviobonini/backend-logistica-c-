namespace Logistica_API.Dtos;

// CAMADA: DTO
// RESPONSABILIDADE: dados do veículo que a API devolve.
// SITUAÇÃO: pronto.
public class VeiculoResponseDTO
{
    public long Id { get; set; }
    public string Placa { get; set; }
    public string Modelo { get; set; }
    public decimal CapacidadeKg { get; set; }
}
