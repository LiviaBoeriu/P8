using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace P8Project.Controllers
{
    public class MeetupController : Controller
    {
        // GET: Meetup
        public ActionResult Index()
        {
            return View();
        }
    }
}