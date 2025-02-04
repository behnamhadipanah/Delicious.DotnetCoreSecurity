using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace NetCoreSecurity.WebClient.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        //[HttpPost]
        //public async Task Logout()
        //{
        //    await HttpContext.SignOutAsync("oidc");
        //    await HttpContext.SignOutAsync("Cookie");
        //}


        [HttpPost]
        public IActionResult Logout()
        {
            return new SignOutResult(new string[] { "oidc", "Cookie" });
        }
    }
}
