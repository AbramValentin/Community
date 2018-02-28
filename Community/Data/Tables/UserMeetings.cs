using System.ComponentModel.DataAnnotations.Schema;

namespace Community.Data.Tables
{
    public class UserMeetings
    {
        public string UserId { get; set; }
        public User User { get; set; }

        public int MeetingId { get; set; }
        public Meeting Meeting { get; set; }
    }
}
