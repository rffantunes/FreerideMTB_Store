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
    public class tbl_ProdutosController : Controller
    {
        private DBModel db = new DBModel();

        // GET: tbl_Produtos
        public ActionResult Index()
        {
            var tbl_Produtos = db.tbl_Produtos.Include(t => t.tbl_Categoria).Include(t => t.tbl_Sub_Categoria);
            return View(tbl_Produtos.ToList());
        }

        // GET: tbl_Produtos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_Produtos tbl_Produtos = db.tbl_Produtos.Find(id);
            if (tbl_Produtos == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Produtos);
        }

        // GET: tbl_Produtos/Create
        public ActionResult Create()
        {
            ViewBag.Categoria = new SelectList(db.tbl_Categoria, "Id_cat", "Nome");
            ViewBag.Sub_Categoria = new SelectList(db.tbl_Sub_Categoria, "Id_sub_cat", "Nome");
            return View();
        }

        // POST: tbl_Produtos/Create Id_produto,
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Nome,Descricao,Preco,Foto,Categoria,Sub_Categoria,Stock")] tbl_Produtos tbl_Produtos)
        {
            if (ModelState.IsValid)
            {
                db.tbl_Produtos.Add(tbl_Produtos);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Categoria = new SelectList(db.tbl_Categoria, "Id_cat", "Nome", tbl_Produtos.Categoria);
            ViewBag.Sub_Categoria = new SelectList(db.tbl_Sub_Categoria, "Id_sub_cat", "Nome", tbl_Produtos.Sub_Categoria);
            return View(tbl_Produtos);
        }

        // GET: tbl_Produtos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_Produtos tbl_Produtos = db.tbl_Produtos.Find(id);
            if (tbl_Produtos == null)
            {
                return HttpNotFound();
            }
            ViewBag.Categoria = new SelectList(db.tbl_Categoria, "Id_cat", "Nome", tbl_Produtos.Categoria);
            ViewBag.Sub_Categoria = new SelectList(db.tbl_Sub_Categoria, "Id_sub_cat", "Nome", tbl_Produtos.Sub_Categoria);
            return View(tbl_Produtos);
        }

        // POST: tbl_Produtos/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_produto,Nome,Descricao,Preco,Foto,Categoria,Sub_Categoria,Stock")] tbl_Produtos tbl_Produtos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbl_Produtos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Categoria = new SelectList(db.tbl_Categoria, "Id_cat", "Nome", tbl_Produtos.Categoria);
            ViewBag.Sub_Categoria = new SelectList(db.tbl_Sub_Categoria, "Id_sub_cat", "Nome", tbl_Produtos.Sub_Categoria);
            return View(tbl_Produtos);
        }

        // GET: tbl_Produtos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_Produtos tbl_Produtos = db.tbl_Produtos.Find(id);
            if (tbl_Produtos == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Produtos);
        }

        // POST: tbl_Produtos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbl_Produtos tbl_Produtos = db.tbl_Produtos.Find(id);
            db.tbl_Produtos.Remove(tbl_Produtos);
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
