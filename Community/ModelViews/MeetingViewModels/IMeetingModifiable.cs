using Community.Data.Tables;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Community.ModelViews.MeetingViewModels
{
    public interface IMeetingModifiable
    {
        int Id { get; set; }

        int UserId { get; set; }

        string Name { get; set; }

        DateTime MeetingDate { get; set; }

        string Description { get; set; }

        string StartTime { get; set; }

        string EndTime { get; set; }

        IFormFile PhotoPath { get; set; }

        string City { get; set; }

        string Street { get; set; }

        int MeetingCategoryId { get; set; }
        IEnumerable<MeetingCategory> MeetingCategories { get; set; }
    }
}
