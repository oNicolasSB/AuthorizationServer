using AuthorizationServer.DTO;

namespace AuthorizationServer.ViewModels;

public class UserDisplayDataViewModel
{
    public string Name { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;

    public UserDisplayDataViewModel() { }
    public UserDisplayDataViewModel(UserDTO userDTO)
    {
        Name = userDTO.Name;
        UserName = userDTO.UserName;
        Email = userDTO.Email;
    }
}