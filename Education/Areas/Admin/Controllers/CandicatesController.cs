using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Education.BLL;
using Education.DAL;
using Education.Areas.Admin.Data;
using Education.Areas.Admin.Data.BusinessModel;
using Newtonsoft.Json;

namespace Education.Areas.Admin.Controllers
{
    public class CandicatesController : Controller
    {
        private IRepository<Candicate> candicateRepository;
        private IRepository<User> userRepository;
        private IPaginationService paginationService;
        public CandicatesController()
        {
            candicateRepository = new DbRepository<Candicate>();
            userRepository = new DbRepository<User>();
            paginationService = new DbPaginationService();
        }

        // GET: Admin/Candicates
        public ActionResult Index()
        {
            var candicates = candicateRepository.Get();
            return View(candicates.ToList());
        }

        public ActionResult GetData(int CurrentPage, int Limit, string Key)
        {
            var cadicate = candicateRepository.Get();
            if (!String.IsNullOrEmpty(Key))
            {
                cadicate = cadicate.Where(x => x.Name.Contains("/" + Key + "/")
                                            || x.Code.Contains("/" + Key + "/")
                                            || x.ParentName.Contains("/" + Key + "/")
                                            || x.ParentPhone.Contains("/" + Key + "/")
                                            || x.Email.Contains("/" + Key + "/")
                                            || x.Phone.Contains("/" + Key + "/"));
            }
            Pagination pagination = paginationService.getInfoPaginate(cadicate.Count(), Limit, CurrentPage);
            var json = JsonConvert.SerializeObject(user.Skip((CurrentPage - 1) * limit).Take(limit));
            var data = json;
            return Json(new
            {
                paginate = pagination,
                data = data,
                Key = Key,
            }, JsonRequestBehavior.AllowGet);
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
