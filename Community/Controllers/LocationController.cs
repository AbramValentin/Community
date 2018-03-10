using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Community.Data;

namespace Community.Controllers
{
    public class LocationController : Controller
    {
        private readonly LocationQuery _locationQuery;

        public LocationController(LocationQuery locationQuery)
        {
            _locationQuery = locationQuery;
        }

        [HttpGet]
        public JsonResult Autocomplete(string term)
        {
            var model = _locationQuery
                .GetCityStartsWith(term);

            return Json(model);
        }
    }
}