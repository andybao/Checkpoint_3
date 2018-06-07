using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CheckPoint_3.Models;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Core;
using System.Data.SqlClient;

namespace CheckPoint_3.Controllers
{
    [Authorize(Roles = "admin, User")]
    public class MsgsController : Controller
    {
        private CheckPointContext db = new CheckPointContext();

        // GET: Msgs
        [Authorize(Roles = "admin")]
        public ActionResult Index()
        {
            try
            {
                MessageAndUser messageAndUser = new MessageAndUser();
                messageAndUser.messages = db.Msgs.ToList();
                messageAndUser.users = db.Site_Users.ToList();
                return View(messageAndUser);
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

        // GET: Msgs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Msgs msgs = db.Msgs.Find(id);
            if (msgs == null)
            {
                return HttpNotFound();
            }
            return View(msgs);
        }

        // GET: Msgs/Create
        [Authorize(Roles = "User")]
        public ActionResult Create(int id)
        {

            try
            {
                int msgId = db.Msgs.Count();
                //var sql = "select count(*) from msgs";
                //int msgId = db.Database.SqlQuery<int>(sql).Single();
                Msgs msg = new Msgs();
                msg.Id = msgId;
                msg.Sender_User_Id = id;
                msg.Pre_Msg_Id = -1;

                List<Site_Users> userList = db.Site_Users.ToList();
                List<Site_Users> ul = new List<Site_Users>();

                if (id == 0)
                {
                    ul.Add(userList[1]);
                    ul.Add(userList[2]);
                }
                else if (id == 1)
                {
                    ul.Add(userList[0]);
                    ul.Add(userList[2]);
                } 
                else if (id == 2)
                {
                    ul.Add(userList[0]);
                    ul.Add(userList[1]);
                }

                MessageAndUser messageAndUser = new MessageAndUser();
                //messageAndUser.users = userList;
                messageAndUser.users = ul;
                messageAndUser.message = msg;


                return View(messageAndUser);
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
        
        // POST: Msgs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "User")]
        public ActionResult Create(FormCollection form)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int id = Convert.ToInt32(form["message.Id"]);
                    int sender_user_id = Convert.ToInt32(form["message.Sender_User_Id"]);
                    int pre_msg_id = Convert.ToInt32(form["message.Pre_Msg_Id"]);
                    //int receiver_user_id = Convert.ToInt32(form["message.Receiver_User_Id"]);
                    int receiver_user_id = Convert.ToInt32(form["user"]);

                    string msg = Convert.ToString(form["message.Msg"]);

                    Msgs message = new Msgs();
                    message.Id = id;
                    message.Msg = msg;
                    message.Pre_Msg_Id = pre_msg_id;
                    message.Receiver_User_Id = receiver_user_id;
                    message.Sender_User_Id = sender_user_id;

                    db.Msgs.Add(message);
                    db.SaveChanges();
                    return RedirectToAction("UserHomePage", "Home", new { id = sender_user_id });
                }
                return View();
            }
            catch (DbUpdateException dbException) //DbUpdateException catches exceptions during database manipulation errors
            {

                ViewBag.DbExceptionMessage = dbException.GetBaseException();
            }
            catch (Exception genericException)
            {
                ViewBag.ExceptionMessage = genericException;
            }

            return View("~/Views/Errors/Details.cshtml");

        }

        // GET: Msgs/Edit/5
        [Authorize(Roles = "admin")]
        public ActionResult Edit(int? id)
        {

            try
            {
                MessageAndUser messageAndUser = new MessageAndUser();
                messageAndUser.message = db.Msgs.Find(id);
                messageAndUser.users = db.Site_Users.ToList();
                return View(messageAndUser);
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

        // POST: Msgs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public ActionResult Edit(FormCollection form)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    int id = Convert.ToInt32(form["message.Id"]);
                    int sender_user_id = Convert.ToInt32(form["message.Sender_User_Id"]);
                    int pre_msg_id = Convert.ToInt32(form["message.Pre_Msg_Id"]);
                    int receiver_user_id = Convert.ToInt32(form["message.Receiver_User_Id"]);
                    //int receiver_user_id = Convert.ToInt32(form["user"]);

                    string msg = Convert.ToString(form["message.Msg"]);

                    Msgs message = new Msgs();
                    message.Id = id;
                    message.Msg = msg;
                    message.Pre_Msg_Id = pre_msg_id;
                    message.Receiver_User_Id = receiver_user_id;
                    message.Sender_User_Id = sender_user_id;

                    db.Entry(message).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View();
            }
            catch (DbUpdateException dbException) //DbUpdateException catches exceptions during database manipulation errors
            {

                ViewBag.DbExceptionMessage = dbException.GetBaseException();
            }
            catch (Exception genericException)
            {
                ViewBag.ExceptionMessage = genericException;
            }

            return View("~/Views/Errors/Details.cshtml");

        }

        // GET: Msgs/Delete/5
        [Authorize(Roles = "admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MessageAndUser messageAndUser = new MessageAndUser();
            messageAndUser.message = db.Msgs.Find(id);
            messageAndUser.users = db.Site_Users.ToList();
            if (messageAndUser.message == null)
            {
                return HttpNotFound();
            }
            return View(messageAndUser);
        }

        // POST: Msgs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            Msgs msgs = db.Msgs.Find(id);
            db.Msgs.Remove(msgs);
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
