using System.Security.Claims;
using AuthorizationServer.DTO;
using AuthorizationServer.Interfaces;
using AuthorizationServer.Models;
using Microsoft.AspNetCore.Identity;

namespace AuthorizationServer.Services;

public class AuthService : IAuthService
{
    private readonly UserManager<User> _userManager;
    private readonly IEmailService _emailService;
    private readonly IUrlGenerator _urlGenerator;
    public AuthService(
        UserManager<User> userManager,
        IEmailService emailService,
        IUrlGenerator urlGenerator)
    {
        _userManager = userManager;
        _emailService = emailService;
        _urlGenerator = urlGenerator;
    }

    public async Task<IdentityResult> RegisterAsync(RegisterDTO registerDTO)
    {
        User user = new()
        {
            Email = registerDTO.Email,
            UserName = registerDTO.Email,
            Name = registerDTO.Name
        };

        var claimName = new Claim("RealName", user.Name);

        IdentityResult result = await _userManager.CreateAsync(user, registerDTO.Password);

        if (!result.Succeeded)
        {
            await _userManager.AddClaimAsync(user, claimName);
            return result;
        }

        string token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
        string confirmationLink = _urlGenerator.GetEmailConfirmationLink(user.Id, token, "https");
        await _emailService.SendEmailAsync("gersonbatatata@gmail.com", user.Email, "Confirm your email", $"Please confirm your account by <a href='{confirmationLink}'>clicking here</a>");

        return result;
    }

    public async Task<bool> ConfirmEmailAsync(string userId, string token)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null) return false;
        var result = await _userManager.ConfirmEmailAsync(user, token);
        return result.Succeeded;
    }
}
