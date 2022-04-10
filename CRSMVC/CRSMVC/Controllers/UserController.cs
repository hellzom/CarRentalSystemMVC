using Microsoft.AspNetCore.Mvc;

namespace CRSMVC.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ViewCars()
        {
            return View();
        }

        public IActionResult BookCar()
        {
            return View();
        }

        public IActionResult ViewBookings()
        {
            return View();
        }

        public IActionResult UserProfile()
        {
            return View();
        }



    }
}
