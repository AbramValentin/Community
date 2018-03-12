using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Community.ViewModels.MeetingViewModels
{
    public class MeetingSubscriberViewModel
    {
        public string UserId { get; set; }
        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }
        public string UserPhotoPath { get; set; }

        public int MeetingId { get; set; }
        public string MeetingName { get; set; }
        public string MeetingPhotoPath { get; set; }
    }
}
