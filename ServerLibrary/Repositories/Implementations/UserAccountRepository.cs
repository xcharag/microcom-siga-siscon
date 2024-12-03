using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using BaseLibrary.DTOs;
using BaseLibrary.Entities;
using BaseLibrary.Responses;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
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

    public async Task<LoginResponse> SignInAsync(Login? user)
    {
        if (user is null) return new LoginResponse(false, "El Modelo esta vacio");
        
        var applicationUser = await FindUserByUsername(user.Username!);
        if (applicationUser is null) return new LoginResponse(false, "Usuario no encontrado");
        
        //Verify Password
        if (!BCrypt.Net.BCrypt.Verify(user.Password, applicationUser.Password))
            return new LoginResponse(false, "Credenciales incorrectas");
        
        var getUserRole = await FindUserRole(applicationUser.CodUsuario);
        if (getUserRole is null) return new LoginResponse(false, "Usuario sin roles asignados");
        
        var getRoleName = await FindRoleName(getUserRole.RoleId);
        if (getRoleName is null) return new LoginResponse(false, "Rol no encontrado");
        
        string jwtToken = GenerateToken(applicationUser, getRoleName.Name!);
        string refreshToken = GenerateRefreshToken();
        
        var findUser = await appDbContext.RefreshTokenInfos.FirstOrDefaultAsync(x => x.UserId == applicationUser.CodUsuario);
        if (findUser is not null)
        {
            findUser!.Token = refreshToken;
            await appDbContext.SaveChangesAsync();
        }
        else
        {
            await AddToDatabase(new RefreshTokenInfo(){ Token = refreshToken, UserId = applicationUser.CodUsuario });
        }
        
        return new LoginResponse(true, "Inicio de Sesion Exitoso", jwtToken, refreshToken);
    }

    public async Task<LoginResponse> RefreshTokenAsync(RefreshToken? token)
    {
        if (token is null) return new LoginResponse(false, "El Modelo esta vacio");
        
        var findToken = await appDbContext.RefreshTokenInfos.FirstOrDefaultAsync(x => x.Token!.Equals(token.Token));
        if (findToken is null) return new LoginResponse(false, "Token de refresco necesario");
        
        var user = await appDbContext.Usuarios.FirstOrDefaultAsync(x => x.CodUsuario == findToken.UserId);
        if (user is null) return new LoginResponse(false, "El token de refresco no puede ser generado porque el usuario no  ha sido encontrado");
        
        var getUserRole = await FindUserRole(user.CodUsuario);
        if (getUserRole is null) return new LoginResponse(false, "Usuario sin roles asignados");
        
        var getRoleName = await FindRoleName(getUserRole.RoleId);
        if (getRoleName is null) return new LoginResponse(false, "Rol no encontrado");
        
        string jwtToken = GenerateToken(user, getRoleName.Name!);
        string refreshToken = GenerateRefreshToken();
        
        var updateRefreshToken = await appDbContext.RefreshTokenInfos.FirstOrDefaultAsync(x => x.UserId == user.CodUsuario);
        if (updateRefreshToken is null) return new LoginResponse(false,
                "El token de refresco no pudo ser generado porque el usuario no el usuario no se ha loggeado");
        
        updateRefreshToken.Token = refreshToken;
        await appDbContext.SaveChangesAsync();
        return new LoginResponse(true, "Token de refresco generado con exito", jwtToken, refreshToken);
    }

    private string GenerateToken(Usuario user, string role)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.Value.Key!));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        var userClaims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.CodUsuario.ToString()),
            new Claim(ClaimTypes.Name, user.FirstName! + " " + user.LastName!),
            new Claim(ClaimTypes.Email, user.Username!),
            new Claim(ClaimTypes.Role, role)
        };

        var token = new JwtSecurityToken(
            issuer: config.Value.Issuer,
            audience: config.Value.Audience,
            claims: userClaims,
            expires: DateTime.Now.AddDays(1),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private static string GenerateRefreshToken() => Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
    
    private async Task<UserRole?> FindUserRole(int userId)
    {
        return await appDbContext.UserRoles.FirstOrDefaultAsync(x => x.UserId == userId);
    }
    
    private async Task<SystemRole?> FindRoleName(int roleId)
    {
        return await appDbContext.SystemRoles.FirstOrDefaultAsync(x => x.Id == roleId);
    }
    
    private async Task<Usuario?> FindUserByUsername(string username)
    {
        return await appDbContext.Usuarios.FirstOrDefaultAsync(x => x.Username!.ToLower().Equals(username.ToLower()));
    }
    
    private async Task<T> AddToDatabase<T>(T model)
    {
        var result = appDbContext.Add(model!);
        await appDbContext.SaveChangesAsync();
        return (T)result.Entity;
    }
}