using CustomLogin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CustomLogin.Controllers
{
    public class LoginController : Controller
    {
        /* 
         Index function displays the login page.
             */
        // GET: Login
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
         Autherize function checks: 
         -if the username and password entered is valid based on validations;
         -if the username and password entered is valid based on database;
         -else displays the corresponding error messages;
         -if the user could log in, the user is redirected to the Home page;
         -saves the userID and the isAdmin attributes in the Session.
             */
        [HttpPost]
        public ActionResult Autherize(CustomLogin.Models.user userModel)
        {
            using(deviceManagementEntities1 db = new deviceManagementEntities1())
            {
                var userDetails = db.users.Where(x => x.userName == userModel.userName && x.password == userModel.password).FirstOrDefault();
                if(userDetails == null)
                {
                    userModel.LoginErrorMessage = "Wrong Username or Password.";
                    return View("Index",userModel);
                }
                else
                {
                    Session["userID"] = userDetails.userID;
                    Session["isAdmin"] = userDetails.isAdmin;
                    Session["userName"] = userDetails.userName;
                    return RedirectToAction("Index","Home");
                }
            }
        }

        /* 
         Register function calls the Index function from RegisterController (displays the Registration page)
             */
        public ActionResult Register()
        {
            return RedirectToAction("Index","Register");
        }

        /* 
         Logout function terminates the Session and calls the Index function from LoginController (redirects to login page)
             */
        public ActionResult LogOut()
        {
            Session.Abandon();
            return RedirectToAction("Index","Login");
        }

        
    }
}