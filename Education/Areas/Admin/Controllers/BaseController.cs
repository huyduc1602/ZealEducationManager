using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace Session7.Areas.Admin.Controllers
{
    public class BaseController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("vi-VN");
            Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("vi-VN");
            if (Session["lang"] != null)
            {
                string Culture = Session["lang"].ToString();
                Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(Culture);
                Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(Culture);
            }
        }
        public ActionResult ChangeLangua(string code)
        {
            Session["lang"] = code;
            return Redirect(Request.UrlReferrer.AbsoluteUri);
        }
    }
}