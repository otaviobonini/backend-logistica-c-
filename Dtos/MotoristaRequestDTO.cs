using System.ComponentModel.DataAnnotations;

namespace Logistica_API.Dtos;

// CAMADA: DTO
// RESPONSABILIDADE: dados que o cliente envia para cadastrar um motorista.
// SITUAÇÃO: pronto.
public class MotoristaRequestDTO
{
    [Required(ErrorMessage = "Nome é obrigatório")]
    [StringLength(150, ErrorMessage = "Nome tem no máximo 150 caracteres")]
    public string Nome { get; set; }

    [Required(ErrorMessage = "CNH é obrigatória")]
    [StringLength(20, ErrorMessage = "CNH tem no máximo 20 caracteres")]
    public string Cnh { get; set; }

    [Required(ErrorMessage = "Validade da CNH é obrigatória")]
    public DateOnly? ValidadeCnh { get; set; }
}
