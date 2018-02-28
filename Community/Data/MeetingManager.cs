using Community.Data.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Community.Data
{
    public class MeetingManager
    {
        private readonly CommunityDbContext _dbContext;


        public MeetingManager(CommunityDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<int> CreateMeeting(Meeting meeting)
        {
            _dbContext.Meetings.Add(meeting);
            return _dbContext.SaveChangesAsync();
        }


    }
}
