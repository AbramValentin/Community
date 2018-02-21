using Community.Data.Tables;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Community.Data
{
    public class User : IdentityUser
    {
        [DataType(DataType.Date)]
        public DateTime RegistrationDate{ get; set; }

        [DataType(DataType.Date)]
        public DateTime BirthDay { get; set; }

        public string PhotoPath { get; set; }

        public ICollection<UserMeetings> UserMeetings { get; set; }

        public ICollection<Meeting> Meeting { get; set; }
    }
}
