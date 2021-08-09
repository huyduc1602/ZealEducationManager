using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Education.DAL;

namespace Education.Areas.Admin.Controllers
{
    public class FaultiesController : Controller
    {
        private EducationManageDbContext db = new EducationManageDbContext();

        // GET: Admin/Faulties
        public ActionResult Index()
        {
            return View(db.Faulties.ToList());
        }

        // GET: Admin/Faulties/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Faulty faulty = db.Faulties.Find(id);
            if (faulty == null)
            {
                return HttpNotFound();
            }
            return View(faulty);
        }

        // GET: Admin/Faulties/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Faulties/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Code,Name,Email,Address,Gender,Image,Phone,Salary,Qualification,Birthday,CreatedAt,UpdatedAt")] Faulty faulty)
        {
            if (ModelState.IsValid)
            {
                db.Faulties.Add(faulty);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(faulty);
        }

        // GET: Admin/Faulties/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Faulty faulty = db.Faulties.Find(id);
            if (faulty == null)
            {
                return HttpNotFound();
            }
            return View(faulty);
        }

        // POST: Admin/Faulties/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Code,Name,Email,Address,Gender,Image,Phone,Salary,Qualification,Birthday,CreatedAt,UpdatedAt")] Faulty faulty)
        {
            if (ModelState.IsValid)
            {
                db.Entry(faulty).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(faulty);
        }

        // GET: Admin/Faulties/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Faulty faulty = db.Faulties.Find(id);
            if (faulty == null)
            {
                return HttpNotFound();
            }
            return View(faulty);
        }

        // POST: Admin/Faulties/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Faulty faulty = db.Faulties.Find(id);
            db.Faulties.Remove(faulty);
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
