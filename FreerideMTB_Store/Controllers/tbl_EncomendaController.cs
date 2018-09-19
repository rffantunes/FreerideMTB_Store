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
    public class tbl_EncomendaController : BaseController
    {
        private FreerideEntities db = new FreerideEntities();


        public List<tbl_Encomenda> getEncomendas()
        {

            ////LINQ Code
            var lsEncomendas = db.tbl_Encomenda.Include(c => c.AspNetUsers);
            return lsEncomendas.ToList();
        }


        // GET: tbl_Encomenda
        public ActionResult Index()
        {
            var tbl_Encomenda = db.tbl_Encomenda.Include(t => t.AspNetUsers);
            return View(tbl_Encomenda.ToList());
        }

        // GET: tbl_Encomenda/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_Encomenda tbl_Encomenda = db.tbl_Encomenda.Find(id);
            if (tbl_Encomenda == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Encomenda);
        }

        // GET: tbl_Encomenda/Create
        public ActionResult Create()
        {
            ViewBag.UserName = new SelectList(db.AspNetUsers, "Id", "Email");
            return View();
        }

        // POST: tbl_Encomenda/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_Encomenda,Quantidade,Data,UserName")] tbl_Encomenda tbl_Encomenda)
        {
            if (ModelState.IsValid)
            {
                db.tbl_Encomenda.Add(tbl_Encomenda);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserName = new SelectList(db.AspNetUsers, "Id", "Email", tbl_Encomenda.UserName);
            return View(tbl_Encomenda);
        }

        // GET: tbl_Encomenda/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_Encomenda tbl_Encomenda = db.tbl_Encomenda.Find(id);
            if (tbl_Encomenda == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserName = new SelectList(db.AspNetUsers, "Id", "Email", tbl_Encomenda.UserName);
            return View(tbl_Encomenda);
        }

        // POST: tbl_Encomenda/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_Encomenda,Quantidade,Data,UserName")] tbl_Encomenda tbl_Encomenda)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbl_Encomenda).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserName = new SelectList(db.AspNetUsers, "Id", "Email", tbl_Encomenda.UserName);
            return View(tbl_Encomenda);
        }

        // GET: tbl_Encomenda/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_Encomenda tbl_Encomenda = db.tbl_Encomenda.Find(id);
            if (tbl_Encomenda == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Encomenda);
        }

        // POST: tbl_Encomenda/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbl_Encomenda tbl_Encomenda = db.tbl_Encomenda.Find(id);
            db.tbl_Encomenda.Remove(tbl_Encomenda);
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
