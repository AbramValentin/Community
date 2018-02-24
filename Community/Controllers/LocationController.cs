using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Community.Data;

namespace Community.Controllers
{
    public class LocationController : Controller
    {
        private LocationsDbContext _dbContext;

        public LocationController(LocationsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public IEnumerable<Regions> GetRegions()
        {
            var list = _dbContext.Regions.ToList();
            return list;
        }

        [HttpGet]
        public IEnumerable<Areas> GetAreas(int id)
        {
            var list = _dbContext.Areas
                .Where(m => m.region_id == id)
                .ToList();

            return list;
        }

        [HttpGet]
        public IEnumerable<Settlements> GetSettlements(int id)
        {
            var list = _dbContext.Settlements
                .Where(m => m.area_id == id)
                .ToList();

            return list;
        }
    }
}