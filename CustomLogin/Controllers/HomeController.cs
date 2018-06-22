using CustomLogin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CustomLogin.Controllers
{
    public class HomeController : Controller
    {
        private deviceManagementEntities1 db = new deviceManagementEntities1();


        // GET: Home
        public ActionResult Index(String sortBy)
        {
            using (deviceManagementEntities1 dbModel = new deviceManagementEntities1())
            {
                if (Session["userID"] != null)
                {
                    int sessionUserID = (int)Session["userID"];

                    ViewBag.SortNameParameter = String.IsNullOrEmpty(sortBy) ? "Name desc" : "";
                    ViewBag.SortTypeParameter = sortBy == "Type" ? "Type desc" : "Type";
                    ViewBag.SortManufacturerParameter = sortBy == "Manufacturer" ? "Manufacturer desc" : "Manufacturer";
                    ViewBag.SortOwnerParameter = sortBy == "Owner" ? "Owner desc" : "Owner";
                    var devices = db.devices.AsQueryable();


                    if (dbModel.devices.Any(x => x.UserID != sessionUserID))
                    {
                        devices = db.devices.Where(x => x.UserID != sessionUserID && x.UserID != null);

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
                            case "Owner desc":
                                devices = devices.OrderByDescending(x => x.user.userName);
                                break;
                            case "Owner":
                                devices = devices.OrderBy(x => x.user.userName);
                                break;
                            default:
                                devices = devices.OrderBy(x => x.DName);
                                break;
                        }

                        return View("Index", devices.ToList());
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    return RedirectToAction("Index", "Login");
                }
            }
        }
    }
}