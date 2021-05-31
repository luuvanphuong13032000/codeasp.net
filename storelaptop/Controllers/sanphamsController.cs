using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using storelaptop.Models;
using PagedList;

namespace storelaptop.Controllers
{
    public class sanphamsController : Controller
    {
        private quantriEntities db = new quantriEntities();

        // GET: sanphams
        public ActionResult Index(string sortOrder, string currentFilter, string SearchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.MaSortParm = String.IsNullOrEmpty(sortOrder) ? "ma_desc" : "";
            ViewBag.GiaSortParm = sortOrder == "Gia" ? "gia_desc" : "Gia";

            if (SearchString != null)
            {
                page = 1;
            }
            else
            {
                SearchString = currentFilter;
            }

            ViewBag.CurrentFilter = SearchString;

            var sanphams = from s in db.sanphams
                           select s;
            if (!String.IsNullOrEmpty(SearchString))
            {
                sanphams = sanphams.Where(s => s.tensanpham.Contains(SearchString)
                                       || s.mota.Contains(SearchString));
            }
            switch (sortOrder)
            {
                case "ma_desc":
                    sanphams = sanphams.OrderByDescending(s => s.masanpham);
                    break;
                case "Gia":
                    sanphams = sanphams.OrderBy(s => s.gia);
                    break;
                case "gia_desc":
                    sanphams = sanphams.OrderByDescending(s => s.gia);
                    break;
                default:  // Name ascending 
                    sanphams = sanphams.OrderBy(s => s.masanpham);
                    break;
            }

            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(sanphams.ToPagedList(pageNumber, pageSize));
        }
        // public ActionResult Index()
        //{
        //  var sanphams = db.sanphams.Include(s => s.danhmucsanpham);
        //return View(sanphams.ToList());
        //}

        // GET: sanphams/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            sanpham sanpham = db.sanphams.Find(id);
            if (sanpham == null)
            {
                return HttpNotFound();
            }
            return View(sanpham);
        }

        // GET: sanphams/Create
        public ActionResult Create()
        {
            ViewBag.madanhmucsanpham = new SelectList(db.danhmucsanphams, "madanhmucsanpham", "tendanhmucsanpham");
            return View();
        }

        // POST: sanphams/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
       // [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create(HttpPostedFileBase anh, sanpham sanpham)
        {
            if (anh != null)
            {
                string path = Path.Combine(Server.MapPath("~/Anh"), Path.GetFileName(anh.FileName));
                anh.SaveAs(path);
                sanpham.anh = anh.FileName;
            }
            if (ModelState.IsValid)
            {
                db.sanphams.Add(sanpham);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.madanhmucsanpham = new SelectList(db.danhmucsanphams, "madanhmucsanpham", "tendanhmucsanpham", sanpham.madanhmucsanpham);
            return View(sanpham);
        }

        // GET: sanphams/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            sanpham sanpham = db.sanphams.Find(id);
            if (sanpham == null)
            {
                return HttpNotFound();
            }
            ViewBag.madanhmucsanpham = new SelectList(db.danhmucsanphams, "madanhmucsanpham", "tendanhmucsanpham", sanpham.madanhmucsanpham);
            return View(sanpham);
        }

        // POST: sanphams/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit([Bind(Include = "masanpham,anh,tensanpham,mota,gia,madanhmucsanpham")] sanpham sanpham)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sanpham).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.madanhmucsanpham = new SelectList(db.danhmucsanphams, "madanhmucsanpham", "tendanhmucsanpham", sanpham.madanhmucsanpham);
            return View(sanpham);
        }

        // GET: sanphams/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            sanpham sanpham = db.sanphams.Find(id);
            if (sanpham == null)
            {
                return HttpNotFound();
            }
            return View(sanpham);
        }

        // POST: sanphams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            sanpham sanpham = db.sanphams.Find(id);
            db.sanphams.Remove(sanpham);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
