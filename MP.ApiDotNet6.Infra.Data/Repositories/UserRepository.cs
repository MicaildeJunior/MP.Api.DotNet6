using Microsoft.EntityFrameworkCore;
using MP.ApiDotNet6.Domain.Entities;
using MP.ApiDotNet6.Domain.Repositories;
using MP.ApiDotNet6.Infra.Data.Context;

namespace MP.ApiDotNet6.Infra.Data.Repositories;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _db;

    public UserRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    // Usuario e Permissoes tem relacionamento, entao é preciso fazer um Join('Include')
    // TheanInclude indica que irá fazer um relacionamento com o ultimo relacionamento que fez
    // Nesse caso UsuarioPermissao
    public async Task<User?> GetUserByEmailAndPasswordAsync(string email, string password)
    {
        return await _db.Users
                        .Include(u => u.UserPermissions).ThenInclude(u => u.Permission)
                        .FirstOrDefaultAsync(u => u.Email == email && u.Password == password);
    }
}
