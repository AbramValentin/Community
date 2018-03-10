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

namespace Community.Controllers
{
    [Authorize]
    public class MeetingController : Controller
    {
        private readonly MeetingManager _meetingManager;
        private readonly UserManager<User> _userManager;
        private readonly MeetingQuery _meetingQuery;
        private readonly LocationQuery _locationQuery;

        public MeetingController(
            UserManager<User> userManager,
            MeetingManager meetingManager,
            MeetingQuery meetingQuery,
            LocationQuery locationQuery
            )
        {
            _userManager = userManager;
            _meetingManager = meetingManager;
            _meetingQuery = meetingQuery;
            _locationQuery = locationQuery;
        }

        [HttpGet]
        public async Task<IActionResult> MyMeetings()
        {
            var userId = _userManager.GetUserId(User);
            var userMeetings = await _meetingQuery.GetUserCreatedMeetings(userId);


            List<MyMeetingInfoViewModel> modelList = new List<MyMeetingInfoViewModel>();

            foreach (var item in userMeetings)
            {
                modelList.Add(
                    new MyMeetingInfoViewModel
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
        public async Task<IActionResult> SubscribedMeetings()
        {
            var userId = _userManager.GetUserId(User);
            var userMeetings = await _meetingQuery.GetUserSubscribedMeetings(userId);

            List<GeneralMeetingInfoViewModel> modelList = new List<GeneralMeetingInfoViewModel>();

            foreach (var item in userMeetings)
            {
                modelList.Add(
                    new GeneralMeetingInfoViewModel
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
        public async Task<IActionResult> MyMeetingInfo(int meetingId)
        {
            var userId = _userManager.GetUserId(User);
            var ownership  = await _meetingQuery.CheckMeetingOwner(meetingId, userId);

            if (ownership == false)
            {
                RedirectPermanent(Url.Action("Index", "Home"));
            }

            var meeting = await _meetingQuery.GetMeetingByIdAsync(meetingId);

            var model =new MyMeetingInfoViewModel{

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
        [AllowAnonymous]
        public async Task<IActionResult> GeneralMeetingInfo(int meetingId)
        {
            var meeting = await _meetingQuery.GetMeetingByIdAsync(meetingId);

            var model = new GeneralMeetingInfoViewModel
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
        public async Task<IActionResult> CreateMeeting()
        {
            MeetingCreateViewModel model = new MeetingCreateViewModel();

            model.MeetingCategories = await _meetingQuery.GetMeetingCategoriesAsync();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateMeeting(MeetingCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.MeetingCategories = await _meetingQuery.GetMeetingCategoriesAsync();
                return View(model);
            }
            
            var fileService = new FileService();
            string photoPath = await fileService.SaveImage(model.PhotoPath);

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
            var checkResult = await _meetingQuery.CheckMeetingOwner(meetingId, userId);

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


        /*INFO ACTION!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!*/

        [HttpGet]
        public async Task<IActionResult> DetailMeeting(int meetingId)
        {
            var meeting = await _meetingQuery.GetMeetingByIdAsync(meetingId);
            var city = await _locationQuery.GetCityByIdAsync(1);
            var category = await _meetingQuery.GetMeetingCategoryByIdAsync(meeting.MeetingCategoryId);
            var userId = _userManager.GetUserId(User);
            var isUserJoined = await _meetingQuery.IsUserSubscribedMeeting(meetingId, userId);

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


        public async Task<IActionResult> SubscribeMeeting(int meetingId, string returnUrl)
        {
            var userId = _userManager.GetUserId(User);
            var result = await _meetingManager.SubscribeMeetingAsync(meetingId, userId);

            if (result == OperationResult.Failed)
            {
                return Content("o_O ... Something happens ...");
            }

            return PartialView("~/Views/Meeting/_UserSubscriptionPartial.cshtml", meetingId);
        }



        public async Task<IActionResult> UnsubscribeMeeting(int meetingId)
        {
            var userId = _userManager.GetUserId(User);
            bool subscribed = await _meetingQuery.IsUserSubscribedMeeting(meetingId, userId);

            if (!subscribed)
            {
                return Content("WTF");
            }

            var result = await _meetingManager.UnsubscribeMeetingAsync(meetingId, userId);

            if (result == OperationResult.Failed)
            {
                return Content("o_O ... Something happens ...");
            }

            return PartialView("~/Views/Meeting/_UserSubscriptionPartial.cshtml", meetingId);
        }


        /* !!!! OWNER CONFIRMATION REQUIRED ~!!!! */
        [HttpGet]
        public async Task<IActionResult> DeleteMeeting(int meetingId)
        {
            var userId = _userManager.GetUserId(User);

            if (await _meetingQuery.CheckMeetingOwner(meetingId, userId) == false)
            {
                return Content("You are not meeting owner");
            }

            var meeting = await _meetingQuery.GetMeetingByIdAsync(meetingId);
            var result = await _meetingManager.RemoveAsync(meeting);

            if (result == OperationResult.Failed)
            {
                return Content("o_O ... Something happens ...");
            }

            return RedirectPermanent(Url.Action("Index", "Home"));
        }
    }
}