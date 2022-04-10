using Microsoft.AspNetCore.Mvc;

namespace CRSMVC.Controllers
{
    public class AuthController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult UserLogin()
        {
            return View();
        }

        public IActionResult UserRegistration()
        {
            return View();
        }

        public IActionResult AdminLogin()
        {
            return View();
        }
    }
}
