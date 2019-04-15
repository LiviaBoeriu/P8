using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using P8Project.Models;

namespace P8Project.Controllers
{
    public class PlayerLoginsController : Controller
    {
        private DiceItUpEntities2 db = new DiceItUpEntities2();

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        //User login
        public ActionResult Profile(PlayerProfile profile)
        {
            ViewData["ProfileTitle"] = db.ProfileTitles.FirstOrDefault(row => row.profile_level == profile.profile_level).title;
            return View(profile);
        }
        // hello kitten
        //check if strings are empty

        // POST: account/login
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LoginButtonClick ([Bind(Include = "email , password")] PlayerLogin playerLogin)
        {
            Console.WriteLine("ib");

            if (ModelState.IsValid)
            {
                var userLogin = db.PlayerLogins.FirstOrDefault(row => row.Email == playerLogin.Email && row.Password == playerLogin.Password);
                if (userLogin != null)
                {
                    var userProfile = db.PlayerProfiles.FirstOrDefault(row => row.player_id == userLogin.Player_ID);

                    return RedirectToAction("Profile", userProfile);
                }
                return View();
            }
            return View();
        }


        //Registration of user
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Registration([Bind(Exclude = "IsEmailVerified,ActivationCode")] PlayerLogin playerLogin)
        {
            bool Status = false;
            string message = "";

            //Model Validation 
            if (ModelState.IsValid)
            {

                //Check if email already exists in the database 
                var isExist = IsEmailExist(playerLogin.Email);
                if (isExist)
                {
                    ModelState.AddModelError("EmailExist", "Mailadressen eksisterer allerede");
                    return View(playerLogin);
                }

                if (playerLogin.Password != playerLogin.ConfirmPassword)
                {
                    ModelState.AddModelError("PasswordsNequal", "Adgangskoderne stemmer ikke overens");
                    return View(playerLogin);

                }

                //From meetup (could be used to save to databse)
                //if (ModelState.IsValid)
                //{
                //    db.Meetups.Add(meetup);
                //    db.SaveChanges();
                //    return RedirectToAction("Index");
                //}


                ////Save to database (attempt to save user input to database)
                //#region
                //using (DiceItUpEntities2 dc = new DiceItUpEntities2())
                //{

                //    //var dc = Database.Open("DiceItUpEntities2");
                //    var insertCommand = "INSERT INTO PlayerLogin (email, password) VALUES (@0, @1)";
                //    dc.Execute(insertCommand, Email, Password);
                //    Response.Redirect("~/PlayerLogin");


                //    dc.PlayerProfiles.Add();
                //    dc.SaveChanges();

                //}
                //#endregion
            }
            else
                {
                message = "Ugyldig forespørgsel";
            }

            ViewBag.Message = message;
            ViewBag.Status = Status;
            return View(playerLogin);
        }

        //Check if email exists
        [NonAction]
        public bool IsEmailExist(string email)
        {
            using (DiceItUpEntities2 dc = new DiceItUpEntities2())
            {
                var v = dc.PlayerLogins.Where(a => a.Email == email).FirstOrDefault();
                return v != null;
            }
        }


        //Save fist name to database
        protected void btnsubmit_Click(object sender, EventArgs e)
        {
            //if (txt_pwd.Text == txt_cnmpwd.Text)
            //{
            //    #region Save to Database

            //    string FirstName = txt_fn.Text;
            //    string LastName = txt_ln.Text;
            //    string Email = txt_email.Text;
            //    string Password = txt_pwd.Text;
            //    string ConfirmPassword = txt_cnmpwd.Text;
            //    string DateOfBirth = dte_pckr.Text;
            //    int Gender = rbl_gndr.Text


            //    var con = ConfigurationManager.ConnectionStrings["DiceItUpEntities2"].ToString();
            //    using (SqlConnection dc = new SqlConnection(con))
                
            //        dc.Open();
            //        SqlCommand cmd = new SqlCommand("PlayerLogin", dc);
            //        cmd.CommandType = CommandType.StoredProcedure;
            //        cmd.Parameters.AddWithValue("@first_name", FirstName);
            //        cmd.Parameters.AddWithValue("@last_name", LastName);
            //        cmd.Parameters.AddWithValue("@email", Email);
            //        cmd.Parameters.AddWithValue("@password", Password);
            //        cmd.Parameters.AddWithValue("@confirm_password", ConfirmPassword);
            //        cmd.Parameters.AddWithValue("@date_of_birth", DateOfBirth);
            //        cmd.Parameters.AddWithValue("@gender", Gender);
            //        cmd.Parameters.Add("@ERROR", SqlDbType.Char, 500);
            //        cmd.Parameters["@ERROR"].Direction = ParameterDirection.Output;
            //        cmd.ExecuteNonQuery();
            //        message = (string)cmd.Parameters["@ERROR"].Value;

            //        dc.Close();
                
            //    #endregion
            //}

            //    else
            //    {
            //        Page.RegisterStartupScript("UserMsg", "<Script language='javascript'>alert('" + "Password mismatch" + "');</script>");
            //    }
            //    lblErrorMsg.Text = message;
            //}

            //var con = ConfigurationManager.ConnectionStrings["DiceItUpEntities2"].ToString();
            //using (SqlConnection dc = new SqlConnection(con))
            //{
            //    string FirstName = "INSERT INTO PlayerProfile WHERE FirstName = @first_name;";
            //    SqlCommand fnCmd = new SqlCommand(FirstName, dc);
            //    fnCmd.Parameters.AddWithValue("@first_name", FirstName);
            //    dc.Open();
            //    using (SqlDataReader oReader = fnCmd.ExecuteReader())
            //    {
            //        while (oReader.Read())
            //        {
            //            matchingPerson.firstName = oReader["FirstName"].ToString();
            //            matchingPerson.lastName = oReader["LastName"].ToString();
            //        }

            //        dc.Close();
            //    }
            //}
            //return matchingPerson;
        }

    }
}

//    private DiceItUpEntities2 db = new DiceItUpEntities2();

//    // GET: PlayerLogins
//    public ActionResult Index()
//    {
//        return View(db.PlayerLogins.ToList());
//    }

//    // GET: PlayerLogins/Details/5
//    public ActionResult Details(int? id)
//    {
//        if (id == null)
//        {
//            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//        }
//        PlayerLogin playerLogin = db.PlayerLogins.Find(id);
//        if (playerLogin == null)
//        {
//            return HttpNotFound();
//        }
//        return View(playerLogin);
//    }

//    // GET: PlayerLogins/Create
//    public ActionResult Create()
//    {
//        return View();
//    }

//    // POST: PlayerLogins/Create
//    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
//    // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
//    [HttpPost]
//    [ValidateAntiForgeryToken]
//    public ActionResult Create([Bind(Include = "player_id,email,password")] PlayerLogin playerLogin)
//    {
//        if (ModelState.IsValid)
//        {
//            db.PlayerLogins.Add(playerLogin);
//            db.SaveChanges();
//            return RedirectToAction("Index");
//        }

//        return View(playerLogin);
//    }

//    // GET: PlayerLogins/Edit/5
//    public ActionResult Edit(int? id)
//    {
//        if (id == null)
//        {
//            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//        }
//        PlayerLogin playerLogin = db.PlayerLogins.Find(id);
//        if (playerLogin == null)
//        {
//            return HttpNotFound();
//        }
//        return View(playerLogin);
//    }

//    // POST: PlayerLogins/Edit/5
//    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
//    // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
//    [HttpPost]
//    [ValidateAntiForgeryToken]
//    public ActionResult Edit([Bind(Include = "player_id,email,password")] PlayerLogin playerLogin)
//    {
//        if (ModelState.IsValid)
//        {
//            db.Entry(playerLogin).State = EntityState.Modified;
//            db.SaveChanges();
//            return RedirectToAction("Index");
//        }
//        return View(playerLogin);
//    }

//    // GET: PlayerLogins/Delete/5
//    public ActionResult Delete(int? id)
//    {
//        if (id == null)
//        {
//            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//        }
//        PlayerLogin playerLogin = db.PlayerLogins.Find(id);
//        if (playerLogin == null)
//        {
//            return HttpNotFound();
//        }
//        return View(playerLogin);
//    }

//    // POST: PlayerLogins/Delete/5
//    [HttpPost, ActionName("Delete")]
//    [ValidateAntiForgeryToken]
//    public ActionResult DeleteConfirmed(int id)
//    {
//        PlayerLogin playerLogin = db.PlayerLogins.Find(id);
//        db.PlayerLogins.Remove(playerLogin);
//        db.SaveChanges();
//        return RedirectToAction("Index");
//    }

//    protected override void Dispose(bool disposing)
//    {
//        if (disposing)
//        {
//            db.Dispose();
//        }
//        base.Dispose(disposing);
//    }
//}

