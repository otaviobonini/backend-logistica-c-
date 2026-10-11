using Logistica_API.Models;

namespace Logistica_API.Repositories;

// CAMADA: Repository
// RESPONSABILIDADE: acesso à tabela motoristas.
// SITUAÇÃO: implementado.
public class MotoristaRepository
{
    private readonly AppDbContext _contexto;

    public MotoristaRepository(AppDbContext contexto)
    {
        _contexto = contexto;
    }

    public Motorista Salvar(Motorista motorista)
    {
        _contexto.Motoristas.Add(motorista);
        _contexto.SaveChanges();
        return motorista;
    }

    public List<Motorista> ListarTodos()
    {
        return _contexto.Motoristas.OrderBy(m => m.Id).ToList();
    }

    public Motorista BuscarPorId(long id)
    {
        return _contexto.Motoristas.Find(id);
    }
}
