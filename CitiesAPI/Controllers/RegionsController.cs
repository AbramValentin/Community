using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CitiesAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/Regions")]
    public class RegionsController : Controller
    {

        private LocationsDbContext _dbContext;

        public RegionsController(LocationsDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        [HttpGet]
        public IEnumerable<Regions> Get()
        {
            var list = _dbContext.Regions.ToList();
            return list;
        }
    }
}