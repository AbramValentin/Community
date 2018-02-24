using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CitiesAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/Settlements")]
    public class SettlementsController : Controller
    {
        private LocationsDbContext _dbContext;

        public SettlementsController(LocationsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet("{id}")]
        public IEnumerable<Settlements> Get(int id)
        {
            var list = _dbContext.Settlements
                .Where(m => m.area_id == id)
                .OrderBy(m => m.village)
                .ToList();

            return list;
        }
    }
}