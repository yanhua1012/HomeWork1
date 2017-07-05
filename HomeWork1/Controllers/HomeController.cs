using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using HomeWork1.ViewModels;

namespace HomeWork1.Controllers
{
    public class HomeController : Controller
    {
        protected Dictionary<string, string> fakeUserDictionary = new Dictionary<string, string> { {"admin", "123" }, { "user", "123" } };

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (fakeUserDictionary.ContainsKey(model.Account))
                {
                    if (model.Password == fakeUserDictionary[model.Account])
                    {
                        ProcessLogin(model);
                        return RedirectToAction("Index", "BalanceEntry");
                    }
                }

                ModelState.AddModelError(string.Empty, "請輸入正確的帳號密碼");

            }

            return View();
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.RemoveAll();

            // 使用名的 Cookie 逾期
            HttpCookie formCookie = new HttpCookie(FormsAuthentication.FormsCookieName, string.Empty);
            formCookie.Expires = DateTime.Now.AddYears(-1);
            Response.Cookies.Add(formCookie);

            // ASP.NET 的 Session Cookie 逾期
            HttpCookie aspNetCookie = new HttpCookie("ASP.NET_SessionId", string.Empty);
            aspNetCookie.Expires = DateTime.Now.AddYears(-1);
            Response.Cookies.Add(aspNetCookie);

            return RedirectToAction("Index", "Home");
        }

        protected void ProcessLogin(LoginViewModel model)
        {
            Session.RemoveAll();
            DateTime issueDate = DateTime.Now;

            var ticket = new FormsAuthenticationTicket(
                version: 1,
                name: model.Account,
                issueDate: issueDate,
                expiration: issueDate.AddMinutes(30),
                isPersistent: model.RememberMe,
                userData: "users",
                cookiePath: FormsAuthentication.FormsCookiePath
            );

            string encryptedTicket = FormsAuthentication.Encrypt(ticket);
            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
            Response.Cookies.Add(cookie);
        }
    }
}