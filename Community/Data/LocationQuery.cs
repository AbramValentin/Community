using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Community.Data.Models;
using System.Globalization;

namespace Community.Data
{
    public class LocationQuery
    {
        private CommunityDbContext _db;
        public List<Location> _locations = new List<Location>();


        public LocationQuery(CommunityDbContext db)
        {
            _db = db;

            _locations = new List<Location>
            {
                new Location{ Id = 1, Name = "Ківерці" },
                new Location{ Id = 2, Name = "Луцьк" },
                new Location{ Id = 3, Name = "Київ" },
                new Location{ Id = 4, Name = "Кіровоград" },
                new Location{ Id = 5, Name = "Кременець" },
                new Location{ Id = 6, Name = "Луганськ" },
                new Location{ Id = 7, Name = "Липне" },
                new Location{ Id = 8, Name = "Дніпропетровськ" },
            };
        }

        public object GetCityStartsWith(string term)
        {
            var result = _db
                .Cities
                .Where(m => m.Name.StartsWith(term, StringComparison.CurrentCultureIgnoreCase))
                .Take(10)
                .Select(m => new { label = m.Name });

            return result;
        }

        public async Task<Area> GetAreaById(int areaId)
        {
            var area = await _db
                .Areas
                .Where(a => a.Id == areaId)
                .SingleAsync();

            return area;
        }

        public async Task<City> GetCityById(int cityId)
        {
            var city = await _db
                .Cities
                .Where(c => c.Id == cityId)
                .SingleAsync();

            return city;
        }

        public  City GetCityByName(string cityName)
        {
            var city =  _db.Cities
                .Where(m => m.Name == cityName)
                .Single();

            return city;
        }

        public async Task<Region> GetRegionById(int regionId)
        {
            var region = await _db
                .Regions
                .Where(r => r.Id == regionId)
                .SingleAsync();

            return region;
        }

        public async Task<Region> GetRegionByAreaId(int areaId)
        {
            var area = await GetAreaById(areaId);
            var region = await GetRegionById(area.RegionId);

            return region;
        }

        public async Task<Region> GetRegionByCityId(int cityId)
        {
            var city = await GetCityById(cityId);
            var area = await GetAreaById(city.AreaId);
            var region = await GetRegionById(area.RegionId);

            return region; 
        }

        public async Task<Area> GetAreaByCityId(int cityId)
        {
            var city = await GetCityById(cityId);
            var area = await GetAreaById(city.AreaId);

            return area;
        }

        public async Task<IEnumerable<Region>> GetRegions()
        {
            var regions = await _db
                .Regions
                .ToListAsync();

            return regions;
        }

        public async Task<IEnumerable<Area>> GetAreasByRegionId(int regionId)
        {
            var areas = await _db.Areas
                .Where(m => m.RegionId == regionId)
                .ToListAsync();

            return areas;
        }

        public async Task<IEnumerable<City>> GetCitiesByAreaId(int areaId)
        {
            var cities = await _db.Cities
                .Where(m => m.AreaId == areaId)
                .ToListAsync();

            return cities;
        }

    }
}
