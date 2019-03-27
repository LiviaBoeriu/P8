using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace P8Project.Models
{
    public class Match
    {
        public int MatchId { get; set; }
        public int FirstPlayerId { get; set; }
        public int SecondPlayerId { get; set; }
        public string MatchState { get; set; } //enum type
    }
}