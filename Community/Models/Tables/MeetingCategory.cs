using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Community.Data.Tables
{
    public class MeetingCategory
    {
        public string Id { get; set; }

        [StringLength(maximumLength: 25, MinimumLength = 3)]
        public string Name { get; set; }

        public ICollection<Meeting> Meetings { get; set; }
    }
}
