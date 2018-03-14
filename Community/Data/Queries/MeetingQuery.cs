using Community.Data.Tables;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Community.Data
{
    public class MeetingQuery
    {
        private readonly CommunityDbContext _db;

        public MeetingQuery(CommunityDbContext db)
        {
            _db = db;
        }
        
        /// <summary>
        /// Checks if particular user is creator of particular meeting.
        /// Returns true if user created this meeting and false if not.
        /// </summary>
        /// <param name="meetingId">Id of meeting to check.</param>
        /// <param name="userId">Id of user to check.</param>
        /// <returns></returns>
        /// 
        public async Task<bool> CheckMeetingOwnerAsync(int meetingId, string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                return false;
            }

            var meeting = await _db.Meetings.FindAsync(meetingId);

            if (meeting == null)
            {
                throw new Exception("meeting with such primary key is not fount (Valentin)");
            }

            return meeting.UserId == userId ? true : false;
        }


        /// <summary>
        /// Checks if user already subscribed for meeting.
        /// Returns true if subscribed and false if not.
        /// </summary>
        /// <param name="meetingId">Id of meeting to check.</param>
        /// <param name="userId">Id of user to check</param>
        /// <returns></returns>
        public async Task<bool> IsUserSubscribedMeetingAsync(int meetingId, string userId)
        {
            if (string.IsNullOrWhiteSpace(userId))
            {
                return false;
            }

            if (await _db.Users.FindAsync(userId) == null)
            {
                throw new Exception("user with such id does not exist");
            }

            if (await _db.Meetings.FindAsync(meetingId) == null)
            {
                throw new Exception("meeting with such id does not exist");
            }

            var count = await _db.UserMeetings
                .Where(m => m.MeetingId == meetingId && m.UserId == userId)
                .CountAsync();

            return count != 0 ? true : false;
        }


        /// <summary>
        /// Returns amount of users that is subscribed and confirmed
        /// by meeting creator as participants for particular meeting.
        /// </summary>
        /// <param name="meetingId">Id of meeting to check.</param>
        /// <returns></returns>
        public async Task<int> GetMeetingConfirmedUsersCountAsync(int meetingId)
        {
            var count = await _db.UserMeetings
                .Where(m => m.MeetingId == meetingId && m.Approved == true)
                .CountAsync();

            return count;
        }


        /// <summary>
        /// Returns amount of users that is subscribed but not confirmed
        /// by meeting creator as participants for particular meeting.
        /// </summary>
        /// <param name="meetingId">Id of meeting to check.</param>
        /// <returns></returns>
        public async Task<int> GetMeetingUnconfirmedUsersCountAsync(int meetingId)
        {
            var count = await _db.UserMeetings
                .Where(m => m.MeetingId == meetingId && m.Approved == false)
                .CountAsync();

            return count;
        }


        /// <summary>
        /// Returns users that is subscribed and confirmed
        /// by meeting creator as participants for particular meeting.
        /// </summary>
        /// <param name="meetingId">Id of meeting to check.</param>
        /// <returns></returns>
        public async Task<IEnumerable<User>> GetMeetingUnconfirmedUsersAsync(int meetingId)
        {
            var users = from u in _db.Users
                        join um in _db.UserMeetings
                        on u.Id equals um.UserId
                        into userList
                        from ul in userList
                        where 
                            ul.Approved == false 
                                && 
                            ul.MeetingId == meetingId
                        select u;

            return await users.ToListAsync();
        }


        /// <summary>
        /// Returns users that is subscribed but not confirmed
        /// by meeting creator as participants for particular meeting.
        /// </summary>
        /// <param name="meetingId">Id of meeting to check.</param>
        /// <returns></returns>
        public async Task<IEnumerable<User>> GetMeetingConfirmedUsersAsync(int meetingId)
        {
            var users = from u in _db.Users
                        join um in _db.UserMeetings
                        on u.Id equals um.UserId
                        into userList
                        from ul in userList
                        where
                            ul.Approved == true
                                &&
                            ul.MeetingId == meetingId
                        select u;

            return await users.ToListAsync();
        }


        /// <summary>
        /// Returns meetings created by user.
        /// </summary>
        /// <param name="idUser">Id of user.</param>
        /// <returns></returns>
        public async Task<IEnumerable<Meeting>> GetUserCreatedMeetingsAsync(string idUser)
        {
            var meetingList = await _db.Meetings
                .Where(m => m.UserId == idUser)
                .ToListAsync();

            return meetingList;
        }


        /// <summary>
        /// Returns meetings joined by user.
        /// </summary>
        /// <param name="idUser">Id of user.</param>
        /// <returns></returns>
        public async Task<IEnumerable<Meeting>> GetUserSubscribedMeetingsAsync(string userId)
        {
            var result = from m in _db.Meetings
                         join um in _db.UserMeetings
                         on m.Id equals um.MeetingId
                         into joinedMeetings
                         from jm in joinedMeetings
                         where jm.UserId == userId
                         select m;

            return await result.ToListAsync();
        }


        /// <summary>
        /// Returns single meeting by given id.
        /// </summary>
        /// <param name="meetingId">Id of meeting to get.</param>
        /// <returns></returns>
        public async Task<Meeting> GetMeetingByIdAsync(int meetingId)
        {
            var meeting = await _db.Meetings.FindAsync(meetingId);
            return meeting;
        }


        /// <summary>
        /// Returns last registered  meetings in database.
        /// </summary>
        /// <param name="amount">Amount of meetings to get.</param>
        /// <returns></returns>
        public async Task<IEnumerable<Meeting>> GetLastRegisteredMeetingsAsync(int amount)
        {
            var meetingList = await _db.Meetings
                .Take(amount)
                .OrderByDescending(m => m.MeetingDate)
                .ToListAsync();

            return meetingList;
        }


        /// <summary>
        /// Returns all available meeting categories.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<MeetingCategory>> GetMeetingCategoriesAsync()
        {
            var list = await _db.MeetingCategories.ToListAsync();
            return list;
        }


        /// <summary>
        /// Returns meeting category by given primary key value.
        /// </summary>
        /// <param name="meetingId">Primary key of meeting category.</param>
        /// <returns></returns>
        public async Task<MeetingCategory> GetMeetingCategoryByIdAsync(int meetingId)
        {
            var category = await _db
                .MeetingCategories.FindAsync(meetingId);

            return category; 
        }
    }
}
