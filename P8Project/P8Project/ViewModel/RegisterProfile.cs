using System.Linq;
using System.Web;


namespace P8Project.ViewModel
{
    using System;
    using System.Collections.Generic;

    public class RegisterProfile
    {
        public string FirstName { get; set; } //first name player
        public string Email { get; set; } //login   
        public string Password { get; set; } //login
        public int PlayerId { get; set; } //login
        public string LastName { get; set; } //last name profile

    }
}