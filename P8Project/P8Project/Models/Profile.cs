using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace P8Project.Models
{
    public class Profile
    {
        public int PlayerId { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PlayerLocation { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        public int ProfileLevel { get; set; }
        public string ProfileTitle { get; set; }
        public string Description { get; set; }
        public string[] TopGames { get; set; }
        public string[] FriendList { get; set; }
        public string[] Preferences { get; set; }

        //NOTE: ratings for profile
    }

}