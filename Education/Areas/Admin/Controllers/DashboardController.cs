using Education.Areas.Admin.Data.DataModel;
using Education.BLL;
using Education.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Education.Areas.Admin.Controllers
{
    public class DashboardController : Controller
    {
        private EducationManageDbContext ctx;
        private IRepository<User> tblUser;
        public DashboardController()
        {
            ctx = new EducationManageDbContext();
            tblUser = new DbRepository<User>();
        }
        // GET: Dashboard
        public ActionResult Index()
        {
            if (Session["idUser"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        //GET: Register

        public ActionResult Register()
        {
            return View();
        }

        //POST: Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(User _user)
        {
            if (ModelState.IsValid)
            {
                var check = tblUser.Get(s => s.Email == _user.Email).FirstOrDefault();
                if (check == null)
                {
                    _user.Password = GetMD5(_user.Password);
                    ctx.Configuration.ValidateOnSaveEnabled = false;
                    tblUser.Add(_user);
                    ctx.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.error = "Email already exists";
                    return View();
                }
            }
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(UserLoginModel u)
        {
            if (ModelState.IsValid)
            {
                string username = u.UserName;
                string password = u.Password;
                var f_password = GetMD5(password);
                var data = tblUser.Get(s => s.UserName.Equals(username) && s.Password.Equals(password));
                if (data.Count() > 0)
                {
                    var dataUser = data.FirstOrDefault();
                    //add session
                    Session["FullName"] = dataUser.FullName;
                    Session["UserName"] = dataUser.UserName;
                    Session["Email"] = dataUser.Email;
                    Session["idUser"] = dataUser.Id;
                    ViewBag.FullName = Session["FullName"];
                    ViewBag.UserName = Session["UserName"];
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.error = "Login failed";
                    return RedirectToAction("Login");
                }
            }
            return View(u);
        }


        //Logout
        public ActionResult Logout()
        {
            Session.Clear();//remove session
            return RedirectToAction("Login");
        }



        //create a string MD5
        public static string GetMD5(string str)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] fromData = Encoding.UTF8.GetBytes(str);
            byte[] targetData = md5.ComputeHash(fromData);
            string byte2String = null;

            for (int i = 0; i < targetData.Length; i++)
            {
                byte2String += targetData[i].ToString("x2");

            }
            return byte2String;
        }

    }
}