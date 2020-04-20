using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace easysched.Controllers
{
    public class PickController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}