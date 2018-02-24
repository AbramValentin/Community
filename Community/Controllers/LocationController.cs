using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Community.Controllers
{
    public class LocationController : Controller
    {
        public IActionResult GetRegions()
        {
           
            return View();
        }
    }
}