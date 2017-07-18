using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BroadridgeTestProject.DAL;
using BroadridgeTestProject.Services;

namespace BroadridgeTestProject.Controllers
{
    public class HomeController : Controller
    {
        [Authorize(Roles = "Users")]
        public ActionResult Index()
        {
            return View();
        }        

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}