using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Web;
using System.Web.Mvc;

namespace Education.Areas.Admin.Data
{
    public class Helper
    {
        public static List<Type> GetControllers(string namespaces)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            IEnumerable<Type> types = assembly.GetTypes()
                .Where(t => typeof(Controller).IsAssignableFrom(t) && t.Namespace.Contains(namespaces))
                .OrderBy(x => x.Name);
            return types.ToList();
        }
        public static List<string> GetActions(Type controller)
        {
            List<string> acctions = new List<string>();
            acctions = controller
                .GetMethods(BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.Public)
                .Where(m =>
                    !m.GetCustomAttributes(typeof(CompilerGeneratedAttribute), true).Any() &&
                    !m.GetCustomAttributes(typeof(AllowAnonymousAttribute), false).Any() &&
                    !m.IsDefined(typeof(NonActionAttribute)) &&
                    m.ReflectedType.IsPublic)
                .OrderBy(x => x.Name).Select(x => x.Name).ToList();
            return acctions;
        }
    }
}