using System.Security.Claims;
using AuthorizationServer.Interfaces;
using AuthorizationServer.ViewModels;

namespace AuthorizationServer.Helpers;

public class GetDisplayData
{
    private readonly IUserService _userService;

    public GetDisplayData(IUserService userService)
    {
        _userService = userService;
    }

    public UserDisplayDataViewModel GetUserDisplayData(ClaimsPrincipal claimsPrincipal)
    {
        try
        {
            var userDTO = _userService.GetUserDTOByClaimsAsync(claimsPrincipal).Result;
            return new UserDisplayDataViewModel(userDTO);
        }
        catch
        {
            return new UserDisplayDataViewModel();
        }
    }

}