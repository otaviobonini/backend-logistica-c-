namespace Logistica_API.Services;

// CAMADA: Service
// RESPONSABILIDADE: classe mãe de todo "não encontrado"; o ApiExceptionFilter devolve 404.
// SITUAÇÃO: implementada.
public class RecursoNaoEncontradoException : Exception
{
    public RecursoNaoEncontradoException(string mensagem) : base(mensagem)
    {
    }
}
