using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FreerideMTB_Store.Models;

namespace FreerideMTB_Store.Controllers
{
    public class tbl_ProdutosController : BaseController
    {
        private FreerideEntities db = new FreerideEntities();

         public tbl_ImagensController imgC = new tbl_ImagensController();

      

        //Criar uma listagem para aceder aos produtos a partir de qualquer página
        public List<tbl_Produtos> getProdutos()
        {          
          
            ////LINQ Code
            var lsProducts = db.tbl_Produtos.Include(c => c.tbl_Categoria).Include(c => c.tbl_Sub_Categoria).Include(c => c.tbl_Marca);
            return lsProducts.ToList();
        }

        

        // GET: tbl_Produtos
        public ActionResult Index(int? CatId, string search)
        {
            //Para criar uma lista onde mostra os produtos e as imagens
            var tbl_Produtos = db.tbl_Produtos.Include(t => t.tbl_Categoria).Include(t => t.tbl_Sub_Categoria).Include(t => t.tbl_Marca);


            //Filtering Sorting Paging
            if (CatId != null)
            {
                tbl_Produtos = tbl_Produtos.Where(s => s.Categoria == CatId);

            }

            if (!String.IsNullOrEmpty(search))
            {
                tbl_Produtos = tbl_Produtos.Where(S => S.Nome.Contains(search));
            }





            //Para criar uma lista onde mostra os produtos e as imagens
            //var tbl_Produtos = db.tbl_Produtos.Include(t => t.tbl_Categoria).Include(t => t.tbl_Sub_Categoria).Include(t => t.tbl_Marca);
            var tbl_Imagens = db.tbl_Imagens;
            ViewBag.ListaImagens = tbl_Imagens;
            ViewBag.ListaCategorias = db.tbl_Categoria.ToList();

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
            ViewBag.Marca = new SelectList(db.tbl_Marca, "Id", "Nome");
            return View();
        }

        // POST: tbl_Produtos/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create([Bind(Include = "Id_produto,Nome,Descricao,Preco,Categoria,Sub_Categoria,Stock,Peso,SKU,Marca")] tbl_Produtos tbl_Produtos, HttpPostedFileBase[] file)
        {
            if (ModelState.IsValid)
            {
                //List<tbl_Imagens> listIMG=new List<tbl_Imagens>();

                //tbl_Produtos.tbl_Imagens = new List<tbl_Imagens>();

            

                foreach (HttpPostedFileBase i in file)
                {
                    if (i != null)
                    {
                        tbl_Imagens intem = new tbl_Imagens();
                        intem.TituloImg = i.FileName;
                        intem.Descricao = "teste";
                        var filePath = Path.Combine((Server.MapPath("~/Imagens/")), i.FileName);
                        intem.Caminho = "/Imagens/" + i.FileName;
                        i.SaveAs(filePath);
                        tbl_Produtos.tbl_Imagens.Add(intem);
                        //listIMG.Add(intem);
                        
                    }
                }
                //try
                //{
                //    tbl_Produtos.tbl_Imagens = listIMG;
                db.tbl_Produtos.Add(tbl_Produtos);
                db.SaveChanges();
                //}
                //catch (DbEntityValidationException ee)
                //{
                //    foreach (var error in ee.EntityValidationErrors)
                //    {
                //        foreach (var thisError in error.ValidationErrors)
                //        {
                //            var errorMessage = thisError.ErrorMessage;
                //        }
                //    }
                //}


                return RedirectToAction("Index");
            }

            ViewBag.Categoria = new SelectList(db.tbl_Categoria, "Id_cat", "Nome", tbl_Produtos.Categoria);
            ViewBag.Sub_Categoria = new SelectList(db.tbl_Sub_Categoria, "Id_sub_cat", "Nome", tbl_Produtos.Sub_Categoria);
            ViewBag.Marca = new SelectList(db.tbl_Marca, "Id", "Nome", tbl_Produtos.Marca);
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
            ViewBag.Marca = new SelectList(db.tbl_Marca, "Id", "Nome", tbl_Produtos.Marca);
            return View(tbl_Produtos);
        }

        // POST: tbl_Produtos/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
       // [ValidateInput(false)]
        public ActionResult Edit([Bind(Include = "Id_produto,Nome,Descricao,Preco,Categoria,Sub_Categoria,Stock,Peso,SKU,Marca")] tbl_Produtos tbl_Produtos, FormCollection collection, HttpPostedFileBase[] file)
        {
            if (ModelState.IsValid)
            {
                var checks = collection.GetValues("check");

                var original = db.tbl_Produtos.Include(c => c.tbl_Imagens).SingleOrDefault(c => c.Id_produto == tbl_Produtos.Id_produto);

                List<tbl_Imagens> listToDel = new List<tbl_Imagens>(original.tbl_Imagens);

                original.tbl_Imagens.Clear();
                tbl_Produtos.tbl_Imagens.Clear();

                foreach (var idd in checks)
                {
                    listToDel.Remove(db.tbl_Imagens.Find(Int64.Parse(idd)));
                    original.tbl_Imagens.Add(db.tbl_Imagens.Find(Int64.Parse(idd)));
                    // tbl_Produtos.tbl_Imagens.Add(db.tbl_Imagens.Find(Int64.Parse(idd)));
                    db.tbl_Imagens.Attach(db.tbl_Imagens.Find(Int64.Parse(idd)));
                }
                


                foreach (HttpPostedFileBase i in file)
                {
                    if (i != null)
                    {
                        tbl_Imagens intem = new tbl_Imagens();
                        intem.TituloImg = i.FileName;
                        intem.Descricao = "teste";
                        var filePath = Path.Combine((Server.MapPath("~/Imagens/")), i.FileName);
                        intem.Caminho = "/Imagens/" + i.FileName;
                        i.SaveAs(filePath);
                        original.tbl_Imagens.Add(intem);
                        tbl_Produtos.tbl_Imagens.Add(intem);
                        //listIMG.Add(intem);

                    }
                }

                var prod = db.tbl_Produtos.Find(tbl_Produtos.Id_produto);
                if (TryUpdateModel(prod))
                {
                    //original = tbl_Produtos;
                    //db.Entry(tbl_Produtos).State = EntityState.Modified;
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }


                //foreach (tbl_Imagens img in listToDel)
                //{
                //    db.Entry(db.tbl_Imagens.Find(img.Id)).State = EntityState.Deleted;

                //    //**************** Opcao para eliminar o ficheiro (cuidado com outros produtos que utilizem a mesma img)********


                //    //var path = Server.MapPath(img.Caminho);
                //    //if (System.IO.File.Exists(path)) 
                //    //{
                //    //    System.IO.File.Delete(path);
                //    //}
                //}





                //db.Entry(tbl_Produtos).State = EntityState.Unchanged;
                //db.tbl_Imagens.RemoveRange(listToDel);
               
            }
            ViewBag.Categoria = new SelectList(db.tbl_Categoria, "Id_cat", "Nome", tbl_Produtos.Categoria);
            ViewBag.Sub_Categoria = new SelectList(db.tbl_Sub_Categoria, "Id_sub_cat", "Nome", tbl_Produtos.Sub_Categoria);
            ViewBag.Marca = new SelectList(db.tbl_Marca, "Id", "Nome", tbl_Produtos.Marca);
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
            var listaImagens = tbl_Produtos.tbl_Imagens;
          //  db.tbl_Imagens.RemoveRange(listaImagens);
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
