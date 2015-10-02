using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IndustryExplorersData.Controllers
{
    //[Authorize]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }

       // [Authorize]
        public ActionResult Data()
        {
            ViewBag.Title = "Home Page";

            return View();
        }
        public ActionResult Index2()
        {
            ViewBag.Title = "Home Page";

            return View();
        }
    }
}
