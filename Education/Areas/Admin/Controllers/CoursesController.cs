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
    [CustomizeAuthorize]
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
            if (Session["user"] == null) {
                return RedirectToAction("Login", "Dashboard");
            }else{
                course.UserId = ((User)Session["user"]).Id;
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
                    string fileName = course.Code + DateTime.Today.ToString("ddmmyyyy")+ System.IO.Path.GetExtension(course.Image.FileName);
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
                    course.Image.SaveAs(path);
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
            CourseModel courseModel = new CourseModel()
            {
                Id = course.Id,
                Code = course.Code,
                Name = course.Name,
                StudyTime = course.StudyTime,
                Price = course.Price,
                SalePrice = course.SalePrice,
                Detail = course.Detail,
                MaximumCandicate = course.MaximumCandicate,
            };
            ViewBag.Image = course.Image;
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(courseModel);
        }

        // POST: Admin/Courses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CourseModel course)
        {
            if (ModelState.IsValid)
            {
                string image;
                if (course.Image != null && course.Image.ContentLength > 0)
                {
                    string fileName =course.Code + DateTime.Today.ToString("ddmmyyyy") + System.IO.Path.GetExtension(course.Image.FileName);
                    string path = Path.Combine(Server.MapPath("~/Areas/Admin/Content/assets/img/course"), fileName);
                    course.Image.SaveAs(path);
                    image = "/Areas/Admin/Content/assets/img/course" + fileName;
                }
                else
                {
                    image = courseRepository.FindById(course.Id).Image;
                }
                Course courseEdit = courseRepository.FindById(course.Id);
                courseEdit.Code = course.Code;
                courseEdit.Name = course.Name;
                courseEdit.StudyTime = course.StudyTime;
                courseEdit.Price = course.Price;
                courseEdit.SalePrice = course.SalePrice;
                courseEdit.Image = image;
                courseEdit.Detail = course.Detail;
                courseEdit.MaximumCandicate = course.MaximumCandicate;
                courseEdit.UpdatedAt = DateTime.Today;

                if (courseRepository.Edit(courseEdit))
                {
                    return RedirectToAction("Index");
                }
                
            }
            return View(course);
        }

        // POST: Admin/Courses/Delete/5
        [HttpPost]
        public ActionResult Delete(int? id)
        {
            Course course = courseRepository.FindById(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            courseRepository.Remove(course);
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
