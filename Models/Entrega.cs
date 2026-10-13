using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Logistica_API.Models;

// CAMADA: Model
// RESPONSABILIDADE: uma carga levada por um veículo e um motorista até o destino.
// SITUAÇÃO: pronta.
[Table("entregas")]
public class Entrega
{
    [Column("id")]
    public long Id { get; set; }

    [Required]
    [MaxLength(255)]
    [Column("descricao")]
    public string Descricao { get; set; }

    [Required]
    [MaxLength(255)]
    [Column("destino")]
    public string Destino { get; set; }

    [Column("peso_kg", TypeName = "numeric(10,2)")]
    public decimal PesoKg { get; set; }

    [Column("data_saida")]
    public DateOnly DataSaida { get; set; }

    [Column("data_conclusao")]
    public DateOnly? DataConclusao { get; set; }

    [Column("veiculo_id")]
    public long VeiculoId { get; set; }

    [DeleteBehavior(DeleteBehavior.Restrict)]
    public Veiculo Veiculo { get; set; }

    [Column("motorista_id")]
    public long MotoristaId { get; set; }

    [DeleteBehavior(DeleteBehavior.Restrict)]
    public Motorista Motorista { get; set; }
}
