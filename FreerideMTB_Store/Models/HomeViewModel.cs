using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;






//************************Model para a página Home ter acesso á Bade de Dados**********************************






namespace FreerideMTB_Store.Models
{
    public class HomeViewModel
    {
        public List<tbl_Produtos> ListaProdutos { get; set; }
        public List<tbl_Imagens> ListaImagens { get; set; }
    }
}