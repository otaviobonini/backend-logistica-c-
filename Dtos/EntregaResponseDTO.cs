namespace Logistica_API.Dtos;

// CAMADA: DTO
// RESPONSABILIDADE: dados da entrega que a API devolve, com veículo e motorista completos.
// SITUAÇÃO: pronto.
public class EntregaResponseDTO
{
    public long Id { get; set; }
    public string Descricao { get; set; }
    public string Destino { get; set; }
    public decimal PesoKg { get; set; }
    public DateOnly DataSaida { get; set; }
    public DateOnly? DataConclusao { get; set; }
    public bool EmAndamento { get; set; }
    public VeiculoResponseDTO Veiculo { get; set; }
    public MotoristaResponseDTO Motorista { get; set; }
}
