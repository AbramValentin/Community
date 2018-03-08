using Community.Data.Tables;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Community.Data
{
    public class MeetingManager
    {
        private readonly CommunityDbContext _db;

        public MeetingManager(CommunityDbContext db)
        {
            _db = db;
        }

        /// <summary>
        /// Creates new Meeting object in database.
        /// </summary>
        /// <param name="meeting">Object to create in database.</param>
        /// <returns></returns>
        public async Task<bool> CheckMeetingOwner(int meetingId, string userId)
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

            return meeting.UserId == userId ? true : false ;
        }

        public async Task<OperationResult> CreateAsync(Meeting meeting)
        {
            if (meeting == null)
            {
                throw new ArgumentNullException();
            }

            _db.Meetings.Add(meeting);
            await _db.SaveChangesAsync();

            return OperationResult.Success;
        }


        /// <summary>
        /// Removes given object from database.
        /// </summary>
        /// <param name="meeting">Object to remove from database.</param>
        /// <returns></returns>
        public async Task<OperationResult> RemoveAsync(Meeting meeting)
        {
            if (meeting == null)
            {
                throw new ArgumentNullException();
            }

            _db.Meetings.Remove(meeting);
            await _db.SaveChangesAsync();

            return OperationResult.Success;
        }


        /// <summary>
        /// Updates data in 
        /// </summary>
        /// <param name="meeting"></param>
        /// <returns></returns>
        public async Task<OperationResult> EditAsync(Meeting meeting)
        {
            var current = _db.Meetings.Find(meeting.Id);

            if (current==null)
            {
                throw new ArgumentNullException();
            }

            _db.Entry(current).CurrentValues.SetValues(meeting);
            await _db.SaveChangesAsync();

            return OperationResult.Success;
        }


        public async Task<bool> IsUserJoinedMeeting(int meetingId, string userId)
        {
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

            return count != 0 ? true : false ;
        }

        public async Task<OperationResult> JoinMeetingAsync(int meetingId, string userId)
        {
            if (await IsUserJoinedMeeting(meetingId, userId) == true)
            {
                return OperationResult.Failed;
            }

            var entity = new UserMeetings
            {
                MeetingId = meetingId,
                UserId = userId,
                Approved = false
            };

            await _db.UserMeetings.AddAsync(entity);
            await _db.SaveChangesAsync();

            return OperationResult.Success;
        }

        public async Task<OperationResult> UnjoinMeetingAsync(int meetingId, string userId)
        {
            if (await IsUserJoinedMeeting(meetingId, userId) == true)
            {
                return OperationResult.Failed;
            } 

            var entity = new UserMeetings
            {
                MeetingId = meetingId,
                UserId = userId,
            };

            _db.UserMeetings.Remove(entity);
            await _db.SaveChangesAsync();

            return OperationResult.Success;
        }

    }

    public enum OperationResult
    {
        Success,
        Failed
    }
}
