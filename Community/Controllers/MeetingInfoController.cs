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
    public class MeetingInfoController : Controller
    {
        private readonly MeetingManager _meetingManager;
        private readonly UserManager<User> _userManager;
        private readonly MeetingQuery _meetingQuery;
        private readonly LocationQuery _locationQuery;

        public MeetingInfoController(
            UserManager<User> userManager,
            MeetingManager meetingManager,
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
        public async Task<IActionResult> Index()
        {
            var userMeetings = await _meetingQuery.GetLastRegisteredMeetingsAsync(10);

            List<MeetingGeneralInfoViewModel> modelList = new List<MeetingGeneralInfoViewModel>();

            foreach (var item in userMeetings)
            {
                modelList.Add(
                    new MeetingGeneralInfoViewModel
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

        [HttpGet]
        public async Task<IActionResult> MeetingsOwn()
        {
            var userId = _userManager.GetUserId(User);
            var userMeetings = await _meetingQuery.GetUserCreatedMeetings(userId);


            List<MeetingOwnInfoViewModel> modelList = new List<MeetingOwnInfoViewModel>();

            foreach (var item in userMeetings)
            {
                modelList.Add(
                    new MeetingOwnInfoViewModel
                    {
                        Id = item.Id,

                        Name = item.Name,

                        CategoryName = (await _meetingQuery.GetMeetingCategoryByIdAsync(item.MeetingCategoryId)).Name,

                        CityName = (await _locationQuery.GetCityByIdAsync(item.CitiesId)).Name,

                        ConfirmedUsersRequests = await _meetingQuery.GetMeetingConfirmedUsersCount(item.Id),

                        UnconfirmedUsersRequests = await _meetingQuery.GetMeetingUnconfirmedUsersCount(item.Id),

                        PhotoPath = item.PhotoPath,

                        ShortDateString = item.MeetingDate.ToShortDateString()
                    });
            }

            return View(modelList);
        }

        [HttpGet]
        public async Task<IActionResult> MeetingsSubscribed()
        {
            var userId = _userManager.GetUserId(User);
            var userMeetings = await _meetingQuery.GetUserSubscribedMeetings(userId);

            List<MeetingGeneralInfoViewModel> modelList = new List<MeetingGeneralInfoViewModel>();

            foreach (var item in userMeetings)
            {
                modelList.Add(
                    new MeetingGeneralInfoViewModel
                    {
                        Id = item.Id,

                        Name = item.Name,

                        CityName = (await _locationQuery.GetCityByIdAsync(item.CitiesId)).Name,

                        CategoryName = (await _meetingQuery.GetMeetingCategoryByIdAsync(item.MeetingCategoryId)).Name,

                        ConfirmedUsersRequests = await _meetingQuery.GetMeetingConfirmedUsersCount(item.Id),

                        PhotoPath = item.PhotoPath,

                        ShortDateString = item.MeetingDate.ToShortDateString()
                    });
            }

            return View(modelList);
        }

        [HttpGet]
        public async Task<IActionResult> MeetingGeneralInfo(int meetingId)
        {
            var meeting = await _meetingQuery.GetMeetingByIdAsync(meetingId);

            var model = new MeetingGeneralInfoViewModel
            {
                Id = meeting.Id,

                Name = meeting.Name,

                Description = meeting.Description,

                CategoryName = (await _meetingQuery.GetMeetingCategoryByIdAsync(meeting.MeetingCategoryId)).Name,

                CityName = (await _locationQuery.GetCityByIdAsync(meeting.CitiesId)).Name,

                ConfirmedUsersRequests = await _meetingQuery.GetMeetingConfirmedUsersCount(meeting.Id),

                PhotoPath = meeting.PhotoPath,

                ShortDateString = meeting.MeetingDate.ToShortDateString()
            };

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> MeetingOwnInfo(int meetingId)
        {
            var userId = _userManager.GetUserId(User);
            var ownership = await _meetingQuery.CheckMeetingOwner(meetingId, userId);

            if (ownership == false)
            {
                RedirectPermanent(Url.Action("Index", "Home"));
            }

            var meeting = await _meetingQuery.GetMeetingByIdAsync(meetingId);

            var model = new MeetingOwnInfoViewModel
            {

                Id = meeting.Id,

                Name = meeting.Name,

                Description = meeting.Description,

                CategoryName = (await _meetingQuery.GetMeetingCategoryByIdAsync(meeting.MeetingCategoryId)).Name,

                CityName = (await _locationQuery.GetCityByIdAsync(meeting.CitiesId)).Name,

                ConfirmedUsersRequests = await _meetingQuery.GetMeetingConfirmedUsersCount(meeting.Id),

                UnconfirmedUsersRequests = await _meetingQuery.GetMeetingUnconfirmedUsersCount(meeting.Id),

                PhotoPath = meeting.PhotoPath,

                ShortDateString = meeting.MeetingDate.ToShortDateString()
            };

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> MeetingUnconfirmedSubscribers(int meetingId)
        {
            var userId = _userManager.GetUserId(User);

            var checkResult = await _meetingQuery.CheckMeetingOwner(meetingId, userId);

            if (checkResult == false)
            {
                return RedirectPermanent(Url.Action("Index", "Home"));
            }

            var unconfirmedSubscribers = await _meetingQuery.GetMeetingUnconfirmedUsers(meetingId);

            var subscribersList = new List<MeetingSubscriberViewModel>();

            var meeting = await _meetingQuery.GetMeetingByIdAsync(meetingId);

            foreach (var item in unconfirmedSubscribers)
            {
                subscribersList.Add(
                        new MeetingSubscriberViewModel
                        {
                            UserId = item.Id,
                            UserFirstName = item.UserFirstName,
                            UserLastName = item.UserLastName,
                            UserPhotoPath = item.PhotoPath ?? " ",

                            MeetingId = meeting.Id,
                            MeetingName = meeting.Name,
                            MeetingPhotoPath = meeting.PhotoPath
                        }
                    );
            }

            return View(subscribersList);
        }

        [HttpGet]
        public async Task<IActionResult> MeetingConfirmedSubscribers(int meetingId)
        {
            var userId = _userManager.GetUserId(User);

            var checkResult = await _meetingQuery.CheckMeetingOwner(meetingId, userId);

            if (checkResult == false)
            {
                return RedirectPermanent(Url.Action("Index", "Home"));
            }

            var unconfirmedSubscribers = await _meetingQuery.GetMeetingConfirmedUsers(meetingId);

            var subscribersList = new List<MeetingSubscriberViewModel>();

            var meeting = await _meetingQuery.GetMeetingByIdAsync(meetingId);

            foreach (var item in unconfirmedSubscribers)
            {
                subscribersList.Add(
                        new MeetingSubscriberViewModel
                        {
                            UserId = item.Id,
                            UserFirstName = item.UserFirstName,
                            UserLastName = item.UserLastName,
                            UserPhotoPath = item.PhotoPath ?? " ",

                            MeetingId = meeting.Id,
                            MeetingName = meeting.Name,
                            MeetingPhotoPath = meeting.PhotoPath
                        }
                    );
            }

            return View(subscribersList);
        }

    }
}
