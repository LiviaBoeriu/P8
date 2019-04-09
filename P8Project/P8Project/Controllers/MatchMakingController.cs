using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using P8Project.Models;

namespace P8Project.Controllers
{
    public class MatchMakingController : Controller
    {
        private DiceItUpEntities2 db = new DiceItUpEntities2();

        // GET: MatchMaking
        public ActionResult Index()
        {
            var playerProfiles = db.PlayerProfiles.Include(p => p.Location).Include(p => p.PlayerLogin).Include(p => p.ProfileTitle);
            return View(playerProfiles.ToList());

        }

        // GET: MatchMaking/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PlayerProfile playerProfile = db.PlayerProfiles.Find(id);
            if (playerProfile == null)
            {
                return HttpNotFound();
            }
            return View(playerProfile);
        }
        // POST: MatchMaking/FilterMatches/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult FilterMatches()
        {
            var playerProfiles = from p in db.PlayerProfiles select p;

            var filteredPlayerProfiles = playerProfiles.Where(p => p.player_id.Equals(1));

            return View(filteredPlayerProfiles);
        }


        // GET: MatchMaking/Create
        public ActionResult Create()
        {
            ViewBag.location_id = new SelectList(db.Locations, "location_id", "zip_code");
            ViewBag.player_id = new SelectList(db.PlayerLogins, "player_id", "email");
            ViewBag.profile_level = new SelectList(db.ProfileTitles, "profile_level", "title");
            return View();
        } 

        // POST: MatchMaking/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "player_id,first_name,last_name,date_of_birth,gender,location_id,profile_level,description,feedback")] PlayerProfile playerProfile)
        {
            if (ModelState.IsValid)
            {
                db.PlayerProfiles.Add(playerProfile);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.location_id = new SelectList(db.Locations, "location_id", "zip_code", playerProfile.location_id);
            ViewBag.player_id = new SelectList(db.PlayerLogins, "player_id", "email", playerProfile.player_id);
            ViewBag.profile_level = new SelectList(db.ProfileTitles, "profile_level", "title", playerProfile.profile_level);
            return View(playerProfile);
        }

        // GET: MatchMaking/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PlayerProfile playerProfile = db.PlayerProfiles.Find(id);
            if (playerProfile == null)
            {
                return HttpNotFound();
            }
            ViewBag.location_id = new SelectList(db.Locations, "location_id", "zip_code", playerProfile.location_id);
            ViewBag.player_id = new SelectList(db.PlayerLogins, "player_id", "email", playerProfile.player_id);
            ViewBag.profile_level = new SelectList(db.ProfileTitles, "profile_level", "title", playerProfile.profile_level);
            return View(playerProfile);
        }

        // POST: MatchMaking/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "player_id,first_name,last_name,date_of_birth,gender,location_id,profile_level,description,feedback")] PlayerProfile playerProfile)
        {
            if (ModelState.IsValid)
            {
                db.Entry(playerProfile).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.location_id = new SelectList(db.Locations, "location_id", "zip_code", playerProfile.location_id);
            ViewBag.player_id = new SelectList(db.PlayerLogins, "player_id", "email", playerProfile.player_id);
            ViewBag.profile_level = new SelectList(db.ProfileTitles, "profile_level", "title", playerProfile.profile_level);
            return View(playerProfile);
        }

        // GET: MatchMaking/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PlayerProfile playerProfile = db.PlayerProfiles.Find(id);
            if (playerProfile == null)
            {
                return HttpNotFound();
            }
            return View(playerProfile);
        }

        // POST: MatchMaking/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PlayerProfile playerProfile = db.PlayerProfiles.Find(id);
            db.PlayerProfiles.Remove(playerProfile);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
