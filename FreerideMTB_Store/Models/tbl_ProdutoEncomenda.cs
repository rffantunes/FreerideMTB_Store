//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FreerideMTB_Store.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tbl_ProdutoEncomenda
    {
        public int Id_produto { get; set; }
        public int Id_Encomenda { get; set; }
        public int Quantidade { get; set; }
        public decimal Preco { get; set; }
    
        public virtual tbl_Encomenda tbl_Encomenda { get; set; }
        public virtual tbl_Produtos tbl_Produtos { get; set; }
    }
}