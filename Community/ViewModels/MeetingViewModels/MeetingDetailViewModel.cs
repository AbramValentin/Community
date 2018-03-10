using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Community.ModelViews.MeetingViewModels
{
    public class MeetingDetailViewModel
    {
        public int Id { get; set; }

        [DataType(DataType.ImageUrl)]
        public string PhotoPath { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Category { get; set; }

        public string CreatorId { get; set; }

        public string CreatorName { get; set; }

        public string Location { get; set; }

        public string Date { get; set; }

        public bool UserJoined { get; set; }
    }
}
