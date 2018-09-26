using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FreerideMTB_Store.Models;

namespace FreerideMTB_Store.Controllers
{    //Autorização de acesso à página
    [Authorize(Roles = "Admin,Editor")]
    //Herança do BaseController para conseguir aceder ás listagens públicas
    public class tbl_MarcaController : BaseController
    {
        private FreerideEntities db = new FreerideEntities();


        //Criar uma listagem para aceder as Marcas a partir de qualquer página
        public List<tbl_Marca> getMarcas()
        {

            ////LINQ Code
            var lsMarcas = db.tbl_Marca;
            return lsMarcas.ToList();
        }



        // GET: tbl_Marca
        public ActionResult Index()
        {
            return View(db.tbl_Marca.ToList());
        }

        // GET: tbl_Marca/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_Marca tbl_Marca = db.tbl_Marca.Find(id);
            if (tbl_Marca == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Marca);
        }

        // GET: tbl_Marca/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: tbl_Marca/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Nome,site")] tbl_Marca tbl_Marca)
        {
            if (ModelState.IsValid)
            {
                db.tbl_Marca.Add(tbl_Marca);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tbl_Marca);
        }

        // GET: tbl_Marca/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_Marca tbl_Marca = db.tbl_Marca.Find(id);
            if (tbl_Marca == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Marca);
        }

        // POST: tbl_Marca/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nome,site")] tbl_Marca tbl_Marca)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbl_Marca).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tbl_Marca);
        }

        // GET: tbl_Marca/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_Marca tbl_Marca = db.tbl_Marca.Find(id);
            if (tbl_Marca == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Marca);
        }

        // POST: tbl_Marca/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbl_Marca tbl_Marca = db.tbl_Marca.Find(id);
            db.tbl_Marca.Remove(tbl_Marca);
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
