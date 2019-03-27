using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace P8Project.Controllers
{
    public class ProfileController : Controller
    {
        // GET: Player
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult DeletePlayer(int id)
        {

            return View();
        }


    }
}