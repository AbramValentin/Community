using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Community
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return Content("main page");
        }
    }
}
