using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CustomLogin.Models;

namespace CustomLogin.Controllers
{
    public class usersController : Controller
    {
        private deviceManagementEntities1 db = new deviceManagementEntities1();

        // GET: users
        public ActionResult Index()
        {
            if (Session["userID"] != null && Session["isAdmin"].Equals(true))
            {
                var users = db.users.Where(u => u.userName != "admin");
                return View(users.ToList());
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        // GET: users/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["userID"] != null && Session["isAdmin"].Equals(true))
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                user user = db.users.Find(id);
                if (user == null)
                {
                    return HttpNotFound();
                }
                return View(user);
            } else
            {
                return RedirectToAction("Index","Login");
            }
        }
        

        // GET: users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["userID"] != null && Session["isAdmin"].Equals(true))
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                user user = db.users.Find(id);
                if (user == null)
                {
                    return HttpNotFound();
                }
                ViewBag.DeviceID = new SelectList(db.devices, "DeviceID", "DName", user.DeviceID);
                return View(user);
            } else
            {
                return RedirectToAction("Index","Login");
            }
        }

        // POST: users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "userID,userName,password,email,isAdmin,Location,DeviceID")] user user)
        {
            if (Session["userID"] != null && Session["isAdmin"].Equals(true))
            {
                if (ModelState.IsValid)
                {
                    db.Entry(user).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                ViewBag.DeviceID = new SelectList(db.devices, "DeviceID", "DName", user.DeviceID);
                return View(user);
            } else
            {
                return RedirectToAction("Index","Login");
            }
        }

        // GET: users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["userID"] != null && Session["isAdmin"].Equals(true))
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                user user = db.users.Find(id);
                if (user == null)
                {
                    return HttpNotFound();
                }
                return View(user);
            } else
            {
                return RedirectToAction("Index","Login");
            }
        }

        // POST: users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Session["userID"] != null && Session["isAdmin"].Equals(true))
            {
                user user = db.users.Find(id);
                db.users.Remove(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            } else
            {
                return RedirectToAction("Index","Login");
            }
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
