using Logistica_API.Models;
using Microsoft.EntityFrameworkCore;

namespace Logistica_API.Repositories;

// CAMADA: Repository
// RESPONSABILIDADE: acesso à tabela entregas, incluindo a consulta usada na R3.
// SITUAÇÃO: implementado.
public class EntregaRepository
{
    private readonly AppDbContext _contexto;

    public EntregaRepository(AppDbContext contexto)
    {
        _contexto = contexto;
    }

    public Entrega Salvar(Entrega entrega)
    {
        _contexto.Entregas.Add(entrega);
        _contexto.SaveChanges();
        return entrega;
    }

    public Entrega BuscarPorId(long id)
    {
        return _contexto.Entregas
            .Include(e => e.Veiculo)
            .Include(e => e.Motorista)
            .FirstOrDefault(e => e.Id == id);
    }

    public bool ExisteEmAndamentoParaVeiculo(long veiculoId)
    {
        return _contexto.Entregas.Any(e => e.VeiculoId == veiculoId && e.DataConclusao == null);
    }

    public void SalvarAlteracoes()
    {
        _contexto.SaveChanges();
    }
}
