using AuthorizationServer.Models;

namespace AuthorizationServer.DTO;

public class UserDTO
{
    public UserDTO() { }

    public UserDTO(User user)
    {
        Id = user.Id;
        Email = user.Email ?? string.Empty;
        Name = user.Name;
        UserName = user.UserName ?? string.Empty;
    }

    public string Id { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;

}