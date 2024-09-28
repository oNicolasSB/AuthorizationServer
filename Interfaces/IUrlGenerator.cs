namespace AuthorizationServer.Interfaces;

public interface IUrlGenerator
{
    string GetEmailConfirmationLink(string userId, string token, string scheme);
}