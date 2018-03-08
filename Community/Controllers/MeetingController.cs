using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Community.ModelViews.MeetingViewModels;
using Community.Data;
using Community.Data.Tables;
using Microsoft.AspNetCore.Identity;
using Community.Services;
using System.IO;
using Microsoft.AspNetCore.Http;

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
        public async Task<IActionResult> Create()
        {
            MeetingCreateViewModel model = new MeetingCreateViewModel();

            model.MeetingCategories = await _meetingQuery.GetMeetingCategoriesAsync();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(MeetingCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.MeetingCategories = await _meetingQuery.GetMeetingCategoriesAsync();
                return View(model);
            }

            string photoPath = "";
            if (model.PhotoPath != null)
            {
                var fileService = new FileService();
                photoPath = await fileService.SaveImage(model.PhotoPath);
            }

            var userId = _userManager.GetUserId(User);

            var meeting = new Meeting
            {
                Name = model.Name,
                Description = model.Description,
                CitiesId = model.CityId = 1,/*!!! DANGER , DEFAULT VALUE !!!*/
                MeetingCategoryId = model.MeetingCategoryId,
                MeetingDate = model.MeetingDate,
                PhotoPath = photoPath,
                UserId = userId,
            };

            var result = await _meetingManager.CreateAsync(meeting);

            if (result == OperationResult.Failed)
            {
                return View(model);
            }

            return RedirectPermanent(Url.Action("Index", "Home"));
        }


        [HttpGet]
        public async Task<IActionResult> EditMeeting(int meetingId)
        {
            var userId = _userManager.GetUserId(User);
            var checkResult = await _meetingManager.CheckMeetingOwner(meetingId, userId);

            if (checkResult==false)
            {
                return RedirectPermanent(Url.Action("Index", "Home"));
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
                currentPhotoPath = meeting.PhotoPath
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditMeeting(MeetingEditViewModel model,int meetingId,string currentPhotoPath)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            string photoPath = null;
            if (model.PhotoSource != null)
            {
                var fileService = new FileService();
                photoPath = await fileService
                    .UpdateImageAsync(model.currentPhotoPath, model.PhotoSource);
            }

            var userId = _userManager.GetUserId(User);

            var meeting = new Meeting
            {
                Id=meetingId,
                Name = model.Name,
                Description = model.Description,
                MeetingCategoryId = model.MeetingCategoryId,
                MeetingDate = model.MeetingDate,
                PhotoPath = photoPath ?? currentPhotoPath,
                UserId = userId,
            };

            var result = await _meetingManager.EditAsync(meeting);

            if (result == OperationResult.Failed)
            {
                return View(model);
            }

            return RedirectPermanent(Url.Action("Index", "Home"));
        }


        [HttpGet]
        public async Task<IActionResult> Detail(int meetingId)
        {
            var meeting = await _meetingQuery.GetMeetingByIdAsync(meetingId);
            var city = await _locationQuery.GetCityByIdAsync(1);
            var category = await _meetingQuery.GetMeetingCategoryByIdAsync(meeting.MeetingCategoryId);
            var userId = _userManager.GetUserId(User);
            var isUserJoined = await _meetingManager.IsUserJoinedMeeting(meetingId, userId);

            var model = new MeetingDetailViewModel
            {
                Id = meeting.Id,
                Name = meeting.Name,
                Description = meeting.Description,
                Date = meeting.MeetingDate.ToShortDateString(),
                Location = city.Name,
                Category = category.Name,
                PhotoPath = meeting.PhotoPath,
                CreatorId = meeting.UserId,
                UserJoined = isUserJoined
            };

            return View(model);
        }
        

        public async Task<IActionResult> Join(int meetingId)
        {
            var userId = _userManager.GetUserId(User);
            var result = await _meetingManager.JoinMeetingAsync(meetingId, userId);

            if (result == OperationResult.Failed)
            {
                return Content("Joining failed");
            }
            return Content("Joined");
            
        }
        
        public async Task<IActionResult> Unjoin(int meetingId)
        {
            var userId = _userManager.GetUserId(User);
            var result = await _meetingManager.UnjoinMeetingAsync(meetingId, userId);

            if (result == OperationResult.Failed)
            {
                return Content("Unjoining failed");
            }

            return Content("Unjoined");
        }

        /* !!!! OWNER CONFIRMATION REQUIRED ~!!!! */
        [HttpGet]
        public async Task<IActionResult> Delete(int meetingId)
        {
            var meeting = await _meetingQuery.GetMeetingByIdAsync(meetingId);
            var result = await _meetingManager.RemoveAsync(meeting);

            if (result == OperationResult.Failed)
            {
                return Content("o_O ... Something happens ...");
            }

            return View(nameof(HomeController.Index));
        }
    }
}