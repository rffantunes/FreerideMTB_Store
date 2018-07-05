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
    public class tbl_ImagensController : Controller
    {
        private FreerideEntities db = new FreerideEntities();

        // GET: tbl_Imagens
        public ActionResult Index()
        {
            return View(db.tbl_Imagens.ToList());
        }

        // GET: tbl_Imagens/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_Imagens tbl_Imagens = db.tbl_Imagens.Find(id);
            if (tbl_Imagens == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Imagens);
        }

        // GET: tbl_Imagens/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: tbl_Imagens/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,TituloImg,Caminho,Descricao")] tbl_Imagens tbl_Imagens)
        {
            if (ModelState.IsValid)
            {
                db.tbl_Imagens.Add(tbl_Imagens);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tbl_Imagens);
        }

        // GET: tbl_Imagens/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_Imagens tbl_Imagens = db.tbl_Imagens.Find(id);
            if (tbl_Imagens == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Imagens);
        }

        // POST: tbl_Imagens/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,TituloImg,Caminho,Descricao")] tbl_Imagens tbl_Imagens)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbl_Imagens).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tbl_Imagens);
        }

        // GET: tbl_Imagens/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_Imagens tbl_Imagens = db.tbl_Imagens.Find(id);
            if (tbl_Imagens == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Imagens);
        }

        // POST: tbl_Imagens/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbl_Imagens tbl_Imagens = db.tbl_Imagens.Find(id);
            db.tbl_Imagens.Remove(tbl_Imagens);
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
