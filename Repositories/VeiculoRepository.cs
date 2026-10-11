using Logistica_API.Models;

namespace Logistica_API.Repositories;

// CAMADA: Repository
// RESPONSABILIDADE: acesso à tabela veiculos.
// SITUAÇÃO: implementado.
public class VeiculoRepository
{
    private readonly AppDbContext _contexto;

    public VeiculoRepository(AppDbContext contexto)
    {
        _contexto = contexto;
    }

    public Veiculo Salvar(Veiculo veiculo)
    {
        _contexto.Veiculos.Add(veiculo);
        _contexto.SaveChanges();
        return veiculo;
    }

    public List<Veiculo> ListarTodos()
    {
        return _contexto.Veiculos.OrderBy(v => v.Id).ToList();
    }

    public Veiculo BuscarPorId(long id)
    {
        return _contexto.Veiculos.Find(id);
    }
}
