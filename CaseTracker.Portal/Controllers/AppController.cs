using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CaseTracker.Portal.Controllers
{
    [Authorize]
    public class AppController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Case(int id)
        {
            return View();
        }

        public IActionResult Courts(int id)
        {
            return View();
        }

        public IActionResult Jurisdictions(int id)
        {
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
