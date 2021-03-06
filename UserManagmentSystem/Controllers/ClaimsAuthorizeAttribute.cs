﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace UserManagmentSystem.Controllers
{
    public class ClaimsAuthorizeAttribute : AuthorizeAttribute
    {
        private string claimType;

        public ClaimsAuthorizeAttribute(string type)
        {
            this.claimType = type;

        }
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            var user = filterContext.HttpContext.User as ClaimsPrincipal;
            if (user != null && user.HasClaim(claimType, claimType))
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