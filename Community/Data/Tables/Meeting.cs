using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Community.Data.Tables
{
    public class Meeting
    {
        public int Id { get; set; }

        [StringLength(maximumLength: 25, MinimumLength = 5)]
        public string Name { get; set; }

        [DataType(DataType.Date)]
        public DateTime MeetingDate { get; set; }

        [StringLength(maximumLength: 2000, MinimumLength = 20)]
        public string Description { get; set; }

        public string PhotoPath { get; set; }

        public int CitiesId { get; set; }

        public string UserId { get; set; }

        public int MeetingCategoryId { get; set; }

        public ICollection<UserMeetings> UserMeetings { get; set; }
    }
}
