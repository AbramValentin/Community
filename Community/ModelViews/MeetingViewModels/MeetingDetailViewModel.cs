using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Community.ModelViews.MeetingViewModels
{
    public class MeetingDetailViewModel
    {
        public string Id { get; set; }

        public string PhotoPath { get; set; }

        public string Name { get; set; }

        public string Category { get; set; }

        public string CreatorId { get; set; }

        public string CreatorName { get; set; }

        public string Location { get; set; }

        public string Date { get; set; }

        public string StartTime { get; set; }

        public string EndTime { get; set; }

        public int UserJoined { get; set; }

        public string Description { get; set; }
    }
}
