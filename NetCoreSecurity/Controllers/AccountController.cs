using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NetCoreSecurity.Models.Dtos;
using System.Security.Claims;

namespace NetCoreSecurity.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }


        #region register

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterDto register)
        {
            if (ModelState.IsValid)
            {
                IdentityUser user = new IdentityUser()
                {
                    Email = register.Email,
                    UserName = register.Email,
                };

                if (!string.IsNullOrEmpty(register.FacultyNumber))
                {
                    IdentityUserClaim<string> userClaim = new IdentityUserClaim<string>()
                    {

                        ClaimType = "FacultyNumber",
                        ClaimValue = register.FacultyNumber

                    };

                }


                var result = await _userManager.CreateAsync(user, register.Password);

                if (result.Succeeded)
                {

                    if (!string.IsNullOrEmpty(register.FacultyNumber))
                    {
                        await _userManager.AddClaimAsync(user, new Claim("FacultyNumber", register.FacultyNumber));

                    }
                    return RedirectToAction("login", "account");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View(register);
        }

        #endregion

        #region login
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDto login, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(login.Email, login.Password, login.RememberMe, false);
                if (result.Succeeded)
                {
                    if (Url.IsLocalUrl(returnUrl))
                        return Redirect(returnUrl);

                    return RedirectToAction(nameof(StudentController.Index), "Student");
                }

                ModelState.AddModelError(string.Empty, "Invalid login attemp");

            }

            return View(login);
        }
        #endregion

        #region Logout

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        #endregion


        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
