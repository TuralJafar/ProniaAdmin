using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Pronia.Areas.ProniaAdmin.Controllers;
using Pronia.mModels;
using Pronia.Utiliters.Enums;
using Pronia.ViewModels.Account;
using System.ComponentModel;

namespace Pronia.Controllers
{
    [AutoValidateAntiforgeryToken]
    public class AccountController:Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
		private readonly RoleManager<IdentityRole> _roleManager;

		public AccountController(UserManager<AppUser> userManager,SignInManager<AppUser>signInManager,RoleManager<IdentityRole>roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
			_roleManager = roleManager;
		}
        public IActionResult Register()
        { 
            return View();
        }
       [HttpPost]
        public async  Task<IActionResult> Register(RegisterVM newUser)           
        {
           if (!ModelState.IsValid) return View();
            AppUser user = new AppUser()
            {
                Name=newUser.Name,
                Email=newUser.Email,
                Surname=newUser.Surname,
                UserName=newUser.Username
            };
         IdentityResult result=  await _userManager.CreateAsync(user, newUser.Password);
            if (result.Succeeded) 
            {
              foreach(IdentityError error in  result.Errors)
              {
                    ModelState.AddModelError("",error.Description);
              }
              return View();
            }
          await  _signInManager.SignInAsync(user, isPersistent: false);

            return RedirectToAction("Index","Home");
        }
        public IActionResult Login() 
        {
            return View();
        }
        [HttpPost]
		public async Task<IActionResult> Login(LoginVM user)
        {
            if(ModelState.IsValid)
            {
                return View();
            }
            AppUser existed = await _userManager.FindByEmailAsync(user.UsernameOrEmail);
            if (existed == null)
            {
                existed = await _userManager.FindByNameAsync(user.UsernameOrEmail);

                if (existed == null)
                {
                    ModelState.AddModelError(string.Empty, "Username or Password not correct");
                    return View();
                }
            }
            var result=await _signInManager.PasswordSignInAsync(existed, user.Password,user.IsRemebered,false);
            if (result.IsLockedOut){
                ModelState.AddModelError(string.Empty, "block try again later");
            }

            if (!result.Succeeded)
            {
                ModelState.AddModelError(string.Empty, "Username,Email or Password not correct");
                return View();
            }
            
            return RedirectToAction("Index","Home");
		}
		public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        public async Task<IActionResult> CreateRoles()
        {
           foreach(var item in Enum.GetValues(typeof(UserRole)))
            {
                if(!await _roleManager.RoleExistsAsync(item.ToString())) 
                {
					await _roleManager.CreateAsync(new IdentityRole{ Name = item.ToString() });
                }
               
            }
			return RedirectToAction("Index", "Home");


		}
		
       
	}
}
