using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Community.ModelViews.MeetingViewModels
{
    public class MeetingIconDetailViewModel
    {
        public int Id { get; set; }

        public string ImgPath { get; set; }

        public string Name { get; set; }

        public int UsersJoined { get; set; }
    }
}
