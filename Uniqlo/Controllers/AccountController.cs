using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using Uniqlo.Abstractions.EmailService;
using Uniqlo.DAL;
using Uniqlo.DTOs.Account;
using Uniqlo.Helper.Email;
using Uniqlo.Helper.Enums;
using Uniqlo.Models;

namespace Uniqlo.Controllers;

public class AccountController : Controller
{
    private readonly AppDbContext _context;
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IMailService _mailService;

    public AccountController(AppDbContext context,
        UserManager<AppUser> userManager,
        SignInManager<AppUser> signInManager,
        RoleManager<IdentityRole> roleManager,
        IMailService mailService)
    {
        _context = context;
        _userManager = userManager;
        _signInManager = signInManager;
        _roleManager = roleManager;
        _mailService = mailService;
    }

    public IActionResult Register()
    {
     
            
        
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterVM vm)
    {

        if (!ModelState.IsValid)
        {
            return View();
        }

        AppUser user = new AppUser();
        {
            user.Email = vm.Email;
            user.Name = vm.Name;
            user.Surname = vm.Surname;
            user.UserName = vm.UserName;
        };

        var result = await _userManager.CreateAsync(user, vm.Password);

        if (!result.Succeeded)
        {
            foreach (var item in result.Errors)
            {
                ModelState.AddModelError("", item.Description);
            }
            return View();
        }

       // await _userManager.AddToRoleAsync(user, UserRoles.Member.ToString());

        return RedirectToAction("Login");
    }




    public IActionResult EmailConfirm(RegisterVM vm)
    {
        return View();
    }

    [HttpPost]
    public IActionResult EmailConfirm(int confirmCode)
    {
        return View();
    }








    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginVm loginVm, string? ReturnUrl)
    {
        if (!ModelState.IsValid)
        {
            return View();
        }

        AppUser user = await _userManager.FindByEmailAsync(loginVm.EmailOrUsername)
            ?? await _userManager.FindByNameAsync(loginVm.EmailOrUsername);


        if (user is null)
        {
            ModelState.AddModelError("", "Username or Email ve ya Password sehvdir");
            return View();
        }

        var result = await _signInManager.CheckPasswordSignInAsync(user, loginVm.Password, true);
        if (result.IsLockedOut)
        {
            ModelState.AddModelError("", "Biraz sonra yeniden sinayin");
            return View();
        }

        if (!result.Succeeded)
        {
            ModelState.AddModelError("", "Username or Email ve ya Password sehvdir");
            return View();
        }

        await _signInManager.SignInAsync(user, loginVm.Remember);

        if (ReturnUrl != null)
        {
            return Redirect(ReturnUrl);
        }

        return RedirectToAction("Index", "Home");
    }

    public async Task<IActionResult> CreateRole()
    {

        foreach (var item in Enum.GetValues(typeof(UserRoles)))
        {
            await _roleManager.CreateAsync(new IdentityRole()
            {
                Name = item.ToString()
            });
        }

        return RedirectToAction("Index", "Home");
    }

    public IActionResult ForgetPassword()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> ForgetPassword(ForgetPasswordVM vm)
    {
        if (!ModelState.IsValid)
        {
            return View();
        }
        AppUser appUser = await _userManager.FindByEmailAsync(vm.Email);
        if (appUser == null)
        {
            return NotFound();
        }

        var token = await _userManager.GeneratePasswordResetTokenAsync(appUser);

        object userStat = new
        {
            userId = appUser.Id,
            token = token
        };

        var link = Url.Action("ResetPassword", "Account", userStat, HttpContext.Request.Scheme);

        MailRequest mailRequest = new MailRequest()
        {
            ToEmail = vm.Email,
            Subject = "Reset Password",
            Body = $"<a href='{link}'>Reset Password</a>"
        };

        await _mailService.SendEmailAsync(mailRequest);

        return RedirectToAction("Login");
    }

    public IActionResult ResetPassword(string userId, string token)
    {
        if (userId == null)
        {
            return BadRequest();
        }
        ResetPasswordVM resetPasswordVm = new ResetPasswordVM()
        {
            token = token,
            userId = userId
        };
        return View(resetPasswordVm);
    }

    [HttpPost]
    public async Task<IActionResult> ResetPassword(ResetPasswordVM vm)
    {
        if (!ModelState.IsValid)
        {
            return View(vm);
        }

        var user = await _userManager.FindByIdAsync(vm.userId);

        if (user == null)
        {
            return NotFound();
        }

        var result = await _userManager.ResetPasswordAsync(user, vm.token, vm.NewPassword);
        if (!result.Succeeded)
        {
            foreach (var item in result.Errors)
            {
                ModelState.AddModelError("", item.Description);
            }
            return View(vm);
        }

        return RedirectToAction("Login");
    }




}