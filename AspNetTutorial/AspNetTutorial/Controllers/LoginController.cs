using AspNetTutorial.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AspNetTutorial.Controllers
{
    public class LoginController : Controller
    {
        private IUserService userService = new UserService();
        //
        // GET: /Login/

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(User u)
        {
            try
            {
                ViewData["UserName"] = Request.Form["UserName"].ToString();
                ViewData["Password"] = Request.Form["Password"].ToString();
                String UserName = Request.Form["UserName"].ToString();
                String Password = Request.Form["Password"].ToString();
                var user = userService.GetByUserNameAndPassword(UserName, Password);
                if (user != null)
                {
                    Session["LogedUserID"] = user.UserId.ToString();
                    Session["LogedUserFullName"] = user.Name.ToString();
                    return RedirectToAction("ShowNotes", "Note");
                }
                else
                {
                    ViewData["ErrorMessage"] = "Invalid name or password";
                    return View(u);
                }
            }
            catch (NullReferenceException)
            {
                return RedirectToAction("ErrorMessage", "User");
            }
            catch (SqlException)
            {
                return RedirectToAction("ErrorMessage", "User");
            }
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(User u)
        {
            try
            {
                ViewData["UserName"] = Request.Form["UserName"].ToString();
                ViewData["Password"] = Request.Form["Password"].ToString();
                String UserName = Request.Form["UserName"].ToString();
                String Password = Request.Form["Password"].ToString();
                User user = null;
                user = userService.GetByUserName(UserName);
                if (user == null)
                {
                    var newUser = userService.Add(new User() { Name = UserName, Password = Password});
                    userService.Save();
                    Session["LogedUserID"] = newUser.UserId.ToString();
                    Session["LogedUserFullName"] = newUser.Name.ToString();
                    return RedirectToAction("ShowNotes", "Note");
                }
                else
                {
                    ViewData["ErrorMessage"] = "Try another name";
                    return View(u);
                }
            }
            catch (NullReferenceException)
            {
                return RedirectToAction("ErrorMessage", "User");
            }
            catch (SqlException)
            {
                return RedirectToAction("ErrorMessage", "User");
            }
        }

        public ActionResult Logout()
        {
            Session["LogedUserID"] = null;
            Session["LogedUserFullName"] = null;
            return RedirectToAction("Login");
        }

    }
}
