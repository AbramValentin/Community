using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Community.Data;

namespace Community.Controllers
{
    public class LocationController : Controller
    {
        private readonly CommunityDbContext _dbContext;

        public LocationController(CommunityDbContext dbContext)
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
                .Where(m => m.RegionId == id)
                .ToList();

            return list;
        }

        [HttpGet]
        public IEnumerable<Settlements> GetSettlements(int id)
        {
            var list = _dbContext.Settlements
                .Where(m => m.AreaId == id)
                .ToList();

            return list;
        }
    }
}