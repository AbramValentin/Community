using Community.Data;
using Community.Data.Tables;
using Community.ModelViews.MeetingViewModels;
using Community.ViewModels.MeetingViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Community
{
    public class HomeController : Controller
    {
        private readonly MeetingManager _meetingManager;
        private readonly UserManager<User> _userManager;
        private readonly MeetingQuery _meetingQuery;
        private readonly LocationQuery _locationQuery;

        public HomeController(
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

        public async Task<IActionResult> Index()
        {
            var userMeetings = await _meetingQuery.GetLastRegisteredMeetingsAsync(10);

            List<GeneralMeetingInfoViewModel> modelList = new List<GeneralMeetingInfoViewModel>();

            foreach (var item in userMeetings)
            {
                modelList.Add(
                    new GeneralMeetingInfoViewModel
                    {
                        Id = item.Id,

                        Name = item.Name,
                        
                        Description = string.Concat(item.Description.Take(15)),

                        CityName = (await _locationQuery.GetCityByIdAsync(item.CitiesId)).Name,

                        CategoryName = (await _meetingQuery.GetMeetingCategoryByIdAsync(item.MeetingCategoryId)).Name,

                        ConfirmedUsersRequests = await _meetingQuery.GetMeetingConfirmedUsersCount(item.Id),

                        PhotoPath = item.PhotoPath,

                        ShortDateString = item.MeetingDate.ToShortDateString()
                    });
            }

            return View(modelList);
        }

       
    }
}
