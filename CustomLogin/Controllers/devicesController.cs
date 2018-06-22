using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CustomLogin.Models;
using System.Data.SqlClient;
using System.Web.Services.Description;

namespace CustomLogin.Controllers
{
    public class devicesController : Controller
    {
        private deviceManagementEntities1 db = new deviceManagementEntities1();


        /* 
         Index function for non-admins: displays all devices assigned to user logged in in a list
         Index function for admins: displays all devices which are assigned to a user in a list
             */
        // GET: devices
        public ActionResult Index(string sortBy)
        {
            {
                if (Session["userID"] != null)
                {
                    int sessionUserID = (int)Session["userID"];

                    ViewBag.SortNameParameter = String.IsNullOrEmpty(sortBy) ? "Name desc" : "";
                    ViewBag.SortTypeParameter = sortBy == "Type" ? "Type desc" : "Type";
                    ViewBag.SortManufacturerParameter = sortBy == "Manufacturer" ? "Manufacturer desc" : "Manufacturer";
                    var devices = db.devices.AsQueryable();

                    if (db.devices.Any(x => x.UserID == sessionUserID))
                    {
                        devices = db.devices.Where(x => x.UserID == sessionUserID);
                        switch(sortBy)
                        {
                            case "Name desc":
                                devices = devices.OrderByDescending(x => x.DName);
                                break;
                            case "Type desc":
                                devices = devices.OrderByDescending(x => x.DType);
                                break;
                            case "Type":
                                devices = devices.OrderBy(x => x.DType);
                                break;
                            case "Manufacturer desc":
                                devices = devices.OrderByDescending(x => x.DManufacturer);
                                break;
                            case "Manufacturer":
                                devices = devices.OrderBy(x => x.DManufacturer);
                                break;
                            default:
                                devices = devices.OrderBy(x => x.DName);
                                break;
                        }
                        return View("Index", devices.ToList());
                    }
                    else
                    {
                        ViewBag.NoDevices = "You don't have any devices yet.";
                        devices = db.devices.Where(x => x.UserID == sessionUserID);
                        return View("Index", devices.ToList());
                    }
                }
                else
                {
                    return RedirectToAction("Index", "Login");
                }
            }
        }

        /* 
        deviceList function displays all the devices without an owner in a list.
            */
        public ActionResult deviceList(String sortBy)
        {
            if (Session["userID"] != null)
            {
                ViewBag.SortNameParameter = String.IsNullOrEmpty(sortBy) ? "Name desc" : "";
                ViewBag.SortTypeParameter = sortBy == "Type" ? "Type desc" : "Type";
                ViewBag.SortManufacturerParameter = sortBy == "Manufacturer" ? "Manufacturer desc" : "Manufacturer";
                var devices = db.devices.AsQueryable();

                if (devices.Any(x => x.UserID == null))
                {
                    devices = db.devices.Where(x => x.UserID == null);

                    switch (sortBy)
                    {
                        case "Name desc":
                            devices = devices.OrderByDescending(x => x.DName);
                            break;
                        case "Type desc":
                            devices = devices.OrderByDescending(x => x.DType);
                            break;
                        case "Type":
                            devices = devices.OrderBy(x => x.DType);
                            break;
                        case "Manufacturer desc":
                            devices = devices.OrderByDescending(x => x.DManufacturer);
                            break;
                        case "Manufacturer":
                            devices = devices.OrderBy(x => x.DManufacturer);
                            break;
                        default:
                            devices = devices.OrderBy(x => x.DName);
                            break;
                    }
                    return View(devices.ToList());
                }
                else
                {
                    ViewBag.NoDevices = "There are no devices in the marketplace yet.";
                    devices = db.devices.Where(x => x.UserID == null);
                    return View("DeviceList", devices.ToList());
                }
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        /*
         Details function: displays the details in a tabel for a specific device
              */
        // GET: devices/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["userID"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                device device = db.devices.Find(id);
                if (device == null)
                {
                    return HttpNotFound();
                }
                return View(device);
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        /* 
         Assign function will assign a specific device to the user logged in. (the devices userID will be the 
            logged in users id.)
             */

        public ActionResult Assign(int? id)
        {
            if (Session["userID"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                device device = db.devices.Find(id);
                if (device == null)
                {
                    return HttpNotFound();
                }
                return View(device);
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Assign(int? id, device device)
        {
            if (Session["userID"] != null)
            {
                if (ModelState.IsValid)
                {
                    db.Entry(device).State = EntityState.Modified;
                    var currentDevice = db.devices.Find(id);
                    int SessionUserID = (int)Session["userID"];
                    currentDevice.UserID = SessionUserID;
                    db.SaveChanges();
                    return RedirectToAction("deviceList");
                }
                return View(device);
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        /* 
         Unassign function will remove the user id from a specific device.
             */

        public ActionResult Unassign(int? id)
        {
            if (Session["userID"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                device device = db.devices.Find(id);
                if (device == null)
                {
                    return HttpNotFound();
                }
                return View(device);
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Unassign(int? id, device device)
        {
            if (Session["userID"] != null)
            {
                if (ModelState.IsValid)
                {
                    db.Entry(device).State = EntityState.Modified;
                    var currentDevice = db.devices.FirstOrDefault(x => x.UserID == device.UserID);
                    currentDevice.UserID = null;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(device);
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }


        /* 
         Create function is to create a new device. Works only for admin.
             */
        // GET: devices/Create
        public ActionResult Create()
        {
            if (Session["userID"] != null)
            {
                ViewBag.UserID = new SelectList(db.users, "userID", "userName");
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }


        /* 
         Create function will create a device with an empty user id (no owner).
             */
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DeviceID,DName,DManufacturer,DType,DOS,DOSVersion,DProcessor,DRAM,UserID")] device device)
        {
            if (Session["userID"] != null)
            {

                if (ModelState.IsValid)
                {
                    db.devices.Add(device);
                    db.SaveChanges();

                    ModelState.Clear();
                    ViewBag.DeviceCreateSuccess = "Device Registered Successfully.";
                    return RedirectToAction("deviceList", new device());
                }
                return View(device);
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        /* 
         Edit function is to edit a specific device. Works only for admin.
             */
        // GET: devices/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["userID"] != null)
            {
                if (Session["isAdmin"].Equals(true))
                {
                    if (id == null)
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    }
                    device device = db.devices.Find(id);
                    if (device == null)
                    {
                        return HttpNotFound();
                    }
                    ViewBag.UserID = new SelectList(db.users, "userID", "userName", Session["userID"].ToString());
                    return View(device);
                }
                else if (Session["userID"] != null)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return RedirectToAction("Index", "Login");
                }
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        // POST: devices/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DeviceID,DName,DManufacturer,DType,DOS,DOSVersion,DProcessor,DRAM,UserID")] device device)
        {
            if (Session["userID"] != null)
            {
                if (Session["isAdmin"].Equals(true))
                {
                    if (ModelState.IsValid)
                    {
                        db.Entry(device).State = EntityState.Modified;
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    ViewBag.UserID = new SelectList(db.users, "userID", "userName", device.UserID);
                    return View(device);
                }
                else if (Session["userID"] != null)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return RedirectToAction("Index", "Login");
                }
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        /* 
         Delete function is to delete a specific device. Works only for admin.
             */
        // GET: devices/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["userID"] != null)
            {
                if (Session["isAdmin"].Equals(true))
                {
                    if (id == null)
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    }
                    device device = db.devices.Find(id);
                    if (device == null)
                    {
                        return HttpNotFound();
                    }
                    return View(device);
                }
                else if (Session["userID"] != null)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return RedirectToAction("Index", "Login");
                }
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        // POST: devices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Session["userID"] != null)
            {
                if (Session["isAdmin"].Equals(true))
                {
                    device device = db.devices.Find(id);
                    db.devices.Remove(device);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else if (Session["userID"] != null)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return RedirectToAction("Index", "Login");
                }
            }
            else
            {
                return RedirectToAction("Index", "Login");
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




        [HttpGet]
        public ActionResult Search(String search)
        {
            if (Session["userID"] != null)
            {
                int sessionUserID = (int)Session["userID"];
                var devices = db.devices.Where(x => x.UserID == null);
                return View("Index", devices.Where(x => x.DName.Contains(search)).ToList());
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        [HttpGet]
        public ActionResult SearchDeviceList(String search)
        {
            if (Session["userID"] != null) {
                int sessionUserID = (int)Session["userID"];
                var devices = db.devices.Where(x => x.UserID == null);
                return View("deviceList", devices.Where(x => x.DName.Contains(search)).ToList());
            } else
            {
                return RedirectToAction("Index","Login");
            }
        }
    }
}

    

