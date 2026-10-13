using System.ComponentModel.DataAnnotations;

namespace Logistica_API.Dtos;

// CAMADA: DTO
// RESPONSABILIDADE: dados que o cliente envia para registrar uma entrega.
// SITUAÇÃO: pronto.
public class EntregaRequestDTO
{
    [Required(ErrorMessage = "Descrição é obrigatória")]
    [StringLength(255, ErrorMessage = "Descrição tem no máximo 255 caracteres")]
    public string Descricao { get; set; }

    [Required(ErrorMessage = "Destino é obrigatório")]
    [StringLength(255, ErrorMessage = "Destino tem no máximo 255 caracteres")]
    public string Destino { get; set; }

    [Range(0.01, 99999999.99, ErrorMessage = "Peso deve ser maior que zero")]
    public decimal PesoKg { get; set; }

    [Required(ErrorMessage = "Veículo é obrigatório")]
    public long? VeiculoId { get; set; }

    [Required(ErrorMessage = "Motorista é obrigatório")]
    public long? MotoristaId { get; set; }
}
