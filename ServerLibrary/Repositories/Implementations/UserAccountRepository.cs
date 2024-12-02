using BaseLibrary.DTOs;
using BaseLibrary.Entities;
using BaseLibrary.Responses;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using ServerLibrary.Data;
using ServerLibrary.Helpers;
using ServerLibrary.Repositories.Contracts;

namespace ServerLibrary.Repositories.Implementations;

public class UserAccountRepository(IOptions<JwtSection> config, AppDbContext appDbContext) : IUserAccount
{
    public async Task<GeneralResponse> CreateAsync(Register? user)
    {
        if (user is null) return new GeneralResponse(false, "El modelo esta vacio");

        var checkUser = await FindUserByUsername(user.Username!);
        if (checkUser is not null) return new GeneralResponse(false, "El usuario ya existe");
        
        //Save User
        var newUser = await AddToDatabase(new Usuario()
        {
            Username = user.Username,
            Password = BCrypt.Net.BCrypt.HashPassword(user.Password),
            FirstName = user.FirstName,
            LastName = user.LastName
        });
        
        //Check, create and assign roles
        var checkAdminRole = await appDbContext.SystemRoles.FirstOrDefaultAsync(x => x.Name!.Equals(Constants.Admin));
        if (checkAdminRole is null)
        {
            var createAdminRole = await AddToDatabase(new SystemRole() { Name = Constants.Admin });
            await AddToDatabase(new UserRole() { RoleId = createAdminRole.Id, UserId = newUser.CodUsuario});
            return new GeneralResponse(true, "Usuario Administrador creado con exito");
        }
        
        var checkUserRole = await appDbContext.SystemRoles.FirstOrDefaultAsync(x => x.Name!.Equals(Constants.User));
        if (checkUserRole is null)
        {
            var createUserRole = await AddToDatabase(new SystemRole() { Name = Constants.User });
            await AddToDatabase(new UserRole() { RoleId = createUserRole.Id, UserId = newUser.CodUsuario});
        }
        else
        {
            await AddToDatabase(new UserRole() { RoleId = checkUserRole.Id, UserId = newUser.CodUsuario});
        }
        return new GeneralResponse(true, "Usuario creado con exito");
    }

    public Task<LoginResponse> SignInAsync(Login user)
    {
        throw new NotImplementedException();
    }
    
    private async Task<Usuario?> FindUserByUsername(string username)
    {
        return await appDbContext.Usuarios.FirstOrDefaultAsync(x => x.Username!.ToLower()!.Equals(username!.ToLower()));
    }
    
    private async Task<T> AddToDatabase<T>(T model)
    {
        var result = appDbContext.Add(model!);
        await appDbContext.SaveChangesAsync();
        return (T)result.Entity;
    }
}