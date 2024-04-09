using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using GigBookin.Models;
using GigBookin.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using GigBookin.Data;

namespace GigBookin.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly UserManager<EventOrganiser> userManager;

        private readonly SignInManager<EventOrganiser> signInManager;
        private readonly RoleManager<IdentityRole<Guid>> roleManager;
        private readonly ApplicationDbContext _context;

        public AccountController(
            UserManager<EventOrganiser> _userManager,
            SignInManager<EventOrganiser> _signInManager,
            RoleManager<IdentityRole<Guid>> _roleManager,
            ApplicationDbContext context)
        {
            userManager = _userManager;
            signInManager = _signInManager;
            roleManager = _roleManager;
            _context = context;
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
                // Assign the "EventOrganiser" role to the newly registered user
                await userManager.AddToRoleAsync(user, "EventOrganiser");

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

        //[HttpPost]
        //[Authorize(Roles = "EventOrganiser")]
        //public async Task<IActionResult> HirePerformer(Guid eventId, Guid performerId)
        //{
        //    var eventOrganiser = await userManager.GetUserAsync(User);
        //    if (eventOrganiser == null)
        //    {
        //        return RedirectToAction("Login", "Account");
        //    }

        //    var @event = await _context.Events.FindAsync(eventId);
        //    if (@event == null)
        //    {
        //        return NotFound();
        //    }

        //    var performer = await _context.Performers.FindAsync(performerId);
        //    if (performer == null)
        //    {
        //        return NotFound();
        //    }

            
        //    if (eventOrganiser.Balance < performer.Price)
        //    {
        //        ModelState.AddModelError("", "Insufficient balance to hire this performer.");
        //        return RedirectToAction("Index", "Home");
        //    }

          
        //    if (performer.Availability == false)
        //    {
        //        ModelState.AddModelError("", "This performer is not available for the specified event time.");
        //        return RedirectToAction("Index", "Home");
        //    }

           
        //    eventOrganiser.Balance -= performer.Price;
        //    _context.Update(eventOrganiser);
        //    await _context.SaveChangesAsync();

            
        //    @event.EventPerformers.Add(new EventPerformer { PerformerId = performer.Id });
        //    _context.Update(@event);
        //    await _context.SaveChangesAsync();

           
        //    var notification = new Notification
        //    {
        //        Message = $"You have been hired for the event '{@event.Name}'."
        //    };
        //    _context.Add(notification);
        //    await _context.SaveChangesAsync();

        //    // Redirect the user to the home page after the hiring process is completed
        //    return RedirectToAction("Index", "Home");
        //}


    }
}


