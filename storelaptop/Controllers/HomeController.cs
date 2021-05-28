using storelaptop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace storelaptop.Controllers
{
    public class HomeController : Controller
    {
        private quantriEntities db = new quantriEntities();
        public ActionResult Index()
        {
            if (Session["user_name"] == null)
            {
                return RedirectToAction("Login");
            }
            return View(db.sanphams.ToList());
            //return View();
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
        public ActionResult Login(string user_name, string pass_word)
        {
            if (user_name != null)
            {
                var usr = db.user_table
                    .Where(m => m.user_name.Equals(user_name))
                    .Where(m => m.pass_word.Equals(pass_word))
                    .FirstOrDefault();
                if (usr != null)
                {
                    ViewBag.h = "chua nhap tai khoan";
                    Session["user_name"] = usr.user_name;
                    Session["full_name"] = usr.full_name;
                    return RedirectToAction("Index");
                }

            }
            return View();

        }
        public ActionResult Logout()
        {
            Session["user_name"] = null;
            Session["full_name"] = null;
            return RedirectToAction("Login");
        }
    }
}