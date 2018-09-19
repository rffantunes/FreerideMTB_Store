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
    [Authorize(Roles = "Admin,Editor")]
    public class tbl_Sub_CategoriaController : BaseController
    {
        private FreerideEntities db = new FreerideEntities();


        public List<tbl_Sub_Categoria> getSubCat()
        {

            ////LINQ Code
            var lsSubCat = db.tbl_Sub_Categoria.Include(c => c.tbl_Categoria);
            return lsSubCat.ToList();
        }



        // GET: tbl_Sub_Categoria
        public ActionResult Index()
        {
            var tbl_Sub_Categoria = db.tbl_Sub_Categoria.Include(t => t.tbl_Categoria);
            return View(tbl_Sub_Categoria.ToList());
        }

        // GET: tbl_Sub_Categoria/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_Sub_Categoria tbl_Sub_Categoria = db.tbl_Sub_Categoria.Find(id);
            if (tbl_Sub_Categoria == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Sub_Categoria);
        }

        // GET: tbl_Sub_Categoria/Create
        public ActionResult Create()
        {
            ViewBag.id_cat = new SelectList(db.tbl_Categoria, "Id_cat", "Nome");
            return View();
        }

        // POST: tbl_Sub_Categoria/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_sub_cat,Nome,id_cat")] tbl_Sub_Categoria tbl_Sub_Categoria)
        {
            if (ModelState.IsValid)
            {
                db.tbl_Sub_Categoria.Add(tbl_Sub_Categoria);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_cat = new SelectList(db.tbl_Categoria, "Id_cat", "Nome", tbl_Sub_Categoria.id_cat);
            return View(tbl_Sub_Categoria);
        }

        // GET: tbl_Sub_Categoria/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_Sub_Categoria tbl_Sub_Categoria = db.tbl_Sub_Categoria.Find(id);
            if (tbl_Sub_Categoria == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_cat = new SelectList(db.tbl_Categoria, "Id_cat", "Nome", tbl_Sub_Categoria.id_cat);
            return View(tbl_Sub_Categoria);
        }

        // POST: tbl_Sub_Categoria/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_sub_cat,Nome,id_cat")] tbl_Sub_Categoria tbl_Sub_Categoria)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbl_Sub_Categoria).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_cat = new SelectList(db.tbl_Categoria, "Id_cat", "Nome", tbl_Sub_Categoria.id_cat);
            return View(tbl_Sub_Categoria);
        }

        // GET: tbl_Sub_Categoria/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_Sub_Categoria tbl_Sub_Categoria = db.tbl_Sub_Categoria.Find(id);
            if (tbl_Sub_Categoria == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Sub_Categoria);
        }

        // POST: tbl_Sub_Categoria/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbl_Sub_Categoria tbl_Sub_Categoria = db.tbl_Sub_Categoria.Find(id);
            db.tbl_Sub_Categoria.Remove(tbl_Sub_Categoria);
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
