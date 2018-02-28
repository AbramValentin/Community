using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Community.ModelViews.MeetingViewModels;
using Community.Data;
using Community.Data.Tables;

namespace Community.Controllers
{
    public class MeetingController : Controller
    {
        private readonly MeetingManager _meetingManager;

        public MeetingController(
            MeetingManager meetingManager
            )
        {
            _meetingManager = meetingManager;
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(MeetingCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var meeting = new Meeting
            {
                
            };

            _meetingManager.CreateMeeting(meeting);

            return View();
        }

        [HttpGet]
        public IActionResult Edit(string id)
        {
            // find meeting
            // send model to view
            // 

            var model = new MeetingEditViewModel
            {
                
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(MeetingEditViewModel model)
        {
            

            return null;
        }

        [HttpGet]
        public IActionResult Details(/*string id*/)
        {
            MeetingDetailViewModel model = new MeetingDetailViewModel
            {
                Id = "1",
                Name = "Ping-Pong Championship",
                Category = "Sport",
                CreatorId = "Null",
                CreatorName = "Boss",
                Location = "Volynska | Kivertsivskiy | Kivertsi",
                Date = DateTime.Now.ToShortDateString(),
                StartTime = DateTime.Now.ToShortTimeString(),
                UserJoined = new Random().Next(0, 1000),
                Description = "Funny game , sport and rest in one place. Join us !"
            };
            return View(model);
        }
    }
}