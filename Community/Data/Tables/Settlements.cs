using Community.Data.Tables;
using System.Collections.Generic;

namespace Community.Data
{
    public class Settlements
    {
        public int Id { get; set; }
        public int AreaId { get; set; }
        public string City { get; set; }
        public ICollection<Meeting> Meetings { get; set; }
    }
}
