using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Community.ModelViews.MeetingViewModels;
using Community.Data;
using Community.Data.Tables;
using Microsoft.AspNetCore.Identity;
using Community.Services;
using Community.ViewModels.MeetingViewModels;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using System;

namespace Community.Controllers
{
    [Authorize]
    public class MeetingManageController : Controller
    {
        private readonly MeetingManager _meetingManager;
        private readonly UserManager<User> _userManager;
        private readonly MeetingQuery _meetingQuery;
        private readonly LocationQuery _locationQuery;
        private readonly FileService _fileService;

        public MeetingManageController(
            UserManager<User> userManager,
            MeetingManager meetingManager,
            MeetingQuery meetingQuery,
            LocationQuery locationQuery,
            FileService fileService
            )
        {
            _userManager = userManager;
            _meetingManager = meetingManager;
            _meetingQuery = meetingQuery;
            _locationQuery = locationQuery;
            _fileService = fileService;
        }


        [HttpGet]
        public async Task<IActionResult> MeetingCreate()
        {
            MeetingCreateViewModel model = new MeetingCreateViewModel();

            model.MeetingCategories = await _meetingQuery.GetMeetingCategoriesAsync();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> MeetingCreate(MeetingCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.MeetingCategories = await _meetingQuery.GetMeetingCategoriesAsync();
                return View(model);
            }

            if (await _locationQuery.GetCityByIdAsync(model.CityId) == null)
            {
                ModelState.AddModelError("CityId", "Please choose a city");
                model.MeetingCategories = await _meetingQuery.GetMeetingCategoriesAsync();
                return View(model);
            }
            
            
            string photoPath = await _fileService.SaveImage(model.PhotoPath);

            var userId = _userManager.GetUserId(User);

            var meeting = new Meeting
            {
                Name = model.Name,
                Description = model.Description,
                CitiesId = model.CityId ,
                MeetingCategoryId = model.MeetingCategoryId,
                MeetingDate = model.MeetingDate,
                PhotoPath = photoPath,
                UserId = userId,
            };

            var result = await _meetingManager.CreateMeetingAsync(meeting);

            if (result == OperationResult.Failed)
            {
                return View(model);
            }

            return RedirectPermanent(Url.Action("Index", "MeetingInfo"));
        }


        [HttpGet]
        public async Task<IActionResult> MeetingEdit(int meetingId)
        {
            var userId = _userManager.GetUserId(User);
            var checkOwnership = await _meetingQuery.CheckMeetingOwnerAsync(meetingId, userId);

            if (checkOwnership == false)
            {
                return RedirectPermanent(Url.Action("Index", "MeetingInfo"));
            }

            var meeting = await _meetingQuery.GetMeetingByIdAsync(meetingId);

            var meetingCategories = await _meetingQuery
                .GetMeetingCategoriesAsync();

            var model = new MeetingEditViewModel
            {
                Id = meeting.Id,
                Name = meeting.Name,
                Description = meeting.Description,                
                MeetingCategoryId = meeting.MeetingCategoryId,
                MeetingCategories = meetingCategories,
                MeetingDate = meeting.MeetingDate,
                CurrentPhotoPath = meeting.PhotoPath,
                CityId = meeting.CitiesId
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> MeetingEdit(MeetingEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            string photoPath = null;
            if (model.PhotoSource != null)
            {
                photoPath = await _fileService
                    .UpdateImageAsync(model.CurrentPhotoPath, model.PhotoSource);
            }

            var userId = _userManager.GetUserId(User);

            var meeting = new Meeting
            {
                Id=model.Id,
                Name = model.Name,
                Description = model.Description,
                MeetingCategoryId = model.MeetingCategoryId,
                MeetingDate = model.MeetingDate,
                PhotoPath = photoPath ?? model.CurrentPhotoPath,
                UserId = userId,
                CitiesId = model.CityId
            };

            var result = await _meetingManager.EditMeetingAsync(meeting);

            if (result == OperationResult.Failed)
            {
                return View(model);
            }

            return RedirectPermanent(Url.Action("MeetingsOwn", "MeetingInfo"));
        }


        public async Task<IActionResult> MeetingDelete(int meetingId)
        {
            var userId = _userManager.GetUserId(User);

            if (await _meetingQuery.CheckMeetingOwnerAsync(meetingId, userId) == false)
            {
                return Content("You are not meeting owner");
            }
            
            var result = await _meetingManager.RemoveMeetingAsync(meetingId);

            if (result == OperationResult.Failed)
            {
                return Content("o_O ... Something happens ...");
            }

            return RedirectPermanent(Url.Action("MeetingsOwn", "MeetingInfo"));
        }

        [HttpPost]
        public async Task<IActionResult> MeetingSubscribe(int meetingId)
        {

            var userId = _userManager.GetUserId(User);

            if (userId == null)
            {
                return RedirectToAction("LogIn", "Account");
            }

            var result = await _meetingManager.SubscribeMeetingAsync(meetingId, userId);

            if (result == OperationResult.Failed)
            {
                return Content("o_O ... Something happens ... MeetingSubscribe");
            }

            return PartialView("~/Views/MeetingPartials/_UserSubscriptionPartial.cshtml", meetingId);
        }

        [HttpPost]
        public async Task<IActionResult> MeetingUnsubscribe(int meetingId)
        {
            var userId = _userManager.GetUserId(User);
            bool subscribed = await _meetingQuery.IsUserSubscribedMeetingAsync(meetingId, userId);

            if (!subscribed)
            {
                return Content("o_O ... Something happens ... MeetingUnsubscribe : User is unsubscribed");
            }

            var result = await _meetingManager.UnsubscribeMeetingAsync(meetingId, userId);

            if (result == OperationResult.Failed)
            {
                return Content("o_O ... Something happens ... MeetingUnsubscribe Unsubscription failed");
            }

            return PartialView("~/Views/MeetingPartials/_UserSubscriptionPartial.cshtml", meetingId);
        }


        [HttpPost]
        public async Task MeetingAcceptUser(string userId, int meetingId)
        {
            var result = await _meetingManager.ApproveMeetingParticipant(meetingId, userId);

            if (result == OperationResult.Failed)
            {
                throw new Exception("Proccess failed");
            }
        }

        [HttpPost]
        public async Task MeetingDeclineUser(string userId, int meetingId)
        {
            var result = await _meetingManager.DeclineMeetingParcipant(meetingId, userId);

            if (result == OperationResult.Failed)
            {
                throw new Exception("Proccess failed");
            }
        }

    }
}