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

        //[HttpGet]
        //public IEnumerable<Region> GetRegions()
        //{
            
        //}

        //[HttpGet]
        //public IEnumerable<Area> GetAreasByRegionId(int regionId)
        //{
            
        //}

        //[HttpGet]
        //public IEnumerable<City> GetCitiesByAreaId(int areaId)
        //{
            
        //}
    }
}