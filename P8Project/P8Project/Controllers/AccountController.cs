using P8Project.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using P8Project.ViewModel;

namespace P8Project.Controllers
{
    

    public class AccountController : Controller
    {
        private readonly DiceItUpEntities4 db = new DiceItUpEntities4();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }
        
        // POST: account/login
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login([Bind(Include = "email , password")] PlayerLogin playerLogin)
        {
            if (ModelState.IsValid)
            {
                var userLogin = db.PlayerLogins.FirstOrDefault(row => row.email == playerLogin.email && row.password == playerLogin.password);
                if (userLogin != null) {
                    var userProfile = db.PlayerProfiles.FirstOrDefault(row => row.player_id == userLogin.player_id);
                    

                    return RedirectToAction("Profile", userProfile);
                }
                return View();
            }
           return View();
        }

        public ActionResult Profile(PlayerProfile profile)
        {
            ViewData["ProfileTitle"] = db.ProfileTitles.FirstOrDefault(row => row.profile_level == profile.profile_level).title;
            ViewData["ProfileLocation"] = db.Locations.FirstOrDefault(row => row.location_id == profile.location_id).city;

            return View(profile);
        }


        // GET: Account/Register
        public ActionResult Register(PlayerProfile playerProfile)
        {
            if (playerProfile.player_id == 0) {
                return View();
            }

            return View("Create");
        }

        //POST: Account/Register
        // To protect from overposting attacks, please enable the specific properties you want to bind to
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register([Bind(Include = "Email, Password, FirstName, LastName")]RegisterProfile registerProfile)
        {
            if (ModelState.IsValid)
            {

                PlayerProfile profile = new PlayerProfile();
                PlayerLogin login = new PlayerLogin();

                var userRegister = db.PlayerLogins.FirstOrDefault(row => row.email == registerProfile.Email && row.password == registerProfile.Password);

                if (userRegister == null)
                {
                    //assign the player id that is registered to our object
                    login.email = registerProfile.Email;
                    login.password = registerProfile.Password;
                    profile.first_name = registerProfile.FirstName;
                    profile.last_name = registerProfile.LastName;
                }

                if (login != null)
                {
                    db.PlayerLogins.Add(login);
                    db.SaveChanges();

                    profile.player_id = login.player_id;

                    db.PlayerProfiles.Add(profile);
                    db.SaveChanges();
                    return RedirectToAction("Register", "Account", registerProfile);
                }

            }
            return View();
        }
    }

}
