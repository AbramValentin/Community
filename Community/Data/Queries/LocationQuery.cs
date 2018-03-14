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

        /// <summary>
        /// Selects asked amount of cities that starts with given string value.
        /// 
        /// Returns Json result in format { value : Name of city, cityId : Id of city},
        /// Where 
        ///     value : is specific attribute for jQuery UI autocompleate functionality.
        ///     Name of city : is name of city for value attribute to display.
        ///     cityId : is attibute that are not part of jQuery UI, and used to detect primary key of city.
        /// </summary>
        /// <param name="term">String that city name should starts with.</param>
        /// <param name="amount">Amount of records to return.</param>
        /// <returns></returns>
        public JsonResult GetCityStartsWith(string term, int amount)
        {
            var result = _db
                .Cities
                .Where(m => m.Name.StartsWith(term, StringComparison.CurrentCultureIgnoreCase))
                .Take(amount)
                .Select(m => new {value = m.Name, cityId = m.Id });

            return new JsonResult(result);
        }

        /// <summary>
        /// Returns Area object by given primary key value, if not fount null value is returned.
        /// </summary>
        /// <param name="areaId">Primary key value.</param>
        /// <returns></returns>
        public async Task<Area> GetAreaByIdAsync(int areaId)
        {
            var area = await _db
                .Areas.FindAsync(areaId);

            return area;
        }

        /// <summary>
        /// Returns City object by given primary key value, if not fount null value is returned.
        /// </summary>
        /// <param name="cityId">Primary key value.</param>
        /// <returns></returns>
        public async Task<City> GetCityByIdAsync(int cityId)
        {
            var city = await _db
                .Cities.FindAsync(cityId);

            return city;
        }

        /// <summary>
        /// Returns Region object by given primary key value, if not fount null value is returned.
        /// </summary>
        /// <param name="regionId">Primary key value.</param>
        /// <returns></returns>
        public async Task<Region> GetRegionById(int regionId)
        {
            var region = await _db
                .Regions.FindAsync(regionId);

            return region;
        }


        /// <summary>
        /// Returns Region object by given primary key value of Area, if not fount null value is returned.
        /// </summary>
        /// <param name="areaId">Primary key value of Area.</param>
        /// <returns></returns>
        public async Task<Region> GetRegionByAreaId(int areaId)
        {
            var area = await GetAreaByIdAsync(areaId);
            var region = await GetRegionById(area.RegionId);

            return region;
        }

        public async Task<Region> GetRegionByCityId(int cityId)
        {
            var city = await GetCityByIdAsync(cityId);
            var area = await GetAreaByIdAsync(city.AreaId);
            var region = await GetRegionById(area.RegionId);

            return region; 
        }

        public async Task<Area> GetAreaByCityId(int cityId)
        {
            var city = await GetCityByIdAsync(cityId);
            var area = await GetAreaByIdAsync(city.AreaId);

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
