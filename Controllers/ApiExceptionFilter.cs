using Logistica_API.Dtos;
using Logistica_API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Logistica_API.Controllers;

// CAMADA: Controller
// RESPONSABILIDADE: transformar exceções e campos inválidos no ErroDTO, num lugar só.
// SITUAÇÃO: implementada.
public class ApiExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        if (context.Exception is RecursoNaoEncontradoException)
        {
            context.Result = Responder(404, context.Exception.Message, context.HttpContext, new List<CampoErroDTO>());
            context.ExceptionHandled = true;
        }
        else if (context.Exception is RegraDeNegocioException)
        {
            context.Result = Responder(400, context.Exception.Message, context.HttpContext, new List<CampoErroDTO>());
            context.ExceptionHandled = true;
        }
    }

    public static IActionResult TratarValidacao(ActionContext context)
    {
        bool corpoIlegivel = context.ModelState.Keys.Any(chave => chave == "" || chave.StartsWith("$"));
        if (corpoIlegivel)
        {
            return Responder(400, "Corpo da requisição inválido: confira o JSON e o formato das datas (yyyy-MM-dd)",
                context.HttpContext, new List<CampoErroDTO>());
        }

        List<CampoErroDTO> campos = new List<CampoErroDTO>();
        foreach (var item in context.ModelState)
        {
            string campo = char.ToLower(item.Key[0]) + item.Key.Substring(1);
            foreach (var erro in item.Value.Errors)
            {
                campos.Add(new CampoErroDTO(campo, erro.ErrorMessage));
            }
        }
        campos = campos.OrderBy(c => c.Campo).ThenBy(c => c.Mensagem).ToList();

        return Responder(400, "Dados inválidos: confira a lista de campos", context.HttpContext, campos);
    }

    private static ObjectResult Responder(int status, string mensagem, HttpContext httpContext, List<CampoErroDTO> campos)
    {
        ErroDTO erro = new ErroDTO(status, mensagem, httpContext.Request.Path, campos);
        return new ObjectResult(erro) { StatusCode = status };
    }
}
