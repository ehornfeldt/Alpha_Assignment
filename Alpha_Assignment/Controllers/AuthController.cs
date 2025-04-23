using Business.Services;
using Domain.Extensions;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Presentation.Models;

namespace Presentation.Controllers;

public class AuthController(IAuthService authService) : Controller
{
    private readonly IAuthService _authService = authService;

    public IActionResult SignUp()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> SignUp(SignUpViewModel model)
    {
        ViewBag.Error = null;
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var signUpFormData =  model.MapTo<SignUpFormData>();
        var result = await _authService.SignUpAsync(signUpFormData);

        if(result.Succeeded)
        {
            return RedirectToAction("SignIn", "Auth");
        }

        ViewBag.Error = result.Error;
        return View(model);
    }

    public IActionResult SignIn(string returnUrl = "~/")
    {
        ViewBag.returnUrl = returnUrl;
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> SignIn(SignInViewModel model, string returnUrl = "~/")
    {
        ViewBag.Error = null;
        ViewBag.returnUrl = returnUrl;
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var signInFormData = model.MapTo<SignInFormData>();
        var result = await _authService.SignInAsync(signInFormData);

        if (result.Succeeded)
        {
            return LocalRedirect(returnUrl);
        }

        ViewBag.Error = result.Error;
        return View(model);

    }
}
