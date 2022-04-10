using Microsoft.AspNetCore.Mvc;

namespace CRSMVC.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ManageUsers()
        {
            return View();
        }

        public IActionResult ManageCars()
        {
            return View();
        }

        public IActionResult AddNewcar()
        {
            return View();
        }

        public IActionResult EditCar()
        {
            return View();
        }

        public IActionResult ViewBookings()
        {
            return View();
        }

        public IActionResult AdminProfile()
        {
            return View();
        }

        public IActionResult ViewUserBookings()
        {
            return View();
        }
    }
}
