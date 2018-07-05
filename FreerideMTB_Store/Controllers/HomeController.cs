using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FreerideMTB_Store.Controllers
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

        public ActionResult Produtos()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        //Criar ViewBag com foreach para cada categoria criar um <li>
        //public ActionResult Produtos()
        //{
        //    ViewBag.Message = "Your contact page.";

        //    return View();
        //}
    }
}