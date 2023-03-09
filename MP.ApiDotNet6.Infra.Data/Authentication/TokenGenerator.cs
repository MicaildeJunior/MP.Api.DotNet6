using Microsoft.IdentityModel.Tokens;
using MP.ApiDotNet6.Domain.Authentication;
using MP.ApiDotNet6.Domain.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MP.ApiDotNet6.Infra.Data.Authentication;

public class TokenGenerator : ITokenGenerator
{
    public dynamic Generator(User user)
    {
        // Vai pegar uma List e transformar ela em um string, cada item da List separado por virgula
        var permission = string.Join(",", user.UserPermissions.Select(x => x.Permission?.PermissionName));

        var claims = new List<Claim>
        {
            new Claim("Email", user.Email),
            new Claim("Id", user.Id.ToString()),
            new Claim("Permissioes", permission)
        };

        var experies = DateTime.Now.AddDays(1);
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("projetoDotNetCore6"));
        var tokenData = new JwtSecurityToken(
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature),
                expires: experies,
                claims: claims
            );

        var token = new JwtSecurityTokenHandler().WriteToken(tokenData);
        return new
        {
            acess_token = token,
            expitarions = experies
        };
    }
}
