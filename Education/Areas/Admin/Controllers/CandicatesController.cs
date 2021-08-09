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
    public class CandicatesController : Controller
    {
        private EducationManageDbContext db = new EducationManageDbContext();

        // GET: Admin/Candicates
        public ActionResult Index()
        {
            var candicates = db.Candicates;
            return View(candicates.ToList());
        }

        // GET: Admin/Candicates/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Candicate candicate = db.Candicates.Find(id);
            if (candicate == null)
            {
                return HttpNotFound();
            }
            return View(candicate);
        }

        // GET: Admin/Candicates/Create
        public ActionResult Create()
        {
            ViewBag.UserId = new SelectList(db.Users, "Id", "UserName");
            return View();
        }

        // POST: Admin/Candicates/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Code,Name,ParentName,Email,Phone,ParentPhone,Image,Gender,Address,Birthday,JoiningDate,UserId,CreateAt,UpdateAt")] Candicate candicate)
        {
            if (ModelState.IsValid)
            {
                db.Candicates.Add(candicate);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            //ViewBag.UserId = new SelectList(db.Users, "Id", "UserName", candicate.UserId);
            return View(candicate);
        }

        // GET: Admin/Candicates/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Candicate candicate = db.Candicates.Find(id);
            if (candicate == null)
            {
                return HttpNotFound();
            }
            //ViewBag.UserId = new SelectList(db.Users, "Id", "UserName", candicate.UserId);
            return View(candicate);
        }

        // POST: Admin/Candicates/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Code,Name,ParentName,Email,Phone,ParentPhone,Image,Gender,Address,Birthday,JoiningDate,UserId,CreateAt,UpdateAt")] Candicate candicate)
        {
            if (ModelState.IsValid)
            {
                db.Entry(candicate).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            //ViewBag.UserId = new SelectList(db.Users, "Id", "UserName", candicate.UserId);
            return View(candicate);
        }

        // GET: Admin/Candicates/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Candicate candicate = db.Candicates.Find(id);
            if (candicate == null)
            {
                return HttpNotFound();
            }
            return View(candicate);
        }

        // POST: Admin/Candicates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Candicate candicate = db.Candicates.Find(id);
            db.Candicates.Remove(candicate);
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
