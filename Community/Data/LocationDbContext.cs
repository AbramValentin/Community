using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Community.Data
{
    public class LocationsDbContext : DbContext
    {
        public virtual DbSet<Areas> Areas { get; set; }
        public virtual DbSet<Regions> Regions { get; set; }
        public virtual DbSet<Settlements> Settlements { get; set; }

        public LocationsDbContext(DbContextOptions<LocationsDbContext> options)
            : base(options)
        {

        }
    }

    public class Settlements
    {
        public int id { get; set; }
        public int region_id { get; set; }
        public int area_id { get; set; }
        public string village { get; set; }
    }

    public class Regions
    {
        public int id { get; set; }
        public string region { get; set; }
    }

    public class Areas
    {
        public int id { get; set; }
        public int region_id { get; set; }
        public string area { get; set; }
    }
}
