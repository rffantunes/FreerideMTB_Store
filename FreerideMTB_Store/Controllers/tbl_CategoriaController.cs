using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FreerideMTB_Store.Models;

namespace FreerideMTB_Store.Controllers
{

    //Este n pode herdar do BaseController para nao entrar num loop recursivo
    public class tbl_CategoriaController : Controller
    {
        private FreerideEntities db = new FreerideEntities();
        

        //Criar uma listagem para aceder as categorias a partir de qualquer página
        public List<tbl_Categoria> getCategorias()
        {

            ////LINQ Code
            var lsCat = db.tbl_Categoria;
            return lsCat.ToList();
        }

        //Criar uma listagem para aceder as Subcategorias a partir de qualquer página
        //public List<tbl_Categoria> getSubCategorias()
        //{

        //    ////LINQ Code
        //    var lsSubCat = db.tbl_Sub_Categoria;
        //    return lsSubCat.ToList();
        //}






            //metodo que permite criar a dropdown das categorias para as views da categoria (por implementar)
            //OnactionExecution é executado antes de qualquer action(Index,Edit,Create,...)
            override
        protected void OnActionExecuting(ActionExecutingContext filterContext)
        {

            ViewBag.ListaCategoria = getCategorias();
            //ViewBag.ListaMarcas = MarcaC.getMarcas();
        }








        // GET: tbl_Categoria
        public ActionResult Index()
        {
            return View(db.tbl_Categoria.ToList());
        }

        // GET: tbl_Categoria/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_Categoria tbl_Categoria = db.tbl_Categoria.Find(id);
            if (tbl_Categoria == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Categoria);
        }

        // GET: tbl_Categoria/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: tbl_Categoria/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_cat,Nome,Descricao")] tbl_Categoria tbl_Categoria)
        {
            if (ModelState.IsValid)
            {
                db.tbl_Categoria.Add(tbl_Categoria);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tbl_Categoria);
        }

        // GET: tbl_Categoria/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_Categoria tbl_Categoria = db.tbl_Categoria.Find(id);
            if (tbl_Categoria == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Categoria);
        }

        // POST: tbl_Categoria/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_cat,Nome,Descricao")] tbl_Categoria tbl_Categoria)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbl_Categoria).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tbl_Categoria);
        }

        // GET: tbl_Categoria/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_Categoria tbl_Categoria = db.tbl_Categoria.Find(id);
            if (tbl_Categoria == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Categoria);
        }

        // POST: tbl_Categoria/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbl_Categoria tbl_Categoria = db.tbl_Categoria.Find(id);
            db.tbl_Categoria.Remove(tbl_Categoria);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
