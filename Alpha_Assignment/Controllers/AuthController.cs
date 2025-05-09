﻿using Business.Services;
using Domain.Extensions;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Presentation.Models;

namespace Presentation.Controllers;

public class AuthController(IAuthService authService) : Controller
{

    private readonly IAuthService _authService = authService;

    [Route("auth/signup")]
    public IActionResult SignUp()
    {
        var model = new SignUpViewModel();
        return View("SignUpView", model);
    }

    [Route("auth/signup")]
    [HttpPost]
    public async Task<IActionResult> SignUp(SignUpViewModel model)
    {
        ViewBag.Error = null;
        if (!ModelState.IsValid)
        {
            Console.WriteLine("ModelState is not valid");
            return View("SignUpView", model);
        }

        var signUpFormData = model.MapTo<SignUpFormData>();
        var result = await _authService.SignUpAsync(signUpFormData);

        if (result.Succeeded)
        {
            Console.WriteLine("SignUp succeeded");
            return RedirectToAction("SignIn", "Auth");
        }

        ViewBag.Error = result.Error;
        return View("SignUpView", model);
    }

    public IActionResult SignIn()
    {
        var model = new SignInViewModel();
        return View("SignInView", model);
    }

    [HttpPost]
    public async Task<ActionResult> SignIn(SignInViewModel model)
    {
        ViewBag.Error = null;
        if(string.IsNullOrEmpty(model.Email) || string.IsNullOrEmpty(model.Password))
        {
            ViewBag.Error = "Incorrect email or password.";
            return View("SignInView", model);
        }
        if (!ModelState.IsValid)
        {
            return View("SignInView", model);
        }

        var signInFormData = model.MapTo<SignInFormData>();
        var result = await _authService.SignInAsync(signInFormData);

        if (result.Succeeded)
        {
            return RedirectToAction("AlphaView", "Alpha");
        }
        return View("SignInView", model);
    }

    [HttpPost]
    public async Task<IActionResult> SignOutUser()
    {
        var result = await _authService.SignOutAsync();
        if (result.Succeeded) {
            return RedirectToAction("SignIn", "Auth");
        }
        return RedirectToAction("AlphaView", "Alpha");
    }
}