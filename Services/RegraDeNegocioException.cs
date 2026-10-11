namespace Logistica_API.Services;

// CAMADA: Service
// RESPONSABILIDADE: regra de negócio violada; o ApiExceptionFilter devolve 400.
// SITUAÇÃO: implementada.
public class RegraDeNegocioException : Exception
{
    public RegraDeNegocioException(string mensagem) : base(mensagem)
    {
    }
}
