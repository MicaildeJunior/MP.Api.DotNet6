using MP.ApiDotNet6.Domain.Authentication;

namespace MP.ApiDotNet6.Api.Authentication;

public class CurrentUser : ICurrentUser
{
    public int Id { get; set; }
    public string Email { get; set; }
    public string Permissions { get; set; }

    // Atraves dessa interface vamos a cessar o AppContext e vamos conseguir acessar as nossar Claim
    // Lembrando que quando geramos um Token ela fica nas nossas Claim
    public CurrentUser(IHttpContextAccessor httpContextAccessor)
    {
        var httpContext = httpContextAccessor.HttpContext;
        var claims = httpContext.User.Claims;
        
        // Agora precisamos validar se elas vão ter as informaçoes ou nao
        if (claims.Any(x => x.Type == "Id"))
        {
            var id = Convert.ToInt32(claims.First(x => x.Type == "Id").Value);
            Id = id;
        }

        if (claims.Any(x => x.Type == "Email"))
        {
            Email = claims.First(x => x.Type == "Email").Value;
        }

        if (claims.Any(x => x.Type == "Permissioes"))
        {
            Permissions = claims.First(x => x.Type == "Permissioes").Value;
        }
    }
}
