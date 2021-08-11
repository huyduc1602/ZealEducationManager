using Education.BLL;
using Education.DAL;
using Education.BLL;
using Education.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Education.Areas.Admin.Data
{
    public class CustomizeAuthorize : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (httpContext.Session["user"] == null)
            {
                return false;
            }
            IRepository<GroupUser> ugRepository = new DbRepository<GroupUser>();
            IRepository<GroupPermission> pgRepository = new DbRepository<GroupPermission>();
            IRepository<Permission> pRepository = new DbRepository<Permission>();
            User users = (User)httpContext.Session["user"];
            GroupUser userGroup = ugRepository.FindById(users.GroupUserId);
            if (userGroup.IsAdmin)
            {
                return true;
            }
            var perGroup = pgRepository.Get().Where(x => x.GroupUserId == users.GroupUserId);
            List<Permission> permissions = new List<Permission>();
            foreach (var item in perGroup)
            {
                permissions.Add(pRepository.FindById(item.Id));
            }
            string currentPermission = httpContext.Request.RequestContext.RouteData.GetRequiredString("controller") + "Controller-"
                + httpContext.Request.RequestContext.RouteData.GetRequiredString("action");
            if (permissions.Count == 0 || !permissions.Any(x => x.Name.Equals(currentPermission)))
            {
                return false;
            }
            return true;

        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new RedirectResult("/Admin/Dashboard/Login");
        }
    }
}