using AspNetTutorial.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AspNetTutorial.Controllers
{
    public class UserController : Controller
    {
        private IUserService userService = new UserService();
        //
        // GET: /User/

        public ActionResult Help()
        {
            try
            {
                if (Session["LogedUserID"] != null)
                {
                    return View();
                }
                else
                {
                    return RedirectToAction("Login", "Login");
                }
            }
            catch (NullReferenceException)
            {
                return RedirectToAction("ErrorMessage", "User");
            }
        }

        public ActionResult ShowProfileSettings()
        {
            try
            {
                if (Session["LogedUserID"] != null)
                {
                    int userId = Int32.Parse(Session["LogedUserID"].ToString());
                    var user = userService.GetById(userId);
                    return View(user);
                }
                else
                {
                    return RedirectToAction("Login", "Login");
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ShowProfileSettings(FormCollection fc)
        {
            try
            {
                int userId = Int32.Parse(Session["LogedUserID"].ToString());
                var user = userService.GetById(userId);
                user.Password = Request.Form["NewPassword"].ToString();
                userService.Update(user);
                userService.Save();
                return View(user);
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

        public ActionResult ErrorMessage()
        {
            return View();
        }

    }
}
