namespace Logistica_API.Services;

// CAMADA: Service
// RESPONSABILIDADE: avisar que um veículo não existe (404).
// SITUAÇÃO: implementada.
public class VeiculoNaoEncontradoException : RecursoNaoEncontradoException
{
    public VeiculoNaoEncontradoException(long id) : base("Veículo não encontrado: " + id)
    {
    }
}
