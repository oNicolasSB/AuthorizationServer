using AuthorizationServer.Data;
using System.Security.Cryptography.X509Certificates;

namespace AuthorizationServer.DependencyInjection;

public static class OpenIddictSetup
{
    public static IServiceCollection ConfigureOpenIddict(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment env)
    {
        services.AddOpenIddict()
            .AddCore(options =>
            {
                options.UseEntityFrameworkCore()
                    .UseDbContext<ApplicationDbContext>();
            })
            .AddServer(options =>
            {
                options
                    .AllowAuthorizationCodeFlow()
                        .RequireProofKeyForCodeExchange()
                    .AllowClientCredentialsFlow()
                    .AllowRefreshTokenFlow();

                options
                    .SetAuthorizationEndpointUris("/connect/authorize")
                    .SetTokenEndpointUris("/connect/token")
                    .SetUserinfoEndpointUris("/connect/userinfo");


                string signInCertificate = configuration["Certificates:SigningCertificate"] ?? string.Empty;
                string signInCertificatePassword = configuration["Certificates:SigningCertificatePassword"] ?? string.Empty;

                string encryptionCertificate = configuration["Certificates:EncryptionCertificate"] ?? string.Empty;
                string encryptionCertificatePassword = configuration["Certificates:EncryptionCertificatePassword"] ?? string.Empty;

                options.AddSigningCertificate(new X509Certificate2(signInCertificate, signInCertificatePassword));
                options.AddEncryptionCertificate(new X509Certificate2(encryptionCertificate, encryptionCertificatePassword));

                if (env.IsDevelopment())
                {
                    options.DisableAccessTokenEncryption();
                }

                options.RegisterScopes("api");

                options
                    .UseAspNetCore()
                    .EnableTokenEndpointPassthrough()
                    .EnableAuthorizationEndpointPassthrough()
                    .EnableUserinfoEndpointPassthrough();
            });

        services.AddHostedService<TestData>();
        return services;
    }
}
