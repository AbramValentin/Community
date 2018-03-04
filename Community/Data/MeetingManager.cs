using Community.Data.Models;
using Community.Data.Tables;
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


        public async Task<OperationResult> JoinMeetingAsync(int meetingId, string userId)
        {
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
            var entity = new UserMeetings
            {
                MeetingId = meetingId,
                UserId = userId,
            };

            _db.UserMeetings.Remove(entity);
            await _db.SaveChangesAsync();

            return OperationResult.Success;
        }



        //public async Task<MeetingEditModel> GetMeetingEditModelById(int idMeeting)
        //{
        //    var meeting = _db.Meetings.Where(m => m.Id == idMeeting).Single();

        //    /*var regionId = _locationQuery.GetRegionByCityId(meeting.CityId).Id;*/
        //    /*var areaId = _locationQuery.GetAreaByCityId(meeting.CityId).Id;*/
        //    /*var areasList = _locationQuery.GetAreasByRegionId(regionId);*/
        //    /*var regionsList = _locationQuery.GetRegions();*/
        //    /*var citiesList = _locationQuery.GetCitiesByAreaId(areaId);*/

        //    var meetingModel = new MeetingEditModel
        //    {
        //        Id = meeting.Id,
        //        Name = meeting.Name,
        //        MeetingDate = meeting.MeetingDate,
        //        Description = meeting.Description,
        //        StartTime = "start hardcode",
        //        EndTime = "end hardcode",
        //        PhotoPath = meeting.PhotoPath,
        //        RegionId = 

        //    };
        //}

    }

    public class MeetingEditModel
    {
        public int Id { get; set; }

        [StringLength(maximumLength: 25, MinimumLength = 5)]
        public string Name { get; set; }

        [DataType(DataType.Date)]
        public DateTime MeetingDate { get; set; }

        [StringLength(maximumLength: 2000, MinimumLength = 20)]
        public string Description { get; set; }

        public string StartTime { get; set; }

        public string EndTime { get; set; }

        [DataType(DataType.ImageUrl)]
        public string PhotoPath { get; set; }

        public int? RegionId { get; set; }
        public IEnumerable<Location> RegionList { get; set; }

        public int? AreaId { get; set; }
        public IEnumerable<Location> AreaList { get; set; }

        public int? CityId { get; set; }
        public IEnumerable<Location> CityList { get; set; }

        [StringLength(maximumLength: 30, MinimumLength = 3)]
        public string Street { get; set; }

        public int CategoryId { get; set; }
        public IEnumerable<MeetingCategory> MeetingCategories { get; set; }
    }

    public enum OperationResult
    {
        Success,
        Failed
    }
}
