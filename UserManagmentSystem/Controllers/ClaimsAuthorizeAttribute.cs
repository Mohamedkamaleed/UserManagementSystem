using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using UserManagmentSystem.Extensions;

namespace UserManagmentSystem.Controllers
{
    public class ClaimsAuthorizeAttribute : AuthorizeAttribute
    {
        private string ActionUniqueName;

        public ClaimsAuthorizeAttribute()
        {
            this.ActionUniqueName = "";

        }
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            var user = filterContext.HttpContext.User as ClaimsPrincipal;

            var HttpMethod = filterContext.HttpContext.Request.HttpMethod.ToLower().FirstCharToUpper();
            var Path = filterContext.HttpContext.Request.Path;
            var Method = Path.Substring(1, Path.Length-1);

            ActionUniqueName = "Http" + HttpMethod + " : " + Method;

            //User class always has a value but when not logged in IsAuthenticated should be checked
            if (user.Identity.IsAuthenticated && user.HasClaim(ActionUniqueName, ActionUniqueName) || Path == "/")
            {
                base.OnAuthorization(filterContext);
            }
            else
            {
                base.HandleUnauthorizedRequest(filterContext);
            }
        }
    }
}