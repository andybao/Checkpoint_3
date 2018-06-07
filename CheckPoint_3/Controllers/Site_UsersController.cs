using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CheckPoint_3.Models;

namespace CheckPoint_3.Controllers
{
    [Authorize(Roles = "admin")]
    public class Site_UsersController : Controller
    {
        private CheckPointContext db = new CheckPointContext();

        // GET: Site_Users
        public ActionResult Index()
        {
            var site_Users = db.Site_Users.Include(s => s.Site_Roles);
            return View(site_Users.ToList());
        }

        // GET: Site_Users/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Site_Users site_Users = db.Site_Users.Find(id);
            if (site_Users == null)
            {
                return HttpNotFound();
            }
            return View(site_Users);
        }

        // GET: Site_Users/Create
        public ActionResult Create()
        {
            ViewBag.User_Role_Code = new SelectList(db.Site_Roles, "Role_Code", "Role_Name");
            return View();
        }

        // POST: Site_Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,First_Name,Last_Name,Email,Passward,User_Role_Code,New_Msg_Id")] Site_Users site_Users)
        {
            if (ModelState.IsValid)
            {
                db.Site_Users.Add(site_Users);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.User_Role_Code = new SelectList(db.Site_Roles, "Role_Code", "Role_Name", site_Users.User_Role_Code);
            return View(site_Users);
        }

        // GET: Site_Users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Site_Users site_Users = db.Site_Users.Find(id);
            if (site_Users == null)
            {
                return HttpNotFound();
            }
            ViewBag.User_Role_Code = new SelectList(db.Site_Roles, "Role_Code", "Role_Name", site_Users.User_Role_Code);
            return View(site_Users);
        }

        // POST: Site_Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,First_Name,Last_Name,Email,Passward,User_Role_Code,New_Msg_Id")] Site_Users site_Users)
        {
            if (ModelState.IsValid)
            {
                db.Entry(site_Users).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.User_Role_Code = new SelectList(db.Site_Roles, "Role_Code", "Role_Name", site_Users.User_Role_Code);
            return View(site_Users);
        }

        // GET: Site_Users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Site_Users site_Users = db.Site_Users.Find(id);
            if (site_Users == null)
            {
                return HttpNotFound();
            }
            return View(site_Users);
        }

        // POST: Site_Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Site_Users site_Users = db.Site_Users.Find(id);
            db.Site_Users.Remove(site_Users);
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
