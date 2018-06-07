using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CheckPoint_3.Models;
using System.Web.Security;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Core;
using System.Data.SqlClient;

namespace CheckPoint_3.Controllers
{
    public class HomeController : Controller
    {
        CheckPointContext db = new CheckPointContext();

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(Site_Users user)
        {
            //Return the number of rows returned from the database (should be 1)

            int count = db.Site_Users.Where(
                u => u.Email == user.Email
                &&
                u.Passward == user.Passward).Count();

            if (count == 1)
            {
                //set the authcookie with your username or any other value. This username is also being used to determine your user role.
                FormsAuthentication.SetAuthCookie(user.Email, false);
                Site_Users u = db.Site_Users.SingleOrDefault(su => su.Email == user.Email);

                if (user.Email == "admin@gmail.com")
                {
                    return RedirectToAction("AdminHomePage", new { id = u.Id });
                } else
                {
                    return RedirectToAction("UserHomePage", new { id = u.Id });
                }
            }

            ViewBag.Message = "Invalid username and/or password";
            return View(user);
        }

        [Authorize(Roles = "User")]
        public ActionResult UserHomePage(int id)
        {
            MessageAndUser messageAndUser = new MessageAndUser();

            messageAndUser.user = db.Site_Users.SingleOrDefault(su => su.Id == id);
            messageAndUser.users = db.Site_Users.ToList();
            messageAndUser.messages = db.Msgs.ToList();

            return View(messageAndUser);
        }

        [Authorize(Roles = "admin")]
        public ActionResult AdminHomePage(int id)
        {
            MessageAndUser messageAndUser = new MessageAndUser();

            messageAndUser.user = db.Site_Users.SingleOrDefault(su => su.Id == id);
            messageAndUser.users = db.Site_Users.ToList();
            messageAndUser.messages = db.Msgs.ToList();

            return View(messageAndUser);

        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Will attempt to retrieve a row from the City table and pass that result to the view. 
        /// </summary>
        /// <param name="form">A value from Name = 'country_name' in the view</param>
        /// <returns>A partial view typed to a single city result</returns>
        [HttpPost]
        public PartialViewResult Users_DDL(FormCollection form)
        {
            //Set up country instance
            Site_Users user = new Site_Users();

            //store the result from the form with Name="Country"
            //string user_id = form["user"];
            try
            {
                int user_id = Convert.ToInt32(form["user"]);
                user = db.Site_Users.SingleOrDefault(c => c.Id == user_id);

            }
            catch (Exception genericException)
            {
                //Catch possible errors from interacting with the database
                ViewBag.ExceptionMessage = genericException.Message;
                return PartialView("~/Views/Errors/_Partial_Error.cshtml");
            }

            //Render the PartialView with the country object. 
            return PartialView("~/Views/Site_Users/_User.cshtml", user);
        }

        public ActionResult Admin_Users()
        {
            try
            {
                var users = db.Site_Users;
                return View(users.ToList());
            }
            catch (DbUpdateException dbException)
            {
                ViewBag.DbExceptionMessage = dbException.Message;
            }
            catch (EntityException entityException)
            {
                //InnerException.Message gives a more detailed message, a message that I wouldn't show on a webpage
                ViewBag.EntityExceptionMessage = entityException.InnerException.Message;

                //This one also gives a message but very vague. 
                //ViewBag.EntityExceptionMessage = entityException.Message;
            }
            catch (SqlException sqlException)
            {
                ViewBag.SqlExceptionNumber = sqlException.Number;
                ViewBag.SqlExceptionMessage = sqlException.Message;
            }
            catch (Exception genericException)
            {
                ViewBag.ExceptionMessage = genericException.Message;
            }

            //If everything goes well, then the return line below won't get reached. 
            return View("~/Views/Errors/Details.cshtml");
        }
    }
}