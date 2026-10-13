using Logistica_API.Models;
using Microsoft.EntityFrameworkCore;

namespace Logistica_API.Repositories;

// CAMADA: Repository
// RESPONSABILIDADE: ligação com o PostgreSQL; cada DbSet é uma tabela criada pelas migrations.
// SITUAÇÃO: implementada.
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Veiculo> Veiculos { get; set; }
    public DbSet<Motorista> Motoristas { get; set; }
    public DbSet<Entrega> Entregas { get; set; }
}
