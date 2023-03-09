using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography.X509Certificates;

namespace MP.ApiDotNet6.Api.Controllers;

[Authorize]
[ApiController]
public class BaseController : ControllerBase
{
    // Só ter ação quando chamar
    [NonAction]
    public bool ValidPermission(List<string> permissionUser, List<string> permissionNeeded)
    {       
        // Se ele tem as informaçoes que precisa ele retorna true se não tiver retorna false
        return permissionNeeded.Any(x => permissionUser.Contains(x));
    }

    /* Com isso a classe que vai receber o nosso token, e o ControllerBase 
       que vai validar tbm, caso a pessoa não ter o acesso retorna a menssagem
       se tiver retorna os serviços
     */
    [NonAction]
    public IActionResult Forbidden()
    {
        var obj = new
        {
            code = "permissao_negada",
            message = "Usuário não tem permissão para acessar esse recurso"
        };
        return new ObjectResult(obj) { StatusCode = 403 };
    }
}
