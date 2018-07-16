using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FreerideMTB_Store.Controllers
{
    public class BaseController : Controller
    {
        override
        protected void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //para todos os controllers acederem as categorias do menu (Herança)
            //excepto controller_Categoria    (Evitar StackOverflow)
            ViewBag.ListaCategoria = CatC.getCategorias();
            //ViewBag.ListaMarcas = MarcaC.getMarcas();
        }

        public tbl_CategoriaController CatC = new tbl_CategoriaController();
        //public tbl_Sub_CategoriaController SubCatC = new tbl_Sub_CategoriaController();
        //public tbl_MarcaController MarcaC = new tbl_MarcaController();
    }
}