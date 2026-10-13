namespace Logistica_API.Services;

// CAMADA: Service
// RESPONSABILIDADE: avisar que uma entrega não existe (404).
// SITUAÇÃO: implementada.
public class EntregaNaoEncontradaException : RecursoNaoEncontradoException
{
    public EntregaNaoEncontradaException(long id) : base("Entrega não encontrada: " + id)
    {
    }
}
