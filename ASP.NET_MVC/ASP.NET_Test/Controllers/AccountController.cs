using ASP.NET_Test.Models;
using ASP.NET_Test.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ASP.NET_Test.Controllers
{
    public class AccountController : Controller
    {
        private IAccountService accountService = new AccountService();

        public ActionResult LoginAccount()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LoginAccount([Bind(Include = "Email,Password")]Account account)
        {
            if (ModelState.IsValid)
            {
                var loginAccount = accountService.GetByEmailAndPassword(account);
                if (loginAccount !=null)
                {
                    Session["LogedUserID"] = loginAccount.AccountId.ToString();
                    Session["LogedUserFullName"] = loginAccount.Email.ToString();
                    return RedirectToAction("ShowCategories", "Category");
                }
                else
                {
                    return View(account);
                }
            }
            return View(account);
        }


        public ActionResult Logout()
        {
            Session["LogedUserID"] = null;
            Session["LogedUserFullName"] = null;
            return RedirectToAction("LoginAccount","Account");
        }

    }
}
