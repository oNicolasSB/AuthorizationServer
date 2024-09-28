using System.Security.Claims;
using AuthorizationServer.DTO;
using AuthorizationServer.Interfaces;
using AuthorizationServer.Models;
using Microsoft.AspNetCore.Identity;

namespace AuthorizationServer.Services;

public class UserService : IUserService
{
    private readonly UserManager<User> _userManager;
    public UserService(UserManager<User> userManager)
    {
        _userManager = userManager;
    }
    public async Task<UserDTO> GetUserDTOByClaimsAsync(ClaimsPrincipal claimsPrincipal)
    {
        string email = claimsPrincipal.FindFirstValue(ClaimTypes.Email) ?? string.Empty;
        return new UserDTO(await _userManager.FindByEmailAsync(email) ?? throw new Exception("User not found"));
    }
}