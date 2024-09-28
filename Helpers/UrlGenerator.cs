using AuthorizationServer.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;

namespace AuthorizationServer.Helpers.UrlGenerator;

public class UrlGenerator : IUrlGenerator
{

    private readonly ActionContext? _context;
    private readonly IUrlHelper? _urlHelper;

    public UrlGenerator(IActionContextAccessor accessor, IUrlHelperFactory urlHelper)
    {
        if (accessor.ActionContext != null)
        {
            _context = accessor.ActionContext;
            _urlHelper = urlHelper.GetUrlHelper(_context);
        }
    }
    public string GetEmailConfirmationLink(string userId, string token, string scheme)
    {
        return _urlHelper?.Action("ConfirmEmail", "Account", new { userId, token }, protocol: "https") ?? throw new Exception("Failed to generate email confirmation link");
    }
}