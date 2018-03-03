using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Community.ModelViews.MeetingViewModels;
using Community.Data;
using Community.Data.Tables;
using Microsoft.AspNetCore.Identity;

namespace Community.Controllers
{
    public class MeetingController : Controller
    {
        private readonly MeetingManager _meetingManager;
        private readonly UserManager<User> _userManager;
        private readonly MeetingQuery _meetingQuery;
        private readonly LocationQuery _locationQuery;

        public MeetingController(
            MeetingManager meetingManager,
            UserManager<User> userManager,
            MeetingQuery meetingQuery,
            LocationQuery locationQuery
            )
        {
            _meetingManager = meetingManager;
            _userManager = userManager;
            _meetingQuery = meetingQuery;
            _locationQuery = locationQuery;
        }


        [HttpGet]
        public async Task<IActionResult> EditMeeting(int meetingId)
        {
            var meeting = _meetingQuery.GetMeetingById(meetingId);

            var city = await _locationQuery.GetCityById(meeting.CitiesId);

            var meetingCategory = await _meetingQuery
                .GetMeetingCategoriesAsync();

            /*Full location data will be provided on user demand, directly to view, by Ajax call*/
            var model = new MeetingEditViewModel
            {
                Id = meeting.Id,
                UserId = meeting.UserId,
                Name = meeting.Name,
                Description = meeting.Description,
                CityId = meeting.CitiesId,
                PhotoPath = meeting.PhotoPath,
                StartTime = meeting.StartTime,
                EndTime = meeting.EndTime,
                MeetingCategoryId = meeting.MeetingCategoryId,
                MeetingDate = meeting.MeetingDate,
                Street = meeting.Street,
                CityName = city.Name,
                MeetingCategories = meetingCategory
            };

            return View(model);
        }


        
         [HttpPost]
         public async Task<IActionResult> EditMeeting(MeetingEditViewModel model)
         {
            var meeting = new Meeting
            {
                Id = model.Id,
                Name = model.Name,
                Description = model.Description,
                CitiesId = model.CityId,
                PhotoPath = model.PhotoPath,
                StartTime = model.StartTime,
                EndTime = model.EndTime,
                MeetingCategoryId = model.MeetingCategoryId,
                MeetingDate = model.MeetingDate,
                Street = model.Street,
                UserId = model.UserId,
            };

            var result  = await _meetingManager.EditAsync(meeting);

            if (result == OperationResult.Failed)
            {
                return View(model);
            }

            return View("Index","Home");
         }
        



    }
}