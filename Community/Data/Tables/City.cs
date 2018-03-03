using Community.Data.Tables;
using System.Collections.Generic;

namespace Community.Data
{
    public class City
    {
        public int Id { get; set; }
        public int AreaId { get; set; }
        public string Name { get; set; }
        public ICollection<Meeting> Meetings { get; set; }
    }
}
