using Education.Areas.Admin.Data;
using Education.Areas.Admin.Data.BusinessModel;
using Education.Areas.Admin.Data.BusinessModel.Interface;
using Education.Areas.Admin.Data.DataModel;
using Education.Areas.Admin.Data.ViewModel;
using Education.BLL;
using Education.DAL;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Education.Areas.Admin.Controllers
{
    public class BatchController : Controller
    {
        private IPaginationService paginationService;
        private IRepository<ClassRoom> batchRepository;
        private IRepository<Course> courseRepository;
        private IRepository<Faulty> faultyRepository;
        private IRepository<User> userRepository;
        private IRepository<ClassRoomFaulty> roomFaultyRepository;
        private IBatchModelService batchModelService;
        public BatchController()
        {
            paginationService = new DbPaginationService();
            batchRepository = new DbRepository<ClassRoom>();
            courseRepository = new DbRepository<Course>();
            faultyRepository = new DbRepository<Faulty>();
            userRepository = new DbRepository<User>();
            roomFaultyRepository = new DbRepository<ClassRoomFaulty>();
            batchModelService = new DbBatchModelService();
        }
        // GET: Admin/Batch
        public ActionResult Index()
        {
            var batchs = batchRepository.Get();
            IEnumerable<BatchView> view = batchModelService.convertListBatchView(batchs);
            return View(view);
        }
        public ActionResult GetData(int CurrentPage, int Limit, string Key = null)
        {
            var batchs = batchModelService.convertListBatchView(batchRepository.Get());
            if (!string.IsNullOrEmpty(Key))
            {
                batchs = batchs.Where(x => x.Code.Contains(Key)
                                        || x.Name.Contains(Key)
                                        || x.Course.Contains(Key)
                                        || x.Faulty.Contains(Key)
                                        || x.FaultyOld.Contains(Key)
                                        || x.User.Contains(Key)
                                      );
            }
            Pagination pagination = paginationService.getInfoPaginate(batchs.Count(), Limit, CurrentPage);
            var json = JsonConvert.SerializeObject(batchs.Skip((CurrentPage - 1) * Limit).Take(Limit));
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
            BatchView view = batchModelService.convertBatchView(batchRepository.FindById(id));
            if (view == null)
            {
                return HttpNotFound();
            }
            return View(view);
        }

        public ActionResult Create()
        {
            ViewBag.CourseId = new SelectList(courseRepository.Get(), "Id", "Name");
            ViewBag.FaultyId = new SelectList(faultyRepository.Get(), "Id", "Name");
            List<object> status = new List<Object> {
                        new { value = 1, text = "New" },
                        new { value = 2, text = "In process" },
                        new { value = 3, text = "It's over" },
                    };
            ViewBag.Status = new SelectList(status.ToList(), "value", "text");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BatchModel model)
        {
            DateTime now = DateTime.Today;
            if (batchRepository.CheckDuplicate(x => x.Name.Equals(model.Name)))
            {
                ModelState.AddModelError("Name", "Batch name available");
            }
            else if (model.StartDate < now)
            {
                ModelState.AddModelError("StartDate", "Admission date must be the future day");
            }
            if (ModelState.IsValid)
            {
                Session["idUser"] = 4;
                var UserId = (int)Session["idUser"];
                ClassRoom batch = batchModelService.convertBatch(model, UserId);
                if (batchRepository.Add(batch))
                {
                    ClassRoomFaulty classRoomFaulty = new ClassRoomFaulty
                    {
                        RoomId = batchRepository.Get(x => x.Code.Equals(batch.Code)).First().Id,
                        FaultyId = model.FaultyId,
                        Status = true,
                    };
                    roomFaultyRepository.Add(classRoomFaulty);
                    return RedirectToAction("Index");
                }
            }
            ViewBag.CourseId = new SelectList(courseRepository.Get(), "Id", "Name", model.CourseId);
            ViewBag.FaultyId = new SelectList(faultyRepository.Get(), "Id", "Name", model.FaultyId);
            List<object> status = new List<Object> {
                        new { value = 1, text = "New" },
                        new { value = 2, text = "In process" },
                        new { value = 3, text = "It's over" },
                    };
            ViewBag.Status = new SelectList(status.ToList(), "value", "text", model.Status);
            return View(model);
        }
        public ActionResult Edit(int? id)
        {
            ClassRoom batch = batchRepository.FindById(id);
            int FaultyId = batch.ClassRoomFaulties.Where(x => x.Status).First().FaultyId;
            BatchModel model = batchModelService.convertBatchModel(batch, FaultyId);
            ViewBag.CourseId = new SelectList(courseRepository.Get(), "Id", "Name", batch.CourseId);
            ViewBag.FaultyId = new SelectList(faultyRepository.Get(), "Id", "Name", FaultyId);
            List<object> status = new List<Object> {
                        new { value = 1, text = "New" },
                        new { value = 2, text = "In process" },
                        new { value = 3, text = "It's over" },
                    };
            ViewBag.Status = new SelectList(status.ToList(), "value", "text", model.Status);
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(BatchModel model)
        {
            DateTime now = DateTime.Today;
            if (batchRepository.CheckDuplicate(x => x.Name.Equals(model.Name)) && batchRepository.Get(x => x.Name.Equals(model.Name)).First().Id != model.Id)
            {
                ModelState.AddModelError("Name", "Batch name available");
            }
            else if (model.StartDate < now)
            {
                ModelState.AddModelError("StartDate", "Admission date must be the future day");
            }
            if (ModelState.IsValid)
            {
                Session["idUser"] = 4;
                var UserId = (int)Session["idUser"];
                ClassRoom old = batchRepository.FindById(model.Id);
                int FaultyId = old.ClassRoomFaulties.Where(x => x.Status).First().FaultyId;
                ClassRoom batch = batchModelService.convertEditBatch(model, old);
                batch.UserId = UserId;
                ClassRoomFaulty classRoom = old.ClassRoomFaulties.Where(x => x.FaultyId == model.FaultyId).FirstOrDefault();
                if (classRoom != null && classRoom.Status)
                {
                    ClassRoomFaulty classRoomFaulty = old.ClassRoomFaulties.Where(x => x.FaultyId == model.FaultyId).First();
                    classRoomFaulty.Status = true;
                    roomFaultyRepository.Edit(classRoomFaulty);
                }
                else if (classRoom == null)
                {
                    ClassRoomFaulty classRoomFaultyOld = old.ClassRoomFaulties.Where(x => x.Status).First();
                    classRoomFaultyOld.Status = false;
                    roomFaultyRepository.Edit(classRoomFaultyOld);
                    ClassRoomFaulty classRoomFaulty = new ClassRoomFaulty
                    {
                        RoomId = model.Id,
                        FaultyId = model.FaultyId,
                        Status = true,
                    };
                    roomFaultyRepository.Add(classRoomFaulty);
                }
                batchRepository.Edit(batch);
                return RedirectToAction("Index");
            }
            ViewBag.CourseId = new SelectList(courseRepository.Get(), "Id", "Name", model.CourseId);
            ViewBag.FaultyId = new SelectList(faultyRepository.Get(), "Id", "Name", model.FaultyId);
            List<object> status = new List<Object> {
                        new { value = 1, text = "New" },
                        new { value = 2, text = "In process" },
                        new { value = 3, text = "It's over" },
                    };
            ViewBag.Status = new SelectList(status.ToList(), "value", "text", model.Status);
            return View(model);
        }
        public ActionResult Remove(int id)
        {
            ClassRoom classRoom = batchRepository.FindById(id);
            if (classRoom.LearningInfos.Count() >0 || classRoom.ClassRoomFaulties.Count() > 0)
            {
                return Json(new { StatusCode = 400, message = "Delete classrooms failed. Classrooms are having students!" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                if (batchRepository.Remove(classRoom))
                {
                    return Json(new
                    {
                        StatusCode = 200,
                        message = "Delete successful classes",
                    }, JsonRequestBehavior.AllowGet);
                }
                return Json(new { StatusCode = 400, message = "Delete classrooms failed. Classrooms are having students!" }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}