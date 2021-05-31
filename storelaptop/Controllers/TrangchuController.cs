using storelaptop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;


namespace storelaptop.Controllers
{
    public class TrangchuController : Controller
    {
        private quantriEntities db = new quantriEntities();
        // GET: Trangchu
        public ActionResult Index()
        {
            return View(db.sanphams.ToList());
           // return View();
        }
        public ActionResult Details(int? id)
        {
            return View(db.sanphams.ToList());
           // return View();
        }
    }
}