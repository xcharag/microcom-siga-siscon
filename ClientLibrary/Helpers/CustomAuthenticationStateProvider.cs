using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using BaseLibrary.DTOs;
using Microsoft.AspNetCore.Components.Authorization;

namespace ClientLibrary.Helpers;

public class CustomAuthenticationStateProvider(LocalStorageService localStorageService) : AuthenticationStateProvider
{
    private readonly ClaimsPrincipal _anonymous = new(new ClaimsIdentity());
    
    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var stringToken = await localStorageService.GetToken();
        if (string.IsNullOrEmpty(stringToken)) return await Task.FromResult(new AuthenticationState(_anonymous));
        
        var deserializeToken = Serializations.DeserializeJsonString<UserSession>(stringToken);
        if (deserializeToken == null) return await Task.FromResult(new AuthenticationState(_anonymous));
        
        var getUserClaims = DecryptToken(deserializeToken?.Token!);
        if (getUserClaims == null) return await Task.FromResult(new AuthenticationState(_anonymous));
        
        var claimsPrincipal = SetClaimPrincipal(getUserClaims);
        return await Task.FromResult(new AuthenticationState(claimsPrincipal));
    }

    public async Task UpdateAuthenticationState(UserSession userSession)
    {
        var claimsPrincipal = new ClaimsPrincipal();
        if (userSession.Token != null || userSession.RefreshToken != null)
        {
            var serializeSession = Serializations.SerializeObj(userSession);
            await localStorageService.SetToken(serializeSession);
            var getUserClaims = DecryptToken(userSession.Token!);
            claimsPrincipal = SetClaimPrincipal(getUserClaims);
        }
        else
        {
            await localStorageService.RemoveToken();
        }
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
    }

    private static CustomUserClaims DecryptToken(string jwtToken)
    {
        if (string.IsNullOrEmpty(jwtToken)) return new CustomUserClaims();
        
        var handler = new JwtSecurityTokenHandler();
        var token = handler.ReadJwtToken(jwtToken);
        var userId = token.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);
        var userName = token.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email);
        var userRole = token.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role);
        var userFullName = token.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name);
        return new CustomUserClaims(userId!.Value, userName!.Value, userFullName!.Value, userRole!.Value);
    }

    private static ClaimsPrincipal SetClaimPrincipal(CustomUserClaims? claims)
    {
        if (claims?.Username is null) return new ClaimsPrincipal();
        return new ClaimsPrincipal(new ClaimsIdentity(
            new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, claims.Id),
                new(ClaimTypes.Email, claims.Username),
                new(ClaimTypes.Role, claims.Role),
                new(ClaimTypes.Name, claims.FullName)
            }, "JwtAuth"));
    }
}