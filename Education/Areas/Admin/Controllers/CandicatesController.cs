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

namespace Education.Areas.Admin.Controllers
{
    public class CandicatesController : Controller
    {
        private EducationManageDbContext ctx;
        private IRepository<Candicate> candicateRepository;
        private IRepository<User> userRepository;
        private IRepository<GroupUser> groupUserRepository;
        private IPaginationService paginationService;
        public CandicatesController()
        {
            ctx = new EducationManageDbContext();
            candicateRepository = new DbRepository<Candicate>();
            userRepository = new DbRepository<User>();
            groupUserRepository = new DbRepository<GroupUser>();
            paginationService = new DbPaginationService();
        }

        // GET: Admin/Candicates
        public ActionResult Index()
        {
            var candicates = candicateRepository.Get();
            return View(candicates);
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
            if (candicate == null)
            {
                return HttpNotFound();
            }
            return View(candicate);
        }

        // GET: Admin/Candicates/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Candicates/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CandicateModel candicate)
        {
            if (candicateRepository.Get(x => x.Code.Equals(candicate.Code)).Count() > 0)
            {
                ModelState.AddModelError("Code", "Student code available");
            }else if (candicateRepository.Get(x => x.Email.Equals(candicate.Email)).Count() > 0)
            {
                ModelState.AddModelError("Email", "Email available");
            }
            else if (userRepository.Get(x => x.UserName.Equals(candicate.UserName)).Count() > 0)
            {
                ModelState.AddModelError("UserName", "Username available");
            }
            else if (candicate.Image == null)
            {
                ModelState.AddModelError("Image", "Student images are not empty");
            }
            if (ModelState.IsValid)
            {
                if (candicate.Image != null && candicate.Image.ContentLength > 0)
                {
                    GroupUser groupUser = groupUserRepository.Get(x => x.Name.Equals("Candicate")).First();
                    int groupId = groupUser.Id;
                    User user = new User
                    {
                        UserName = candicate.UserName,
                        Password = candicate.Password,
                        Email = candicate.Email,
                        FullName = candicate.FullName,
                        GroupUserId = groupUser.Id,
                        CreatedDate = DateTime.Today,
                    };
                    userRepository.Add(user);
                    User u = userRepository.Get(x => x.UserName.Equals(candicate.UserName)).SingleOrDefault();
                    string lastName = candicate.Image.FileName;
                    string[] words = lastName.Split('.');
                    int size = words.Count();
                    string fileName = candicate.UserName + DateTime.Today.ToString("ddmmyyyy") + "." + words[size - 1];
                    Candicate student = new Candicate
                    {
                        Code = candicate.Code,
                        Name = candicate.Name,
                        Email = candicate.Email,
                        ParentName = candicate.ParentName,
                        ParentPhone = candicate.ParentPhone,
                        Image = fileName,
                        Gender = candicate.Gender,
                        Phone = candicate.Phone,
                        Address = candicate.Address,
                        Birthday = candicate.Birthday,
                        JoiningDate = candicate.JoiningDate,
                        CreatedAt = DateTime.Today,
                        UserId = u.Id,
                    };
                    candicateRepository.Add(student);
                    string path = Path.Combine(Server.MapPath("~/Areas/Admin/Content/assets/img/student"), fileName);
                    candicate.Image.SaveAs(path);
                    return RedirectToAction("Index");
                }
            }
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
                candicateRepository.Edit(candicate);
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
            Candicate candicate = candicateRepository.FindById(id);
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
            Candicate candicate = candicateRepository.FindById(id);
            candicateRepository.Remove(candicate);
            return RedirectToAction("Index");
        }

        /*protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                ctx.Dispose();
            }
            base.Dispose(disposing);
        }*/
    }
}
