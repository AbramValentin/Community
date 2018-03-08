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
        /// Returns meetings created by user.
        /// </summary>
        /// <param name="idUser">Id of user.</param>
        /// <returns></returns>
        public IEnumerable<Meeting> GetUserCreatedMeetings(string idUser)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// Returns meetings joined by user.
        /// </summary>
        /// <param name="idUser">Id of user.</param>
        /// <returns></returns>
        public IEnumerable<Meeting> GetUserJoinedMeetings(string idUser)
        {
            throw new NotImplementedException();
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
        /// Returns particular amount of latest registered  meetings in database.
        /// </summary>
        /// <param name="amount">Amount of meetings to get.</param>
        /// <returns></returns>
        public IEnumerable<Meeting> GetLastRegisteredMeetings(int amount)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Returns meetings filtered by given city id.
        /// </summary>
        /// <param name="cityId">Id of city to filter by.</param>
        /// <returns></returns>
        public IEnumerable<Meeting> GetMeetingsByCityId(int cityId)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// Returns meetings filtered by given city id.
        /// </summary>
        /// <param name="cityId">Id of city to filter by.</param>
        /// <param name="skip">Amount of first selected items to skip.</param>
        /// <param name="take">Amount of items to return.</param>
        /// <returns></returns>
        public IEnumerable<Meeting> GetMeetingsByCityId(int cityId, int skip, int take)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// Returns meetings filtered by given category id.
        /// </summary>
        /// <param name="categoryId">Id of category to filter by.</param>
        /// <returns></returns>
        public IEnumerable<Meeting> GetMeetingsByCategoryId(int categoryId)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// Returns meetings filtered by given category id.
        /// </summary>
        /// <param name="categoryId">Id of category to filter by.</param>
        /// <param name="skip">Amount of first selected items to skip.</param>
        /// <param name="take">Amount of items to return.</param>
        /// <returns></returns>
        public IEnumerable<Meeting> GetMeetingsByCategoryId(int categoryId, int skip, int take)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// Returns meetings filtered by city id and category id. 
        /// </summary>
        /// <param name="cityId">Id of city to filter by.</param>
        /// <param name="categoryId">Id of category to filter by.</param>
        /// <returns></returns>
        public IEnumerable<Meeting> GetMeetingsByCityIdAndCategoryId(int cityId, int categoryId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Returns meetings filtered by city id and category id. 
        /// </summary>
        /// <param name="cityId">Id of city to filter by.</param>
        /// <param name="categoryId">Id of category to filter by.</param>
        /// <param name="skip">Amount of first selected items to skip.</param>
        /// <param name="take">Amount of items to return.</param>
        /// <returns></returns>
        public IEnumerable<Meeting> GetMeetingByCityIdAndCategoryId(int cityId, int categoryId, int skip, int take)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<MeetingCategory>> GetMeetingCategoriesAsync()
        {
            var list = await _db.MeetingCategories.ToListAsync();
            return list;
        }

        public async Task<MeetingCategory> GetMeetingCategoryByIdAsync(int meetingId)
        {
            var category = await _db
                .MeetingCategories
                .Where(c => c.Id == meetingId)
                .SingleAsync();

            return category; 
        }
    }
}
