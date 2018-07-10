using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FreerideMTB_Store.Controllers
{
    public class HomeController : Controller
    {

        public tbl_ImagensController imgC = new tbl_ImagensController();
        public tbl_ProdutosController ProdC = new tbl_ProdutosController();



        public ActionResult Index(FreerideMTB_Store.Models.HomeViewModel VM)
        {
            VM.ListaImagens = imgC.getImagens();
            VM.ListaProdutos = ProdC.getProdutos();

            return View(VM);
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