namespace Logistica_API.Dtos;

// CAMADA: DTO
// RESPONSABILIDADE: um campo inválido dentro da lista "campos" do ErroDTO.
// SITUAÇÃO: implementada.
public class CampoErroDTO
{
    public string Campo { get; set; }
    public string Mensagem { get; set; }

    public CampoErroDTO(string campo, string mensagem)
    {
        Campo = campo;
        Mensagem = mensagem;
    }
}
