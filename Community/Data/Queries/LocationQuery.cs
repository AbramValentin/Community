using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace Community.Data
{
    public class LocationQuery
    {
        private CommunityDbContext _db;


        public LocationQuery(CommunityDbContext db)
        {
            _db = db;
        }

        public object GetCityStartsWith(string term)
        {
            var result = _db
                .Cities
                .Where(m => m.Name.StartsWith(term, StringComparison.CurrentCultureIgnoreCase))
                .Take(10)
                .Select(m => new {value = m.Name, label = m.Name ,custom = m.Id });

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

        public async Task<City> GetCityByIdAsync(int cityId)
        {
            var city = await _db
                .Cities
                .Where(c => c.Id == cityId)
                .FirstOrDefaultAsync();

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
            var city = await GetCityByIdAsync(cityId);
            var area = await GetAreaById(city.AreaId);
            var region = await GetRegionById(area.RegionId);

            return region; 
        }

        public async Task<Area> GetAreaByCityId(int cityId)
        {
            var city = await GetCityByIdAsync(cityId);
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
