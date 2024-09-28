using Microsoft.AspNetCore.Identity;

namespace AuthorizationServer.Models;

public class User : IdentityUser
{
    public string Name { get; set; } = string.Empty;
}
