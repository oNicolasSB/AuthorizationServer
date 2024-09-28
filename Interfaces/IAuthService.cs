using AuthorizationServer.DTO;
using Microsoft.AspNetCore.Identity;

namespace AuthorizationServer.Interfaces;

public interface IAuthService
{
    Task<IdentityResult> RegisterAsync(RegisterDTO registerDTO);
    Task<bool> ConfirmEmailAsync(string userId, string token);
}