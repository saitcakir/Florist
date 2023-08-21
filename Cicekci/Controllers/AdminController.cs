using Cicekci.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Cicekci.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Login()
        {

            HttpCookie authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
            if (authCookie == null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("FlowerList", "Flower");
            }
        }
        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            LoginModel defaultAdmin = new LoginModel()
            {
                Name = "Admin",
                Password = "12345"
            };
            if (ModelState.IsValid)
            {
                if (defaultAdmin.Name == model.Name && defaultAdmin.Password == model.Password)
                {
                    FormsAuthentication.SetAuthCookie(defaultAdmin.Name, false);

                    return RedirectToAction("FlowerList", "Flower");
                }
                else
                {
                    ModelState.AddModelError("", $"Lütfen kullanıcı ad ve şifrenizi doğru giriniz!");
                    return View(model);
                }

            }
            ModelState.AddModelError("", $"Lütfen bilgileri eksiksiz giriniz!");
            return View(model);
        }
    }
}