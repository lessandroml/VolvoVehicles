using Microsoft.AspNetCore.Mvc;
using VolvoTrucks.Data;


namespace VolvoTrucks.Controllers
{
    public class HomeController : Controller
    {
        private readonly VolvoVehiclesContext _context;

        public HomeController(VolvoVehiclesContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
