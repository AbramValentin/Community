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
        /// Creates meeting in database.
        /// </summary>
        /// <param name="meeting">Meeting object to create.</param>
        /// <returns></returns>
        public async Task<OperationResult> CreateMeetingAsync(Meeting meeting)
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
        /// Removes meeting that has given primary key value from database.
        /// </summary>
        /// <param name="meeting">Primary key of meeting to remove from database.</param>
        /// <returns></returns>
        public async Task<OperationResult> RemoveMeetingAsync(int meetingId)
        {
            var meeting = await _db.Meetings.FindAsync(meetingId);

            if (meeting == null)
            {
                throw new ArgumentNullException("meetingId");
            }

            _db.Meetings.Remove(meeting);
            await _db.SaveChangesAsync();

            return OperationResult.Success;
        }


        /// <summary>
        /// Edit meeting in database.
        /// </summary>
        /// <param name="meeting">Meeting object with changed values.</param>
        /// <returns></returns>
        public async Task<OperationResult> EditMeetingAsync(Meeting meeting)
        {
            var current = await _db.Meetings.FindAsync(meeting.Id);

            if (current==null)
            {
                throw new ArgumentNullException();
            }

            if (meeting.CitiesId == 0) { meeting.CitiesId = current.CitiesId; }

            _db.Entry(current).CurrentValues.SetValues(meeting);
            await _db.SaveChangesAsync();

            return OperationResult.Success;
        }


   
        /// <summary>
        /// Subscribes user for meeting, with approved status equals to false.
        /// </summary>
        /// <param name="meetingId">Primary key of meeting to subscribe.</param>
        /// <param name="userId">Primary key of user to subscribe for meeting.</param>
        /// <returns></returns>
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


        /// <summary>
        /// Cancel user subscription for meeting.
        /// </summary>
        /// <param name="meetingId">Primary key of meeting to cancel subscription.</param>
        /// <param name="userId">Primary key of user that will be unsubscribed.</param>
        /// <returns></returns>
        public async Task<OperationResult> UnsubscribeMeetingAsync(int meetingId, string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                throw new ArgumentNullException("userId");
            }

            var entity =  await _db.UserMeetings.FindAsync(userId, meetingId);

            _db.UserMeetings.Remove(entity);
            await _db.SaveChangesAsync();

            return OperationResult.Success;
        }


        /// <summary>
        /// Approves user subscription for meeting by setting approved status to true.
        /// </summary>
        /// <param name="meetingId">Primary key of meeting to approve user subscription.</param>
        /// <param name="userId">Primary key of user to approve subscription.</param>
        /// <returns></returns>
        public async Task<OperationResult> ApproveMeetingParticipant(int meetingId, string userId)
        {
            if (string.IsNullOrWhiteSpace(userId))
            {
                throw new ArgumentNullException("userId");
            }

            var meeting = await _db.UserMeetings.FindAsync(userId, meetingId);
                

            var entity = new UserMeetings
            {
                UserId = userId,
                MeetingId = meetingId,
                Approved = true
            };

            _db.Entry(meeting).CurrentValues.SetValues(entity);
            await _db.SaveChangesAsync();

            return OperationResult.Success;
        }

        /// <summary>
        /// Declines user subscription for meeting by deleting user from subscribers.
        /// </summary>
        /// <param name="meetingId">Primary key of meeting to decline user subscription.</param>
        /// <param name="userId">Primary key of user to decline subscription.</param>
        /// <returns></returns>
        public async Task<OperationResult> DeclineMeetingParcipant(int meetingId, string userId)
        {
            if (string.IsNullOrWhiteSpace(userId))
            {
                throw new ArgumentNullException("userId");
            }

            var meeting = await _db.UserMeetings.FindAsync(userId, meetingId);

            _db.UserMeetings.Remove(meeting);
            await _db.SaveChangesAsync();

            return OperationResult.Success;
        }

    }


    /// <summary>
    /// Indicates was operation finished successfully or not.
    /// Returns OperationResult.Success if successfully or 
    /// OperationResult.Failed if not.
    /// </summary>
    public enum OperationResult
    {
        Success,
        Failed
    }
}
