using Microsoft.AspNetCore.Mvc;
using Vidly.Models.VM;
using Vidly.Models;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using Vidly.Data;
using Vidly.Models.Roles;
using Microsoft.EntityFrameworkCore;

namespace Vidly.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly AppDbContext _context;
        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager,
          AppDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

        public async Task<IActionResult> Users()
        {
            var users = await _context.Users.ToListAsync();
            return View(users);
        }


        public IActionResult Login()
        {
            var response = new LoginVm();
            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVm loginVm)
        {
            if (!ModelState.IsValid) return View(loginVm);
            var user = await _userManager.FindByEmailAsync(loginVm.EmailAddress);
            if (user != null)
            {
                var paswordCheck = await _userManager.CheckPasswordAsync(user, loginVm.Password);
                if (paswordCheck)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, loginVm.Password, false, false);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }

                TempData["Error"] = "Wrong credentials. Please, try again!";
                return View(loginVm);

            }

            TempData["Error"] = "Wrong credentials. Please, try again!";
            return View(loginVm);
        }

        public IActionResult Register()
        {
            var response = new RegisterVm();
            return View(response);


        }
        [HttpPost]

        public async Task<IActionResult> Register(RegisterVm registerVm)
        {

            if (!ModelState.IsValid) return View(registerVm);
            var user = await _userManager.FindByEmailAsync(registerVm.EmailAddress);
            if (user != null)
            {
                TempData["Error"] = "This Email address is already in use";
                return View(registerVm);
            }

            var newUser = new ApplicationUser()
            {
                FullName = registerVm.FullName,
                Email = registerVm.EmailAddress,
                UserName = registerVm.EmailAddress,
                DrivingLicense = registerVm.DrivingLicense,
                Phone = registerVm.Phone

            };

            var newUserResponse = await _userManager.CreateAsync(newUser, registerVm.Password);

            if (!newUserResponse.Succeeded)
            {
                foreach (var error in newUserResponse.Errors)
                {

                    ModelState.AddModelError(string.Empty, error.Description);

                }

                return View(registerVm);
            }

            await _userManager.AddToRoleAsync(newUser, UserRoles.User);
            return View("RegisterCompleted");
        }
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
