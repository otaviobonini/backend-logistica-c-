using System.ComponentModel.DataAnnotations;

namespace Logistica_API.Dtos;

// CAMADA: DTO
// RESPONSABILIDADE: dados que o cliente envia para cadastrar um veículo.
// SITUAÇÃO: pronto.
public class VeiculoRequestDTO
{
    [Required(ErrorMessage = "Placa é obrigatória")]
    [StringLength(10, ErrorMessage = "Placa tem no máximo 10 caracteres")]
    public string Placa { get; set; }

    [Required(ErrorMessage = "Modelo é obrigatório")]
    [StringLength(100, ErrorMessage = "Modelo tem no máximo 100 caracteres")]
    public string Modelo { get; set; }

    [Range(0.01, 99999999.99, ErrorMessage = "Capacidade deve ser maior que zero")]
    public decimal CapacidadeKg { get; set; }
}
