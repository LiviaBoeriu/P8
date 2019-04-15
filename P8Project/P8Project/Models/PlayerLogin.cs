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
    using System.Linq;
    using System.Web;
    using System.ComponentModel.DataAnnotations;

    public partial class PlayerLogin
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PlayerLogin()
        {
            this.Matches = new HashSet<Match>();
            this.Meetups = new HashSet<Meetup>();
            this.MeetupParticipants = new HashSet<MeetupParticipant>();
            this.PlayerProfiles = new HashSet<PlayerProfile>();
            this.PlayerRelationships = new HashSet<PlayerRelationship>();
            this.PlayerRelationships1 = new HashSet<PlayerRelationship>();
            this.Genres = new HashSet<Genre>();
            this.Games = new HashSet<Game>();
        }
    
 
        public int Player_ID { get; set; }

        [Display(Name = "Fornavn")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Fornavn skal udfyldes")]
        public string FirstName { get; set; }

        [Display(Name = "Efternavn")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Efternavn skal udfyldes")]
        public string LastName { get; set; }

        [Display(Name = "Email")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Email skal udfyldes")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name = "F�dselsdato")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "F�dselsdato skal udfyldes")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime DateOfBirth { get; set; }

        [Display(Name = "K�n")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "K�n skal udfyldes")]
        public char Gender { get; set; }

        [Display(Name = "Adgangskode")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Adgangskode skal udfyldes")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "Minimum 6 bogstaver er kr�vet")]
        public string Password { get; set; }

        [Display(Name = "Gentag adgangskode")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "De indtastede adgangskoder stemmer ikke overens")]
        public string ConfirmPassword { get; set; }

    


    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Match> Matches { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Meetup> Meetups { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MeetupParticipant> MeetupParticipants { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PlayerProfile> PlayerProfiles { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PlayerRelationship> PlayerRelationships { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PlayerRelationship> PlayerRelationships1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Genre> Genres { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Game> Games { get; set; }
    }
}
