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
    public class MeetupsController : Controller
    {
        private DiceItUpEntities4 db = new DiceItUpEntities4();

        // GET: Meetups
        public ActionResult Index()
        {
            var meetups = db.Meetups.Include(m => m.Game).Include(m => m.Genre).Include(m => m.Location).Include(m => m.PlayerLogin);
            return View(meetups.ToList());
        }

        // GET: Meetups/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Meetup meetup = db.Meetups.Find(id);
            if (meetup == null)
            {
                return HttpNotFound();
            }
            return View(meetup);
        }

        // GET: Meetups/Create
        public ActionResult Create()
        {
            ViewBag.game_id = new SelectList(db.Games, "game_id", "game_title");
            ViewBag.genre_id = new SelectList(db.Genres, "genre_id", "game_genre");
            ViewBag.location_id = new SelectList(db.Locations, "location_id", "zip_code");
            ViewBag.player_id = new SelectList(db.PlayerLogins, "player_id", "email");
            return View();
        }

        // POST: Meetups/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "meetup_id,location_id,meetup_date,start_time,end_time,meetup_access,meetup_state,game_id,genre_id,meetup_note,player_id")] Meetup meetup)
        {
            if (ModelState.IsValid)
            {
                db.Meetups.Add(meetup);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.game_id = new SelectList(db.Games, "game_id", "game_title", meetup.game_id);
            ViewBag.genre_id = new SelectList(db.Genres, "genre_id", "game_genre", meetup.genre_id);
            ViewBag.location_id = new SelectList(db.Locations, "location_id", "zip_code", meetup.location_id);
            ViewBag.player_id = new SelectList(db.PlayerLogins, "player_id", "email", meetup.player_id);
            return View(meetup);
        }

        // GET: Meetups/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Meetup meetup = db.Meetups.Find(id);
            if (meetup == null)
            {
                return HttpNotFound();
            }
            ViewBag.game_id = new SelectList(db.Games, "game_id", "game_title", meetup.game_id);
            ViewBag.genre_id = new SelectList(db.Genres, "genre_id", "game_genre", meetup.genre_id);
            ViewBag.location_id = new SelectList(db.Locations, "location_id", "zip_code", meetup.location_id);
            ViewBag.player_id = new SelectList(db.PlayerLogins, "player_id", "email", meetup.player_id);
            return View(meetup);
        }

        // POST: Meetups/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "meetup_id,location_id,meetup_date,start_time,end_time,meetup_access,meetup_state,game_id,genre_id,meetup_note,player_id")] Meetup meetup)
        {
            if (ModelState.IsValid)
            {
                db.Entry(meetup).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.game_id = new SelectList(db.Games, "game_id", "game_title", meetup.game_id);
            ViewBag.genre_id = new SelectList(db.Genres, "genre_id", "game_genre", meetup.genre_id);
            ViewBag.location_id = new SelectList(db.Locations, "location_id", "zip_code", meetup.location_id);
            ViewBag.player_id = new SelectList(db.PlayerLogins, "player_id", "email", meetup.player_id);
            return View(meetup);
        }

        // GET: Meetups/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Meetup meetup = db.Meetups.Find(id);
            if (meetup == null)
            {
                return HttpNotFound();
            }
            return View(meetup);
        }

        // POST: Meetups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Meetup meetup = db.Meetups.Find(id);
            db.Meetups.Remove(meetup);
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
