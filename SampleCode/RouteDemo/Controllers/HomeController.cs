using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RouteDemo.Controllers
{
    public class HomeController : Controller
    {
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
        public string Other(int year,int month,int day)
        {
            return $"{year}-{month}-{day}";
        }

        public ActionResult refuse()
        {
            ViewBag.Message = "请不要使用Chrome浏览器";
            return View();
        }
    }
}