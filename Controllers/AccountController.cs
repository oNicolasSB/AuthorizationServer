using System.Security.Claims;
using AuthorizationServer.DTO;
using AuthorizationServer.Interfaces;
using AuthorizationServer.Models;
using AuthorizationServer.ViewModels.Account;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AuthorizationServer.Controllers;

public class AccountController : Controller
{
    private readonly SignInManager<User> _signInManager;
    private readonly UserManager<User> _userManager;
    private readonly IAuthService _authService;

    public AccountController(SignInManager<User> signInManager, UserManager<User> userManager, IAuthService authService)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _authService = authService;
    }
    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (!ModelState.IsValid) return View(model);

        try
        {
            RegisterDTO registerDTO = new()
            {
                Email = model.Email,
                Name = model.Name,
                Password = model.Password
            };

            var result = await _authService.RegisterAsync(registerDTO);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View(model);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            ModelState.AddModelError("", "An error occurred during registration. Please try again later.");
            return View(model);
        }

        TempData["SuccessMessage"] = "Registration successful. Please check your email for confirmation.";
        return RedirectToAction("Login");
    }
    [HttpGet]
    public async Task<IActionResult> ConfirmEmail(string userId, string token)
    {
        bool result = await _authService.ConfirmEmailAsync(userId, token);
        if (result) TempData["SuccessMessage"] = "Email confirmed successfully";
        else TempData["ErrorMessage"] = "Error confirming email";
        return RedirectToAction("Login");
    }

    [HttpGet]
    public IActionResult Login(string? returnUrl = null)
    {
        ViewData["ReturnUrl"] = returnUrl;
        if (User.Identity?.IsAuthenticated ?? false) return RedirectToAction("Index", "Home");

        ViewBag.ErrorMessage = TempData["ErrorMessage"];
        ViewBag.SuccessMessage = TempData["SuccessMessage"];

        return View();
    }

    [HttpPost]
    [AllowAnonymous]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginViewModel model, string? returnUrl = null)
    {
        ViewData["ReturnUrl"] = returnUrl;

        try
        {
            if (!ModelState.IsValid) return View(model);
            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
            if (!result.Succeeded)
            {
                if (result.RequiresTwoFactor)
                {
                    return RedirectToAction("LoginTwoFactor", "Account", new { email = model.Email });
                }
                if (result.IsLockedOut)
                {
                    ModelState.AddModelError("Login", "Your account is locked out. Please try again later.");
                }
                else
                {
                    ModelState.AddModelError("Login", "Invalid login attempt.");
                }

                return View(model);
            }

            if (!string.IsNullOrEmpty(returnUrl))
                return Redirect(returnUrl);
            return RedirectToAction("Index", "Home");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            ModelState.AddModelError("", "An error occurred during login. Please try again later.");
            return View(model);
        }
    }

    public async Task<IActionResult> Logout(string returnUrl = null)
    {
        await _signInManager.SignOutAsync();

        if (!string.IsNullOrEmpty(returnUrl))
            return Redirect(returnUrl);
        return RedirectToAction("Index", "Home");
    }
}
