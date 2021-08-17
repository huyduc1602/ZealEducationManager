using Education.Areas.Admin.Data.BusinessModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Education.Areas.Admin.Controllers
{
    public class LearningInfoController : Controller
    {
        private IPaginationService paginationService;
        public LearningInfoController()
        {
            paginationService = new DbPaginationService();
        }
        // GET: Admin/LearningInfo
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Create()
        {
            return View();
        }
    }
}