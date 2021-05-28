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

namespace storelaptop.Controllers
{
    public class tintucsController : Controller
    {
        private quantriEntities db = new quantriEntities();

        // GET: tintucs
        public ActionResult Index(string SearchString)
        {
            var tintucs = from t in db.tintucs.Include(t => t.danhmuctintuc)
                          select t;
            if (!String.IsNullOrEmpty(SearchString))
            {
                tintucs = tintucs.Where(s => s.tentintuc.Contains(SearchString));

            }
            return View(tintucs);

        }
        // public ActionResult Index()
        //{
        //  var tintucs = db.tintucs.Include(t => t.danhmuctintuc);
        //return View(tintucs.ToList());
        //}

        // GET: tintucs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tintuc tintuc = db.tintucs.Find(id);
            if (tintuc == null)
            {
                return HttpNotFound();
            }
            return View(tintuc);
        }

        // GET: tintucs/Create
        public ActionResult Create()
        {
            ViewBag.madanhmuctintuc = new SelectList(db.danhmuctintucs, "madanhmuctintuc", "tendanhmuctintuc");
            return View();
        }

        // POST: tintucs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create(HttpPostedFileBase anh, tintuc tintuc)
        {
            if (anh != null)
            {
                string path = Path.Combine(Server.MapPath("~/Anh"), Path.GetFileName(anh.FileName));
                anh.SaveAs(path);
                tintuc.anh = anh.FileName;
            }
            if (ModelState.IsValid)
            {
                db.tintucs.Add(tintuc);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.madanhmuctintuc = new SelectList(db.danhmuctintucs, "madanhmuctintuc", "tendanhmuctintuc", tintuc.madanhmuctintuc);
            return View(tintuc);
        }

        // GET: tintucs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tintuc tintuc = db.tintucs.Find(id);
            if (tintuc == null)
            {
                return HttpNotFound();
            }
            ViewBag.madanhmuctintuc = new SelectList(db.danhmuctintucs, "madanhmuctintuc", "tendanhmuctintuc", tintuc.madanhmuctintuc);
            return View(tintuc);
        }

        // POST: tintucs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        // [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit([Bind(Include = "matintuc,anh,tentintuc,mota,chitiet,madanhmuctintuc")] tintuc tintuc)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tintuc).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.madanhmuctintuc = new SelectList(db.danhmuctintucs, "madanhmuctintuc", "tendanhmuctintuc", tintuc.madanhmuctintuc);
            return View(tintuc);
        }

        // GET: tintucs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tintuc tintuc = db.tintucs.Find(id);
            if (tintuc == null)
            {
                return HttpNotFound();
            }
            return View(tintuc);
        }

        // POST: tintucs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tintuc tintuc = db.tintucs.Find(id);
            db.tintucs.Remove(tintuc);
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
