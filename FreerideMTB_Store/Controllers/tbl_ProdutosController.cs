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
using PagedList;

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

        [Authorize(Roles = "Admin")]
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

            
                //Correr todos os ficheiros disponiveis no formulario
                foreach (HttpPostedFileBase i in file)
                {
                    //se existirem ficheiros 
                    if (i != null)
                    {
                        //preparar um novo registo do tipo tbl_Imagens
                        tbl_Imagens intem = new tbl_Imagens();
                        //especificacao de atributos
                        intem.TituloImg = i.FileName;
                        intem.Descricao = "teste";
                        //criar um caminho com o nome do ficheiro concatenado
                        var filePath = Path.Combine((Server.MapPath("~/Imagens/")), i.FileName);
                        //especificacao do caminho
                        intem.Caminho = "/Imagens/" + i.FileName;
                        //guardar o ficheiro no caminho
                        i.SaveAs(filePath);
                        //criar uma nova ligacao de muitos para muitos(tbl.Imagens.ADD)
                        tbl_Produtos.tbl_Imagens.Add(intem);
                        //listIMG.Add(intem);
                        
                    }
                }
               
                db.tbl_Produtos.Add(tbl_Produtos);
                db.SaveChanges();
              

                return RedirectToAction("Index");
            }

            ViewBag.Categoria = new SelectList(db.tbl_Categoria, "Id_cat", "Nome", tbl_Produtos.Categoria);
            ViewBag.Sub_Categoria = new SelectList(db.tbl_Sub_Categoria, "Id_sub_cat", "Nome", tbl_Produtos.Sub_Categoria);
            ViewBag.Marca = new SelectList(db.tbl_Marca, "Id", "Nome", tbl_Produtos.Marca);
            return View(tbl_Produtos);
        }

        [Authorize(Roles = "Admin")]
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
       [ValidateInput(false)]
        public ActionResult Edit([Bind(Include = "Id_produto,Nome,Descricao,Preco,Categoria,Sub_Categoria,Stock,Peso,SKU,Marca")] tbl_Produtos tbl_Produtos, FormCollection collection, HttpPostedFileBase[] file)
        {
            if (ModelState.IsValid)
            {
                //recolha das check boxs ativas
                var checks = collection.GetValues("check");

                //guardar referencia da base de dados do produto que estamos a trabalhar
                var original = db.tbl_Produtos.Include(c => c.tbl_Imagens).SingleOrDefault(c => c.Id_produto == tbl_Produtos.Id_produto);

                //Lista de imagens para extrair (Questão de muitos para muitos)
                List<tbl_Imagens> listToDel = new List<tbl_Imagens>(original.tbl_Imagens);

                //Limpar a navegacao da chave estrangeira
                original.tbl_Imagens.Clear();
                tbl_Produtos.tbl_Imagens.Clear();


                //para cada checkbox ativa 
                if (checks!=null) {
                    foreach (var idd in checks)
                    {
                        //retiramos da lista para eliminar
                        listToDel.Remove(db.tbl_Imagens.Find(Int64.Parse(idd)));
                        original.tbl_Imagens.Add(db.tbl_Imagens.Find(Int64.Parse(idd)));
                        // tbl_Produtos.tbl_Imagens.Add(db.tbl_Imagens.Find(Int64.Parse(idd)));


                        //criar uma nova navegacao de chave estrangeira para as imagens do formulario
                        db.tbl_Imagens.Attach(db.tbl_Imagens.Find(Int64.Parse(idd)));
                    }
                }
                
                

                //para cada ficheiro carregado
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

                //guardar referencia do produto a editar

                var prod = db.tbl_Produtos.Find(tbl_Produtos.Id_produto);

                // fazer uma tentativa de update ao produto da referencia
                if (TryUpdateModel(prod))
                {
                    //original = tbl_Produtos;
                    //db.Entry(tbl_Produtos).State = EntityState.Modified;


                    //atualizar a base de dados
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
        [Authorize(Roles = "Admin")]
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
            //Duvida em relacao ao funcionamento de muitos para muitos (apagar ou nao img da DB)(2 produtos podem usar a mesma img)
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
