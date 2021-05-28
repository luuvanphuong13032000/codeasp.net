using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using storelaptop.Models;

namespace storelaptop.Controllers
{
    public class danhmucsanphamsController : Controller
    {
        private quantriEntities db = new quantriEntities();

        // GET: danhmucsanphams
        public ActionResult Index(string SearchString)
        {
            var hangsp = from h in db.danhmucsanphams
                         select h;
            if (!String.IsNullOrEmpty(SearchString))
            {
                hangsp = hangsp.Where(s => s.tendanhmucsanpham.Contains(SearchString));
            }
            return View(hangsp);
            //return View(db.danhmucsanphams.ToList());
        }

        // GET: danhmucsanphams/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            danhmucsanpham danhmucsanpham = db.danhmucsanphams.Find(id);
            if (danhmucsanpham == null)
            {
                return HttpNotFound();
            }
            return View(danhmucsanpham);
        }

        // GET: danhmucsanphams/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: danhmucsanphams/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "madanhmucsanpham,tendanhmucsanpham")] danhmucsanpham danhmucsanpham)
        {
            if (ModelState.IsValid)
            {
                db.danhmucsanphams.Add(danhmucsanpham);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(danhmucsanpham);
        }

        // GET: danhmucsanphams/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            danhmucsanpham danhmucsanpham = db.danhmucsanphams.Find(id);
            if (danhmucsanpham == null)
            {
                return HttpNotFound();
            }
            return View(danhmucsanpham);
        }

        // POST: danhmucsanphams/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "madanhmucsanpham,tendanhmucsanpham")] danhmucsanpham danhmucsanpham)
        {
            if (ModelState.IsValid)
            {
                db.Entry(danhmucsanpham).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(danhmucsanpham);
        }

        // GET: danhmucsanphams/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            danhmucsanpham danhmucsanpham = db.danhmucsanphams.Find(id);
            if (danhmucsanpham == null)
            {
                return HttpNotFound();
            }
            return View(danhmucsanpham);
        }

        // POST: danhmucsanphams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            danhmucsanpham danhmucsanpham = db.danhmucsanphams.Find(id);
            db.danhmucsanphams.Remove(danhmucsanpham);
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
