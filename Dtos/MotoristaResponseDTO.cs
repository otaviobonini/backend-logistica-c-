namespace Logistica_API.Dtos;

// CAMADA: DTO
// RESPONSABILIDADE: dados do motorista que a API devolve.
// SITUAÇÃO: pronto.
public class MotoristaResponseDTO
{
    public long Id { get; set; }
    public string Nome { get; set; }
    public string Cnh { get; set; }
    public DateOnly ValidadeCnh { get; set; }
}
