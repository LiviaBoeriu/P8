using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DiceItUp.Models;

namespace DiceItUp.Controllers
{
    public class PlayerProfilesController : Controller
    {
        private DiceItUpEntities1 db = new DiceItUpEntities1();

        // GET: PlayerProfiles
        public ActionResult Index()
        {
            var playerProfiles = db.PlayerProfiles.Include(p => p.Location).Include(p => p.PlayerLogin).Include(p => p.ProfileTitle);
            return View(playerProfiles.ToList());
        }

        // GET: PlayerProfiles/Details/5
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

            ViewBag.Matches = db.Matches.ToList().Where(row => row.first_player_id == id && row.match_state.ToLower() != "rejected");
            ViewBag.AcceptedMatches = db.Matches.ToList().Where(row => row.second_player_id == id && row.match_state.ToLower() == "accepted");
            ViewBag.Invitations = db.Matches.ToList().Where(row => row.second_player_id == id && row.match_state.ToLower() != "rejected" && row.match_state.ToLower() != "accepted");
            ViewData["Gender"] = playerProfile.gender.ToUpper() == "M" ? "Male" : "Female";
            return View(playerProfile);
        }

        // GET: PlayerProfiles/Create
        public ActionResult Create()
        {
            ViewBag.location_id = new SelectList(db.Locations, "location_id", "zip_code");
            ViewBag.player_id = new SelectList(db.PlayerLogins, "player_id", "email");
            ViewBag.profile_level = new SelectList(db.ProfileTitles, "profile_level", "title");
            return View();
        }

        // POST: PlayerProfiles/Create
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

        // GET: PlayerProfiles/Edit/5
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

            ViewBag.locations = db.Locations.ToList();
            return View(playerProfile);
        }

        // POST: PlayerProfiles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Edit([Bind(Include = "player_id,first_name,last_name,date_of_birth,gender,location_id,profile_level,description,feedback,avatar_path")] PlayerProfile playerProfile)
        {
            if (ModelState.IsValid)
            {
                db.Entry(playerProfile).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", new { id = playerProfile.player_id});
            }
            
            return View();
        }

        // GET: PlayerProfiles/Delete/5
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

        // POST: PlayerProfiles/Delete/5
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

        public ActionResult Matchmaking(int id)
        {
            PlayerProfile playerProfile = db.PlayerProfiles.Find(id);

            //ViewBag.Players = db.PlayerProfiles.ToList().Where(row => row.location_id == playerProfile.location_id && row.player_id != id);
            var players = db.PlayerProfiles.ToList().Where(row => row.location_id == playerProfile.location_id && row.player_id != id);
            var filteredPlayers = new List<PlayerProfile>();

            foreach ( var player in players ) {
                var match = db.Matches.ToList().Where(row => 
                    (row.first_player_id == id && row.second_player_id == player.player_id) || 
                    (row.first_player_id == player.player_id && row.second_player_id == id)
                );

                if (!match.Any())
                {
                    filteredPlayers.Add(player);
                }
            }

            ViewBag.Players = filteredPlayers;

            return View(playerProfile);
        }

        [HttpPost]
        public void Invite(int playerId, int opponentId) {
            int someId = opponentId;

            var match = new Match();
            match.first_player_id = playerId;
            match.second_player_id = opponentId;
            match.match_state = "Pending";
            match.first_player_state = "Accepted";
            match.second_player_state = "Pending";

            db.Matches.Add(match);
            db.SaveChanges();
        }

        [HttpPost]
        public void Respond(int playerId, int opponentId, string response) {
            var match = db.Matches.SingleOrDefault(row => row.first_player_id == opponentId && row.second_player_id == playerId);

            if (response == "Accepted" || response == "Rejected") {
                match.second_player_state = response;
                match.match_state = response;

                db.SaveChanges();
            }
        }
    }
}
