using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Education.Areas.Admin.Data;
using Education.Areas.Admin.Data.BusinessModel;
using Education.Areas.Admin.Data.DataModel;
using Education.BLL;
using Education.DAL;
using Newtonsoft.Json;

namespace Education.Areas.Admin.Controllers
{
    public class CoursesController : Controller
    {
        private EducationManageDbContext ctx;
        private IRepository<Course> courseRepository;
        private IPaginationService paginationService;
        public CoursesController()
        {
            ctx = new EducationManageDbContext();
            courseRepository = new DbRepository<Course>();
            paginationService = new DbPaginationService();
        }

        // GET: Admin/Courses
        public ActionResult Index()
        {
            var courses = courseRepository.Get();
            return View(courses);
        }
        public ActionResult GetData(int CurrentPage, int Limit, string Key)
        {
            var course = courseRepository.Get();
            if (!String.IsNullOrEmpty(Key))
            {
                course = course.Where(x => x.Name.Contains("/" + Key + "/")
                                            || x.Code.Contains("/" + Key + "/")
                                            || x.Detail.Contains("/" + Key + "/"));
            }
            Pagination pagination = paginationService.getInfoPaginate(course.Count(), Limit, CurrentPage);
            var json = JsonConvert.SerializeObject(course.Skip((CurrentPage - 1) * Limit).Take(Limit));
            var data = json;
            return Json(new
            {
                paginate = pagination,
                data = data,
                key = Key,
            }, JsonRequestBehavior.AllowGet);
        }

        // GET: Admin/Courses/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = courseRepository.FindById(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        // GET: Admin/Courses/Create
        public ActionResult Create()
        {
            //Check login
            if (Session["idUser"] == null)
            {
                return RedirectToAction("Login", "Dashboard");
            }
            return View();
        }

        // POST: Admin/Courses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CourseModel course)
        {
            //Check login
            if (Session["idUser"] == null) {
                return RedirectToAction("Login", "Dashboard");
            }else{
                course.UserId = (int)Session["idUser"];
            }
            //Check validate
            if (courseRepository.Get(x => x.Code.Equals(course.Code)).Count() > 0){
                ModelState.AddModelError("Code", "Course code available");
            }else if (courseRepository.Get(x => x.Name.Equals(course.Name)).Count() > 0){
                ModelState.AddModelError("Name", "Course name available");
            }else if (course.Image == null){
                ModelState.AddModelError("Image", "Student images are not empty");
            }
            //Add data
            if (ModelState.IsValid)
            {
                if (course.Image != null && course.Image.ContentLength > 0)
                {
                    string fileName = "Course" + course.Code + DateTime.Today.ToString("ddmmyyyy");
                    string path = Path.Combine(Server.MapPath("~/Areas/Admin/Content/assets/img/course"), fileName);
                    Course courseAdd = new Course
                    {
                        Code = course.Code,
                        Name = course.Name,
                        StudyTime = course.StudyTime,
                        Price = course.Price,
                        SalePrice = course.SalePrice,
                        Image = "/Areas/Admin/Content/assets/img/course" + fileName,
                        Detail = course.Detail,
                        MaximumCandicate = course.MaximumCandicate,
                        CreatedAt = DateTime.Today,
                        UpdatedAt = DateTime.Today,
                        UserId = course.UserId
                    };
                    courseRepository.Add(courseAdd);
                    //course.Image.SaveAs(path);
                    return RedirectToAction("Index");
                }

            }
            return View(course);
        }

        // GET: Admin/Courses/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = courseRepository.FindById(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserId = new SelectList(ctx.Users, "Id", "UserName", course.UserId);
            return View(course);
        }

        // POST: Admin/Courses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,StudyTime,CouseId,Price,SalePrice,Detail,Image,MaximumCandicate,UserId,CreateAt,UpdateAt")] Course course)
        {
            if (ModelState.IsValid)
            {
                ctx.Entry(course).State = EntityState.Modified;
                ctx.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserId = new SelectList(ctx.Users, "Id", "UserName", course.UserId);
            return View(course);
        }

        // GET: Admin/Courses/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = courseRepository.FindById(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        // POST: Admin/Courses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Course course = courseRepository.FindById(id);
            courseRepository.Remove(course);
            ctx.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                ctx.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
