using Education.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Education.DAL;
using Education.Areas.Admin.Data;

namespace Education.Areas.Admin.Controllers
{
    public class BussinessesController : Controller
    {
        private IRepository<Bussiness> bussinesses;
        private IRepository<Permission> permissions;
        public BussinessesController()
        {
            bussinesses = new DbRepository<Bussiness>();
            permissions = new DbRepository<Permission>();
        }
        // GET: Admin/Bussinesses
        public ActionResult Index()
        {
            IEnumerable<Bussiness> data = bussinesses.Get();
            return View(data);
        }
        public ActionResult Update()
        {
            //Lấy các controller(Bussiness) trong Admin
            var controllers = Helper.GetControllers("Education.Areas.Admin.Controllers");
            //Lưu vào db
            foreach (var item in controllers)
            {
                Bussiness b = new Bussiness();
                b.Id = item.Name;
                b.Name = "Manager "+ item.Name;

               if(!bussinesses.CheckDuplicate(x=>x.Id.Equals(b.Id)))
                    bussinesses.Add(b);
                //Lấy các action(Permissin) trong controller
                var acts = Helper.GetActions(item);
                foreach (var act in acts)
                {
                    Permission p = new Permission();
                    p.Name = item.Name + act;
                    if (act.Equals("Index"))
                    {
                        p.Description = "View List";
                    }else if (act.Equals("Create"))
                    {
                        p.Description = "Create";
                    }else if (act.Equals("Edit"))
                    {
                        p.Description = "Edit";
                    }else if (act.Equals("Details"))
                    {
                        p.Description = "View Deatail";
                    }else if (act.Equals("Delete"))
                    {
                        p.Description = "Delete";
                    }
                    p.BussinessId = item.Name;
                    if (!permissions.CheckDuplicate(x => x.Name.Equals(p.Name)))
                        permissions.Add(p);
                }
            }
            return RedirectToAction("Index");
        }
    }
}