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
        public string Id { get; set; }

        [StringLength(maximumLength: 25, MinimumLength = 5)]
        public string Name { get; set; }

        [DataType(DataType.Date)]
        public DateTime MeetingDate { get; set; }

        [StringLength(maximumLength: 2000, MinimumLength = 20)]
        public string Description { get; set; }

        public int? UsersMin { get; set; }

        public int? UsersMax { get; set; }

        public string PhotoPath { get; set; }

        public int? CityId { get; set; }

        [StringLength(maximumLength: 30, MinimumLength = 3)]
        public string Street { get; set; }

        public User Creator { get; set; }

        public MeetingCategory MeetingCategory { get; set; }

        public ICollection<UserMeetings> UserMeetings { get; set; }
    }
}
