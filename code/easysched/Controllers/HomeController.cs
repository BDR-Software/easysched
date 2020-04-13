using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using easysched.Models;
using Microsoft.AspNetCore.Http;
using easysched.Data;

namespace easysched.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly easyschedContext _context;

        public HomeController(ILogger<HomeController> logger, easyschedContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            if (HttpContext.Session.GetInt32("UserLoggedIn") == 1)
            {
                var currentEmployee = _context.Employee.FirstOrDefault(e => e.Id == HttpContext.Session.GetInt32("LoggedInEmployeeID"));
                ViewData["EmployeeName"] = currentEmployee.FullName;
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Logins");
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult RegistrationUnfinished()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
