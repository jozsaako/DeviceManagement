using CustomLogin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CustomLogin.Controllers
{
    public class RegisterController : Controller
    {

        public ActionResult Index()
        {
            if (Session["userID"] == null) {
                return View();
            } else
            {
                return RedirectToAction("Index","Home");
            }
        }

        /* 
         Add function checks:
         -if the username entered exists in database. If does a corresponding error message pops;
         -isAdmin attribute is automatically set to false, because there is only one admin ;
         -saves the entered user attributes if they fulfill the criterias. If not, corresponding error messages will appear.
             */
        [HttpGet]
        public ActionResult Add(int id = 0)
        {
            user userModel = new user();
            return View("Index", userModel);
        }

        [HttpPost]
        public ActionResult Add(user userModel)
        {
            using (deviceManagementEntities1 dbModel = new deviceManagementEntities1())
            {
                if (dbModel.users.Any(x => x.userName == userModel.userName))
                {
                    ViewBag.DuplicateMessage = "Username Already Exists.";
                    return View("Index", userModel);
                }
                userModel.isAdmin = false;
                dbModel.users.Add(userModel);
                dbModel.SaveChanges();
            }
            ModelState.Clear();
            ViewBag.SuccessMessage = "Registeration Successful.";
            return View("Index", new user());
        }

        /* 
         Login function calls the Index function from LoginController (redirects user to Login page).
             */
        public ActionResult Login()
        {
            return RedirectToAction("Index", "Login");
        }
    }
}