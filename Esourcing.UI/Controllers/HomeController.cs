using Esourcing.UI.Models;
using ESourcing.Core.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Esourcing.UI.Controllers
{
    public class HomeController : Controller
    {
        public UserManager<AppUser> _userManager { get; }
        public SignInManager<AppUser> _signInManager { get; }

        public HomeController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel loginModel ,string returnUrl)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(loginModel.Email);
                if (user!=null)
                {

                    await _signInManager.SignOutAsync();

                    var result = await _signInManager.PasswordSignInAsync(user, loginModel.Password, false, false);

                    if (result.Succeeded)
                    {
                        var isgirdi = _signInManager.IsSignedIn(User);
                        HttpContext.Session.SetString("IsAdmin", user.IsAdmin.ToString());
                        return LocalRedirect(returnUrl);
                    }
                    else
                        ModelState.AddModelError("", "Email address is not valid or password");
                }
                else
                    ModelState.AddModelError("", "Email address is not valid or password");
            }
            return View();
        }
        

        [HttpPost]
        public IActionResult Logout()
        {
            _signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }


        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(AppUserModel signupModel)
        {
            if (ModelState.IsValid)
            {
                AppUser usr = new AppUser();
                usr.FirstName = signupModel.FirstName;
                usr.Email = signupModel.Email;
                usr.LastName = signupModel.LastName;
                usr.PhoneNumber = signupModel.PhoneNumber;
                usr.UserName = signupModel.UserName;
                if (signupModel.UserSelectTypeId == 1)
                {
                    usr.IsBuyer = true;
                    usr.IsSeller = false;
                }
                else
                {
                    usr.IsSeller = true;
                    usr.IsBuyer = false;
                }
                var result = await _userManager.CreateAsync(usr, signupModel.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction("Login", "Home");
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                   
                }
            }
            return View(signupModel);
        }
    }
}
