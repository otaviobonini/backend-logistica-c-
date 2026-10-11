using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Logistica_API.Models;

// CAMADA: Model
// RESPONSABILIDADE: um veículo da frota, com a capacidade de carga usada na R1.
// SITUAÇÃO: pronta.
[Table("veiculos")]
public class Veiculo
{
    [Column("id")]
    public long Id { get; set; }

    [Required]
    [MaxLength(10)]
    [Column("placa")]
    public string Placa { get; set; }

    [Required]
    [MaxLength(100)]
    [Column("modelo")]
    public string Modelo { get; set; }

    [Column("capacidade_kg", TypeName = "numeric(10,2)")]
    public decimal CapacidadeKg { get; set; }
}
