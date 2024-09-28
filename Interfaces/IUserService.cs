using System.Security.Claims;
using AuthorizationServer.DTO;

namespace AuthorizationServer.Interfaces;

public interface IUserService
{
    Task<UserDTO> GetUserDTOByClaimsAsync(ClaimsPrincipal claimsPrincipal);
}