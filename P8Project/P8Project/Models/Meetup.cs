using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace P8Project.Models
{
    //Maybe we'll change the name
    public class Meetup
    {
        public int MeetupId { get; set; }
        public DateTime MeetupDate { get; set; }
        public DateTime MeetupStartTime { get; set; }
        public DateTime MeetupEndTime { get; set; }
        public string MeetupLocation { get; set; }
        public string MeetupGame { get; set; }
        // public MeetupOwner MeetupOwner 
        public string[] ParticipantList { get; set; }
        public string MeetupState { get; set; } //enum type
        public string MeetupAccess { get; set; } //enum type
        public string MeetuoNotes { get; set; }
    }
}