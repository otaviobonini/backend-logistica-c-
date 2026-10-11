using Microsoft.AspNetCore.WebUtilities;

namespace Logistica_API.Dtos;

// CAMADA: DTO
// RESPONSABILIDADE: formato único de todo erro da API (400 e 404).
// SITUAÇÃO: implementada.
public class ErroDTO
{
    public int Status { get; set; }
    public string Erro { get; set; }
    public string Mensagem { get; set; }
    public string Caminho { get; set; }
    public string Timestamp { get; set; }
    public List<CampoErroDTO> Campos { get; set; }

    public ErroDTO(int status, string mensagem, string caminho, List<CampoErroDTO> campos)
    {
        Status = status;
        Erro = ReasonPhrases.GetReasonPhrase(status);
        Mensagem = mensagem;
        Caminho = caminho;
        Timestamp = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss");
        Campos = campos;
    }
}
