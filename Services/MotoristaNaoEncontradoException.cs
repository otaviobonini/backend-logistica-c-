namespace Logistica_API.Services;

// CAMADA: Service
// RESPONSABILIDADE: avisar que um motorista não existe (404).
// SITUAÇÃO: implementada.
public class MotoristaNaoEncontradoException : RecursoNaoEncontradoException
{
    public MotoristaNaoEncontradoException(long id) : base("Motorista não encontrado: " + id)
    {
    }
}
