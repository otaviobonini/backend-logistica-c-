using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Logistica_API.Models;

// CAMADA: Model
// RESPONSABILIDADE: um motorista da transportadora, com a validade da CNH usada na R2.
// SITUAÇÃO: pronta.
[Table("motoristas")]
public class Motorista
{
    [Column("id")]
    public long Id { get; set; }

    [Required]
    [MaxLength(150)]
    [Column("nome")]
    public string Nome { get; set; }

    [Required]
    [MaxLength(20)]
    [Column("cnh")]
    public string Cnh { get; set; }

    [Column("validade_cnh")]
    public DateOnly ValidadeCnh { get; set; }
}
