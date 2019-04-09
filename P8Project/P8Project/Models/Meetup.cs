//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace P8Project.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Meetup
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Meetup()
        {
            this.MeetupParticipants = new HashSet<MeetupParticipant>();
        }
    
        public int meetup_id { get; set; }
        public Nullable<int> location_id { get; set; }
        public Nullable<System.DateTime> meetup_date { get; set; }
        public Nullable<System.TimeSpan> start_time { get; set; }
        public Nullable<System.TimeSpan> end_time { get; set; }
        public string meetup_access { get; set; }
        public string meetup_state { get; set; }
        public Nullable<int> game_id { get; set; }
        public Nullable<int> genre_id { get; set; }
        public string meetup_note { get; set; }
        public Nullable<int> player_id { get; set; }
    
        public virtual Game Game { get; set; }
        public virtual Genre Genre { get; set; }
        public virtual Location Location { get; set; }
        public virtual PlayerLogin PlayerLogin { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MeetupParticipant> MeetupParticipants { get; set; }
    }
}