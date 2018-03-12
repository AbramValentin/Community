﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Community.ViewModels.MeetingViewModels
{
    public class MeetingGeneralInfoViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string CategoryName { get; set; }

        public string CityName { get; set; }

        public int ConfirmedUsersRequests { get; set; }

        public string ShortDateString { get; set; }

        public string Description { get; set; }

        public string PhotoPath { get; set; }
    }
}
