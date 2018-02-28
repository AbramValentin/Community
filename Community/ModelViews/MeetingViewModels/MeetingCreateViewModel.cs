using Community.Data.Models;
using Community.Data.Tables;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Community.ModelViews.MeetingViewModels
{
    public class MeetingCreateViewModel
    {

        [StringLength(maximumLength: 25, MinimumLength = 5)]
        public string Name { get; set; }

        [DataType(DataType.Date)]
        public DateTime MeetingDate { get; set; }

        [StringLength(maximumLength: 2000, MinimumLength = 20)]
        public string Description { get; set; }

        public string StartTime { get; set; }

        public string EndTime { get; set; }

        [DataType(DataType.ImageUrl)]
        public string PhotoPath { get; set; }

        public int? RegionId { get; set; }
        public IEnumerable<Location> RegionList { get; set; }

        public int? AreaId { get; set; }
        public IEnumerable<Location> AreaList { get; set; }

        public int? CityId { get; set; }
        public IEnumerable<Location> CityList { get; set; }

        [StringLength(maximumLength: 30, MinimumLength = 3)]
        public string Street { get; set; }

        public int CategoryId { get; set; }
        public IEnumerable<MeetingCategory> MeetingCategories { get; set; }

    }
}
