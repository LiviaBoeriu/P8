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

        [HttpGet]
        public ActionResult Create()
        {
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


                //Save to database
                #region Save to Database
                using (DiceItUpEntities2 dc = new DiceItUpEntities2())
                {
                    dc.PlayerLogins.Add(playerLogin);
                    dc.SaveChanges();

                }
                #endregion
            }
            else
            {
                message = "Ugyldig forespørgsel";
            }

            ViewBag.Message = message;
            ViewBag.Status = Status;
            return View(playerLogin);
        }

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
        [HttpPost]
        [ValidateAntiForgeryToken]
        protected void btnsubmit_Click(object sender, EventArgs e)
        {
            #region Save to Database

            string UserName = txtuser.Text;
            string Password = txtpwd.Text;
            string ConfirmPassword = txtcnmpwd.Text;
            string FirstName = lblfn.Text;
            string LastName = txtlname.Text;
            string Email = txtEmail.Text;
            string Phoneno = txtphone.Text;
            string Location = txtlocation.Text;
            string Created_By = txtuser.Text;

            var con = ConfigurationManager.ConnectionStrings["DiceItUpEntities2"].ToString();
            using (SqlConnection dc = new SqlConnection(con))
            {
                dc.Open();
                SqlCommand cmd = new SqlCommand("PlayerLogin", dc);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@first_name", FirstName);
                cmd.Parameters.AddWithValue("@last_name", LastName);
                cmd.Parameters.AddWithValue("@UserName", UserName);
                cmd.Parameters.AddWithValue("@email", Email);
                cmd.Parameters.AddWithValue("@password", Password);

                cmd.Parameters.AddWithValue("@PhoneNo", Phoneno);
                cmd.Parameters.AddWithValue("@Location", Location);
                cmd.Parameters.AddWithValue("@Created_By", Created_By);
                cmd.Parameters.Add("@ERROR", SqlDbType.Char, 500);
                cmd.Parameters["@ERROR"].Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                message = (string)cmd.Parameters["@ERROR"].Value;

                dc.Close();
        }

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
}

