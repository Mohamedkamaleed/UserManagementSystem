using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace UserManagmentSystem.Controllers
{
    public class ImplementedMethods
    {
        public List<string> ActiveMethods;


        public ImplementedMethods()
        {
            var asm = Assembly.GetExecutingAssembly();
            ActiveMethods = asm.GetTypes()
                .Where(type => typeof(Controller)
                    .IsAssignableFrom(type))
                .SelectMany(type => type.GetMethods())
                .Where(method => method.IsPublic
                    && !method.IsDefined(typeof(NonActionAttribute))
                    && (
                    method.CustomAttributes.Any(s => s.AttributeType == typeof(HttpPostAttribute)) ||
                    method.CustomAttributes.Any(s => s.AttributeType == typeof(HttpGetAttribute))
                    )
                    && (

                    !method.CustomAttributes.Any(s => s.AttributeType == typeof(AllowAnonymousAttribute))
                    )
                    && method.CustomAttributes.Any(s => s.AttributeType == typeof(ClaimsAuthorizeAttribute))
                    && (
                        method.ReturnType == typeof(ActionResult) ||
                        method.ReturnType == typeof(Task<ActionResult>) ||

                        method.ReturnType == typeof(String)

                        //method.ReturnType == typeof(IHttpResult) ||
                        )
                    )
                .Select(m => m.CustomAttributes.FirstOrDefault(s => s.AttributeType == typeof(HttpPostAttribute) || s.AttributeType == typeof(HttpGetAttribute)).AttributeType.Name.Replace("Attribute", "") + " : " + m.DeclaringType.ToString().Split('.')[2].Replace("Controller", "") + "/" + m.Name).ToList();
        }

    }
}