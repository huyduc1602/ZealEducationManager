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
using Education.Areas.Admin.Data.DataModel;
using System.IO;
using Education.Areas.Admin.Data.BusinessModel.Interface;

namespace Education.Areas.Admin.Controllers
{
    public class CandicatesController : Controller
    {
        private IRepository<Candicate> candicateRepository;
        private IRepository<User> userRepository;
        private IRepository<GroupUser> groupUserRepository;
        private IPaginationService paginationService;
        private ICandicateModelService CandicateModelService;
        public CandicatesController()
        {
            candicateRepository = new DbRepository<Candicate>();
            userRepository = new DbRepository<User>();
            groupUserRepository = new DbRepository<GroupUser>();
            paginationService = new DbPaginationService();
            CandicateModelService = new DbCandicateModelService();
        }

        // GET: Admin/Candicates
        public ActionResult Index()
        {
            var candicates = candicateRepository.Get();
            return View(candicates);
        }

        public ActionResult GetData(int CurrentPage, int Limit, string Key = null)
        {
            var cadicate = candicateRepository.Get();
            if (!string.IsNullOrEmpty(Key))
            {
                cadicate = cadicate.Where(x => x.Name.Contains(Key)
                                            || x.Code.Contains(Key)
                                            || x.ParentName.Contains(Key)
                                            || x.ParentPhone.Contains(Key)
                                            || x.Email.Contains(Key)
                                            || x.Phone.Contains(Key));
            }
            Pagination pagination = paginationService.getInfoPaginate(cadicate.Count(), Limit, CurrentPage);
            var json = JsonConvert.SerializeObject(cadicate.Skip((CurrentPage - 1) * Limit).Take(Limit));
            var data = json;
            return Json(new
            {
                paginate = pagination,
                data = data,
                key = Key,
            }, JsonRequestBehavior.AllowGet);
        }
        // GET: Admin/Candicates/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Candicate candicate = candicateRepository.FindById(id);
            User u = userRepository.FindById(candicate.UserId);
            CandicateModel model = CandicateModelService.ConvertCandicateModel(candicate, u);
            if (candicate == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        // GET: Admin/Candicates/Create
        public ActionResult Create()
        {
            List<object> gender = new List<object> {
                        new { value = 2, text = "Select Gender" },
                        new { value = 0, text = "Female" },
                        new { value = 1, text = "Male" },
                    };
            SelectList listItems = new SelectList(gender.ToList(), "value", "text");
            ViewBag.Gender = listItems;
            return View();
        }

        // POST: Admin/Candicates/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CandicateModel candicate)
        {
            DateTime now = DateTime.Today;
            // Check Unique Data 
            if (candicateRepository.CheckDuplicate(x => x.Code.Equals(candicate.Code)))
            {
                ModelState.AddModelError("Code", "Student code available");
            }else if (candicateRepository.CheckDuplicate(x => x.Email.Equals(candicate.Email)))
            {
                ModelState.AddModelError("Email", "Email available");
            }
            else if (userRepository.CheckDuplicate(x => x.UserName.Equals(candicate.UserName)))
            {
                ModelState.AddModelError("UserName", "Username available");
            }
            else if (candicate.Gender == 2)
            {
                ModelState.AddModelError("Gender", "You must choose the student's gender");
            }
            else if (candicate.Image == null) // check null image
            {
                ModelState.AddModelError("Image", "Student images are not empty");
            }
            else if ((now.Year - candicate.Birthday.Year) < 16)
            {
                ModelState.AddModelError("Birthday", "Students must be over 16 years old");
            }
            if (ModelState.IsValid)
            {
                if (candicate.Image != null && candicate.Image.ContentLength > 0)
                {
                    // Create User candicate
                    User user = CandicateModelService.ConvertUser(candicate);
                    if (userRepository.Add(user))
                    {
                        User u = userRepository.Get(x => x.UserName.Equals(candicate.UserName)).SingleOrDefault();
                        // Create info candicate
                        string lastName = candicate.Image.FileName;
                        string[] words = lastName.Split('.');
                        int size = words.Count();
                        string fileName = candicate.UserName + "-" + DateTime.Now.ToString("H-m-dd-M-yyyy") + "." + words[size - 1];
                        Candicate student = CandicateModelService.ConvertCandicate(candicate);
                        student.Image = fileName;
                        student.UserId = u.Id;
                        if (candicateRepository.Add(student))
                        {
                            // save image candicate
                            string path = Path.Combine(Server.MapPath("~/Areas/Admin/Content/assets/img/student"), fileName);
                            candicate.Image.SaveAs(path);
                            return RedirectToAction("Index");
                        }
                    }
                }
            }
            List<object> gender = new List<Object> {
                        new { value = 2, text = "Select Gender" },
                        new { value = 0, text = "Female" },
                        new { value = 1, text = "Male" },
                    };
            SelectList listItems = new SelectList(gender.ToList(), "value", "text", candicate.Gender);
            ViewBag.Gender = listItems;
            return View(candicate);
        }

        // GET: Admin/Candicates/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Candicate candicate = candicateRepository.FindById(id);
            User u = userRepository.FindById(candicate.UserId);
            CandicateModel model = CandicateModelService.ConvertCandicateModel(candicate, u); 
            if (candicate == null)
            {
                return HttpNotFound();
            }
            List<object> gender = new List<object> {
                        new { value = 2, text = "Select Gender" },
                        new { value = 0, text = "Female" },
                        new { value = 1, text = "Male" },
                    };
            ViewBag.Gender = new SelectList(gender.ToList(), "value", "text", model.Gender);
            //ViewBag.UserId = new SelectList(db.Users, "Id", "UserName", candicate.UserId);
            return View(model);
        }

        // POST: Admin/Candicates/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CandicateModel candicate)
        {
            DateTime now = DateTime.Today;
            // Check Unique Data 
            if (candicateRepository.CheckDuplicate(x => x.Code.Equals(candicate.Code)) && candicateRepository.Get(x => x.Code.Equals(candicate.Code)).First().Id != candicate.Id)
            {
                ModelState.AddModelError("Code", "Student code available");
            }
            else if (candicateRepository.CheckDuplicate(x => x.Email.Equals(candicate.Email)) && candicateRepository.Get(x => x.Email.Equals(candicate.Email)).First().Id != candicate.Id)
            {
                ModelState.AddModelError("Email", "Email available");
            }
            else if (userRepository.CheckDuplicate(x => x.UserName.Equals(candicate.UserName)) && candicateRepository.FindById(candicate.Id).UserId != userRepository.Get(x => x.UserName.Equals(candicate.UserName)).First().Id)
            {
                ModelState.AddModelError("UserName", "Username available");
            }
            else if (candicate.Gender == 2)
            {
                ModelState.AddModelError("Gender", "You must choose the student's gender");
            }
            else if ((now.Year - candicate.Birthday.Year) < 16)
            {
                ModelState.AddModelError("Birthday", "Students must be over 16 years old");
            }
            if (ModelState.IsValid)
            {
                Candicate old = candicateRepository.FindById(candicate.Id);
                // Update User Candicate
                User uOld = userRepository.FindById(old.UserId);
                User user = CandicateModelService.ConvertEditUser(candicate, uOld);
                if (userRepository.Edit(user))
                {
                    // Update info candicate
                    Candicate student = CandicateModelService.ConvertEditCandicate(candicate, old);
                    if (candicate.Image != null)
                    {
                        string lastName = candicate.Image.FileName;
                        string[] words = lastName.Split('.');
                        int size = words.Count();
                        string fileName = candicate.UserName + "-" + DateTime.Now.ToString("H-m-dd-M-yyyy") + "." + words[size - 1];
                        string file = Server.MapPath("~/Areas/Admin/Content/assets/img/student/" + old.Image);
                        FileInfo f = new FileInfo(file);
                        if (f.Exists)
                        {
                            f.Delete();
                        }
                        // save image candicate
                        string path = Path.Combine(Server.MapPath("~/Areas/Admin/Content/assets/img/student"), fileName);
                        candicate.Image.SaveAs(path);
                        student.Image = fileName;
                    }
                    candicateRepository.Edit(student);
                    return RedirectToAction("Index");
                }
            }
            List<object> gender = new List<Object> {
                        new { value = 2, text = "Select Gender" },
                        new { value = 0, text = "Female" },
                        new { value = 1, text = "Male" },
                    };
            ViewBag.Gender = new SelectList(gender.ToList(), "value", "text", candicate.Gender);
            return View(candicate);
        }

        public ActionResult Remove(int id)
        {
            Candicate candicate = candicateRepository.FindById(id);
            if (candicate.LearningInfos.Count() > 0)
            {
                return Json(new { StatusCode = 400, message = "Deleting students unsuccessful. Students entered classes!" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                candicateRepository.Remove(candicate);
                // Delete Image Old
                string file = Server.MapPath("~/Areas/Admin/Content/assets/img/student/" + candicate.Image);
                FileInfo f = new FileInfo(file);
                if (f.Exists)
                {
                    f.Delete();
                }
                userRepository.Remove(candicate.UserId);
                return Json(new
                {
                    StatusCode = 200,
                    message = "Delete successful candicate!",
                }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}
