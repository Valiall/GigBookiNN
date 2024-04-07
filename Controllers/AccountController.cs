using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using GigBookin.Models;
using GigBookin.Models.Entities;
using Microsoft.AspNetCore.Authorization;

namespace GigBookin.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly UserManager<EventOrganiser> userManager;

        private readonly SignInManager<EventOrganiser> signInManager;
        private readonly RoleManager<IdentityRole<Guid>> roleManager;

        public AccountController(
            UserManager<EventOrganiser> _userManager,
            SignInManager<EventOrganiser> _signInManager,
            RoleManager<IdentityRole<Guid>> _roleManager)
        {
            userManager = _userManager;
            signInManager = _signInManager;
            roleManager = _roleManager;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            if (User?.Identity?.IsAuthenticated ?? false)
            {
                return RedirectToAction("Index", "Home");
            }

            var model = new RegisterViewModel();

            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = new EventOrganiser()
            {
                Email = model.Email,
                UserName = model.UserName
            };

            var result = await userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {

                return RedirectToAction("Index", "Home");
            }

            foreach (var item in result.Errors)
            {
                ModelState.AddModelError("", item.Description);
            }

            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            if (User?.Identity?.IsAuthenticated ?? false)
            {
                return RedirectToAction("Index", "Home");
            }

            var model = new LoginViewModel();

            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await userManager.FindByNameAsync(model.UserName);

            if (user != null)
            {
                var result = await signInManager.PasswordSignInAsync(user, model.Password, false, false);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
            }

            ModelState.AddModelError("", "Invalid login");

            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> CreateRoles()
        {
            await roleManager.CreateAsync(new IdentityRole<Guid>("Admin"));
            await roleManager.CreateAsync(new IdentityRole<Guid>("EventOrganiser"));
            return RedirectToAction("Index", "Home");
        }
        public async Task<IActionResult> AddUsersToRoles()
        {
            string email1 = "demirebva.valentina@gmail.com";
            string email2 = "emiliastancheva@gmail.com";

            var user= await userManager.FindByEmailAsync(email1);
            var user2 = await userManager.FindByEmailAsync(email2);


            if (user != null)
            {
                await userManager.AddToRoleAsync(user, "Admin");
            }

            if (user2 != null)
            {
                await userManager.AddToRoleAsync(user2, "EventOrganiser");
            }
            return RedirectToAction("Index", "Home");
        }

    }
}


