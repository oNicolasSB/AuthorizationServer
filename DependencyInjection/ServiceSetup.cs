using AuthorizationServer.Email;
using AuthorizationServer.Helpers;
using AuthorizationServer.Helpers.UrlGenerator;
using AuthorizationServer.Interfaces;
using AuthorizationServer.Models;
using AuthorizationServer.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace AuthorizationServer.DependencyInjection;

public static class ServiceSetup
{
    public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
        services.AddHttpContextAccessor();
        services.AddOptions();
        services.Configure<SMTPSettings>(configuration.GetSection("SMTP"));


        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IUrlGenerator, UrlGenerator>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IEmailService, EmailService>();
        services.AddScoped<GetDisplayData>();

        services.AddScoped<SignInManager<User>>();
        services.AddScoped<UserManager<User>>();
        return services;
    }
}