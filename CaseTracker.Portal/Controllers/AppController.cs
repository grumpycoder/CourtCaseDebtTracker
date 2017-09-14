using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CaseTracker.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CaseTracker.Portal.Controllers
{
    [Authorize]
    public class AppController : Controller
    {
        private readonly AppDbContext context;
        public AppController(AppDbContext _context)
        {
            context = _context;
        }

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
