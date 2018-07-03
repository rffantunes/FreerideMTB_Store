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
    public class tbl_ProdutoEncomendaController : Controller
    {
        private FreerideEntities db = new FreerideEntities();

        // GET: tbl_ProdutoEncomenda
        public ActionResult Index()
        {
            var tbl_ProdutoEncomenda = db.tbl_ProdutoEncomenda.Include(t => t.tbl_Encomenda).Include(t => t.tbl_Produtos);
            return View(tbl_ProdutoEncomenda.ToList());
        }

        // GET: tbl_ProdutoEncomenda/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_ProdutoEncomenda tbl_ProdutoEncomenda = db.tbl_ProdutoEncomenda.Find(id);
            if (tbl_ProdutoEncomenda == null)
            {
                return HttpNotFound();
            }
            return View(tbl_ProdutoEncomenda);
        }

        // GET: tbl_ProdutoEncomenda/Create
        public ActionResult Create()
        {
            ViewBag.Id_Encomenda = new SelectList(db.tbl_Encomenda, "Id_Encomenda", "UserName");
            ViewBag.Id_produto = new SelectList(db.tbl_Produtos, "Id_produto", "Nome");
            return View();
        }

        // POST: tbl_ProdutoEncomenda/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_produto,Id_Encomenda,Quantidade,Preco")] tbl_ProdutoEncomenda tbl_ProdutoEncomenda)
        {
            if (ModelState.IsValid)
            {
                db.tbl_ProdutoEncomenda.Add(tbl_ProdutoEncomenda);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Id_Encomenda = new SelectList(db.tbl_Encomenda, "Id_Encomenda", "UserName", tbl_ProdutoEncomenda.Id_Encomenda);
            ViewBag.Id_produto = new SelectList(db.tbl_Produtos, "Id_produto", "Nome", tbl_ProdutoEncomenda.Id_produto);
            return View(tbl_ProdutoEncomenda);
        }

        // GET: tbl_ProdutoEncomenda/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_ProdutoEncomenda tbl_ProdutoEncomenda = db.tbl_ProdutoEncomenda.Find(id);
            if (tbl_ProdutoEncomenda == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id_Encomenda = new SelectList(db.tbl_Encomenda, "Id_Encomenda", "UserName", tbl_ProdutoEncomenda.Id_Encomenda);
            ViewBag.Id_produto = new SelectList(db.tbl_Produtos, "Id_produto", "Nome", tbl_ProdutoEncomenda.Id_produto);
            return View(tbl_ProdutoEncomenda);
        }

        // POST: tbl_ProdutoEncomenda/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_produto,Id_Encomenda,Quantidade,Preco")] tbl_ProdutoEncomenda tbl_ProdutoEncomenda)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbl_ProdutoEncomenda).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Id_Encomenda = new SelectList(db.tbl_Encomenda, "Id_Encomenda", "UserName", tbl_ProdutoEncomenda.Id_Encomenda);
            ViewBag.Id_produto = new SelectList(db.tbl_Produtos, "Id_produto", "Nome", tbl_ProdutoEncomenda.Id_produto);
            return View(tbl_ProdutoEncomenda);
        }

        // GET: tbl_ProdutoEncomenda/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_ProdutoEncomenda tbl_ProdutoEncomenda = db.tbl_ProdutoEncomenda.Find(id);
            if (tbl_ProdutoEncomenda == null)
            {
                return HttpNotFound();
            }
            return View(tbl_ProdutoEncomenda);
        }

        // POST: tbl_ProdutoEncomenda/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbl_ProdutoEncomenda tbl_ProdutoEncomenda = db.tbl_ProdutoEncomenda.Find(id);
            db.tbl_ProdutoEncomenda.Remove(tbl_ProdutoEncomenda);
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
