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


   

        public async Task<OperationResult> SubscribeMeetingAsync(int meetingId, string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                throw new ArgumentNullException("userId");
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

        public async Task<OperationResult> UnsubscribeMeetingAsync(int meetingId, string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                throw new ArgumentNullException("userId");
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
