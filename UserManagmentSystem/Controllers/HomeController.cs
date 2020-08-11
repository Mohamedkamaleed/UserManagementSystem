using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UserManagmentSystem.Controllers
{
    public class HomeController : Controller
    {
        [ClaimsAuthorize("HttpGet : Home/Index")]
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [ClaimsAuthorize("HttpGet : Home/About")]
        [HttpGet]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [ClaimsAuthorize("HttpGet : Home/Contact")]
        [HttpGet]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}