using P8Project.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace P8Project.Controllers
{
    

    public class AccountController : Controller
    {
        private readonly DiceItUpEntities2 db = new DiceItUpEntities2();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Profile(PlayerProfile profile)
        {
            ViewData["ProfileTitle"] = db.ProfileTitles.FirstOrDefault(row => row.profile_level == profile.profile_level).title;
            ViewData["ProfileLocation"] = db.Locations.FirstOrDefault(row => row.location_id == profile.location_id).city;

            return View(profile);
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


        // GET: Account/Register
        public ActionResult Register()
        {
            return View();
        }

        //POST: Account/Register
        // To protect from overposting attacks, please enable the specific properties you want to bind to
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register([Bind(Include = "email , password")] PlayerLogin playerLogin, [Bind(Include = "first_name, last_name")] PlayerProfile playerProfile)
        {
            if (ModelState.IsValid)
            {
                var userLogin = db.PlayerLogins.FirstOrDefault(row => row.email == playerLogin.email && row.password == playerLogin.password);
                if (userLogin == null)
                {
                    db.PlayerLogins.Add(playerLogin);
                    db.PlayerProfiles.Add(playerProfile);
                    db.SaveChanges();
                    return RedirectToAction("Login");
                }

            }
            return View();
        }

    }

}
