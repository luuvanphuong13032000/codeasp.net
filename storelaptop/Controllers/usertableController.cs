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
    public class usertableController : Controller
    {
        private quantriEntities db = new quantriEntities();

        // GET: usertable
        public ActionResult Index()
        {
            return View(db.user_table.ToList());
        }

        // GET: usertable/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            user_table user_table = db.user_table.Find(id);
            if (user_table == null)
            {
                return HttpNotFound();
            }
            return View(user_table);
        }

        // GET: usertable/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: usertable/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,user_name,pass_word,full_name")] user_table user_table)
        {
            if (ModelState.IsValid)
            {
                db.user_table.Add(user_table);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(user_table);
        }

        // GET: usertable/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            user_table user_table = db.user_table.Find(id);
            if (user_table == null)
            {
                return HttpNotFound();
            }
            return View(user_table);
        }

        // POST: usertable/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,user_name,pass_word,full_name")] user_table user_table)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user_table).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user_table);
        }

        // GET: usertable/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            user_table user_table = db.user_table.Find(id);
            if (user_table == null)
            {
                return HttpNotFound();
            }
            return View(user_table);
        }

        // POST: usertable/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            user_table user_table = db.user_table.Find(id);
            db.user_table.Remove(user_table);
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
